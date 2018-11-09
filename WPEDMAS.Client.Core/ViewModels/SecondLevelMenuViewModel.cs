using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPEDMAS.Client.Core.ViewModels
{
    public class SecondLevelMenuViewModel : DependencyObject
    {
        public string Name { get; set; }
        public ImageSource IconImage { get; set; }
        public float IconWidth { get; set; }
        public float IconHeight { get; set; }
        public Type NavigationType { get; set; }

        public static readonly DependencyProperty ContentProperty =
            ContentPresenter.ContentProperty.AddOwner(typeof(SecondLevelMenuViewModel));

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
    }
}
