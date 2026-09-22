using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class BaseDialogWindow : Window
    {
        public virtual string GetDialogResult()
        {
            return string.Empty;
        }
    }
}
