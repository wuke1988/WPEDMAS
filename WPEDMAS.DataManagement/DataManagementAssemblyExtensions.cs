using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPEDMAS.Client.Core.ViewModels;

namespace WPEDMAS.DataManagement
{
    public static class DataManagementAssemblyExtensions
    {
        private readonly static Uri _dataManagementTopLevelWidgetUri = new Uri("/WPEDMAS.DataManagement;component/DataManagementTopLevelWidget.xaml", UriKind.Relative);

        private readonly static Lazy<TopLevelWidget> _dataManagementTopLevelWidget
            = new Lazy<TopLevelWidget>(() => (TopLevelWidget)Application.LoadComponent(_dataManagementTopLevelWidgetUri));

        [Export]
        public static TopLevelWidget DataManagementTopLevelWidget
        {
            get { return _dataManagementTopLevelWidget.Value; }
        }

        public static void AddDataManagement(this ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(DataManagementAssemblyExtensions).Assembly);
        }
    }
}
