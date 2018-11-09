using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WPEDMAS.Common.QueryClient
{
    public static class CommnonQueryClientAssemblyExtensions
    {
        public static void AddCommonQueryClient(this ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(CommnonQueryClientAssemblyExtensions).Assembly);
        }
    }
}