using Catel.Fody;
using Catel.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.Common.QueryClient.Providers;
using WPEDMAS.QueryClient.Config;
using WPEDMAS.QueryClient.Providers;

namespace WPEDMAS.QueryClient.ViewModels
{
    public class GeneralQueryViewModel : QueryViewModelBase<GeneralQueryConfig>
    {
        private bool _lazyUpdate = false;
        public GeneralQueryViewModel([NotNull] GeneralQueryConfig config)
            : base(config)
        {
            var toolbar = config.ToolBar?.ViewModel as QueryToolBarViewModel;
            if (toolbar != null)
                toolbar.FilterUpdated += Toolbar_FilterUpdated;
        }
        protected override Common.QueryClient.Providers.IQueryProvider BuildQueryModel()
        {
            var modelType = typeof(QueryProvider<>).MakeGenericType(Config.EntityType);
            return (IWithFilterSchemeQueryProvider)this.GetTypeFactory().CreateInstanceWithParametersAndAutoCompletion(modelType, ExportPredefinedScheme().Export(Config.EntityType));
        }

        private PredefinedScheme ExportPredefinedScheme()
        {
            return PropertyFilterMapping.Export(Config.FilterFromContainerMapping, Config.ToolBar.FilterContent);
        }
        private void Toolbar_FilterUpdated(object sender, EventArgs e)
        {
            UpdateFilterScheme();
        }
        private void UpdateFilterScheme()
        {

        }

    }
}
