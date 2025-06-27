using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace JARVIS_AI
{
    public static class ChatBot_Logo
    {
        //this static class displays all the logos for the chatbot
      
        public static string DisplayIntroLogo()
        {

            string filePath = "ChatBotLogoPart1.txt";

            try
            {

                string fileContents = File.ReadAllText(filePath);
               
                return fileContents;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file. Please check the file path and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }



        }
     

        public static string DisplayGoodbyeLogo()
        {
            string filePath = "ChatBotLogoPart2.txt";

            try
            {

                string fileContents = File.ReadAllText(filePath);

                return fileContents;

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while reading the file. Please check the file path and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }



        }






    }// end of class
}
