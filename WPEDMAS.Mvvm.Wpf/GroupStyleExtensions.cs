using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPEDMAS.Mvvm.Wpf
{
    public static class GroupStyleExtensions
    {
        public static readonly DependencyProperty AppendProperty = DependencyProperty.RegisterAttached("Append",typeof(GroupStyle),
            typeof(GroupStyleExtensions),new PropertyMetadata(OnAppendChanged));
        public static readonly DependencyProperty AppendManyProperty = DependencyProperty.RegisterAttached("AppendMany", typeof(IEnumerable<GroupStyle>),
            typeof(GroupStyleExtensions),new PropertyMetadata(OnAppendManyChanged));
        public static GroupStyle GetAppend(DependencyObject d)
        {
            return (GroupStyle)d.GetValue(AppendProperty);
        }
        public static void SetAppend(DependencyObject d, GroupStyle value)
        {
            d.SetValue(AppendProperty, value);
        }
        public static IEnumerable<GroupStyle> GetAppendMany(DependencyObject d)
        {
            return (IEnumerable<GroupStyle>)d.GetValue(AppendManyProperty);
        }
        public static void SetAppendMany(DependencyObject d, IEnumerable<GroupStyle> value)
        {
            d.SetValue(AppendManyProperty, value);
        }
        private static void OnAppendChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = (ItemsControl)d;
            var newStyle = (GroupStyle)e.NewValue;
            itemsControl.GroupStyle.Add(newStyle);
        }
        private static void OnAppendManyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = (ItemsControl)d;
            var newStyle = (IEnumerable<GroupStyle>)e.NewValue;
            newStyle?.Sink(itemsControl.GroupStyle.Add);
        }

    }
}
