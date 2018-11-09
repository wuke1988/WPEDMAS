using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPEDMAS.Client.Core.ViewModels
{
    public class TopLevelWidget : DependencyObject
    {
        public Geometry Data { get; set; }
        public float IconWidth { get; set; }
        public float IconHeight { get; set; }
        public string Title { get; set; }
        public Type ContentType { get; set; }

        public object Content
        {
            get
            {
                return IoC.GetInstance(ContentType, null);
            }
        }
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(TopLevelWidget),
            new PropertyMetadata(true));
    }
}
