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
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class TypeChoose : Window,CommandExecuted
    {
        private TypeClass selected_type;
        public ObservableCollection<TypeClass> types_list
        {
            get;
            set;
        }

        public TypeChoose()
        {
            InitializeComponent();
            this.DataContext = this;
            types_list = Lists.Type_list;
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
             if(dgrMain.SelectedItem != null)
             {
                selected_type = (TypeClass)dgrMain.SelectedItem;
             }else
             {
                selected_type = null;
             }
            this.Close();
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        //find by name
        private void Find(object sender, TextChangedEventArgs e)
        {
            Find2_tf.Text = "";
            foreach (TypeClass type in types_list)
            {
                if (Find_tf.Text.Equals(type.Type_Name))
                {
                    dgrMain.SelectedItem = type;
                }
            }
        }

        private void Find_by_mark(object sender, TextChangedEventArgs e)
        {
            Find_tf.Text = "";
            foreach (TypeClass type in types_list)
            {
                if (Find2_tf.Text.Equals(type.Type_Mark))
                {
                    dgrMain.SelectedItem = type;
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

        public TypeClass Selected_type
        {
            get
            {
                return selected_type;
            }
            set
            {
                if (value != selected_type)
                {
                    selected_type = value;

                }
            }

        }
    }
}
