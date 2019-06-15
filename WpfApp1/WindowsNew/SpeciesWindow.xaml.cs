using Microsoft.Win32;
using System;
using System.Collections;
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

using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ChooseWindows;
using WpfApp1.Help;
using WpfApp1.Model;
using WpfApp1.Validate;
using WpfApp1.WindowsChoose;

namespace WpfApp1
{
    //New Species
    public partial class SpeciesWindow : Window, CommandExecuted
    {
       
        private Species species;
        private TypeClass selected_type;
        public ObservableCollection<Etiquette> selected_etiquettes
        {
            get;
            set;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private double income;
        private string sp_name;
        private string mark;
        private string date;
        private string description;
        private bool dangerous;
        private bool red_list;
        private bool settled_region;
        private string etiquettes_mark;
        private Image icon = new Image();
        private Image source = new Image();

        public SpeciesWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            icon.Source = Species_Icon.Source;
            source.Source = Species_Icon.Source;
            selected_etiquettes = new ObservableCollection<Etiquette>();
        }

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        //Choosing type
        private void Choose_click(object sender, RoutedEventArgs e)
        {
            TypeChoose typechoose = new TypeChoose();
            typechoose.ShowDialog();

            TypeClass temp = typechoose.Selected_type;

            if (temp != null)
            {
                Type_tf.Text = temp.Type_Mark;

                YesNo dialog = new YesNo();
                dialog.textBlock.Text = "Do you want to use type image for species?";
                dialog.ShowDialog();
                if (dialog.YES == 1)
                {
                    Species_Icon.Source = temp.Type_Image.Source;
                }                
            }
        }

        private void Select_Etiq(object sender, RoutedEventArgs e)
        {
            EtiquetteChoose ec = new EtiquetteChoose();
            ec.ShowDialog();

            Etiquette temp = ec.Selected_etiquette;
            if (temp != null)
            {
                bool exists = false;
               
                foreach (Etiquette et in selected_etiquettes)
                {
                    if (temp.Etiquette_Mark.Equals(et.Etiquette_Mark))
                    {
                        
                        exists = true;
                     
                    }
                }

                if (!exists)
                {
                    selected_etiquettes.Add(temp);
                }
                else
                {
                    MessageBox.Show("Etiquette already selected!");
                }
           
            }

        }

        private void Save_Species(object sender, RoutedEventArgs e) { 

            if (Check(sp_name) == false && Check(mark) == false && Check(Name_tf.Text)==false && Check(Species_Mark.Text)==false)
            {

                if (Species_Mark.IsEnabled)
                {
                    icon.Source = Species_Icon.Source;
                    try
                    {
                        DateTime dateTime = (DateTime)DatePicker.SelectedDate;
                        date = dateTime.ToShortDateString();
                    }
                    catch
                    {
                        date = new DateTime().ToShortDateString();
                    }


                    if ((bool)DangerousYes.IsChecked)
                        dangerous = true;
                    if ((bool)RedListYes.IsChecked)
                        red_list = true;
                    if ((bool)SettledYes.IsChecked)
                        settled_region = true;

                    if (!Check(Type_tf.Text))
                    {
                        foreach (TypeClass type in Lists.Type_list)
                        {
                            if (type.Type_Mark.Equals(Type_tf.Text))
                            {
                                selected_type = type;
                            }

                        }
                    }
                    ObservableCollection<Etiquette> temp_etiquettes = new ObservableCollection<Etiquette>();

                    if (selected_etiquettes != null)
                        foreach (Etiquette et in selected_etiquettes)
                        {
                            if (et.Etiquette_Enabled)
                            {
                                temp_etiquettes.Add(et);
                                etiquettes_mark += et.Etiquette_Mark + " ";
                            }
                        }

                    species = new Species(mark, income, sp_name, date, description, dangerous, red_list, settled_region,
                        Endange_Status.Text, Toursistic_Status.Text, icon.Source.ToString(), Type_tf.Text, selected_type, temp_etiquettes, etiquettes_mark, icon);
                  
                    Check check = new Check(species);
                    if (!check.exists)
                        this.Close();
                }
                else
                {
                    update(Lists.SpeciesList);
                    update(Lists.MapSpecies);
                    this.Close();
                }
            }
            else
            {
                string message = "Species ";
                if (Check(sp_name) || Check(Name_tf.Text))
                    message += "*name, ";
                if (Check(mark) || Check(Species_Mark.Text))
                    message += "*mark, ";
                MessageBox.Show(message + " is empty or invalid!!!");
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (Species_Mark.IsEnabled)
            {
                Name_tf.Clear();
                Species_Mark.Clear();
                Income_tf.Text = "0";
                Description_tf.Clear();
                DangerousNo.IsChecked = true;
                RedListNo.IsChecked = true;
                SettledNo.IsChecked = true;
                Toursistic_Status.Text = "";
                Endange_Status.Text = "";
                Species_Icon.Source = source.Source;
                DatePicker.Text = "";
                Type_tf.Clear();
                selected_etiquettes.Clear();
            }else
            {
                this.Close();
            }
    }

        private void Select_Image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();


            fileDialog.Title = "Choose icon";
            fileDialog.Filter = "Images|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (fileDialog.ShowDialog() == true)
            {
                Species_Icon.Source = new BitmapImage(new Uri(fileDialog.FileName));


                icon.Source = Species_Icon.Source;

            }
        }

