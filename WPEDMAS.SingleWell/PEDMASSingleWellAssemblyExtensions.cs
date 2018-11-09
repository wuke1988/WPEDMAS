using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPEDMAS.Client.Core.ViewModels;
using WPEDMAS.Common.QueryClient;
using WPEDMAS.QueryClient;

namespace WPEDMAS.SingleWell
{
    public static class PEDMASSingleWellAssemblyExtensions
    {
        private readonly static Uri _singleWellTopLevelWidgetUri = new Uri("/WPEDMAS.SingleWell;component/SingleWellTopLevelWidget.xaml", UriKind.Relative);
        private readonly static Lazy<TopLevelWidget> _singleWellTopLevelWidget = new Lazy<TopLevelWidget>(
            () => (TopLevelWidget)Application.LoadComponent(_singleWellTopLevelWidgetUri));
        [Export]
        public static TopLevelWidget SingleWellTopLevelWidget
        {
            get { return _singleWellTopLevelWidget.Value; }
        }

        public static void AddSingleWell(this ICollection<Assembly> assemblies)
        {
            assemblies.AddQueryClient();
            assemblies.Add(typeof(PEDMASSingleWellAssemblyExtensions).Assembly);
        }
    }
}
