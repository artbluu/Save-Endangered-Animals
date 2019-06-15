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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Help;
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Window2 : Window, INotifyPropertyChanged,CommandExecuted
    {
        private string etiquetes_names;
        private Species selected;

        public event PropertyChangedEventHandler PropertyChanged;

       
        private ICollectionView _View;
        public ICollectionView View
        {
            get
            {
                return _View;
            }
            set
            {
                _View = value;
                OnPropertyChanged("View");
            }
        }
        private bool _GroupView;
        public bool GroupView
        {
            get
            {
                return _GroupView;
            }
            set
            {
                if (value != _GroupView && View != null)
                {
                    View.GroupDescriptions.Clear();
                    if (value)
                    {
                        View.GroupDescriptions.Add(new PropertyGroupDescription("Species_Type"));
                    }
                    _GroupView = value;
                    OnPropertyChanged("GroupView");

                    OnPropertyChanged("View");
                }
            }
        }
        public ObservableCollection<Species> SpeciesList
        {
            get;
            set;
        }
        public ObservableCollection<Etiquette> EtiquetteList
        {
            get;
            set;
        }

        public Window2()
        {
            InitializeComponent();
            this.DataContext = this;
            SpeciesList = Lists.SpeciesList;
            EtiquetteList = Lists.Etiquette_list;  
            View = CollectionViewSource.GetDefaultView(SpeciesList);
            GroupView = false;
        }

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private void DgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Species a = (Species)dgrMain.SelectedItem;
            foreach(Etiquette etiq in EtiquetteList)
            {
                etiquetes_names += etiq.Etiquette_Mark + " ";
            }
           
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            selected = (Species)dgrMain.SelectedItem;
            if (selected != null)
            {
                SpeciesWindow sw = new SpeciesWindow();
                sw.selected_etiquettes = selected.selected_etiquettes;
                sw.Show();
                sw.Name_tf.Text = selected.Species_Name;
                sw.Species_Mark.Text = selected.Mark;
                sw.Species_Mark.IsEnabled = false;
                sw.Income_tf.Text = selected.Income.ToString();
                sw.Description_tf.Text = selected.Species_Description;
                if (selected.Species_Dangerous)
                    sw.DangerousYes.IsChecked = true;
                if (selected.Species_Red)
                    sw.RedListYes.IsChecked = true;
                if (selected.Species_Settled)
                    sw.SettledYes.IsChecked = true;
                sw.Toursistic_Status.Text = selected.Species_Touristic;
                sw.Endange_Status.Text = selected.Species_Endange;
            
                sw.Species_Icon.Source = selected.Species_Image.Source;
                sw.Reset.Content = "Cancel";
                sw.DatePicker.Text = selected.Discovered;

                sw.Type_tf.Text = selected.Species_Type;
               
              
                this.Close();


            }
            else
            {
                MessageBox.Show("First you have to choose type!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            selected = (Species)dgrMain.SelectedItem;
            if (selected != null)
            {
                Lists.SpeciesList.Remove(selected);
                foreach(Species sp in Lists.MapSpecies)
                {
                    if (selected.Mark.Equals(sp.Mark))
                    {
                        Lists.MapSpecies.Remove(sp);
                        MainWindow.getInstance().RefreshPublic();
                        MessageBox.Show("Successfully deleted species from map : " + sp.Mark);
                        break;
                    }

                }

            }
            else
            {
                MessageBox.Show("First you have to choose type!");
            }
        
            }
        //Find by name
        private void Find(object sender, TextChangedEventArgs e)
        {
            foreach (Species sp in SpeciesList)
            {
                if (Find_tf.Text.Equals(sp.Species_Name))
                {
                    if (Find2_tf.Text.Equals(""))
                    {
                        dgrMain.SelectedItem = sp;
                    }else
                    {
                        if (Find2_tf.Text.Equals(sp.Species_Type))
                        {
                            dgrMain.SelectedItem = sp;
                        }

                    }
                    
                }
            }
        }
        //Fing by mark of type
        private void Find_by_mark(object sender, TextChangedEventArgs e)
        {
            foreach (Species sp in SpeciesList)
            {
                if (Find2_tf.Text.Equals(sp.Species_Type))
                {
                    if (Find_tf.Text.Equals(""))
                    {
                        dgrMain.SelectedItem = sp;
                    }
                    else
                    {
                        if (Find_tf.Text.Equals(sp.Species_Name))
                        {
                            dgrMain.SelectedItem = sp;
                        }
                    }
                   
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

        public string Etiquettes_Names
        {
            get
            {
                return etiquetes_names;
            }
            set
            {
                if (value != etiquetes_names)
                {
                    etiquetes_names = value;
                    OnPropertyChanged("Etiquettes_Names");
                }
            }

        }


    }
}
