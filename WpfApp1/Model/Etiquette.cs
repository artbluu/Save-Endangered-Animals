using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace WpfApp1.Model
{
    [Serializable]
    public class Etiquette
    {
        private string mark;
        private string description;
        private Color color;
        private string colorName;
        private bool enabled;
        private int argb;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public Etiquette() { }
        public Etiquette(string mark,string description, Color color, string colorName,int argb) 
        {
            this.mark = mark;
            this.description = description;
            this.color = color;
            this.colorName = colorName;
            this.enabled = true;
            this.argb = argb;
            
        }
        public Etiquette(string mark)
        {
            this.mark = mark;
            this.enabled = true;

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
                color = System.Drawing.Color.FromArgb(argb);
                  
                //color = System.Drawing.Color.FromName(colorName);
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
        public Boolean Etiquette_Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (value != enabled)
                {
                    enabled = value;
                    OnPropertyChanged("Etiquette_Enabled");
                }
            }

        }
        public int Etiquette_Argb
        {
            get
            {
                return argb;
            }
            set
            {
                if (value != argb)
                {
                    argb = value;
                    OnPropertyChanged("Etiquette_Argb");
                }
            }


        }
    }
}
