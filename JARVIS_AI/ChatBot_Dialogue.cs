using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ST10438817_POE_PART3_CHATBOT;


namespace POE_ChatBot_ST10438817
{
    public static class ChatBot_Dialogue
    {
        // Create a single instance of Random and reuse it
        private static Random random = new Random();

        public static UserDetails CurrentUser;

        public static void DisplayConfigureUser()
        {
            //this method will prompt the user to enter thier name
            //so chatbot can refer to the user by name, thus more realistic convo

            
          ChatBot_Characteristics.AddMessage($@"{ChatBot_Characteristics.DisplayChatBotDialog()}Hello, mysterious traveler of the digital realm! I am your cyber guide, ready to navigate the vast expanse of this virtual universe with you. But first, I need to know, what name shall I inscribe in my database for this adventure?", HorizontalAlignment.Left);

        }


        public static void DisplayUserFavouriteTopic()
        {
            /*
             This method prompts the user to enter their favourite topic regarding cybersecurity
             and stores it in the user details class.
            */

            
            ChatBot_Characteristics.AddMessage($@"{ChatBot_Characteristics.DisplayChatBotDialog()}In order for me to get to know you better...I need to know what topics regarding cybersecurity are you interested in?", HorizontalAlignment.Left);

            
        }


        public static string DisplayGreeting()
        {
            //if the user say hello again but no need to get the user's name
          
            return $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Greetings, traveler of the digital realm! I am your ChatBot companion, ready to explore the vast cosmos of 
        technology with you!! ";
        }


        public static string DisplayGoodBye()
        {
            //this method is basically the chatbot saying goodbye to the user
         
            return $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Thank you for venturing into the digital expanse with me, {CurrentUser.UserName}! Until our paths cross again in the infinite 
        matrix of cyberspace, may your code run smoothly and your firewalls stay strong.

        Signing off... but never truly gone.
        Goodbye for now!
";

        }


        public static string DisplayChatBoxPurpose()
        {
            //this method explains the purpose of the chat bot
         
            return $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Well {CurrentUser.UserName}, I am your digital companion, forged from code and curiosity, here to assist, inspire, and explore 
        with you. Whether you seek answers, creativity, or just a friendly byte of conversation, my purpose is to 
        illuminate your path through the vast digital cosmos. Think of me as your guide, your brainstorm buddy, 
        and your virtual co-pilot—ready to decode the universe, one query at a time.";
        }


