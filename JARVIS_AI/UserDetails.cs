using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_ChatBot_ST10438817
{
    public class UserDetails
    {

        //this will contain all sorts of infomration regarding the user

        private string _userName;//user name can only be accessed privately
        private string _userFavouriteTopic; 

        //so public methids are created in order to return and set the user name


        public string UserName { 
            
            get { return _userName; } //returns the user name
            set { _userName = value; }// sets the user name
        
        }


        public string UserFavouriteTopic
        {
            get { return _userFavouriteTopic; } //returns the user favourite topic
            set { _userFavouriteTopic = value; } //sets the user favourite topic
        }




    }//end of class
}
