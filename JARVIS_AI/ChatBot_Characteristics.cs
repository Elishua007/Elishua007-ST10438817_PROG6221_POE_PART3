using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;


namespace POE_ChatBot_ST10438817
{
    internal static class ChatBot_Characteristics
    {
       


        public static string DisplayUserDialog()
        {
            //this method displays USER: .......... in the interface of the chat
       
            return ($"USER\n ");
        }//end of method

        public static string DisplayChatBotDialog()
        {
            //this method displays JARVIS: .......... in the interface of the chat
           
            return ($"JARVIS\n");
        }//end of method


        public static void AddMessage(string message, HorizontalAlignment alignment)
        {
            //this method is just for decoration make the chatbot look realistic and nice

            TextBlock msgText = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14
            };

            SolidColorBrush bubbleColor = alignment == HorizontalAlignment.Left
                    ? new SolidColorBrush(Color.FromRgb(50, 50, 50))      // Bot: dark gray
                    : new SolidColorBrush(Color.FromRgb(16, 163, 127));   // User: teal-green

            Border bubble = new Border
            {
                Background = bubbleColor,
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(4),
                Padding = new Thickness(10),
                MaxWidth = 600,
                HorizontalAlignment = alignment,
                Child = msgText
            };



            bubble.Child = msgText;

            StackPanel chatResponse = Application.Current.MainWindow.FindName("ChatResponse") as StackPanel;

            if (chatResponse != null)
            {
                chatResponse.Children.Add(bubble);
            }
            else
            {
                MessageBox.Show("ChatResponse StackPanel not found!");
            }


        }


    }//end of class
}
