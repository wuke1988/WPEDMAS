using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPEDMAS.Mvvm.Wpf.Converters;

namespace WPEDMAS.Mvvm.Wpf
{
    public static class MvvmAssemblyExtensions
    {
        static MvvmAssemblyExtensions()
        {
            TypeConverterRegister.Register<BindingExpression, BindingConvertor>();
        }

        public static void AddMvvm(this ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(Tomato.Mvvm.BindingHelper).Assembly);
            assemblies.Add(typeof(MvvmAssemblyExtensions).Assembly);
        }
    }
}
