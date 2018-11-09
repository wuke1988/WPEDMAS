using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Tomato.Mvvm;

namespace WPEDMAS.Common.QueryClient.Providers
{
    public abstract class QueryProviderBase<T> : BindableBase, IQueryProvider
    {
        private volatile int _isRefreshing = 0;
        public bool IsRefreshing
        {
            get { return _isRefreshing != 0; }
            set
            {
                _isRefreshing = value ? 1 : 0;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                SetProperty(ref _pageIndex, value);
                OnPageIndexChanged();
            }
        }
        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                SetProperty(ref _pageSize, value);
                OnPageSizeChanged();
            }
        }
        private int _itemsCount;
        public int ItemsCount
        {
            get { return _itemsCount; }
            set
            {
                SetProperty(ref _itemsCount, value);
                OnPropertyChanged(nameof(ItemsCount));
            }
        }

        private ObservableCollection<T> _queryResult;
        public IEnumerable QueryResult
        {
            get
            {
                if (_queryResult == null)
                    Refresh();
                return _queryResult;
            }
        }

        private bool _hasSupendedRefresh = false;
        private bool _refreshSuspended = true;
        protected bool HasQueryResult => _queryResult != null;
        public Func<IEnumerable, IEnumerable> QueryResultDecorator { get; set; }
        public GridViewColumnCollection ColumnFormatter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FlowDocument DocumentFormatter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CanExport => throw new NotImplementedException();

        public event EventHandler ResultUpdated;

        public QueryProviderBase(int pageIndex,int pageSize)
        {
            _pageIndex = pageIndex;
            _pageSize = pageSize;
            _refreshSuspended = false;
        }
        public void Delete(IEnumerable enumerable)
        {
            throw new NotImplementedException();
        }

        public void Export()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            OnRefresh();
        }
        protected virtual void OnRefresh()
        {
            RefreshCurrentPage();
        }
        public void RefreshCurrentPageIfNotSuspended()
        {
            if (!_refreshSuspended)
                RefreshCurrentPage();
            else
                _hasSupendedRefresh = true;
        }
        public void RefreshIfNotSuspended()
        {
            if (!_refreshSuspended)
                OnRefresh();
            else
                _hasSupendedRefresh = true;
        }
        public async void RefreshCurrentPage()
        {
            if (Interlocked.CompareExchange(ref _isRefreshing, 1, 0) == 0)
            {
                try
                {
                    OnPropertyChanged(nameof(IsRefreshing));
                    if (_queryResult == null)
                        _queryResult = new ObservableCollection<T>();
                    _queryResult.Clear();
                    (DecorateQueryResult(await OnQueryCurrentPage())).Sink(_queryResult.Add);
                    ItemsCount = await OnGetItemsCount();
                    OnPropertyChanged(nameof(CanExport));
                    ResultUpdated?.Invoke(this,EventArgs.Empty);
                }
                finally
                {
                    IsRefreshing = false;
                }
            }   
        }
        protected abstract Task<IEnumerable<T>> OnQueryCurrentPage();

        protected abstract Task<IEnumerable<T>> OnQueryAllData();

        protected abstract Task<int> OnGetItemsCount();

        public void ResumeRefresh()
        {
            _refreshSuspended = false;
            if (_hasSupendedRefresh)
            {
                _hasSupendedRefresh = false;
                OnRefresh();
            }
        }

        public void SuspendRefresh()
        {
            _refreshSuspended = true;
        }
        private IEnumerable<T> DecorateQueryResult(IEnumerable<T> result)
        {
            return QueryResultDecorator == null ? result : QueryResultDecorator(result).Cast<T>();
        }
        private void OnPageSizeChanged()
        {
            RefreshCurrentPageIfNotSuspended();
        }
        private void OnPageIndexChanged()
        {
            RefreshCurrentPageIfNotSuspended();
        }

    }
}
