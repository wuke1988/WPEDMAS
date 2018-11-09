using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.Client.Core.ViewModels;
using WPEDMAS.QueryClient.Config;

namespace WPEDMAS.QueryClient.Navigation
{
    public class QueryNavigationItem: GroupedNavigationItemViewModel
    {
        public QueryConfigBase Config { get; set; }
        public override void OnClose()
        {
            Config?.ResetViewModel();
        }
    }
}
