using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Validate
{
    //Validation of textfields
    public class StringToDoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, "$");
                }
                MessageBox.Show("Only numbers are allowed!!");
                return new ValidationResult(false, "!");
            }
            catch
            {
                return new ValidationResult(false, "!");
            }
        }
    }

    public class MinMaxValidationRule : ValidationRule
    {
        public double Min
        {
            get;
            set;
        }

        public double Max
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is double)
            {
                double d = (double)value;
                if (d < Min)
                {
                    MessageBox.Show("Value too small!");
                    return new ValidationResult(false, "s");
                   
                }
                if (d > Max)
                {
                    MessageBox.Show("Value too large!");
                    return new ValidationResult(false, "b");
                }
                return new ValidationResult(true, "$");
            }
            else
            {
                return new ValidationResult(false, "##");
            }
        }
    }
}
