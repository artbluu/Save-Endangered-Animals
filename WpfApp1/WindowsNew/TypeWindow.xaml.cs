using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Model;
using Microsoft.Win32;
using System.Windows.Controls;
using WpfApp1.Help;

namespace WpfApp1
{
    /// New Type
    public partial class TypeWindow : Window,CommandExecuted
    {
        private string name;
        private string mark;
        private string description;
        private Image icon = new Image();
        public event PropertyChangedEventHandler PropertyChanged;

        public TypeWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            icon.Source = Insects_png.Source;
            
        }
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        { 
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Title = "Choose icon: ";
            fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                Insects_png.Source = new BitmapImage(new Uri(fileDialog.FileName));
                icon.Source = Insects_png.Source;
            }
        }
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Check(name)==false && Check(mark)==false && Check(Name_tf.Text)==false && Check(Mark_tf.Text)==false)
            {
                if (Mark_tf.IsEnabled)
                {
                    icon.Source = Insects_png.Source;                    
                    TypeClass type = new TypeClass(name, mark, description, icon, icon.Source.ToString());
                    Check check = new Check(type);
                    if (!check.exists)
                        this.Close();
                }
                else
                {
                    Update();
                    this.Close();
                }
            }
            else
            {
                string message = "Type ";
              
                if (Check(name) || Check(Name_tf.Text))
                    message += "name, ";
                if (Check(mark) || Check(Mark_tf.Text))
                    message += "mark";
                MessageBox.Show(message + " is empty or invalid!!!");
            }
        }

        private bool Check(string a)
        {
            return String.IsNullOrEmpty(a);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update()
        {
            foreach (TypeClass var in Lists.Type_list)
            {
                if (var.Type_Mark.Equals(Mark_tf.Text))
                {
                    var.Type_Description = description;
                    var.Type_Name = name;
                    var.Type_Image = icon;
                    var.Type_Path = icon.Source.ToString();

                }
            }

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

        public string Type_Name
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
                    OnPropertyChanged("Type_Name");
                }
            }

        }
        public string Type_Mark
        {
            get
            {
                return mark;
            }
            set
            {
                if (value != mark)
                {
                    mark = value;
                    OnPropertyChanged("Type_Mark");
                }
            }

        }
        public string Type_Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Type_Description");
                }
            }

        }
        public Image Type_Image
        {
            get
            {
                return icon;
            }
            set
            {
                if (value != icon)
                {
                    icon = value;
                    OnPropertyChanged("Type_Image");
                }
            }

        }
    }
}
