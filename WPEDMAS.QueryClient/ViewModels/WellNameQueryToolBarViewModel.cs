using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.QueryClient.Config;

namespace WPEDMAS.QueryClient.ViewModels
{
    public class WellNameQueryToolBarViewModel : IFilterUpdateSource
    {
        public string WellName { get; set; }
        public event EventHandler FilterUpdated;

        public void Refresh()
        {
            FilterUpdated?.Invoke(this,EventArgs.Empty);
        }
    }
}
