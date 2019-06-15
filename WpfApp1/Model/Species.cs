using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace WpfApp1.Model


{
    [Serializable]
    public class Species : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private string name = "";
        private string mark = "";
        private double income;
        private string date;
        [NonSerialized]
        private Image icon = new Image();
        private string description;
        private bool dangerous;
        private bool red_list;
        private bool settled_region;
        private string endange_status;
        private string touristic_status;
        private string icon_path;
        private string type;
        private string etiquettes_mark;
        private TypeClass selected_type;
        private double x = -1;
        private double y = -1;
        public ObservableCollection<Etiquette> selected_etiquettes
        {
            get;
            set;
        }



        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public Species()
        {
           
        }
        public Species(string mark, double income, string name, string date, string description, bool dangerous, bool red_list, bool settled_region,
            string endange_status, string touristic_status,string icon_path, string type, TypeClass selected_type,
            ObservableCollection<Etiquette> selected_etiquettes, string etiquettes_mark, Image icon)
        {
            this.name = name;
            this.mark = mark;
            this.income = income;
            this.date = date;
            this.description = description;
            this.dangerous = dangerous;
            this.red_list = red_list;
            this.settled_region = settled_region;
            this.endange_status = endange_status;
            this.touristic_status = touristic_status;
            this.icon = icon;
            this.icon_path = icon_path;
            this.type = type;
            this.selected_type = selected_type;
            this.selected_etiquettes = selected_etiquettes;
            this.etiquettes_mark = etiquettes_mark;

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
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Species_Name");
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
        [XmlIgnore]
        public Image Species_Image
        {
            get
            {
                //because XmlIngore 
               icon.Source = new BitmapImage(new Uri(icon_path));

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
        public string Species_Endange
        {
            get
            {
                return endange_status;
            }
            set
            {
                if (value != endange_status)
                {
                    endange_status = value;
                    OnPropertyChanged("Species_Endange");
                }
            }

        }
        public string Species_Touristic
        {
            get
            {
                return touristic_status;
            }
            set
            {
                if (value != touristic_status)
                {
                    touristic_status = value;
                    OnPropertyChanged("Species_Touristic");
                }
            }

        }
        public string Species_Path
        {
            get
            {
                return icon_path;
            }
            set
            {
                if (value != icon_path)
                {
                    icon_path = value;
                    OnPropertyChanged("Species_Path");
                }
            }

        }
        public string Species_Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Species_Type");
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
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (value != x)
                {
                    x = value;
                    OnPropertyChanged("x");
                }
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value != y)
                {
                    y = value;
                    OnPropertyChanged("y");
                }
            }
        }






    }
}

