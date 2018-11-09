using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomato.Mvvm;
using IQueryProvider = WPEDMAS.Common.QueryClient.Providers.IQueryProvider;
using WPEDMAS.QueryClient.Config;
using Catel.Fody;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

namespace WPEDMAS.QueryClient.ViewModels
{
    public abstract class QueryViewModelBase<TConfig> : BindableBase, IViewAware where TConfig : QueryConfigBase
    {
        public TConfig Config { get; }
        private IQueryProvider _query;
        public IQueryProvider Query
        {
            get
            {
                if (_query == null)
                {
                    BindData();
                }
                return _query;
            }
            set
            {
                SetProperty(ref _query,value);
            }
        }
        public DataTemplate StatusPanelTemplate => Config.StatusPanelTemplate;
        public DataTemplate CommandButtonsPanelTemplate => Config.CommandButtonsPanelTemplate;
        private bool _needNotifyQueryResultListViewStyle = false;
        public Style QueryResultListViewStyle
        {
            get
            {
                if (_query == null)
                    _needNotifyQueryResultListViewStyle = true;
                else
                    return BuildQueryResultListViewStyle();
                return null;
            }
        }

        public event EventHandler<ViewAttachedEventArgs> ViewAttached = delegate (object param0, ViewAttachedEventArgs param1)
        { };

        public QueryViewModelBase([NotNull]TConfig config)
        {
            Config = config;
        }
        
        private void BindData()
        {
            var query = BuildQueryModel();
            query.QueryResultDecorator = Config.QueryResultDecorator;
            Query = query;
            if (_needNotifyQueryResultListViewStyle)
            {
                _needNotifyQueryResultListViewStyle = false;
                OnPropertyChanged(nameof(QueryResultListViewStyle));
            }
        }
        protected abstract IQueryProvider BuildQueryModel();

        protected virtual FlowDocument CreateContentPageTemplate()
        {
            return Config.ContentPageTemplate;
        }
        protected void RefreshQueryResultListViewStyle()
        {
            OnPropertyChanged(nameof(QueryResultListViewStyle));
        }

        #region 构建查询结果listview样式
        private Style BuildQueryResultListViewStyle()
        {
            return null;
        }
        #endregion

        private static GridViewColumnWidthConverter columnWidthConverter = new GridViewColumnWidthConverter();
        #region 生成链接查询列
        /// <summary>
        /// 将普通的GridViewColumn转化为LinkGridViewColumn并返回
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected virtual GridViewColumn ReCreateGridViewColumn(GridViewColumn column)
        {
            return null;
        }
        #endregion

        private readonly IDictionary<object, object> views = new Dictionary<object, object>();
        protected IDictionary<object, object> Views
        {
            get
            {
                return this.views;
            }
        }

        void IViewAware.AttachView(object view, object context = null)
        {
            this.Views[context ?? ViewAware.DefaultContext] = view;
            object firstNonGeneratedView = PlatformProvider.Current.GetFirstNonGeneratedView(view);
            PlatformProvider.Current.ExecuteOnFirstLoad(firstNonGeneratedView,new Action<object>(this.OnViewLoaded));
            this.OnViewAttached(firstNonGeneratedView,context);
            this.ViewAttached(this, new ViewAttachedEventArgs {
                View= firstNonGeneratedView,
                Context =context
            });
            IActivate activate = this as IActivate;
            if (activate == null || activate.IsActive)
            {
                PlatformProvider.Current.ExecuteOnLayoutUpdated(firstNonGeneratedView,new Action<object>(this.OnViewReady));
                return;
            }
            AttachViewReadyOnActivated(activate,firstNonGeneratedView);
        }
        private static void AttachViewReadyOnActivated(IActivate activate, object nonGeneratedView)
        {
            WeakReference viewReference = new WeakReference(nonGeneratedView);
            EventHandler<ActivationEventArgs> handler = null;
            handler = delegate (object s, ActivationEventArgs e)
            {
                ((IActivate)s).Activated -= handler;
                object target = viewReference.Target;
                if (target != null)
                {
                    PlatformProvider.Current.ExecuteOnLayoutUpdated(target, o => (s as QueryViewModelBase<TConfig>).OnViewReady(o));
                }
            };
            activate.Activated += handler;
        }
        /// <summary>
        /// Called when a view is attached.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="context">The context in which the view appears.</param>
        protected virtual void OnViewAttached(object view, object sender)
        {
            var element = view as FrameworkElement;
            if (element!=null)
            {
                element.Unloaded += QueryViewModelBase_Unloaded;
            }
        }
        /// <summary>
        /// Called the first time the page's LayoutUpdated event fires after it is navigated to.
        /// </summary>
        /// <param name="view"></param>
        protected virtual void OnViewReady(object view)
        {
        }

        private void QueryViewModelBase_Unloaded(object sender, RoutedEventArgs e)
        {
            views.Where(o => o.Value == sender).ToList().Sink(o => views[o.Key] = null);
        }

        /// <summary>
        /// Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name="view"></param>
        protected virtual void OnViewLoaded(object view)
        {

        }
        public object GetView(object context = null)
        {
            object result;
            this.Views.TryGetValue(context ?? ViewAware.DefaultContext, out result);
            return result;
        }
        public virtual void TryClose(bool? dialogResult = null)
        {
            PlatformProvider.Current.GetViewCloseAction(this, Views.Values, dialogResult).OnUIThread();
        }
    }
}
