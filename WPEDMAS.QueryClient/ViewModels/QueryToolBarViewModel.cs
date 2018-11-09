using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomato.Mvvm;
using WPEDMAS.QueryClient.Config;

namespace WPEDMAS.QueryClient.ViewModels
{
    public class QueryToolBarViewModel : BindableBase
    {
        private readonly IQueryToolBarConfig _config;
        public IFilterUpdateSource FileterContent => _config.FilterContent;
        public event EventHandler FilterUpdated;

        public QueryToolBarViewModel(IQueryToolBarConfig config)
        {
            _config = config;
            FileterContent.FilterUpdated += FileterContent_FilterUpdated;
        }

        private void FileterContent_FilterUpdated(object sender, EventArgs e)
        {
            FilterUpdated?.Invoke(this,EventArgs.Empty);
        }
    }
}
