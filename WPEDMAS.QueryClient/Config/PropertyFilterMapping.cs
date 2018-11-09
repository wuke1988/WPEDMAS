using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPEDMAS.Mvvm.Wpf;
using WPEDMAS.QueryClient.Providers;
using Orc.FilterBuilder;

namespace WPEDMAS.QueryClient.Config
{
    public class PropertyFilterMapping
    {
        public PropertyPath SourceProperty { get; set; }
        public string TargetProperty { get; set; }
        public IValueConverter SourceConverter { get; set; }
        public Orc.FilterBuilder.Condition Condition { get; set; }

        internal PredefinedCondition Export(object source)
        {
            return new PredefinedCondition
            {
                PropertyName = TargetProperty,
                Condition = Condition,
                Value = new Binding { Path = SourceProperty, Converter = SourceConverter }.GetBindingValue(source)
            };
        }
        public static PredefinedScheme Export(IEnumerable<PropertyFilterMapping> mappings, object source)
        {
            return new PredefinedScheme (mappings.Select(o=>o.Export(source)));
        }
        
    }
}
