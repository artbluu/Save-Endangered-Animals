using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginClass.xaml
    /// </summary>
    public partial class LoginClass : Window, CommandExecuted
    {
        public LoginClass()
        {
            InitializeComponent();
            this.DataContext = this;
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

        private void Register(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
           
            Registration sw =  new Registration();
            sw.Show();
            this.Close();
           
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Boolean exists = false;
            Boolean wrong_password = true;
            foreach(User user in Lists.User_list)
            {
                if (user.Username.Equals(Login_tf.Text))
                {
                    exists = true;
                    if (user.Password.Equals(Password_tf.Password))
                    {
                        wrong_password = false;
                    }
                }
                
            }
            if (exists)
            {
                if (!wrong_password)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                } else
                {
                    MessageBox.Show("Wrong password! Try again");
                    Password_tf.Clear();
                }
            }else
            {
                MessageBox.Show("Username does not exist!! If you dont have account please register!");
                Login_tf.Clear();
                Password_tf.Clear();
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                string str = "Log";
                HelpProvider.ShowHelp(str);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
