using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
  
    public partial class TypeView : Window,CommandExecuted
    {
        //Selected type for changing
        private TypeClass selected;
        public ObservableCollection<TypeClass> types_list
        {
            get;
            set;
        }

        public TypeView()
        {
            InitializeComponent();
            this.DataContext = this;
            types_list = Lists.Type_list;
        }
        private void Change_Type(object sender, RoutedEventArgs e)
        {
            selected = (TypeClass)dgrMain.SelectedItem;
            if (selected != null)
            {
                
                TypeWindow tw = new TypeWindow();
                tw.Show();
                tw.Name_tf.Text = selected.Type_Name;
                tw.Mark_tf.Text = selected.Type_Mark;
                tw.Mark_tf.IsEnabled = false;
                tw.Description_tf.Text = selected.Type_Description;
                tw.Cancel.Content = "Delete";
                tw.Insects_png.Source = selected.Type_Image.Source;
             
                this.Close();
            }
            else
            {
                MessageBox.Show("First you have to choose type!");
            }

        }

        private void Delete_Type(object sender, RoutedEventArgs e)
        {
            selected = (TypeClass)dgrMain.SelectedItem;
            Lists.Type_list.Remove(selected);
            if(selected != null)
                MessageBox.Show("Type successfully deleted!");
            else
                MessageBox.Show("First you have to choose type!");

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
    }
}
