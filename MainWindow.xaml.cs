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




        //        public MainWindow()
        //        {
        //            InitializeComponent();
        //            Loaded += Window_Loaded;

        //            //this is subscribing to an event
        //            Chatbot_Quiz.DisplayQuizMessage += OnQuizMessageReceived;

        //            ChatBot_Task.DisplayTaskMessage += OnTaskManagerMessageReceived;


        //        }





        //        private void Window_Loaded(object sender, RoutedEventArgs e)
        //        {
        //            string chatBotLogo = ChatBot_Logo.DisplayIntroLogo();
        //            ChatBotLogo.Text = chatBotLogo;



        //            StartQuiz.Visibility = Visibility.Collapsed;
        //            ReturnChatBot.Visibility = Visibility.Collapsed;
        //            TaskAssistant.Visibility = Visibility.Collapsed;
        //            ActivityLog.Visibility = Visibility.Collapsed;
        //            ChatUI.Visibility = Visibility.Collapsed;

        //            this.Height = 700;
        //        }
















        //        private async void Window_KeyDown(object sender, KeyEventArgs e)
        //        {
        //            if (e.Key == Key.Enter && ChatUI.Visibility != Visibility.Visible)
        //            {
        //                // Make sure loading bar and logo are visible
        //                ChatBotLogo.Visibility = Visibility.Visible;


        //                // Hide loading visuals
        //                ChatBotLogo.Text = "";
        //                ChatBotLogo.Visibility = Visibility.Collapsed;


        //                // Show chatbot UI
        //                ChatUI.Visibility = Visibility.Visible;
        //                StartQuiz.Visibility = Visibility.Visible;
        //                TaskAssistant.Visibility = Visibility.Visible;
        //                ActivityLog.Visibility = Visibility.Visible;
        //                ReturnChatBot.Visibility = Visibility.Collapsed;

        //                this.Height = 500;

        //                UserInputBox.Focus();

        //                ChatBot_Dialogue.DisplayConfigureUser();
        //                PlayChatBotVoice();

        //                // Optionally disable buttons initially
        //                UserInputBox.IsEnabled = false;
        //                Send_Button.IsEnabled = false;
        //                StartQuiz.IsEnabled = false;
        //                TaskAssistant.IsEnabled = false;
        //                ActivityLog.IsEnabled = false;

        //                e.Handled = true;
        //            }
        //        }

















        //        private void PlayChatBotVoice()
        //        {
        //            string filePath = "JARVIS.wav";

        //            try
        //            {
        //                var audioFile = new AudioFileReader(filePath);
        //                var outputDevice = new WaveOutEvent();
        //                outputDevice.Init(audioFile);
        //                outputDevice.Play();

        //                // Clean up after playback finishes
        //                outputDevice.PlaybackStopped += (s, e) =>
        //                {

        //                    UserInputBox.IsEnabled = true;
        //                    Send_Button.IsEnabled = true;
        //                    StartQuiz.IsEnabled = true; 
        //                    TaskAssistant.IsEnabled = true;
        //                    ActivityLog.IsEnabled = true;

        //                };
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error playing audio: " + ex.Message);
        //            }
        //        }



















        //        private bool ConfigureUser()
        //        {
        //            string UserInput = UserInputBox.Text.Trim();

        //            try
        //            {
        //                if (string.IsNullOrWhiteSpace(UserInput))
        //                {
        //                    ChatBot_Characteristics.AddMessage("Input cannot be empty. Please try again.", HorizontalAlignment.Left);
        //                    throw new Exception("Input was empty.");
        //                }

        //                ChatBot_Characteristics.AddMessage(UserInput, HorizontalAlignment.Right);
        //                UserInputBox.Clear();

        //                if (IsWaitingForUsername)
        //                {
        //                    ud.UserName = UserInput;
        //                    IsWaitingForUsername = false;
        //                    IsWaitingForTopic = true;

        //                    ChatBot_Dialogue.DisplayChatBotTopics(ud);
        //                    ChatBot_Dialogue.DisplayUserFavouriteTopic();

        //                    return true; // Continue
        //                }

        //                if (UserInput.Contains("bye") || UserInput.Contains("goodbye"))
        //                {
        //                    GoodbyeLogo();
        //                    return false; // Stop processing further
        //                }

        //                if (IsWaitingForTopic)
        //                {
        //                    HashSet<string> validTopics = new HashSet<string> { "passwords", "phishing", "privacy" };
        //                    string[] words = UserInput.ToLower().Split(' ');

        //                    foreach (string word in words)
        //                    {
        //                        if (validTopics.Contains(word))
        //                        {
        //                            ud.UserFavouriteTopic = word;
        //                            IsWaitingForTopic = false;

        //                            ChatBot_Characteristics.AddMessage(
        //                                $"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! I'll remember you're interested in {word}. It's a vital part of cybersecurity!",
        //                                HorizontalAlignment.Left
        //                            );

        //                            return true; // Continue
        //                        }
        //                    }

        //                    // No valid topic found
        //                    ChatBot_Characteristics.AddMessage(
        //                        $"{ChatBot_Characteristics.DisplayChatBotDialog()}Sorry, I don't have information on that topic. Please choose: passwords, phishing, or privacy.",
        //                        HorizontalAlignment.Left
        //                    );
        //                    return true; // Continue
        //                }

        //                // Future: handle general chat input here

        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //            return true; // Continue by default
        //        }





        //        private void SendButton_Click(object sender, RoutedEventArgs e)
        //        {

        //            ConfigureUser();




        //        }


        //        private void GoodbyeLogo()
        //        {
        //            ChatUI.Visibility = Visibility.Collapsed;
        //            ChatBotLogo.Visibility = Visibility.Visible;

        //            string goodbyeLogo = ChatBot_Logo.DisplayGoodbyeLogo();
        //            ChatBotLogo.Text = goodbyeLogo;
        //        }
















        //        // QUIZ HANDLIG CODE

        //        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        //        {
        //            ChatUI.Visibility = Visibility.Collapsed;
        //            QuizUI.Visibility = Visibility.Visible;
        //            StartQuiz.Visibility = Visibility.Collapsed; // Hide quiz button after starting the quiz
        //            ReturnChatBot.Visibility = Visibility.Visible; // Show return button to go back to chat
        //            this.Height = 450.0;

        //            QuizQuestionArea.Children.Clear();
        //            Chatbot_Quiz.StartQuiz(); // This resets the quiz and shows the first question
        //            QuizAnswerBox.Focus();
        //        }


        //        private void ReturnChatBot_Click(object sender, RoutedEventArgs e)
        //        {
        //            QuizUI.Visibility = Visibility.Collapsed;
        //            ChatUI.Visibility = Visibility.Visible;
        //            StartQuiz.Visibility = Visibility.Visible; // Show quiz button again  
        //            ReturnChatBot.Visibility = Visibility.Collapsed; // Hide return button when back in chat  
        //            this.Height = 500.0;
        //        }



        //        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        //        {
        //            string userAnswer = QuizAnswerBox.Text.Trim();
        //            if (string.IsNullOrEmpty(userAnswer)) return;

        //            // If user wants to see past scores
        //            if (userAnswer.ToLower().Contains("past scores") || userAnswer.ToLower().Contains("previous scores") ||
        //               userAnswer.ToLower().Contains("quiz results") || userAnswer.ToLower().Contains("my score") ||
        //               userAnswer.ToLower().Contains("old scores") || userAnswer.ToLower().Contains("quiz history") ||
        //               userAnswer.ToLower().Contains("show score"))
        //            {
        //                // Only allow this when quiz is not active
        //                if (!Chatbot_Quiz.IsQuizActive)
        //                {
        //                    AddQuizMessage($"USER: {userAnswer}", HorizontalAlignment.Right);

        //                    string pastScores = Chatbot_Quiz.GetPastScores();
        //                    AddQuizMessage(pastScores, HorizontalAlignment.Left);
        //                }
        //                else
        //                {
        //                    AddQuizMessage("You can only view past scores after the quiz, not during.", HorizontalAlignment.Left);
        //                }

        //                QuizAnswerBox.Text = "";
        //                return;
        //            }

        //            // Otherwise treat input as quiz answer
        //            AddQuizMessage($"USER: {userAnswer}", HorizontalAlignment.Right);
        //            Chatbot_Quiz.SubmitAnswer(userAnswer);
        //            QuizAnswerBox.Text = "";
        //            QuizScrollBar.ScrollToEnd(); // Scroll to the end after submitting an answer
        //        }



        //        private void AddQuizMessage(string message, HorizontalAlignment alignment)
        //        {
        //            TextBlock textBlock = new TextBlock
        //            {
        //                Text = message,
        //                Foreground = Brushes.White,
        //                TextWrapping = TextWrapping.Wrap,
        //                Margin = new Thickness(5),
        //            };

        //            SolidColorBrush bubbleColor = alignment == HorizontalAlignment.Left
        //                ? new SolidColorBrush(Color.FromRgb(50, 50, 50))      // Bot: dark gray
        //                : new SolidColorBrush(Color.FromRgb(0, 122, 204));   // User: bright blue

        //            Border bubble = new Border
        //            {
        //                Background = bubbleColor,
        //                CornerRadius = new CornerRadius(12),
        //                Margin = new Thickness(4),
        //                Padding = new Thickness(10),
        //                MaxWidth = 600,
        //                HorizontalAlignment = alignment,
        //                Child = textBlock
        //            };

        //            QuizQuestionArea.Children.Add(bubble);
        //            QuizScrollBar.ScrollToEnd();  // Assuming you wrap the quiz in a ScrollViewer
        //        }



        //        private void OnQuizMessageReceived(string message, HorizontalAlignment alignment)
        //        {
        //            Dispatcher.Invoke(() =>
        //            {
        //                Border bubble = new Border
        //                {
        //                    Background = alignment == HorizontalAlignment.Left
        //                        ? new SolidColorBrush(Color.FromRgb(50, 50, 50)) // bot bubble color
        //                        : new SolidColorBrush(Color.FromRgb(0, 122, 204)), // user bubble color
        //                    CornerRadius = new CornerRadius(12),
        //                    Margin = new Thickness(4),
        //                    Padding = new Thickness(10),
        //                    MaxWidth = 600,
        //                    HorizontalAlignment = alignment,
        //                    Child = new TextBlock
        //                    {
        //                        Text = message,
        //                        Foreground = Brushes.White,
        //                        TextWrapping = TextWrapping.Wrap
        //                    }
        //                };

        //                QuizQuestionArea.Children.Add(bubble);
        //            });
        //        }





        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Chatbot_Quiz.DisplayQuizMessage += OnQuizMessageReceived;
            ChatBot_Task.DisplayTaskMessage += OnTaskManagerMessageReceived;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChatBotLogo.Text = ChatBot_Logo.DisplayIntroLogo();
            StartQuiz.Visibility = Visibility.Collapsed;
            ReturnChatBot.Visibility = Visibility.Collapsed;
            TaskAssistant.Visibility = Visibility.Collapsed;
            ActivityLog.Visibility = Visibility.Collapsed;
            ChatUI.Visibility = Visibility.Collapsed;
            this.Height = 700;
        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && ChatUI.Visibility != Visibility.Visible)
            {
                ChatBotLogo.Visibility = Visibility.Collapsed;
                ChatUI.Visibility = Visibility.Visible;
                StartQuiz.Visibility = Visibility.Visible;
                TaskAssistant.Visibility = Visibility.Visible;
                ActivityLog.Visibility = Visibility.Visible;
                ReturnChatBot.Visibility = Visibility.Collapsed;
                this.Height = 500;

                UserInputBox.Focus();
                ChatBot_Dialogue.DisplayConfigureUser();
                PlayChatBotVoice();

                UserInputBox.IsEnabled = false;
                Send_Button.IsEnabled = false;
                StartQuiz.IsEnabled = false;
                TaskAssistant.IsEnabled = false;
                ActivityLog.IsEnabled = false;
                e.Handled = true;
            }
        }

        private void PlayChatBotVoice()
        {
            try
            {
                var audioFile = new AudioFileReader("JARVIS.wav");
                var outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();
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

        private bool ConfigureUser()
        {
            ChatBotScrollBar.ScrollToEnd();

            string UserInput = UserInputBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(UserInput))
            {
                ChatBot_Characteristics.AddMessage("Input cannot be empty. Please try again.", HorizontalAlignment.Left);
                return true;
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
                return true;
            }

            if (UserInput.Contains("bye") || UserInput.Contains("goodbye"))
            {
                GoodbyeLogo();
                return false;
            }

            if (IsWaitingForTopic)
            {
                var topics = new[] { "passwords", "phishing", "privacy" };
                foreach (string word in UserInput.ToLower().Split(' '))
                {
                    if (topics.Contains(word))
                    {
                        ud.UserFavouriteTopic = word;
                        IsWaitingForTopic = false;
                        ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Great! I'll remember you're interested in {word}. It's a vital part of cybersecurity!", HorizontalAlignment.Left);
                        return true;
                    }
                }
                ChatBot_Characteristics.AddMessage($"{ChatBot_Characteristics.DisplayChatBotDialog()}Sorry, I don't have information on that topic. Please choose: passwords, phishing, or privacy.", HorizontalAlignment.Left);
                return true;
            }

            return true;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureUser();
        }

        private void GoodbyeLogo()
        {
            ChatUI.Visibility = Visibility.Collapsed;
            ChatBotLogo.Visibility = Visibility.Visible;
            ChatBotLogo.Text = ChatBot_Logo.DisplayGoodbyeLogo();
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            ChatUI.Visibility = Visibility.Collapsed;
            QuizUI.Visibility = Visibility.Visible;
            StartQuiz.Visibility = Visibility.Collapsed;
            ActivityLog.Visibility = Visibility.Collapsed;
            TaskAssistant.Visibility = Visibility.Collapsed;
            ReturnChatBot.Visibility = Visibility.Visible;
            this.Height = 450;

            QuizQuestionArea.Children.Clear();
            Chatbot_Quiz.StartQuiz();
            QuizAnswerBox.Focus();
        }

        private void ReturnChatBot_Click(object sender, RoutedEventArgs e)
        {
            QuizUI.Visibility = Visibility.Collapsed;
            TaskUI.Visibility = Visibility.Collapsed;
            ChatUI.Visibility = Visibility.Visible;
            TaskAssistant.Visibility = Visibility.Visible;
            StartQuiz.Visibility = Visibility.Visible;
            ActivityLog.Visibility = Visibility.Visible;
            ReturnChatBot.Visibility = Visibility.Collapsed;
            this.Height = 500;
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            string userAnswer = QuizAnswerBox.Text.Trim();
            if (string.IsNullOrEmpty(userAnswer)) return;

            AddQuizMessage($"USER: {userAnswer}", HorizontalAlignment.Right);

            if (userAnswer.ToLower().Contains("score"))
            {
                AddQuizMessage(Chatbot_Quiz.GetPastScores(), HorizontalAlignment.Left);
                QuizAnswerBox.Text = "";
                return;
            }

            Chatbot_Quiz.SubmitAnswer(userAnswer);
            QuizAnswerBox.Text = "";
            QuizScrollBar.ScrollToEnd();
        }

        private void AddQuizMessage(string message, HorizontalAlignment alignment)
        {
            var textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            var bubble = new Border
            {
                Background = alignment == HorizontalAlignment.Left ? new SolidColorBrush(Color.FromRgb(50, 50, 50)) : new SolidColorBrush(Color.FromRgb(0, 122, 204)),
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(4),
                Padding = new Thickness(10),
                MaxWidth = 600,
                HorizontalAlignment = alignment,
                Child = textBlock
            };

            QuizQuestionArea.Children.Add(bubble);
            QuizScrollBar.ScrollToEnd();
        }

        private void OnQuizMessageReceived(string message, HorizontalAlignment alignment)
        {
            Dispatcher.Invoke(() => AddQuizMessage(message, alignment));
        }

        private void TaskAssistant_Click(object sender, RoutedEventArgs e)
        {
            ChatUI.Visibility = Visibility.Collapsed;
            QuizUI.Visibility = Visibility.Collapsed;
            TaskUI.Visibility = Visibility.Visible;
            StartQuiz.Visibility = Visibility.Collapsed;
            ReturnChatBot.Visibility = Visibility.Visible;
            ActivityLog.Visibility = Visibility.Collapsed;
            TaskAssistant.Visibility = Visibility.Collapsed;
            TaskChatArea.Children.Clear();
            this.Height = 450;

            ChatBot_Task.ActivateTaskManager();

        }

        private void SubmitTaskCommand_Click(object sender, RoutedEventArgs e)
        {
            string taskInput = TaskInputBox.Text.Trim();
            if (string.IsNullOrEmpty(taskInput)) return;

            AddTaskMessage($"USER: {taskInput}", HorizontalAlignment.Right);
            ChatBot_Task.ProcessUserInput(taskInput);
            TaskInputBox.Text = "";
            TaskScrollBar.ScrollToEnd();
        }

        private void AddTaskMessage(string message, HorizontalAlignment alignment)
        {
            var textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5)
            };

            var bubble = new Border
            {
                Background = alignment == HorizontalAlignment.Left ? new SolidColorBrush(Color.FromRgb(50, 50, 50)) : new SolidColorBrush(Color.FromRgb(0, 122, 204)),
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(4),
                Padding = new Thickness(10),
                MaxWidth = 600,
                HorizontalAlignment = alignment,
                Child = textBlock
            };

            TaskChatArea.Children.Add(bubble);
            TaskScrollBar.ScrollToEnd();
        }

        private void OnTaskManagerMessageReceived(string message, HorizontalAlignment alignment)
        {
            Dispatcher.Invoke(() => AddTaskMessage(message, alignment));
        }



        private void ActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ChatBotScrollBar.ScrollToEnd();

            string log = ChatBot_Activity_Log.DisplayActivityLog();
            ChatBot_Characteristics.AddMessage(log, HorizontalAlignment.Left);

            ChatBot_Activity_Log.ActivityLog("ACTIVITY", "User Accessed Activity Log");

            








        }
    }
}






   
