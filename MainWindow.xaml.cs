using System.Diagnostics;
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
using ST10438817_POE_PART2_CHATBOT;
using NAudio;
using NAudio.Wave;
using System.Windows.Threading;
using ST10438817_POE_PART3_CHATBOT.JARVIS_AI;
using System.Runtime.ExceptionServices;
using System;


namespace ST10438817_POE_PART3_CHATBOT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static bool IsWaitingForUsername = true;

        public static bool IsWaitingForTopic = false;

        public static bool IsConfiguredUser = false;



        public static UserDetails ud = new UserDetails();


        private static Random random = new Random();



        /// <summary>
        /// Constructor - Initializes components and sets up event handlers
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;

            // Subscribe to external component events
            DataDictionary.OnFavoriteTopicMentioned += OnFavoriteTopicMentioned;
            Chatbot_Quiz.DisplayQuizMessage += OnQuizMessageReceived;
            ChatBot_Task.DisplayTaskMessage += OnTaskManagerMessageReceived;
        }

        /// <summary>
        /// Handles when the user's favorite topic is mentioned
        /// </summary>
      
        private void OnFavoriteTopicMentioned(string topic)
        {
            // Predefined responses for when favorite topic is referenced
            string[] favouriteTopicResponses = {
            $"Ohh yes, I remember — {topic} was your favourite cybersecurity topic!",
            $"That's right! You're really into {topic}, aren't you?",
            $"Oh yes, you brought {topic} up earlier — very relevant in today's digital world!",
            $"I remember you mentioned {topic} before. Let's dive deeper into it!"
        };

            // Add a random response to the chat UI
            ChatBot_Characteristics.AddMessage(
                favouriteTopicResponses[random.Next(favouriteTopicResponses.Length)],
                HorizontalAlignment.Left);
        }

        /// <summary>
        /// Window loaded event handler - Sets up initial UI state
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize UI elements
            LoadingBar.Visibility = Visibility.Visible; // Hide progress bar initially
            ChatBotLogo.Text = ChatBot_Logo.DisplayIntroLogo();
            
            StartQuiz.Visibility = Visibility.Collapsed;
            ReturnChatBot.Visibility = Visibility.Collapsed;
            TaskAssistant.Visibility = Visibility.Collapsed;
            ActivityLog.Visibility = Visibility.Collapsed;
            ChatUI.Visibility = Visibility.Collapsed;
            this.Height = 400;  // Set initial window height
            this.Width = 500;   // Set initial window width
        }

        /// <summary>
        /// Handles key presses in the window
        /// </summary>
        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Start chat when Enter is pressed and chat UI isn't visible
            if (e.Key == Key.Enter && ChatUI.Visibility != Visibility.Visible)
            {
                //load the loading bar must slowly load to 100% in green
                for (int i = 0; i <= 100; i++)
                {
                    LoadingBar.Value = i;
                    await Task.Delay(25); // Simulate loading delay
                }

                

                // Switch from logo to chat interface
                ChatBotLogo.Visibility = Visibility.Collapsed;
                LoadingBar.Visibility = Visibility.Collapsed; // Hide loading bar
                ChatUI.Visibility = Visibility.Visible;

               

                // Show available feature buttons
                StartQuiz.Visibility = Visibility.Visible;
                TaskAssistant.Visibility = Visibility.Visible;
                ActivityLog.Visibility = Visibility.Visible;
                ReturnChatBot.Visibility = Visibility.Collapsed;

                this.Height = 500;  // Adjust window height for chat mode
                this.Width = 800;

                // Initialize chat session
                UserInputBox.Focus();
                ChatBot_Dialogue.DisplayConfigureUser();

                

                PlayChatBotVoice();

                // Temporarily disable controls during voice playback
                UserInputBox.IsEnabled = false;
                Send_Button.IsEnabled = false;
                StartQuiz.IsEnabled = false;
                TaskAssistant.IsEnabled = false;
                ActivityLog.IsEnabled = false;

                e.Handled = true;  // Mark event as handled
            }
        }

        /// <summary>
        /// Plays the chatbot's welcome voice message
        /// </summary>
        private void PlayChatBotVoice()
        {
            try
            {
                // Initialize audio playback
                var audioFile = new AudioFileReader("JARVIS.wav");
                var outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();

                // Re-enable controls when playback completes
                outputDevice.PlaybackStopped += (s, e) =>
                {
                    UserInputBox.IsEnabled = true;
                    Send_Button.IsEnabled = true;
                    StartQuiz.IsEnabled = true;
                    TaskAssistant.IsEnabled = true;
                    ActivityLog.IsEnabled = true;
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing audio: " + ex.Message);
            }
        }

        /// <summary>
        /// Configures user profile based on input
        /// </summary>
        /// <returns>True if configuration should continue, False if ending</returns>
        private bool ConfigureUser()
        {
            // Scroll to bottom of chat and get user input
            ChatBotScrollBar.ScrollToEnd();
            string UserInput = UserInputBox.Text.Trim();

            // Validate non-empty input
            if (string.IsNullOrWhiteSpace(UserInput))
            {
                ChatBot_Characteristics.AddMessage("Input cannot be empty. Please try again.", HorizontalAlignment.Left);
                return true;
            }

            // Display user message in chat
            ChatBot_Characteristics.AddMessage(UserInput, HorizontalAlignment.Right);
            UserInputBox.Clear();

            // First step: Collect username
            if (IsWaitingForUsername)
            {
                ud.UserName = UserInput;
                ChatBot_Dialogue.CurrentUser = ud;

                // Move to next configuration step
                IsWaitingForUsername = false;
                IsWaitingForTopic = true;

                // Initialize responses and present topic options
                DataDictionary.DisplayResponse();
                ChatBot_Dialogue.DisplayChatBotTopics(ud);
                ChatBot_Dialogue.DisplayUserFavouriteTopic();
                return true;
            }

            // Check for exit commands
            if (UserInput.Contains("bye") || UserInput.Contains("goodbye"))
            {
                GoodbyeLogo();
                return false;
            }

            // Second step: Collect favorite topic
            if (IsWaitingForTopic)
            {
                var topics = new[] { "passwords", "phishing", "privacy" };

                // Check each word in input for valid topic
                foreach (string word in UserInput.ToLower().Split(' '))
                {
                    if (topics.Contains(word))
                    {
                        ud.UserFavouriteTopic = word;
                        IsWaitingForTopic = false;
                        ChatBot_Characteristics.AddMessage(
                            $"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! I'll remember you're interested in {word}. It's a vital part of cybersecurity!",
                            HorizontalAlignment.Left);
                        return true;
                    }
                }

                // No valid topic found
                ChatBot_Characteristics.AddMessage(
                    $"{ChatBot_Characteristics.DisplayChatBotDialog()}Sorry, I don't have information on that topic. Please choose: passwords, phishing, or privacy.",
                    HorizontalAlignment.Left);
                return true;
            }

            // Mark user as fully configured
            IsConfiguredUser = true;
            ChatBot_Dialogue.CurrentUser = ud;
            return true;
        }

        /// <summary>
        /// Handles send button click event
        /// </summary>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputBox.Text.Trim();

            // Ignore empty input
            if (string.IsNullOrEmpty(userInput))
            {
                return;
            }

            // Process user configuration or message
            bool configured = ConfigureUser();

            if (configured && IsConfiguredUser)
            {
                ProcessUserMessage(userInput);
            }

            UserInputBox.Clear();
        }

        /// <summary>
        /// Displays goodbye screen and hides chat interface
        /// </summary>
        private void GoodbyeLogo()
        {
            ChatUI.Visibility = Visibility.Collapsed;
            ChatBotLogo.Visibility = Visibility.Visible;
            ChatBotLogo.Text = ChatBot_Logo.DisplayGoodbyeLogo();
        }

        /// <summary>
        /// Switches to quiz interface
        /// </summary>
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            // Hide chat UI and show quiz UI
            ChatUI.Visibility = Visibility.Collapsed;
            QuizUI.Visibility = Visibility.Visible;

            // Adjust visible buttons
            StartQuiz.Visibility = Visibility.Collapsed;
            ActivityLog.Visibility = Visibility.Collapsed;
            TaskAssistant.Visibility = Visibility.Collapsed;
            ReturnChatBot.Visibility = Visibility.Visible;

            this.Height = 450;  // Adjust window height for quiz

            // Initialize quiz session
            QuizQuestionArea.Children.Clear();
            Chatbot_Quiz.StartQuiz();
            QuizAnswerBox.Focus();
        }

        /// <summary>
        /// Returns to main chat from quiz or task interfaces
        /// </summary>
        private void ReturnChatBot_Click(object sender, RoutedEventArgs e)
        {
            // Hide specialized UIs and show main chat
            QuizUI.Visibility = Visibility.Collapsed;
            TaskUI.Visibility = Visibility.Collapsed;
            ChatUI.Visibility = Visibility.Visible;

            // Restore feature buttons
            TaskAssistant.Visibility = Visibility.Visible;
            StartQuiz.Visibility = Visibility.Visible;
            ActivityLog.Visibility = Visibility.Visible;
            ReturnChatBot.Visibility = Visibility.Collapsed;

            this.Height = 500;  // Restore chat window height
        }

        /// <summary>
        /// Handles quiz answer submission
        /// </summary>
        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            string userAnswer = QuizAnswerBox.Text.Trim();

            // Ignore empty answers
            if (string.IsNullOrEmpty(userAnswer)) return;

            // Display user's answer
            AddQuizMessage($"USER: {userAnswer}", HorizontalAlignment.Right);

            // Special case: Score request
            if (userAnswer.ToLower().Contains("score"))
            {
                AddQuizMessage(Chatbot_Quiz.GetPastScores(), HorizontalAlignment.Left);
                QuizAnswerBox.Text = "";
                return;
            }

            // Process quiz answer
            Chatbot_Quiz.SubmitAnswer(userAnswer);
            QuizAnswerBox.Text = "";
            QuizScrollBar.ScrollToEnd();
        }

        /// <summary>
        /// Adds a message to the quiz UI
        /// </summary>
        private void AddQuizMessage(string message, HorizontalAlignment alignment)
        {
            // Create message text element
            var textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            // Create chat bubble container
            var bubble = new Border
            {
                Background = alignment == HorizontalAlignment.Left
                    ? new SolidColorBrush(Color.FromRgb(50, 50, 50))  // Dark for bot messages
                    : new SolidColorBrush(Color.FromRgb(0, 122, 204)), // Blue for user messages
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(4),
                Padding = new Thickness(10),
                MaxWidth = 600,
                HorizontalAlignment = alignment,
                Child = textBlock
            };

            // Add to UI and scroll to bottom
            QuizQuestionArea.Children.Add(bubble);
            QuizScrollBar.ScrollToEnd();
        }

        /// <summary>
        /// Handles incoming quiz messages from the quiz component
        /// </summary>
        private void OnQuizMessageReceived(string message, HorizontalAlignment alignment)
        {
            // Ensure UI thread execution
            Dispatcher.Invoke(() => AddQuizMessage(message, alignment));
        }

        /// <summary>
        /// Switches to task assistant interface
        /// </summary>
        private void TaskAssistant_Click(object sender, RoutedEventArgs e)
        {
            // Hide other UIs and show task UI
            ChatUI.Visibility = Visibility.Collapsed;
            QuizUI.Visibility = Visibility.Collapsed;
            TaskUI.Visibility = Visibility.Visible;

            // Adjust visible buttons
            StartQuiz.Visibility = Visibility.Collapsed;
            ReturnChatBot.Visibility = Visibility.Visible;
            ActivityLog.Visibility = Visibility.Collapsed;
            TaskAssistant.Visibility = Visibility.Collapsed;

            // Clear previous messages and set height
            TaskChatArea.Children.Clear();
            this.Height = 450;

            // Initialize task manager
            ChatBot_Task.ActivateTaskManager();
        }

        /// <summary>
        /// Handles task command submission
        /// </summary>
        private void SubmitTaskCommand_Click(object sender, RoutedEventArgs e)
        {
            string taskInput = TaskInputBox.Text.Trim();

            // Display user input if not empty
            if (!string.IsNullOrWhiteSpace(taskInput))
                AddTaskMessage($"USER: {taskInput}", HorizontalAlignment.Right);

            // Process task command
            ChatBot_Task.ProcessUserInput(taskInput);
            TaskInputBox.Text = "";
            TaskScrollBar.ScrollToEnd();
        }

        /// <summary>
        /// Adds a message to the task UI
        /// </summary>
        private void AddTaskMessage(string message, HorizontalAlignment alignment)
        {
            // Create message text element
            var textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            // Create chat bubble container
            var bubble = new Border
            {
                Background = alignment == HorizontalAlignment.Left
                    ? new SolidColorBrush(Color.FromRgb(50, 50, 50))  // Dark for bot messages
                    : new SolidColorBrush(Color.FromRgb(0, 122, 204)), // Blue for user messages
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(4),
                Padding = new Thickness(10),
                MaxWidth = 600,
                HorizontalAlignment = alignment,
                Child = textBlock
            };

            // Add to UI and scroll to bottom
            TaskChatArea.Children.Add(bubble);
            TaskScrollBar.ScrollToEnd();
        }

        /// <summary>
        /// Handles incoming task messages from the task component
        /// </summary>
        private void OnTaskManagerMessageReceived(string message, HorizontalAlignment alignment)
        {
            // Ensure UI thread execution
            Dispatcher.Invoke(() => AddTaskMessage(message, alignment));
        }

        /// <summary>
        /// Displays activity log in chat
        /// </summary>
        private void ActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ChatBotScrollBar.ScrollToEnd();

            // Get and display activity log
            string log = ChatBot_Activity_Log.DisplayActivityLog();
            ChatBot_Characteristics.AddMessage(log, HorizontalAlignment.Left);

            // Record this access in the log
            ChatBot_Activity_Log.ActivityLog("ACTIVITY", "User Accessed Activity Log");
        }

        /// <summary>
        /// Processes and responds to user messages
        /// </summary>
        private void ProcessUserMessage(string userInput)
        {
            // Normalize input
            userInput = userInput.Trim().ToLower();
            bool matched = false;

            // 1. First check sentiment responses
            if (!matched)
            {
                // Check password-related sentiments
                foreach (var key in DataDictionary.sentimentResponses.Keys.Where(k =>
                    k.Contains("password", StringComparison.OrdinalIgnoreCase)))
                {
                    if (userInput.Contains(key.ToLower()))
                    {
                        var responses = DataDictionary.sentimentResponses[key];
                        string response = responses[random.Next(responses.Count)];
                        ChatBot_Characteristics.AddMessage(response, HorizontalAlignment.Left);
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    // Check phishing-related sentiments
                    foreach (var key in DataDictionary.sentimentResponses.Keys.Where(k =>
                        k.Contains("phishing", StringComparison.OrdinalIgnoreCase)))
                    {
                        if (userInput.Contains(key.ToLower()))
                        {
                            var responses = DataDictionary.sentimentResponses[key];
                            string response = responses[random.Next(responses.Count)];
                            ChatBot_Characteristics.AddMessage(response, HorizontalAlignment.Left);
                            matched = true;
                            break;
                        }
                    }
                }

                if (!matched)
                {
                    // Check privacy-related sentiments
                    foreach (var key in DataDictionary.sentimentResponses.Keys.Where(k =>
                        k.Contains("privacy", StringComparison.OrdinalIgnoreCase)))
                    {
                        if (userInput.Contains(key.ToLower()))
                        {
                            var responses = DataDictionary.sentimentResponses[key];
                            string response = responses[random.Next(responses.Count)];
                            ChatBot_Characteristics.AddMessage(response, HorizontalAlignment.Left);
                            matched = true;
                            break;
                        }
                    }
                }
            }

            // 2. Then check cybersecurity topics
            if (!matched)
            {
                foreach (var key in DataDictionary.cyberResponses.Keys)
                {
                    if (userInput.Contains(key.ToLower()))
                    {
                        ChatBot_Characteristics.AddMessage(DataDictionary.GetRandomCyberResponse(key), HorizontalAlignment.Left);
                        matched = true;
                        break;
                    }
                }
            }

            // 3. Finally check general chat responses
            if (!matched)
            {
                foreach (var key in DataDictionary.chatResponses.Keys)
                {
                    if (userInput.Contains(key.ToLower()))
                    {
                        ChatBot_Characteristics.AddMessage(DataDictionary.chatResponses[key], HorizontalAlignment.Left);
                        matched = true;
                        break;
                    }
                }
            }

            // No matches found - show error
            if (!matched)
            {
                ChatBot_Characteristics.AddMessage(ChatBot_Dialogue.DisplayErrorMessage(), HorizontalAlignment.Left);
            }

            // Check for mention of favorite topic
            if (ChatBot_Dialogue.CurrentUser != null &&
                !string.IsNullOrEmpty(ChatBot_Dialogue.CurrentUser.UserFavouriteTopic) &&
                userInput.Contains(ChatBot_Dialogue.CurrentUser.UserFavouriteTopic, StringComparison.OrdinalIgnoreCase))
            {
                DataDictionary.NotifyFavoriteTopicMentioned(ChatBot_Dialogue.CurrentUser.UserFavouriteTopic);
            }

            // Check for exit commands
            if (userInput == "bye" || userInput == "goodbye" || userInput == "exit" || userInput == "see you later")
            {
                GoodbyeLogo();
            }
        }








    




    }
}






   
