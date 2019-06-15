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

namespace WpfApp1.ChooseWindows
{

    public partial class EtiquetteChoose : Window,CommandExecuted
    {
        private Etiquette selected_etiquette;
        public ObservableCollection<Etiquette> Etiquette_list
        {
            get;
            set;
        }

        public EtiquetteChoose()
        {
            InitializeComponent();
            this.DataContext = this;
            Etiquette_list = new ObservableCollection<Etiquette>();
           
            foreach (Etiquette etiq in Lists.Etiquette_list)
            {
                etiq.Etiquette_Enabled = true;
                Etiquette_list.Add(etiq);
            }

        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {

            if (dgrMain.SelectedItem != null)
            {
                selected_etiquette = (Etiquette)dgrMain.SelectedItem;     
            }
            else
            {
                selected_etiquette = null;
            }
            this.Close();
        }

        private void Find(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            foreach (Etiquette et in Etiquette_list)
            {
                if (Find_tf.Text.Equals(et.Etiquette_Mark))
                {
                    dgrMain.SelectedItem = et;
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

        public Etiquette Selected_etiquette
        {
            get
            {
                return selected_etiquette;
            }
            set
            {
                if (value != selected_etiquette)
                {
                    selected_etiquette = value;

                }
            }

        }
    }
}
