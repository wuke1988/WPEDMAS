using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPEDMAS.QueryClient.Extensions
{
    public static class GridViewColumnExtensions
    {
        public static readonly DependencyProperty VisibilityProperty = 
            DependencyProperty.RegisterAttached("Visibility",typeof(Visibility),typeof(GridViewColumnExtensions),new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty VisibleWidthProperty =
            DependencyProperty.RegisterAttached("VisibleWidth",typeof(double),typeof(GridViewColumnExtensions),new PropertyMetadata(double.NaN));

        public static Visibility GetVisibility(DependencyObject d)
        {
            return (Visibility)d.GetValue(VisibilityProperty);
        }
        public static void SetVisibility(DependencyObject d, Visibility value)
        {
            d.SetValue(VisibilityProperty,value);
        }
        public static double GetVisibleWidth(DependencyObject d)
        {
            return (double)d.GetValue(VisibleWidthProperty);
        }

        public static void SetVisibleWidth(DependencyObject d, double value)
        {
            d.SetValue(VisibleWidthProperty, value);
        }
    }
}
