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

        // List to store tasks
        private static List<TaskItem> Tasks = new();

        // Current action being processed (create, update, delete, complete)
        private static string currentAction = null;

        // Index of the selected task for update/delete operations
        private static int? selectedTaskIndex = null;

        // Temporary TaskItem used during task creation or update
        private static TaskItem tempTask = null;

        // Flag to track reminder input waiting state (not used in your code but reserved)
        private static bool isWaitingForReminderInput = false;

        // Indicates whether the task manager is active
        public static bool IsActive { get; private set; }

        // Event to display messages to UI, with alignment control
        public static event Action<string, HorizontalAlignment> DisplayTaskMessage;

        // Timer to periodically check for task reminders
        private static Timer reminderTimer;

        // Represents a task with title, description, reminder, notification flag, and completed status
        private class TaskItem
        {
            public string TaskTitle { get; set; }
            public string TaskDescription { get; set; }
            public DateTime? TaskReminderTime { get; set; }
            public bool TaskReminderNotified { get; set; } = false;
            public bool TaskCompletedStatus { get; set; } = false;
        }




        // Static constructor to start reminder timer
        static ChatBot_Task()
        {
            reminderTimer = new Timer(_ => ProcessTaskReminders(), null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
        }




        // Activate the task manager and display welcome message with commands
        public static void ActivateTaskManager()
        {
            IsActive = true;
            currentAction = null;
            selectedTaskIndex = null;
            tempTask = null;
            isWaitingForReminderInput = false;

            DisplayTaskMessage?.Invoke(
                "WELCOME TO THE TASK MANAGER!\n" +
                "-------------------------------------------------------------------\n" +
                "You can manage your tasks with the following commands:\n" +
                "CREATE - Create New Task\n" +
                "UPDATE - Modify Existing Task\n" +
                "DELETE - Remove Task\n" +
                "LISTS - View All Tasks\n" +
                "COMPLETED - Mark Task Done",
                HorizontalAlignment.Left
            );
        }





        // Process user input based on current action or initiate new actions
        public static void ProcessUserInput(string input)
        {
            if (!IsActive) return;

            input = input.Trim().ToLower();

            // If no ongoing action, interpret command
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
                else if (input.Contains("complete"))
                {
                    currentAction = "complete";
                    DisplayTaskMessage?.Invoke("Enter Task Number to Mark as Completed (or type 'list' to view tasks)", HorizontalAlignment.Left);
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid Input! You can only Create, Update, Delete, Lists Tasks.", HorizontalAlignment.Left);
                }

                return;
            }

            // Process input based on the ongoing action
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
                case "complete":
                    CompleteTask(input);
                    break;
            }
        }




        // Handle creation of a new task step-by-step
        private static void CreateTask(string input)
        {
            if (string.IsNullOrEmpty(tempTask.TaskTitle))
            {
                // Validate non-empty title
                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayTaskMessage?.Invoke("Task Title cannot be empty. Please enter a valid title.", HorizontalAlignment.Left);
                    return;
                }
                tempTask.TaskTitle = input;
                DisplayTaskMessage?.Invoke("Enter Task Description", HorizontalAlignment.Left);
            }
            else if (string.IsNullOrEmpty(tempTask.TaskDescription))
            {
                // Validate non-empty description
                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayTaskMessage?.Invoke("Task Description cannot be empty. Please enter a valid description.", HorizontalAlignment.Left);
                    return;
                }
                tempTask.TaskDescription = input;
                DisplayTaskMessage?.Invoke("Enter Reminder Date and Time (e.g. 2025-06-30 14:30), or 'skip' to skip", HorizontalAlignment.Left);
            }
            else if (!isWaitingForReminderInput)
            {
                // Validate reminder input
                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayTaskMessage?.Invoke("Reminder input cannot be empty. Please enter a valid date/time or type 'skip'.", HorizontalAlignment.Left);
                    return;
                }

                if (input == "skip")
                {
                    tempTask.TaskReminderTime = null;
                    CompleteTaskCreation();
                }
                else if (DateTime.TryParse(input, out DateTime reminder))
                {
                    tempTask.TaskReminderTime = reminder;
                    CompleteTaskCreation();
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid Date Format. Please try again or type 'skip'.", HorizontalAlignment.Left);
                }
            }
        }




        // Finalize and add new task to list
        private static void CompleteTaskCreation()
        {
            Tasks.Add(tempTask);
            DisplayTaskMessage?.Invoke(@$"TASK CREATED 
--------------------------------------------------------
TASK TITLE: {tempTask.TaskTitle}
TASK DESCRIPTION: {tempTask.TaskDescription}
TASK REMINDER: {(tempTask.TaskReminderTime?.ToString() ?? "No reminder")}
TASK COMPLETED STATUS: {tempTask.TaskCompletedStatus}
--------------------------------------------------------", HorizontalAlignment.Left);

            ChatBot_Activity_Log.ActivityLog("TASK", $"Created Task: {tempTask.TaskTitle}");
            ResetState();
        }





        // Apply changes from tempTask to the selected task
        private static void ApplyUpdateTaskDetails()
        {
            var task = Tasks[selectedTaskIndex.Value];
            task.TaskTitle = tempTask.TaskTitle;
            task.TaskDescription = tempTask.TaskDescription;
            task.TaskReminderTime = tempTask.TaskReminderTime;
            task.TaskReminderNotified = false;

            // Keep the completion status unchanged
            task.TaskCompletedStatus = task.TaskCompletedStatus;

            ChatBot_Activity_Log.ActivityLog("TASK", $"Updated Task: {task.TaskTitle}");

            DisplayTaskMessage?.Invoke(@$"TASK UPDATED 
---------------------------------------------------------
TASK TITLE: {task.TaskTitle}
TASK DESCRIPTION: {task.TaskDescription}
TASK REMINDER: {(task.TaskReminderTime?.ToString() ?? "No reminder")}
TASK COMPLETED STATUS: {task.TaskCompletedStatus}", HorizontalAlignment.Left);

            ResetState();
        }






        // Handle task update process step-by-step
        private static void UpdateTask(string UserInput)
        {
            // First select the task index if not already selected
            if (selectedTaskIndex == null)
            {
                if (UserInput.Contains("list"))
                {
                    DisplayTaskMessage?.Invoke(DisplayTaskList(), HorizontalAlignment.Left);
                    return;
                }

                if (int.TryParse(UserInput, out int userIndex))
                {
                    int index = userIndex - 1;

                    if (index >= 0 && index < Tasks.Count)
                    {
                        selectedTaskIndex = index;
                        tempTask = new TaskItem();
                        DisplayTaskMessage?.Invoke("Enter New Task Title", HorizontalAlignment.Left);
                    }
                    else
                    {
                        DisplayTaskMessage?.Invoke("Invalid Task Number. Please enter a number from the task list.", HorizontalAlignment.Left);
                    }
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid Input! Please enter a valid task number.", HorizontalAlignment.Left);
                }

                return;
            }

            // Collect new title, description, and reminder step-by-step with validation
            if (string.IsNullOrEmpty(tempTask.TaskTitle))
            {
                if (string.IsNullOrWhiteSpace(UserInput))
                {
                    DisplayTaskMessage?.Invoke("Task Title cannot be empty. Please enter a valid title.", HorizontalAlignment.Left);
                    return;
                }
                tempTask.TaskTitle = UserInput;
                DisplayTaskMessage?.Invoke("Enter New Task Description", HorizontalAlignment.Left);
            }
            else if (string.IsNullOrEmpty(tempTask.TaskDescription))
            {
                if (string.IsNullOrWhiteSpace(UserInput))
                {
                    DisplayTaskMessage?.Invoke("Task Description cannot be empty. Please enter a valid description.", HorizontalAlignment.Left);
                    return;
                }
                tempTask.TaskDescription = UserInput;
                DisplayTaskMessage?.Invoke("Enter New Reminder Date and Time (e.g. 2025-06-30 14:30), or type 'skip'", HorizontalAlignment.Left);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(UserInput))
                {
                    DisplayTaskMessage?.Invoke("Reminder input cannot be empty. Please enter a valid date/time or type 'skip'.", HorizontalAlignment.Left);
                    return;
                }

                if (UserInput.Trim().ToLower() == "skip")
                {
                    tempTask.TaskReminderTime = null;
                    ApplyUpdateTaskDetails();
                }
                else if (DateTime.TryParse(UserInput, out DateTime reminder))
                {
                    tempTask.TaskReminderTime = reminder;
                    ApplyUpdateTaskDetails();
                }
                else
                {
                    DisplayTaskMessage?.Invoke("Invalid Date Format. Please try again or type 'skip'.", HorizontalAlignment.Left);
                }
            }
        }



        // Delete a task by user input index
        private static void DeleteTask(string UserInput)
        {
            if (UserInput.Contains("list"))
            {
                DisplayTaskMessage?.Invoke(DisplayTaskList(), HorizontalAlignment.Left);
                return;
            }

            if (int.TryParse(UserInput, out int userNumber))
            {
                int index = userNumber - 1;

                if (index >= 0 && index < Tasks.Count)
                {
                    string title = Tasks[index].TaskTitle;
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
            else
            {
                DisplayTaskMessage?.Invoke("Invalid input. Please enter a valid task number.", HorizontalAlignment.Left);
            }
        }



        // Mark a task as completed by index input
        private static void CompleteTask(string UserInput)
        {
            if (UserInput.Contains("list"))
            {
                DisplayTaskMessage?.Invoke(DisplayTaskList(), HorizontalAlignment.Left);
                return;
            }

            if (int.TryParse(UserInput, out int index) && index >= 1 && index <= Tasks.Count)
            {
                var task = Tasks[index - 1];
                task.TaskCompletedStatus = true;
                ChatBot_Activity_Log.ActivityLog("TASK", $"Completed Task: {task.TaskTitle}");
                DisplayTaskMessage?.Invoke($"COMPLETED TASK\n---------------------------------------\n {index}. {task.TaskTitle}", HorizontalAlignment.Left);
                ResetState();
            }
            else
            {
                DisplayTaskMessage?.Invoke("Invalid Task Number. Please try again or type 'list'.", HorizontalAlignment.Left);
            }
        }




        // Generate and return formatted task list string
        private static string DisplayTaskList()
        {
            if (!Tasks.Any())
                return "No Tasks Available!";

            string list = "TASK LIST\n\n";

            for (int i = 0; i < Tasks.Count; i++)
            {
                var t = Tasks[i];
                string reminder = t.TaskReminderTime.HasValue ? t.TaskReminderTime.Value.ToString("g") : "No reminder";

                list += $"{i + 1}.\n";
                list += $"TASK TITLE: {t.TaskTitle}\n";
                list += $"TASK DESCRIPTION: {t.TaskDescription}\n";
                list += $"TASK REMINDER: {reminder}\n";
                list += $"TASK COMPLETED STATUS: {t.TaskCompletedStatus}\n";
                list += "----------------------------------\n";
            }

            ChatBot_Activity_Log.ActivityLog("TASK", "Displayed Task List");

            return list;
        }




        // Check for tasks due reminders and notify if not already notified
        private static void ProcessTaskReminders()
        {
            var now = DateTime.Now;
            foreach (var task in Tasks.Where(t => t.TaskReminderTime.HasValue && t.TaskReminderTime <= now && !t.TaskReminderNotified))
            {
                DisplayTaskMessage?.Invoke($"REMINDER \n-------------------------------------------\n TASK: {task.TaskTitle} is due now!", HorizontalAlignment.Left);
                ChatBot_Activity_Log.ActivityLog("TASK", $"Reminder Notified for Task: {task.TaskTitle}");
                task.TaskReminderNotified = true;
            }
        }



        // Reset temporary states after completing an action
        private static void ResetState()
        {
            currentAction = null;
            selectedTaskIndex = null;
            tempTask = null;
            isWaitingForReminderInput = false;
        }

    }



}









