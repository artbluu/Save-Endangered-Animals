using System.Drawing;
using System.Collections.ObjectModel;

using System.Windows;

using WpfApp1.Model;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp1.Help;

namespace WpfApp1.ViewWindows
{

    public partial class EtiquettePreview : Window,INotifyPropertyChanged,CommandExecuted
    {
        private Etiquette selected;
        public ObservableCollection<Etiquette> EtiquetteList
        {
            get;
            set;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private ICollectionView _View;
        private bool _GroupView;


        public EtiquettePreview()
        {
            InitializeComponent();
            this.DataContext = this;
            EtiquetteList = Lists.Etiquette_list;
            View = CollectionViewSource.GetDefaultView(EtiquetteList);
            GroupView = false;

        }

        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            selected = (Etiquette)dgrMain.SelectedItem;
            if (selected != null)
            {
                EtiquetteWindow ew = new EtiquetteWindow();
                ew.Show();
                ew.Mark_tf.Text = selected.Etiquette_Mark;
                ew.Mark_tf.IsEnabled = false;
                ew.Description_tf.Text = selected.Etiquette_Description;

                Color color = System.Drawing.Color.FromArgb(selected.Etiquette_Color.A, selected.Etiquette_Color.R, selected.Etiquette_Color.G, selected.Etiquette_Color.B);
                ew.ColorPicker.SelectedColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
              
                this.Close();
            }
            else
            {
                MessageBox.Show("First you have to choose etiqutte!");
            }
          
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            selected = (Etiquette)dgrMain.SelectedItem;
            if (selected != null)
            {
                Lists.Etiquette_list.Remove(selected);
            }
            else
            {
                MessageBox.Show("First you have to choose etiqutte!");
            }
        }

        //find by mark
        private void Find(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            foreach(Etiquette et in EtiquetteList)
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
                        View.GroupDescriptions.Add(new PropertyGroupDescription("Etiquette_Color"));
                    }
                    _GroupView = value;
                    OnPropertyChanged("GroupView");

                    OnPropertyChanged("View");
                }
            }
        }

    }
}
