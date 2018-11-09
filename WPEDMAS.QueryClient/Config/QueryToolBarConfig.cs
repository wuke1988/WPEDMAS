using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WPEDMAS.Client.Core.ViewModels;

namespace WPEDMAS.QueryClient.Config
{
    public interface IFilterUpdateSource
    {
        event EventHandler FilterUpdated;
        void Refresh();
    }
    public interface IQueryToolBarConfig:IViewModelProvider
    {
        IFilterUpdateSource FilterContent { get; set; }
    }
    public abstract class QueryToolBarConfigBase : IQueryToolBarConfig
    {
        private Lazy<object> _viewModel;      
        public object ViewModel => _viewModel.Value;
        public virtual object ViewModelActivator => new ViewModels.QueryToolBarViewModel(this);
        public abstract IFilterUpdateSource FilterContent { get; set; }
        public QueryToolBarConfigBase()
        {
            ResetViewModel();
        }
        private void ResetViewModel()
        {
            _viewModel = new Lazy<object>(()=>ViewModelActivator);
        }       
    }

    [ContentProperty(nameof(FilterContent))]
    public class QueryToolBarConfig: QueryToolBarConfigBase
    {
        public override IFilterUpdateSource FilterContent { get; set; }
        public QueryToolBarConfig()
        {
        }
    }
}
