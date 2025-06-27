using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using FuzzySharp;
using POE_ChatBot_ST10438817;
using ST10438817_POE_PART3_CHATBOT.JARVIS_AI;



namespace ST10438817_POE_PART2_CHATBOT
{
    public static class Chatbot_Quiz
    {
        
        private static readonly List<string> MultipleChoiceAnswers = new List<string>
        {
            "c", "d", "c", "b"
        };


        private static readonly List<string> Questions = new List<string>
        {
            @"1. What is the safest way to create a strong password?
              A) Use your pet’s name
              B) Use your birthdate
              C) Use a mix of letters, numbers, and symbols
              D) Use the word password",

            @"2. Two-factor authentication requires:
              A) Just a password
              B) A password and a backup file
              C) A username and date of birth
              D) A password and a second form of verification",

            @"3. When should you update your antivirus software?
              A) Once a year
              B) Only if your PC is slow
              C) Whenever a new update is available
              D) Never",

            @"4. What does a firewall do?
              A) Stores passwords
              B) Protects against unauthorized access
              C) Cleans your hard drive
              D) Opens network ports",

            "5. Why is using the same password for multiple accounts dangerous?",
            "6. Name one way to identify a phishing email.",
            "7. What is social engineering?",
            "8. Explain the risks of using public Wi-Fi and how to stay safe.",
            "9. Describe what an activity log is in the context of cybersecurity and its benefits.",
            "10. How does a firewall protect your device and network?",
            "11. Discuss the role of a cybersecurity awareness chatbot.",
            "12. Describe the steps a user should take after suspecting their device is infected with malware.",
            "13. Describe how phishing attacks work.",
            "14. What’s one benefit of using a password manager?",
            "15. How can users protect their personal information on social media?"
        };


        private static readonly List<string> QuestionsAnswers = new List<string>
        {
            "Using the same password for multiple accounts is dangerous because if one account is hacked, all your other accounts can be easily accessed using the same password.",
            "Check for suspicious links or email addresses that don’t match the sender.",
            "Social engineering involves manipulating people into giving up confidential information.",
            "Public Wi-Fi is often unsecured, allowing hackers to intercept your data.",
            "An activity log records user actions and access events, helping detect unauthorized activity and ensuring accountability.",
            "A firewall acts as a barrier between your device and potential threats from the internet. It filters incoming and outgoing traffic based on security rules, blocking unauthorized access and helping prevent malware attacks.",
            "A cybersecurity chatbot educates users about online threats, offers safety tips, and helps build good habits. It provides instant, personalized advice, quizzes for learning, reminders, and simulated conversations to reinforce cybersecurity practices.",
            "Immediately disconnect from the internet, run a full antivirus scan, remove any suspicious files, change all passwords, and update your software. If the problem persists, consider restoring the system or seeking professional help.",
            "Phishing attacks trick users into revealing personal data by pretending to be trustworthy sources, often via email or text.",
            "It helps you generate and store strong, unique passwords for each account.",
            "Users can protect their personal information on social media sby setting their profiles to private, avoiding sharing sensitive details like home address or phone number, using strong passwords, enabling two-factor authentication, and being cautious about accepting friend requests from strangers."
        };

        private static int currentQuestionIndex = 0;
        private static int score = 0;

        public static bool IsQuizActive { get; private set; }// check is the quiz is currently happening


        // Event for Quiz UI to subscribe to: sends message + alignment for bubble style
        public static event Action<string, HorizontalAlignment> DisplayQuizMessage;


        private static readonly List<string> PastScores = new List<string>();// track past scores






        public static void StartQuiz()
        {
            //Initialize quiz state and scores and prep the session for questions
            currentQuestionIndex = 0;
            score = 0;
            IsQuizActive = true;

            ChatBot_Activity_Log.ActivityLog("QUIZ", "User Started The Quiz");

            DisplayQuizMessage?.Invoke("Starting Quiz! Answer the following questions:", HorizontalAlignment.Left);
            DisplayCurrentQuestion();
        }



        private static void DisplayCurrentQuestion()
        {
            // methd used to display each question after each answer
            if (currentQuestionIndex < Questions.Count)
            {
                DisplayQuizMessage?.Invoke(Questions[currentQuestionIndex], HorizontalAlignment.Left);
            }
        }




        public static void SubmitAnswer(string userAnswer)
        {
            //method that is processed once the user submits their final asnwer
            
            if (!IsQuizActive) return;

            userAnswer = userAnswer.Trim().ToLower();

            if (currentQuestionIndex < MultipleChoiceAnswers.Count)
            {
                string correct = MultipleChoiceAnswers[currentQuestionIndex];
                if (userAnswer == correct)
                {
                    DisplayQuizMessage?.Invoke("Correct!", HorizontalAlignment.Left);
                    score++;
                    ChatBot_Activity_Log.ActivityLog("QUIZ", $"User Answered Question {currentQuestionIndex + 1} Correctly. \n Current score: {score}/{currentQuestionIndex + 1}");
                }
                else
                {
                    DisplayQuizMessage?.Invoke($"Incorrect. The correct answer was: {correct}", HorizontalAlignment.Left);
                    ChatBot_Activity_Log.ActivityLog("QUIZ", $"User Answered Question {currentQuestionIndex + 1} Incorrectly. \n Current score: {score}/{currentQuestionIndex + 1}");

                }
            }
            else
            {
                int openEndedIndex = currentQuestionIndex - MultipleChoiceAnswers.Count;
                if (openEndedIndex < QuestionsAnswers.Count)
                {
                    string expectedAnswer = QuestionsAnswers[openEndedIndex];
                    if (FuzzyMatch(userAnswer, expectedAnswer))
                    {
                        DisplayQuizMessage?.Invoke("Nice answer! Keep Going!!", HorizontalAlignment.Left);
                        score++;
                        ChatBot_Activity_Log.ActivityLog("QUIZ", $"User Answered Open-Ended Question {currentQuestionIndex + 1} Correctly. \n Current score: {score}/{currentQuestionIndex + 1}");
                    }
                    else
                    {
                        DisplayQuizMessage?.Invoke($"Hmm, not quite. Correct answer: {expectedAnswer}", HorizontalAlignment.Left);
                        ChatBot_Activity_Log.ActivityLog("QUIZ", $"User Answered Open-Ended Question {currentQuestionIndex + 1} Incorrectly. \n Current score: {score}/{currentQuestionIndex + 1}");

                    }
                }
            }




            // Move to the next question
            currentQuestionIndex++;

            if (currentQuestionIndex < Questions.Count)
            {
                DisplayCurrentQuestion();
            }
            else
            {
                IsQuizActive = false;
                string finalScoreMessage = $"Quiz complete! Your final score: {score}/{Questions.Count}";
                DisplayQuizMessage?.Invoke(finalScoreMessage, HorizontalAlignment.Left);
                ChatBot_Activity_Log.ActivityLog("QUIZ", $"User Completed The Quiz. Final Score: {score}/{Questions.Count}");

                // Save the final score to history
                PastScores.Add(finalScoreMessage);
            }
        }



        public static bool FuzzyMatch(string userInput, string correctAnswer, int threshold = 60)
        {
            int score = Fuzz.PartialRatio(userInput.ToLower(), correctAnswer.ToLower());
            return score >= threshold;
        }

        public static string GetPastScores()
        {
            if (PastScores.Count == 0)
            {
                return "No past scores found yet. Try completing a quiz first!";
            }

            StringBuilder result = new StringBuilder("Your past quiz scores:\n");

            for (int i = 0; i < PastScores.Count; i++)
            {
                result.AppendLine($"Quiz {i + 1}: {PastScores[i]}");
            }

            return result.ToString();
        }




    }//end of class
}


