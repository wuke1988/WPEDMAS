using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPEDMAS.Mvvm.Wpf
{
    public static class BindingHelper
    {
        private static readonly TextBlock dummyElement = new TextBlock();
        public static object GetBindingValue(this BindingBase binding, object dataContext)
        {
            dummyElement.DataContext = dataContext;
            dummyElement.SetBinding(FrameworkElement.TagProperty, binding);
            var value = dummyElement.Tag;
            BindingOperations.ClearBinding(dummyElement, FrameworkElement.TagProperty);
            dummyElement.DataContext = null;
            return value;
        }
    }
}
