using Orc.FilterBuilder.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDMAS.Client.Core
{
    public interface IQueryHandler
    {
        Task<IEnumerable> QueryPage(FilterScheme filterScheme,int pageIndex,int pageSize);
        Task<IEnumerable> QueryAll(FilterScheme filter);
        Task<int> GetItemsCount(FilterScheme filterScheme);
    }
    public interface IQueryHandler<T>:IQueryHandler
    {
        Task<IEnumerable<T>> QueryPage(FilterScheme filterScheme, int pageIndex, int pageSize);
        Task<IEnumerable<T>> QueryAll(FilterScheme filter);

    }
}
