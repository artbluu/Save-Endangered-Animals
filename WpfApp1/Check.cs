using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Model;

namespace WpfApp1
{
    public class Check 
    {
        //Checking if already in sistem
        public bool exists = false;
        public Check(Object o) {

            if(o.GetType()  == typeof(TypeClass))
            {
                TypeClass type = (TypeClass)o;
                if (type.Type_Mark.Length <= 20)
                {
                    foreach (TypeClass var in Lists.Type_list)
                    {
                        if (type.Type_Mark.Equals(var.Type_Mark))
                        {
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        Lists.Type_list.Add(type);
                    }
                    else
                    {
                        MessageBox.Show("Mark : " + type.Type_Mark + " already exists! Try other mark!");
                    }
                }
                else
                {
                    exists = true;
                    MessageBox.Show("Type mark contains  " + type.Type_Mark.Length + " characters , you can't have more than 20");
                }
            }
            else if(o.GetType() == typeof(Species))
            {
                Species species = (Species)o;

                if (species.Mark.Length <= 20)
                {
                    foreach (Species var in Lists.SpeciesList)
                    {
                        if (var.Mark.Equals(species.Mark))
                        {
                            exists = true;
                        }

                    }
                    if (!exists)
                    {
                        Lists.SpeciesList.Add(species);
                    }
                    else
                    {
                        MessageBox.Show("Species mark : " + species.Mark + " already exists! Try other mark!");
                    }
                }
                else
                {
                    exists = true;
                    MessageBox.Show("Species mark contains  " + species.Mark.Length + " characters, you can't have more than 20");
                }

            }
            else if(o.GetType() == typeof(Etiquette))
            {
                Etiquette etiquette = (Etiquette)o;
                if (etiquette.Etiquette_Mark.Length <= 20)
                {
                    foreach (Etiquette var in Lists.Etiquette_list)
                    {
                        if (var.Etiquette_Mark.Equals(etiquette.Etiquette_Mark))
                        {
                            exists = true;
                        }

                    }
                    if (!exists)
                    {
                        Lists.Etiquette_list.Add(etiquette);
                    }
                    else
                    {
                        MessageBox.Show("Etiquette mark : " + etiquette.Etiquette_Mark + " already exists! Try other mark!");
                    }
                }else
                {

                    exists = true;
                    MessageBox.Show("Etiquette mark contains  " + etiquette.Etiquette_Mark.Length + " characters, you can't have more than 20");
                }
            }
        }


    }
}
