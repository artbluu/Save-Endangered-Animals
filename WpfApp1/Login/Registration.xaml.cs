using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Help;
using WpfApp1.Model;

namespace WpfApp1.Login
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window,CommandExecuted
    {
        private string name;
        private string surname;
        private string username;
        private string password;
        private Boolean new_user;
        public event PropertyChangedEventHandler PropertyChanged; //Sta je delegat!!! Pitanje na 

        

        public Registration()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            password = Password_tf.Password;
            if ((Boolean)YesNew.IsChecked)
                new_user = true;
            User user = new User(name,surname,username,password,new_user);
            bool exist = false;
            string message = "Username already exist!! Try again";
            if (Username_tf.Text != "")
            {
                foreach (User u in Lists.User_list)
                {
                    if (u.Username.Equals(username))
                    {
                        exist = true;
                    }

                }
            }else
            {
                exist = true;
                message = "Username is missing, please fill text field!";
            }
            
            if (!exist)
            {
                if (password.Length < 8)
                {
                    MessageBox.Show("Passowrd must contain at least 8 characters!! Try again!");
                    Password_tf.Clear();
                    Re_Password_tf.Clear();

                }
                else
                {
                    if (Password_tf.Password == Re_Password_tf.Password)
                    {
                        Lists.User_list.Add(user);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please re-enter same password!!");
                        Re_Password_tf.Clear();

                    }
                }
            }
            else
            {
                MessageBox.Show(message);
                Username_tf.Clear();
            }
       
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
           
        }

        public void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str);
            }
        }

        private void App_Closing(object sender, CancelEventArgs e)
        {
            LoginClass lc = new LoginClass();
            lc.Show();
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
