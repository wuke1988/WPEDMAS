using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPEDMAS.Common.QueryClient.Providers
{
    public interface IQueryProvider: INotifyPropertyChanged
    {
        /// <summary>
        /// 获取或设置当前页码
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 获取或设置页行数
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 获取页的数量
        /// </summary>
        int ItemsCount { get; set; }
        /// <summary>
        /// 查询结果
        /// </summary>
        IEnumerable QueryResult { get; }
        /// <summary>
        /// 查询结果装饰（过滤）器
        /// </summary>
        Func<IEnumerable, IEnumerable> QueryResultDecorator { get; set; }
        /// <summary>
        /// 导出excel数据列？
        /// </summary>
        GridViewColumnCollection ColumnFormatter { get; set; }
        /// <summary>
        /// 导出word的内容页？
        /// </summary>
        FlowDocument DocumentFormatter { get; set; }
        /// <summary>
        /// 是否可以导出
        /// </summary>
        bool CanExport { get; }
        /// <summary>
        /// 获取是否正在刷新
        /// </summary>
        bool IsRefreshing { get; }

        event EventHandler ResultUpdated;
        /// <summary>
        /// 挂起刷新
        /// </summary>
        void SuspendRefresh();
        /// <summary>
        /// 继续刷新
        /// </summary>
        void ResumeRefresh();
        /// <summary>
        /// 刷新当前页
        /// </summary>
        void RefreshCurrentPage();
        void Refresh();
        void Export();
        void Delete(IEnumerable enumerable);

    }
}
