using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ST10438817_POE_PART3_CHATBOT.JARVIS_AI
{
    public static class ChatBot_Task
    {




        private static List<TaskItem> Tasks = new();

        private static string currentAction = null;

        private static int? selectedTaskIndex = null;

        private static TaskItem tempTask = null;

        private static bool isWaitingForReminderInput = false;

        public static bool IsActive { get; private set; }

        public static event Action<string, HorizontalAlignment> DisplayTaskMessage;

        private static Timer reminderTimer;






        private class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? ReminderTime { get; set; }
            public bool ReminderNotified { get; set; } = false;
        }








        static ChatBot_Task()
        {
            reminderTimer = new Timer(_ => ProcessTaskReminders(), null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        }


        public static void ActivateTaskManager()
        {
            IsActive = true;
            currentAction = null;
            selectedTaskIndex = null;
            tempTask = null;
            isWaitingForReminderInput = false;
            DisplayTaskMessage?.Invoke("WELCOME TO THE TASK MANAGER!\n-------------------------------------------------------------------\n" +
"You can manage your tasks with the following commands:\n" +
"CREATE - Create New Task\n" +
"UPDATE - Modify Existing Task\n" +
"DELETE - Remove Task\n" +
"LISTS - View All Tasks", HorizontalAlignment.Left);


        }



        public static void ProcessUserInput(string input)
        {
            if (!IsActive) return;

            input = input.Trim().ToLower();

            // If no current action, decide action based on input
            if (currentAction == null)
            {
                if (input.Contains("create"))
                {
                    currentAction = "create";
                    tempTask = new TaskItem();
                    DisplayTaskMessage?.Invoke("Enter Task Title", HorizontalAlignment.Left);
                }
                else if (input.Contains("update"))
                {
                    currentAction = "update";
                    DisplayTaskMessage?.Invoke("Enter Task Number To Update (or type 'list' to see tasks)", HorizontalAlignment.Left);
                }
                else if (input.Contains("delete"))
                {
                    currentAction = "delete";
                    DisplayTaskMessage?.Invoke("Enter Task Number To Delete (or type 'list' to see tasks)", HorizontalAlignment.Left);
                }
                else if (input.Contains("list"))
                {
                    DisplayTaskMessage?.Invoke(DisplayTaskList(), HorizontalAlignment.Left);
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Please Type: Create, Update, Delete, List", HorizontalAlignment.Left);
                }

                return;
            }

            // Process current action steps
            switch (currentAction)
            {
                case "create":
                    CreateTask(input);
                    break;
                case "update":
                    UpdateTask(input);
                    break;
                case "delete":
                    DeleteTask(input);
                    break;
                
            }
        }





        private static void CreateTask(string input)
        {
            if (string.IsNullOrEmpty(tempTask.Title))
            {
                tempTask.Title = input;
                DisplayTaskMessage?.Invoke("Enter Task Description", HorizontalAlignment.Left);
            }
            else if (string.IsNullOrEmpty(tempTask.Description))
            {
                tempTask.Description = input;
                DisplayTaskMessage?.Invoke("Enter Reminder Date and Time (e.g. 2025-06-30 14:30), or 'skip' to skip", HorizontalAlignment.Left);
            }
            else if (!isWaitingForReminderInput)
            {
                if (input == "skip")
                {
                    tempTask.ReminderTime = null;
                    CompleteTaskCreation();
                }
                else if (DateTime.TryParse(input, out DateTime reminder))
                {
                    tempTask.ReminderTime = reminder;
                    CompleteTaskCreation();
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid Date Format. Please try again or type 'skip'", HorizontalAlignment.Left);
                }
            }
        }

        private static void CompleteTaskCreation()
        {
            Tasks.Add(tempTask);
            DisplayTaskMessage?.Invoke(@$"TASK CREATED 
--------------------------------------------------------
TITLE: {tempTask.Title}
DESCRIPTION: {tempTask.Description}
REMINDER: {(tempTask.ReminderTime?.ToString() ?? "No reminder")}
--------------------------------------------------------", HorizontalAlignment.Left);
            ChatBot_Activity_Log.ActivityLog("TASK", $"Created Task: {tempTask.Title}");

            ResetState();
        }




        private static void ApplyUpdateTaskDetails()
        {
            var task = Tasks[selectedTaskIndex.Value];
            task.Title = tempTask.Title;
            task.Description = tempTask.Description;
            task.ReminderTime = tempTask.ReminderTime;
            task.ReminderNotified = false;

            ChatBot_Activity_Log.ActivityLog("TASK", $"Updated Task: {task.Title}");

            DisplayTaskMessage?.Invoke(@$"TASK UPDATED 
---------------------------------------------------------
TITLE: {task.Title}
DESCRIPTION: {task.Description}
REMINDER: {(task.ReminderTime?.ToString() ?? "No reminder")}", HorizontalAlignment.Left);

            ResetState();
        }





        private static void UpdateTask(string input)
        {
            if (selectedTaskIndex == null)
            {
                if (input == "list")
                {
                    DisplayTaskMessage?.Invoke(DisplayTaskList(), HorizontalAlignment.Left);
                    return;
                }

                // Attempt to parse input as task number
                if (int.TryParse(input, out int userIndex))
                {
                    int index = userIndex - 1; // Convert to 0-based index because of numbeing system being different and adjusted 

                    if (index >= 0 && index < Tasks.Count)
                    {
                        selectedTaskIndex = index;
                        tempTask = new TaskItem(); // New temp task for update
                        DisplayTaskMessage?.Invoke("Enter New Task Title", HorizontalAlignment.Left);
                    }
                    else
                    {
                        DisplayTaskMessage?.Invoke("Invalid Task Number. Please enter a number from the task list.", HorizontalAlignment.Left);
                    }
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid input. Please enter a valid task number.", HorizontalAlignment.Left);
                }

                return;

            }



            // Continue with update process
            if (string.IsNullOrEmpty(tempTask.Title))
            {
                tempTask.Title = input;
                DisplayTaskMessage?.Invoke("Enter New Task Description", HorizontalAlignment.Left);
            }
            else if (string.IsNullOrEmpty(tempTask.Description))
            {
                tempTask.Description = input;
                DisplayTaskMessage?.Invoke("Enter New Reminder Date and Time (e.g. 2025-06-30 14:30), or type 'skip'", HorizontalAlignment.Left);
            }
            else
            {
                if (input.Trim().ToLower() == "skip")
                {
                    tempTask.ReminderTime = null;
                    ApplyUpdateTaskDetails();
                }
                else if (DateTime.TryParse(input, out DateTime reminder))
                {
                    tempTask.ReminderTime = reminder;
                    ApplyUpdateTaskDetails();
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid Date Format. Please try again or type 'skip'.", HorizontalAlignment.Left);
                }
            }
        }




        private static void DeleteTask(string input)
        {
            if (input == "list")
            {
                DisplayTaskMessage?.Invoke(DisplayTaskList(), HorizontalAlignment.Left);
                return;
            }

            if (int.TryParse(input, out int index) && index >= 0 && index <= Tasks.Count)
            {
                string title = Tasks[index].Title;
                Tasks.RemoveAt(index);
                ChatBot_Activity_Log.ActivityLog("TASK", $"Deleted Task: {title}");
                DisplayTaskMessage?.Invoke($"DELETED TASK: {title}", HorizontalAlignment.Left);
                ResetState();
            }
            else
            {
                DisplayTaskMessage?.Invoke("Invalid Task Number. Please try again or type 'list'.", HorizontalAlignment.Left);
            }
        }




        private static string DisplayTaskList()
        {
            if (!Tasks.Any())
                return "No Tasks Available!";

            string list = "TASK LIST\n\n";

            for (int i = 0; i < Tasks.Count; i++)
            {
                var t = Tasks[i];
                string reminder = t.ReminderTime.HasValue ? t.ReminderTime.Value.ToString("g") : "No reminder";

                list += $"{i + 1}.\n";
                list += $"Title: {t.Title}\n";
                list += $"Description: {t.Description}\n";
                list += $"Reminder: {reminder}\n";
                list += "----------------------------------\n";
            }

            ChatBot_Activity_Log.ActivityLog("TASK", "Displayed Task List");

            return list;
        }




        private static void ProcessTaskReminders()
        {
            var now = DateTime.Now;
            foreach (var task in Tasks.Where(t => t.ReminderTime.HasValue && t.ReminderTime <= now && !t.ReminderNotified))
            {
                
                DisplayTaskMessage?.Invoke($"REMINDER \n-------------------------------------------\n TASK: {task.Title} is due now!", HorizontalAlignment.Left);
                ChatBot_Activity_Log.ActivityLog("TASK", $"Reminder Notified for Task: {task.Title}");
                task.ReminderNotified = true;
            }
        }



        private static void ResetState()
        {
            currentAction = null;
            selectedTaskIndex = null;
            tempTask = null;
            isWaitingForReminderInput = false;
        }






    }
}
