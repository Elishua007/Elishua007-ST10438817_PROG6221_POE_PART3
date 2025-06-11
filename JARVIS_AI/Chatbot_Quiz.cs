using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POE_ChatBot_ST10438817;
using FuzzySharp;
using System.Windows;
using System.Runtime.Intrinsics.X86;
using System.Windows.Media;
using System.Xml.Linq;

namespace ST10438817_POE_PART2_CHATBOT
{
    internal class Chatbot_Quiz
    {
        public static List<string> MultipleChoiceAnwsers = new List<string>()
        {
            "c", "d", "c", "b"
        };


        public static List<string> Questions = new List<string>()
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

         @"5. Why is using the same password for multiple accounts dangerous?",
         @"6. Name one way to identify a phishing email.",
         @"7. What is social engineering and how can users protect themselves from it?",
         @"8. Explain the risks of using public Wi-Fi and how to stay safe.",
         @"9. Describe what an activity log is in the context of cybersecurity and its benefits.",
         @"10.How does a firewall protect your device and network?",
         @"11. Discuss the role of a cybersecurity awareness chatbot and how it can help users stay safe online.",
         @"12. Describe the steps a user should take after suspecting their device is infected with malware.",
         @"13. Describe how phishing attacks work and how to avoid falling for them.",
         @"14. What’s one benefit of using a password manager?",
         @"15. How can users protect their personal information on social media?",
      

        };


        public static List<string> QuestionsAnswers = new List<string>()
        {
            "Using the same password for multiple accounts is dangerous because if one account is hacked, all your other accounts can be easily accessed using the same password.",
            "Check for suspicious links or email addresses that don’t match the sender.",
            "Social engineering involves manipulating people into giving up confidential information. Examples include phishing and baiting. To protect yourself, never trust unknown sources, verify identities, and think before clicking on links.",
            "Public Wi-Fi is often unsecured, allowing hackers to intercept your data. To stay safe, avoid logging into sensitive accounts, use a VPN, and never share personal info over public networks.",
            "Phishing attacks trick users into revealing personal data by pretending to be trustworthy sources, often via email or text. To avoid them, check for suspicious links, spelling errors, and never share sensitive information online without verification.",
            "A firewall acts as a barrier between your device and potential threats from the internet. It filters incoming and outgoing traffic based on security rules, blocking unauthorized access and helping prevent malware attacks.",
            "A cybersecurity chatbot educates users about online threats, offers safety tips, and helps build good habits. It provides instant, personalized advice, quizzes for learning, reminders, and simulated conversations to reinforce cybersecurity practices.",
            "Immediately disconnect from the internet, run a full antivirus scan, remove any suspicious files, change all passwords, and update your software. If the problem persists, consider restoring the system or seeking professional help.",
            "Phishing attacks trick users into revealing personal data by pretending to be trustworthy sources, often via email or text. To avoid them, check for suspicious links, spelling errors, and never share sensitive information online without verification.",
            "It helps you generate and store strong, unique passwords for each account.",
            "Users can protect their personal information on social media by setting their profiles to private, avoiding sharing sensitive details like home address or phone number, using strong passwords, enabling two-factor authentication, and being cautious about accepting friend requests from strangers.",


        };


        public static bool FuzzyMatch(string userInputAnswer, string correctAnswer)
        {
            int score = Fuzz.PartialRatio(userInputAnswer.ToLower(), correctAnswer.ToLower());
            return score >= 75;// the minimum score for a match is 75%, which i think is good enough
        }




        public static void DisplayQuiz()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine("WELCOME TO CHATBOT QUIZ MANIA!");
            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("MULTIPLE CHOICE QUESTIONS");
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.WriteLine("________________________________________________________________________________________________________________________");


            int score = 0;

            for (int i = 0; i < MultipleChoiceAnwsers.Count; i++)
            {

                ChatBot_Characteristics.AddMessage(ChatBot_Dialogue.QuizQuestions(i), HorizontalAlignment.Left); // assuming QuizQuestions(i) displays the question

                string userAnswer = Console.ReadLine().ToLower();

                if (userAnswer == MultipleChoiceAnwsers[i])
                {
                    ChatBot_Characteristics.ChatBot_Colour();
                    Console.WriteLine($"{ChatBot_Characteristics.DisplayChatBotDialog()}Correct! Well done!");
                    score++;
                }
                else
                {
                    ChatBot_Characteristics.ChatBot_Colour();
                    Console.WriteLine($"{ChatBot_Characteristics.DisplayChatBotDialog()}Incorrect. The correct answer was {MultipleChoiceAnwsers[i]}.");
                }
            }


            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("CHALLENGING QUESTIONS");
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.WriteLine("");

            for (int i = 0; i < QuestionsAnswers.Count; i++)
            {
                
                ChatBot_Dialogue.QuizQuestions(i + MultipleChoiceAnwsers.Count); // offset index

                
                Console.Write($"{ChatBot_Characteristics.DisplayUserDialog()}");
                string userInput = Console.ReadLine().ToLower();

                bool isCorrect = FuzzyMatch(userInput, QuestionsAnswers[i]);

              
                if (isCorrect)
                {
                    Console.WriteLine($"{ChatBot_Characteristics.DisplayChatBotDialog()}Nice answer! Super Champ!!!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"{ChatBot_Characteristics.DisplayChatBotDialog()}Hmm, that wasn't quite right. Correct answer:{QuestionsAnswers[i]}");
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.WriteLine("________________________________________________________________________________________________________________________");


            
            Console.WriteLine($"\nYour final quiz score: {score}/{MultipleChoiceAnwsers.Count + QuestionsAnswers.Count}");
        }







    }
}
