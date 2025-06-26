using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace POE_ChatBot_ST10438817
{

    public static class DataDictionary
    {

  

        public delegate void FavoriteTopicHandler(string topic);

        public static event FavoriteTopicHandler OnFavoriteTopicMentioned;

        // Create a single instance of Random and reuse it
        private static Random random = new Random();

        public static Dictionary<string, string> chatResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, List<string>> cyberResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, List<string>> sentimentResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        public static void NotifyFavoriteTopicMentioned(string topic)
        {
            OnFavoriteTopicMentioned?.Invoke(topic);
        }

        public static string GetRandomCyberResponse(string key)
        {
            // This method will allow me to display random responses to the user for cyber content
            if (cyberResponses.TryGetValue(key, out List<string> randomResponses) && randomResponses.Count > 0)
            {
                return randomResponses[random.Next(randomResponses.Count)];
            }
            return "I don't have a response for that.";
        }

        public static string GetRandomSentimentResponse(string key)
        {
            // This method will allow me to display random responses to the user for sentiment content
            if (sentimentResponses.TryGetValue(key, out List<string> randomResponses) && randomResponses.Count > 0)
            {
                return randomResponses[random.Next(randomResponses.Count)];
            }
            return "I don't have a response for that.";
        }

        public static void DisplayResponse()
        {
            // Greetings
            List<string> greetings = new List<string> { "hello", "hi", "hey", "greetings" };
            foreach (var greeting in greetings)
            {
                chatResponses.Add(greeting, ChatBot_Dialogue.DisplayGreeting());
            }

            // Goodbyes
            List<string> goodbyes = new List<string> { "goodbye", "bye", "see you later", "exit" };
            foreach (var goodbye in goodbyes)
            {
                chatResponses.Add(goodbye, ChatBot_Dialogue.DisplayGoodBye());
            }

            // Thanks
            List<string> thanks = new List<string> { "thanks", "thank", "thank you so much", "thank you" };
            foreach (var thank in thanks)
            {
                chatResponses.Add(thank, ChatBot_Dialogue.DisplayYourWelcomeMessage());
            }

            // Purposes
            List<string> purposes = new List<string> { "purpose", "what is your purpose?", "what is your purpose" };
            foreach (var purpose in purposes)
            {
                chatResponses.Add(purpose, ChatBot_Dialogue.DisplayChatBoxPurpose());
            }

            // Cybersecurity topics
            List<string> phishings = new List<string> { "phishing", "facts about phishing", "talk about phishing", "discuss about phishing", "what is phishing?" };
            foreach (var phishing in phishings)
            {
                cyberResponses.Add(phishing, ChatBot_Dialogue.PhishingFacts());
            }

            List<string> passwords = new List<string> { "passwords", "facts about passwords", "talk about passwords", "discuss about passwords" };
            foreach (var password in passwords)
            {
                cyberResponses.Add(password, ChatBot_Dialogue.PasswordFacts());
            }

            List<string> privacies = new List<string> { "privacy", "facts about privacy", "talk about privacy", "discuss about privacy" };
            foreach (var privacy in privacies)
            {
                cyberResponses.Add(privacy, ChatBot_Dialogue.PrivacyFacts());
            }

            // Sentiment responses
            List<string> passwordSentiment = new List<string>
            {
                "concerned about password",
                "worried about password",
                "frustrated about password",
                "password safety",
                "password problem",
                "password issue",
                "password worry",
                "scared of password"
            };

            List<string> phishingSentiment = new List<string>
            {
                "concerned about phishing",
                "worried about phishing",
                "frustrated about phishing",
                "phishing fear",
                "phishing problem",
                "phishing issue",
                "phishing worry",
                "scared of phishing"
            };

            List<string> privacySentiment = new List<string>
            {
                "concerned about privacy",
                "worried about privacy",
                "frustrated about privacy",
                "privacy fear",
                "privacy problem",
                "privacy issue",
                "privacy worry",
                "scared of privacy"
            };

            foreach (var passwordSentimentFact in passwordSentiment)
            {
                sentimentResponses.Add(passwordSentimentFact, ChatBot_Dialogue.PasswordSentiment());
            }

            foreach (var phishingSentimentFact in phishingSentiment)
            {
                sentimentResponses.Add(phishingSentimentFact, ChatBot_Dialogue.PhishingSentiment());
            }

            foreach (var privacySentimentFact in privacySentiment)
            {
                sentimentResponses.Add(privacySentimentFact, ChatBot_Dialogue.PrivacySentiment());
            }
        }




    }//end of method



 }//end of class