        private void update(ObservableCollection<Species> listForUpdate)
        {
            
            foreach(Species var in listForUpdate)
            {
                if (var.Mark.Equals(Species_Mark.Text))
                {
                    var.Species_Name = Name_tf.Text;

                    var.Income = Double.Parse(Income_tf.Text);
                    var.Species_Description = Description_tf.Text;
                    string date_string;
                    try
                    {
                        DateTime dateTime = (DateTime)DatePicker.SelectedDate;
                        date_string = dateTime.ToShortDateString();
                    }
                    catch
                    {
                        date_string = new DateTime().ToShortDateString();
                    }
                    var.Discovered = date_string;
                    var.Species_Dangerous = false;
                    var.Species_Red = false;
                    var.Species_Settled = false;
                    var.Species_Endange = Endange_Status.Text;
                    var.Species_Touristic = Toursistic_Status.Text;

                    if ((bool)DangerousYes.IsChecked)
                        var.Species_Dangerous = true;
                    if ((bool)RedListYes.IsChecked)
                        var.Species_Red = true;
                    if ((bool)SettledYes.IsChecked)
                        var.Species_Settled = true;
                    var.Species_Type = Type_tf.Text;
                    var.Species_Image.Source = Species_Icon.Source;
                    var.Species_Path = Species_Icon.Source.ToString();

                    ObservableCollection<Etiquette> temp_etiquettes = new ObservableCollection<Etiquette>();
                    String et_mark = "";
                    if (selected_etiquettes != null)
                        foreach (Etiquette et in selected_etiquettes)
                        {
                            if (et.Etiquette_Enabled)
                            {
                                temp_etiquettes.Add(et);
                                et_mark += et.Etiquette_Mark + " ";
                            }
                        }
                    var.selected_etiquettes = temp_etiquettes;
                    var.Etiquettes_Marks = et_mark;
                    MainWindow.getInstance().RefreshPublic();
                }



            }
        }

        private bool Check(string a)
        {
            return String.IsNullOrEmpty(a);
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
        public double Income
        {
            get
            {
                return income;
            }
            set
            {
                if (value != income)
                {
                    income = value;
                    OnPropertyChanged("Income");
                }
            }
        }
        public string Species_Name
        {
            get
            {
                return sp_name;
            }
            set
            {
                if (value != sp_name)
                {
                    sp_name = value;
                    OnPropertyChanged("Species_Name");
                }
            }

        }
        public string Mark
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
                    OnPropertyChanged("Mark");
                }
            }
        }
        public string Discovered
        {
            get
            {
                return date;
            }
            set
            {
                if (value != date)
                {
                    date = value;

                    OnPropertyChanged("Discovered");
                }

            }

        }
        public Image Species_Image
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
                    OnPropertyChanged("Species_Image");
                }
            }

        }
        public string Species_Description
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
                    OnPropertyChanged("Species_Description");
                }
            }
        }
        public bool Species_Dangerous
        {
            get
            {
                return dangerous;
            }
            set
            {
                if (value != dangerous)
                {
                    dangerous = value;
                    OnPropertyChanged("Species_Dangerous");
                }
            }
        }
        public bool Species_Red
        {
            get
            {
                return red_list;
            }
            set
            {
                if (value != red_list)
                {
                    red_list = value;
                    OnPropertyChanged("Species_Red");
                }
            }
        }
        public bool Species_Settled
        {
            get
            {
                return settled_region;
            }
            set
            {
                if (value != settled_region)
                {
                    settled_region = value;
                    OnPropertyChanged("Species_Settled");
                }
            }
        }
        public string Etiquettes_Marks
        {
            get
            {
                return etiquettes_mark;
            }
            set
            {
                if (value != etiquettes_mark)
                {
                    etiquettes_mark = value;
                    OnPropertyChanged("Etiquettes_Marks");
                }
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
                    OnPropertyChanged("Selected_type");
                }
            }

        }       
       
    }

}
