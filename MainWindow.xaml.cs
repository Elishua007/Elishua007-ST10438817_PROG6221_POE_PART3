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

            //this is subscribing to an event
            Chatbot_Quiz.DisplayQuizMessage += OnQuizMessageReceived;
           

        }

       



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string chatBotLogo = ChatBot_Logo.DisplayIntroLogo();
            ChatBotLogo.Text = chatBotLogo;
            StartQuiz.Visibility = Visibility.Collapsed; // Hide quiz button initially
            ReturnChatBot.Visibility = Visibility.Collapsed; // Hide return button initially
            this.Height = 700;

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
                StartQuiz.Visibility = Visibility.Visible; // Show quiz button
                ReturnChatBot.Visibility = Visibility.Collapsed; // Hide return button initially
                this.Height = 500;


                // Focus input box
                UserInputBox.Focus();
                
                ChatBot_Dialogue.DisplayConfigureUser();


                e.Handled = true;
            }

          
        }



        private bool ConfigureUser()
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

                    return true; // Continue
                }

                if (UserInput.Contains("bye") || UserInput.Contains("goodbye"))
                {
                    GoodbyeLogo();
                    return false; // Stop processing further
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

                            return true; // Continue
                        }
                    }

                    // No valid topic found
                    ChatBot_Characteristics.AddMessage(
                        $"{ChatBot_Characteristics.DisplayChatBotDialog()}Sorry, I don't have information on that topic. Please choose: passwords, phishing, or privacy.",
                        HorizontalAlignment.Left
                    );
                    return true; // Continue
                }

                // Future: handle general chat input here

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true; // Continue by default
        }





        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            
            ConfigureUser();



        }


        private void GoodbyeLogo()
        {
            ChatUI.Visibility = Visibility.Collapsed;
            ChatBotLogo.Visibility = Visibility.Visible;

            string goodbyeLogo = ChatBot_Logo.DisplayGoodbyeLogo();
            ChatBotLogo.Text = goodbyeLogo;
        }
















        // QUIZ HANDLIG CODE

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            ChatUI.Visibility = Visibility.Collapsed;
            QuizUI.Visibility = Visibility.Visible;
            StartQuiz.Visibility = Visibility.Collapsed; // Hide quiz button after starting the quiz
            ReturnChatBot.Visibility = Visibility.Visible; // Show return button to go back to chat
            this.Height = 450.0;

            QuizQuestionArea.Children.Clear();
            Chatbot_Quiz.StartQuiz(); // This resets the quiz and shows the first question
            QuizAnswerBox.Focus();
        }


        private void ReturnChatBot_Click(object sender, RoutedEventArgs e)
        {
            QuizUI.Visibility = Visibility.Collapsed;
            ChatUI.Visibility = Visibility.Visible;
            StartQuiz.Visibility = Visibility.Visible; // Show quiz button again  
            ReturnChatBot.Visibility = Visibility.Collapsed; // Hide return button when back in chat  
            this.Height = 500.0;
        }



        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            string userAnswer = QuizAnswerBox.Text.Trim();
            if (string.IsNullOrEmpty(userAnswer)) return;

            // If user wants to see past scores
            if (userAnswer.ToLower().Contains("past scores") || userAnswer.ToLower().Contains("previous scores") ||
               userAnswer.ToLower().Contains("quiz results") || userAnswer.ToLower().Contains("my score") ||
               userAnswer.ToLower().Contains("old scores") || userAnswer.ToLower().Contains("quiz history") ||
               userAnswer.ToLower().Contains("show score"))
            {
                // Only allow this when quiz is not active
                if (!Chatbot_Quiz.IsQuizActive)
                {
                    AddQuizMessage($"USER: {userAnswer}", HorizontalAlignment.Right);

                    string pastScores = Chatbot_Quiz.GetPastScores();
                    AddQuizMessage(pastScores, HorizontalAlignment.Left);
                }
                else
                {
                    AddQuizMessage("You can only view past scores after the quiz, not during.", HorizontalAlignment.Left);
                }

                QuizAnswerBox.Text = "";
                return;
            }

            // Otherwise treat input as quiz answer
            AddQuizMessage($"USER: {userAnswer}", HorizontalAlignment.Right);
            Chatbot_Quiz.SubmitAnswer(userAnswer);
            QuizAnswerBox.Text = "";
            QuizScrollBar.ScrollToEnd(); // Scroll to the end after submitting an answer
        }



        private void AddQuizMessage(string message, HorizontalAlignment alignment)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
            };

            SolidColorBrush bubbleColor = alignment == HorizontalAlignment.Left
                ? new SolidColorBrush(Color.FromRgb(50, 50, 50))      // Bot: dark gray
                : new SolidColorBrush(Color.FromRgb(0, 122, 204));   // User: bright blue

            Border bubble = new Border
            {
                Background = bubbleColor,
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(4),
                Padding = new Thickness(10),
                MaxWidth = 600,
                HorizontalAlignment = alignment,
                Child = textBlock
            };

            QuizQuestionArea.Children.Add(bubble);
            QuizScrollBar.ScrollToEnd();  // Assuming you wrap the quiz in a ScrollViewer
        }



        private void OnQuizMessageReceived(string message, HorizontalAlignment alignment)
        {
            Dispatcher.Invoke(() =>
            {
                Border bubble = new Border
                {
                    Background = alignment == HorizontalAlignment.Left
                        ? new SolidColorBrush(Color.FromRgb(50, 50, 50)) // bot bubble color
                        : new SolidColorBrush(Color.FromRgb(0, 122, 204)), // user bubble color
                    CornerRadius = new CornerRadius(12),
                    Margin = new Thickness(4),
                    Padding = new Thickness(10),
                    MaxWidth = 600,
                    HorizontalAlignment = alignment,
                    Child = new TextBlock
                    {
                        Text = message,
                        Foreground = Brushes.White,
                        TextWrapping = TextWrapping.Wrap
                    }
                };

                QuizQuestionArea.Children.Add(bubble);
            });
        }








    }
}