using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WpfApp1.Model;

namespace WpfApp1
{
    [Serializable]
    public class Lists
    {
        public static ObservableCollection<Species> SpeciesList;
        public static ObservableCollection<Etiquette> Etiquette_list;
        public static ObservableCollection<TypeClass> Type_list;
        public static ObservableCollection<User> User_list;
        public static ObservableCollection<Species> MapSpecies;

        public Lists() {
           
            var requiredPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)));
            var current_file = requiredPath.Substring(6);
            var path_etiquette = current_file + "\\Etiquettes.xml";
            var path_type = current_file + "\\Types.xml";
            var path_species = current_file + "\\Species.xml";
            var path_user = current_file + "\\Users.xml";
            var path_map_species = current_file + "\\MapSpecies.xml";

            //Loading lists from files or creating new if they don't exist
            try
            {
                using (FileStream fs = new FileStream(path_map_species, FileMode.Open)) //double check that...
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(ObservableCollection<Species>));

                    MapSpecies = (ObservableCollection<Species>)_xSer.Deserialize(fs);
                }
            }
            catch
            {
                MapSpecies = new ObservableCollection<Species>();
            }

            try
            {
                using (FileStream fs = new FileStream(path_etiquette, FileMode.Open)) //double check that...
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(ObservableCollection<Etiquette>));

                    Etiquette_list = (ObservableCollection<Etiquette>)_xSer.Deserialize(fs);
                }
            }
            catch
            {
                Etiquette_list = new ObservableCollection<Etiquette>();
            }

            try
            {
                using (FileStream fs = new FileStream(path_type, FileMode.Open)) //double check that...
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(ObservableCollection<TypeClass>));

                    Type_list = (ObservableCollection<TypeClass>)_xSer.Deserialize(fs);
                }
            }
            catch
            {
                Type_list = new ObservableCollection<TypeClass>();
            }

            try
            {
                using (FileStream fs = new FileStream(path_species, FileMode.Open)) //double check that...
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(ObservableCollection<Species>));

                    SpeciesList = (ObservableCollection<Species>)_xSer.Deserialize(fs);
                }
            }
            catch
            {
                SpeciesList = new ObservableCollection<Species>();
            }

            try
            {
                using (FileStream fs = new FileStream(path_user, FileMode.Open)) //double check that...
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(ObservableCollection<User>));

                    User_list = (ObservableCollection<User>)_xSer.Deserialize(fs);
                }
            }
            catch
            {
                User_list = new ObservableCollection<User>();
            }

        }

        
    }
}
