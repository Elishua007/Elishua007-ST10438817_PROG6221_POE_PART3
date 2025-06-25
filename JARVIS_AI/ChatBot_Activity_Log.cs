using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10438817_POE_PART3_CHATBOT.JARVIS_AI
{
    public static class ChatBot_Activity_Log
    {


        // List to store all activity messages
        private static readonly List<string> activityLog = new();

        
        public static void ActivityLog(string activityType, string description)
        {

            // Log an activity with its type/category and description

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string entry = $"[{timestamp}] [{activityType.ToUpper()}] {description}";
            activityLog.Add(entry);
        }




        public static string DisplayActivityLog()
        {
            if (!activityLog.Any())
                return "No recent activities logged.";

            var recent = activityLog
                .TakeLast(5)
                .Reverse()
                .ToList();

            // Add numeric bullets
            string numberedList = "";
            for (int i = 0; i < recent.Count; i++)
            {
                numberedList += $"{i + 1}. {recent[i]}\n";
            }

            return "RECENT ACTIVITIES\n-------------------------------------------------------------------------------------------\n" + numberedList+"\n";
        }



    }





}

