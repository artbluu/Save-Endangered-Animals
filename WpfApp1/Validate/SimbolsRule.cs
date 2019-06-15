using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Validate
{
    public class SimbolsRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
  {
            string err = "*".PadLeft(1);
            string err2 = "!".PadLeft(1);

            try
            {
                var tag = value as string;
                if (String.IsNullOrWhiteSpace(tag))
                {
                    return new ValidationResult(false, err);
                }
                Regex r = new Regex(@"^[šŠđĐčČćĆžŽa-zA-Z0-9_' ']+$");
                if (!r.IsMatch(tag))
                {
                    MessageBox.Show("Forbidden symbol used!!!");

                    return new ValidationResult(false, err2);

                }
                return new ValidationResult(true, null);
            }
            catch
            {

                return new ValidationResult(false, "Symbols error!");
            }
        }
    }
}
