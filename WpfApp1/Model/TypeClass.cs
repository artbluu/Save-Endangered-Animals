using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace WpfApp1.Model
{
    [Serializable]
    public class TypeClass
    {
        private string name;
        private string mark;
        private string description;
        [NonSerialized]
        private Image icon = new Image();
        private string icon_path;
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public TypeClass() { }
        public TypeClass(string name, string mark, string description, Image icon,string icon_path)
        {
            this.name = name;
            this.mark = mark;
            this.description = description;
            this.icon = icon;
            this.icon_path = icon_path;
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
        [XmlIgnore]
        public Image Type_Image
        {
            get
            {
                icon.Source =  new BitmapImage(new Uri(icon_path));

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
        public string Type_Path
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
                    OnPropertyChanged("Type_Path");
                }
            }

        }
    }
    
}