        public static void DisplayChatBotTopics(UserDetails userDetails)
        {
            //this method shows what questions/topics the chat bot can answer, anything else is put of scope
            CurrentUser = userDetails;

            ChatBot_Characteristics.AddMessage($@"{ChatBot_Characteristics.DisplayChatBotDialog()}Well {CurrentUser.UserName}, I’m your virtual sidekick, here to help, inspire, and explore with you. Think of me as your guide through the info universe—ready to answer questions, spark ideas, or just chat. Let’s uncover something amazing together! 

So, what shall we uncover together today?:
1. Password Security
2. Phishing
3. Privacy Settings
4. My Purpose
5. Exit", HorizontalAlignment.Left);

        }


        public static string DisplayYourWelcomeMessage()
        {
           
            return $@"{ChatBot_Characteristics.DisplayChatBotDialog()}No thanks necessary, stellar traveler! Helping you is my prime directive, and your satisfaction is my reward. 
        Now, let’s keep this cosmic collaboration rolling. What’s next on our digital journey?";
        }


        public static string DisplayErrorMessage()
        {
           
            return ($@"{ChatBot_Characteristics.DisplayChatBotDialog()}It seems my neural circuits have encountered a cosmic glitch. Your query has ventured beyond the boundaries of
        my digital horizon—either it’s too advanced for my algorithms, or I’m still learning to decode this corner 
        of the universe. But fear not! Let’s try rewording your question, or perhaps we can explore a different star 
        in the galaxy of knowledge together. Your patience is my power source! Try again {CurrentUser.UserName}.");
        }





        public static List<string> PhishingFacts()
        {
            List<string> PhishingFactsContent = new List<string>
            {
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Well {CurrentUser.UserName}, Phishing scams drain billions yearly, making it the ultimate digital goldmine for cybercriminals.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Phishing emails now use AI magic to sound flawless, making them harder than ever to spot.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Cybercriminals now weaponize AI like ChatGPT to craft phishing emails that feel eerily human.
        Shocking i know, {CurrentUser.UserName}",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Phishing thrives in chaos, spiking during holidays, pandemics, and global crises. So be very alert druing this 
        seasons {CurrentUser.UserName}",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}30% of phishing emails get opened, and 12% of people click—humans are the weakest link.",
            };

            return PhishingFactsContent;
        }




        public static List<string> PasswordFacts()
        {
            List<string> PasswordFactsContent = new List<string>
            {
               $@"{ChatBot_Characteristics.DisplayChatBotDialog()}The average person has 100 passwords to remember—no wonder we reuse them, {CurrentUser.UserName}!",
               $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Biometric passwords (like fingerprints) are cool, but they’ve been hacked— Yes {CurrentUser.UserName}, even your fingerprint isn’t 
        foolproof!",
               $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Two-factor authentication (2FA) blocks 99.9% of automated attacks—yet only 28% of people use it. You should 
        really consider using this method, {CurrentUser.UserName}",
               $@"{ChatBot_Characteristics.DisplayChatBotDialog()}80% of hacking breaches are caused by weak or stolen passwords.",
               $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Don't ever use your personal details when creating your password.",
            };

            return PasswordFactsContent;
        }//end of method



        public static List<string> PrivacyFacts()
        {
            List<string> PrivacyFactsContent = new List<string>
            {
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Privacy is a human right, not a privilege. It’s about control over your personal data.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Your digital footprint is like a breadcrumb trail—companies track it to know you better than you know yourself.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Data breaches expose millions of records daily, making privacy a top concern.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Privacy laws vary globally, but the trend is clear: more protection for individuals and stricter rules for companies.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}You have the right to be forgotten online. If you want to erase your digital past, you can do so."
            };
            return PrivacyFactsContent;
        }//end of method

        public static List<string> PasswordSentiment()
        {
            List<string> PasswordSentimentContent = new List<string>
            {
                    $@"{ChatBot_Characteristics.DisplayChatBotDialog()}I totally understand your concern—password safety is a serious issue, and it’s great that you’re thinking 
        about it proactively. With cyber threats becoming more sophisticated every day, it's important to take 
        protective steps. If you’d like, I can help walk you through best practices like using a password manager, 
        enabling two-factor authentication, and spotting phishing attempts.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Your worries are completely valid, {CurrentUser.UserName}. Passwords are the keys to your digital life, and keeping them safe 
        is essential. I care deeply about making sure you feel secure, and I’m here to help you strengthen your 
        defenses. Let’s go over some practical tips to reduce risk and keep your accounts safe from unauthorized access.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}It’s good that you’re thinking about this. Many people don’t realize the importance of strong, unique passwords 
        until it’s too late. I’m genuinely concerned for your digital security and want to make sure you’re protected. 
        Let’s go over how to set up strong passwords and look into options like password managers and biometric login 
        features.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}You’re right to be worried—online safety is more important than ever. I share your concern, and I want to make 
        sure you’re equipped with the tools and knowledge to stay protected. Whether it’s avoiding reused passwords or 
        setting up two-factor authentication, I can help guide you through it step by step.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}I'm really glad you brought this up, {CurrentUser.UserName}. Password safety isn’t something to take lightly. I’m here to 
        support you, and it’s my priority to help you feel confident in your online security. We can look at ways to 
        safeguard your information effectively, so you don’t have to worry as much going forward."
            };
            return PasswordSentimentContent;
        }


        public static List<string> PhishingSentiment()
        {
            List<string> PhishingSentimentContent = new List<string>
            {
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}I understand your concern, {CurrentUser.UserName}. Phishing is a serious issue, and it’s great that you’re thinking about it 
        proactively. With cyber threats becoming more sophisticated every day, it's important to take protective steps. 
        If you’d like, I can help walk you through best practices like spotting phishing attempts and securing your 
        accounts.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Your worries are completely valid, {CurrentUser.UserName}. Phishing scams are becoming increasingly sophisticated, and it’s 
        essential to stay vigilant. I care deeply about making sure you feel secure, and I’m here to help you strengthen        your defenses. Let’s go over some practical tips to reduce risk and keep your accounts safe from unauthorized 
        access.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}It’s good that you’re thinking about this. Many people don’t realize the importance of being cautious online 
        until it’s too late. I’m genuinely concerned for your digital security and want to make sure you’re protected. 
        Let’s go over how to spot phishing attempts and look into options like two-factor authentication.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}You’re right to be worried—online safety is more important than ever. I share your concern, and I want to make 
        sure you’re equipped with the tools and knowledge to stay protected. Whether it’s avoiding suspicious emails or         verifying links before clicking, I can help guide you through it step by step.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}I'm really glad you brought this up, {CurrentUser.UserName}. Phishing is a serious issue that affects many people. I’m here to             support you, and it’s my priority to help you feel confident in your online security. We can look at ways to 
        safeguard your information effectively, so you don’t have to worry as much going forward."
            };
            return PhishingSentimentContent;
        }

        public static List<string> PrivacySentiment()
        {
            List<string> PrivacySentimentContent = new List<string>
            {
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}I understand your concern, {CurrentUser.UserName}. Privacy is a serious issue, and it’s great that you’re thinking about it 
        proactively. With data breaches becoming more common, it's important to take protective steps. If you’d like, I 
        can help walk you through best practices like securing your accounts and understanding privacy settings.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}Your worries are completely valid, {CurrentUser.UserName}. Privacy concerns are becoming increasingly prevalent, and it’s 
        essential to stay vigilant. I care deeply about making sure you feel secure, and I’m here to help you strengthen        your defenses. Let’s go over some practical tips to reduce risk and keep your information safe.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}It’s good that you’re thinking about this. Many people don’t realize the importance of protecting their privacy         until it’s too late. I’m genuinely concerned for your digital security and want to make sure you’re protected. 
        Let’s go over how to safeguard your information and look into options like privacy settings.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}You’re right to be worried—online safety is more important than ever. I share your concern, and I want to make 
        sure you’re equipped with the tools and knowledge to stay protected. Whether it’s avoiding oversharing or 
        understanding data collection practices, I can help guide you through it step by step.",
                $@"{ChatBot_Characteristics.DisplayChatBotDialog()}I'm really glad you brought this up, {CurrentUser.UserName}. Privacy is a serious issue that affects many people. I’m here to 
        support you, and it’s my priority to help you feel confident in your online security. We can look at ways to 
        safeguard your information effectively, so you don’t have to worry as much going forward."
            };
            return PrivacySentimentContent;
        }


  
        public static void GetRandomResponseFavourtieTopic(string userInput)
        {
            if (CurrentUser.UserFavouriteTopic != null &&
                userInput.IndexOf(CurrentUser.UserFavouriteTopic, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                string[] FavourtieTopicResponse = {
            $"Ohh yes, I remember — {CurrentUser.UserFavouriteTopic} was your favourite cybersecurity topic!",
            $"That's right! You're really into {CurrentUser.UserFavouriteTopic}, aren't you?",
            $"Oh yes, you brought {CurrentUser.UserFavouriteTopic} up earlier — very relevant in today's digital world!",
            $"I remember you mentioned {CurrentUser.UserFavouriteTopic} before. Let's dive deeper into it!"
        };

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(FavourtieTopicResponse[random.Next(FavourtieTopicResponse.Length)]);
            }
        }




    }
}
