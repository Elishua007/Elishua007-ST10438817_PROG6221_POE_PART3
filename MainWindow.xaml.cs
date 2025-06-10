using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JARVIS_AI;
using POE_ChatBot_ST10438817;

namespace ST10438817_POE_PART3_CHATBOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool IsWaitingForUsername { get; set; } = true;
        public bool IsWaitingForTopic { get; set; } = false;

        public delegate void FavoriteTopicHandler(string topic);

        public static event FavoriteTopicHandler OnFavoriteTopicMentioned;
        private static UserDetails CurrentUser { get; set; }


        public static UserDetails ud = new UserDetails();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string chatBotLogo = ChatBot_Logo.DisplayIntroLogo();
            ChatBotLogo.Text = chatBotLogo;

            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ChatUI.Visibility != Visibility.Visible)
            {
                // Hide logo
                ChatBotLogo.Text = "";
                ChatBotLogo.Visibility = Visibility.Collapsed;

                // Show chat UI container
                ChatUI.Visibility = Visibility.Visible;


                // Focus input box
                UserInputBox.Focus();
                
                ChatBot_Dialogue.DisplayConfigureUser();


                e.Handled = true;
            }

          
        }



        //private void ConfigureUser()
        //{
        //    try
        //    {
        //        string UserInput = UserInputBox.Text.Trim();

        //        ChatBot_Characteristics.AddMessage(UserInput, HorizontalAlignment.Right);

        //        if (string.IsNullOrEmpty(UserInput))
        //        {
        //            ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Invalid username. Please try again!", HorizontalAlignment.Left);
        //        }
        //        else
        //        {
        //            // Store username
        //            ud.UserName = UserInput;

        //            // Display chatbot's next step or topic prompt
        //            ChatBot_Dialogue.DisplayChatBotTopics(ud);

        //            // Clear input box and focus for next input
        //            UserInputBox.Clear();
        //            UserInputBox.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}


        //private void UserFavouriteTopic() 
        //{



        //    try
        //    {

        //        HashSet<string> validTopics = new HashSet<string> { "passwords", "phishing", "privacy" };//impelenting a generic collection
        //        bool isChecked = false;

        //        string UserInput = UserInputBox.Text.Trim();

        //        ChatBot_Characteristics.AddMessage(UserInput, HorizontalAlignment.Right);

        //        if (string.IsNullOrWhiteSpace(UserInput))
        //        {

        //            ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Invalid favourite topic. Please try again.", HorizontalAlignment.Left);
        //            throw new Exception("Invalid favourite topic.");

        //        }

        //        string[] splitWords = UserInput.ToLower().Split(' '); //splits the sentence into single words and populates array

        //        foreach (string word in splitWords)
        //        {
        //            if (validTopics.Contains(word))
        //            {
        //                isChecked = true;
        //                ud.UserFavouriteTopic = word;
        //                Console.WriteLine($"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! I'll remember that you're interested in {ud.UserFavouriteTopic}. It's a crucial part of cybersecurity.");
        //                break;
        //            }
        //        }

        //        if (!isChecked)
        //        {

        //            ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Sorry, I don't have information on that topic.",HorizontalAlignment.Left);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        //    }


        //}



        private void ConfigureUser()
        {
            string UserInput = UserInputBox.Text.Trim();

            try
            {
                if (string.IsNullOrWhiteSpace(UserInput))
                {
                    ChatBot_Characteristics.AddMessage("Input cannot be empty. Please try again.", HorizontalAlignment.Left);
                    throw new Exception("Input was empty.");
                }

                ChatBot_Characteristics.AddMessage(UserInput, HorizontalAlignment.Right);
                UserInputBox.Clear();

                if (IsWaitingForUsername)
                {
                    ud.UserName = UserInput;
                    IsWaitingForUsername = false;
                    IsWaitingForTopic = true;

                   ChatBot_Dialogue.DisplayChatBotTopics(ud);
                    ChatBot_Dialogue.DisplayUserFavouriteTopic();

                    return;
                }

                if (IsWaitingForTopic)
                {
                    HashSet<string> validTopics = new HashSet<string> { "passwords", "phishing", "privacy" };
                    string[] words = UserInput.ToLower().Split(' ');

                    foreach (string word in words)
                    {
                        if (validTopics.Contains(word))
                        {
                            ud.UserFavouriteTopic = word;
                            IsWaitingForTopic = false;

                            ChatBot_Characteristics.AddMessage(
                                $"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! I'll remember you're interested in {word}. It's a vital part of cybersecurity!",
                                HorizontalAlignment.Left
                            );

                            // Optionally: trigger content/quiz next
                            return;
                        }
                    }

                    // No valid topic found
                    ChatBot_Characteristics.AddMessage(
                        $"{ChatBot_Characteristics.DisplayChatBotDialog()}Sorry, I don't have information on that topic. Please choose: passwords, phishing, or privacy.",
                        HorizontalAlignment.Left
                    );
                    return;
                }

                // Future: handle general chat input here

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureUser();
            DisplayResponseLogic(ud); // Pass the user details to the response logic


        }


        private string _currentFollowUpTopic; // Track the current topic for follow-up

        private void DisplayResponseLogic(UserDetails userDetails)
        {
            HashSet<string> validTopics = new HashSet<string> { "passwords", "phishing", "privacy" };
            CurrentUser = userDetails;
            string userInput = UserInputBox.Text.Trim().ToLower();

            // Handle follow-up responses first
            if (!string.IsNullOrEmpty(_currentFollowUpTopic))
            {
                HandleFollowUpResponse(userInput, _currentFollowUpTopic);
                _currentFollowUpTopic = null; // Reset after handling
                return;
            }

            // Normal response handling
            if (DataDictionary.chatResponses.ContainsKey(userInput))
            {
                ChatBot_Characteristics.AddMessage(DataDictionary.chatResponses[userInput], HorizontalAlignment.Left);
            }
            else if (DataDictionary.cyberResponses.ContainsKey(userInput))
            {
                string response = DataDictionary.GetRandomCyberResponse(userInput);
                ChatBot_Characteristics.AddMessage(response, HorizontalAlignment.Left);

                // Check for valid topics for follow-up
                foreach (string word in userInput.Split(' '))
                {
                    if (validTopics.Contains(word))
                    {
                        _currentFollowUpTopic = word; // Store for follow-up
                        ChatBot_Characteristics.AddMessage(
                            $"{ChatBot_Characteristics.DisplayChatBotDialog()}Would you like to know more about {word}? (yes/no)",
                            HorizontalAlignment.Left);
                        UserInputBox.Clear();
                        return; // Exit and wait for next input
                    }
                }
            }
            else if (DataDictionary.sentimentResponses.ContainsKey(userInput))
            {
                ChatBot_Characteristics.AddMessage(DataDictionary.GetRandomSentimentResponse(userInput), HorizontalAlignment.Left);
            }
            else
            {
                ChatBot_Characteristics.AddMessage(ChatBot_Dialogue.DisplayErrorMessage(), HorizontalAlignment.Left);
            }

            // Check favorite topic
            if (CurrentUser?.UserFavouriteTopic != null &&
                userInput.Contains(CurrentUser.UserFavouriteTopic.ToLower()))
            {
                OnFavoriteTopicMentioned?.Invoke(CurrentUser.UserFavouriteTopic);
            }
        }

        private void HandleFollowUpResponse(string userInput, string topic)
        {
            if (userInput == "yes" || userInput == "sure" || userInput == "absolutely")
            {
                ChatBot_Characteristics.AddMessage(
                    $"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! Here's more about {topic}:",
                    HorizontalAlignment.Left);
                ChatBot_Characteristics.AddMessage(
                    DataDictionary.GetRandomCyberResponse(topic),
                    HorizontalAlignment.Left);
            }
            else if (userInput == "no" || userInput == "not really" || userInput == "no thanks")
            {
                ChatBot_Characteristics.AddMessage(
                    $"{ChatBot_Characteristics.DisplayChatBotDialog()}No problem! Ask me anything else.",
                    HorizontalAlignment.Left);
            }
            else
            {
                ChatBot_Characteristics.AddMessage(
                    $"{ChatBot_Characteristics.DisplayChatBotDialog()}Please answer with 'yes' or 'no'.",
                    HorizontalAlignment.Left);
                _currentFollowUpTopic = topic; // Keep the same topic for follow-up
            }
        }




        //        private void DisplayResponseLogic(UserDetails userDetails)
        //        {



        //            HashSet<string> validTopics = new HashSet<string> { "passwords", "phishing", "privacy" };//impelenting a generic collection
        //            CurrentUser = userDetails;

        //            string UserInput = UserInputBox.Text.Trim();


        //            string[] splitWords = UserInput.ToLower().Split(' ');


        //                if (DataDictionary.chatResponses.ContainsKey(UserInput))
        //                {

        //                    ChatBot_Characteristics.AddMessage($"{DataDictionary.chatResponses[UserInput]}",HorizontalAlignment.Left);

        //                }
        //                else if (DataDictionary.cyberResponses.ContainsKey(UserInput))
        //                {

        //                    ChatBot_Characteristics.AddMessage($"{DataDictionary.GetRandomCyberResponse(UserInput)}",HorizontalAlignment.Left);

        //                    foreach (string word in splitWords)
        //                    {
        //                        if (validTopics.Contains(word))
        //                        {
        //                            ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Would you like to know more about {word}? It's a crucial part of cybersecurity.", HorizontalAlignment.Left);
        //                            ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayUserDialog()}", HorizontalAlignment.Left);


        //                          UserInputBox.Clear();
        //                          UserInputBox.Focus();
        //                        // Wait for user response
        //                        // This is a simplified example; in a real application, you might want to use an event or callback
        //                        // to handle the user's response asynchronously.
        //                          string response = UserInputBox.Text.Trim();

        //                            if (response == "yes" || response == "sure" || response == "absolutely")
        //                            {
        //                                ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! Here's some more information about {word}:", HorizontalAlignment.Left);
        //                                ChatBot_Characteristics.AddMessage($"{DataDictionary.GetRandomCyberResponse(word)}", HorizontalAlignment.Left);
        //                            }
        //                            else if (response == "no" || response == "not really" || response == "no thanks")
        //                            {
        //                                ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}No problem! If you have any other questions, feel free to ask.", HorizontalAlignment.Left);
        //                            }
        //                            else
        //                            {
        //                                ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}I didn't quite understand that.", HorizontalAlignment.Left);
        //                            }
        //                        }
        //                    }


        //                }
        //                else if (DataDictionary.sentimentResponses.ContainsKey(UserInput))
        //                {

        //                    ChatBot_Characteristics.AddMessage($"{DataDictionary.GetRandomSentimentResponse(UserInput)}", HorizontalAlignment.Left);

        //                }
        //                //else if (userInput.Contains("quiz") || userInput.Contains("test me"))
        //                //{
        //                //    Chatbot_Quiz.DisplayQuiz();
        //                //}

        //                else
        //                {

        //                    ChatBot_Characteristics.AddMessage($"{ChatBot_Dialogue.DisplayErrorMessage()}", HorizontalAlignment.Left);
        //                }
        //            // Check if the input matches the favorite topic and trigger event
        //            // Replace your current check with:
        //            if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.UserFavouriteTopic) &&
        //UserInput.IndexOf(CurrentUser.UserFavouriteTopic, StringComparison.OrdinalIgnoreCase) >= 0)
        //            {
        //                OnFavoriteTopicMentioned?.Invoke(CurrentUser.UserFavouriteTopic);
        //            }




        //            // ChatBot_Logo.DisplayGoodbyeLogo();
        //        }









    }
}