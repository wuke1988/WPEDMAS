using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPEDMAS.Client.Core.ViewModels;
using WPEDMAS.Client.ViewModels;
using WPEDMAS.DataManagement;
using WPEDMAS.Mvvm.Wpf;
using WPEDMAS.SingleWell;

namespace WPEDMAS.Client
{
    public class WPEDMASBootstrapper : BootstrapperBase, IDisposable
    {
        private CompositionContainer _container;

        public WPEDMASBootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var type1 = IoC.GetInstance(typeof(IShell), null);
            var type2 = IoC.GetInstance(typeof(ShellViewModel), null);
            DisplayRootViewFor<IShell>();
        }
        protected override void Configure()
        {
            _container = new CompositionContainer(new AggregateCatalog(
                 AssemblySource.Instance.Select(o => new AssemblyCatalog(o)).OfType<ComposablePartCatalog>()));

            //如果还有自己的部件都加在这个地方
            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
        }
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies.AddMvvm();
            assemblies.Add(typeof(WPEDMASBootstrapper).Assembly);
            assemblies.AddSingleWell();
            assemblies.AddDataManagement();

            return assemblies.Distinct();
        }
        //获取某一特定类型的所有实例
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        //根据传过来的类型和名称获取实例
        protected override object GetInstance(Type service, string key)
        {
            string _contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(_contract);
            if (exports.Any())
            {
                return exports.First();
            }
            return base.GetInstance(service, key);
            //throw new Exception(string.Format("找不到{0}实例", _contract));
        }
        //将实例传递给 Ioc 容器，使依赖关系注入
        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
