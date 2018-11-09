using Catel.Fody;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.QueryClient.ViewModels;

namespace WPEDMAS.QueryClient.Config
{
    public class GeneralQueryConfig : QueryConfigBase
    {
        public bool HasToolBar => ToolBar == null ? false : true;
        public QueryToolBarConfig ToolBar { get; set; }
        public Collection<PropertyFilterMapping> FilterFromContainerMapping { get; set; } = new Collection<PropertyFilterMapping>();
        public override object ViewModelActivator => ToolBar == null ? null : new GeneralQueryViewModel(this);
        public GeneralQueryConfig()
        {

        }
    }
}
