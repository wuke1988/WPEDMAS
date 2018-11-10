using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.Common.QueryClient;
using WPEDMAS.Mvvm.Wpf;

namespace WPEDMAS.QueryClient
{
    public static class WPEDMASQueryClientAssemblyExtensions
    {
        public static void AddQueryClient(this ICollection<Assembly> assemblies)
        {
            assemblies.AddMvvm();
            assemblies.AddCommonQueryClient();
            assemblies.Add(typeof(WPEDMASQueryClientAssemblyExtensions).Assembly);
        }
    }
}
