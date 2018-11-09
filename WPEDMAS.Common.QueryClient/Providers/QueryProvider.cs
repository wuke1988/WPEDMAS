using Catel.Fody;
using Orc.FilterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPEDMAS.Client.Core;

namespace WPEDMAS.Common.QueryClient.Providers
{
    public class QueryProvider<T>:WithFilterSchemeQueryProviderBase<T>
    {
        private readonly IQueryHandler<T> _queryHandler;

        public QueryProvider([NotNull]IQueryHandler<T> queryHandler)
            : base(0, 20)
        {
            _queryHandler = queryHandler;
        }
        public QueryProvider(FilterScheme scheme, [NotNull]IQueryHandler<T> queryHandler)
            :base(scheme,0,20)
        {
            _queryHandler = queryHandler;
        }
        protected override Task<IEnumerable<T>> OnQueryCurrentPage()
        {
            return _queryHandler.QueryPage(CurrentScheme, PageIndex, PageSize);
        }
        protected override Task<IEnumerable<T>> OnQueryAllData()
        {
            return _queryHandler.QueryAll(CurrentScheme);
        }
        protected override Task<int> OnGetItemsCount()
        {
            return _queryHandler.GetItemsCount(CurrentScheme);
        }
    }
}
