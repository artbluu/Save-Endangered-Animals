using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    [Serializable]
    public class User
    {
        private string name;
        private string surname;
        private string username;
        private string password;
        private Boolean new_user;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public User()
        {

        }
        public User(string name,string surname,string username, string password, Boolean new_user) 
        {
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.password = password;
            this.new_user = new_user;
        }

      
        public string User_Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("User_Name");
                }
            }

        }
        public string User_Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged("User_Surname");
                }
            }

        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }

        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (value != password)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }

        }
        public Boolean New_User
        {
            get
            {
                return new_user;
            }
            set
            {
                if (value != new_user)
                {
                    new_user = value;
                    OnPropertyChanged("New_User");
                }
            }

        }
        
    }
}
