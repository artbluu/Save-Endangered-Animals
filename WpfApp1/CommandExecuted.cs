using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Help;

namespace WpfApp1
{
    public interface CommandExecuted
    {
        void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e);
    }
}
