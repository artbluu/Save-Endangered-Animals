using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Shapes;
using WpfApp1.Model;
using WpfApp1.Help;

namespace WpfApp1
{
    public partial class EtiquetteWindow : Window,CommandExecuted
    {
        private string mark;
        private string description;
        private Color color;
        private string colorName;
        public event PropertyChangedEventHandler PropertyChanged;

        public EtiquetteWindow()
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

        private void Save_Etiquette(object sender, RoutedEventArgs e)
        {


            if (!String.IsNullOrWhiteSpace(mark) && !String.IsNullOrWhiteSpace(Mark_tf.Text))
            {
                if (Mark_tf.IsEnabled)
                {
                    Etiquette etiquette = new Etiquette(mark, description, color, colorName, color.ToArgb());
                    Check check = new Check(etiquette);
                    if (!check.exists)
                    {
                        this.Close();
                    }

                }
                else
                {
                    Update();
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Etiquette not saved! You can't leave etiquette mark empty!");
            }
        }

        private void Color_Changed(object sender, RoutedEventArgs e)
        {
          
            colorName = ColorPicker.SelectedColorText;
            System.Windows.Media.Color tf_color = ColorPicker.SelectedColor.Value;
            string b = tf_color.ToString();
            color = System.Drawing.Color.FromArgb(tf_color.A, tf_color.R, tf_color.G, tf_color.B);

        }

        private void Update()
        {
            foreach (Etiquette var in Lists.Etiquette_list)
            {
                if (var.Etiquette_Mark.Equals(Mark_tf.Text))
                {
                    var.Etiquette_Description = description;
                    var.Etiquette_Color = color;
                    var.Etiquette_ColorName = colorName;
                    var.Etiquette_Argb = color.ToArgb();

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

        private void Cancel_Etiquette(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public string Etiquette_Mark
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
                    OnPropertyChanged("Etiquette_Mark");
                }
            }

        }
        public string Etiquette_Description
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
                    OnPropertyChanged("Etiquette_Description");
                }
            }

        }
        public Color Etiquette_Color
        {
            get
            {
                return color;
            }
            set
            {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged("Etiquette_Color");
                }
            }

        }
        public string Etiquette_ColorName
        {
            get
            {
                return colorName;
            }
            set
            {
                if (value != colorName)
                {
                    colorName = value;
                    OnPropertyChanged("Etiquette_ColorName");
                }
            }

        }

       
    }
}
