using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Tomato.Mvvm;

namespace WPEDMAS.QueryClient.Config
{
    public enum ContentPageDisplayTarget
    {
        Inline,
        NewWindow
    }
    /// <summary>
    /// 查询配置基类
    /// </summary>
    public abstract class QueryConfigBase : BindableBase
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; set; }
        /// <summary>
        /// 查询结果列
        /// </summary>
        public Collection<GridViewColumn> Columns { get; set; } = new Collection<GridViewColumn>();

        /// <summary>
        /// 可勾选显示列
        /// </summary>
        private GridViewColumnCollection _displayColumns;

        public GridViewColumnCollection DisplayColumns
        {
            get { return _displayColumns; }
            set { SetProperty(ref _displayColumns, value); }
        }
        public Collection<GroupStyle> GroupStyle { get; set; } = new Collection<GroupStyle>();
        public FlowDocument ContentPageTemplate { get; set; }
        public DataTemplate StatusPanelTemplate { get; set; }
        public DataTemplate CommandButtonsPanelTemplate { get; set; }
        public CollectionViewSource QueryResultViewSource { get; set; } = new CollectionViewSource();
        public ContentPageDisplayTarget ContentPageDisplayTarget { get; set; } = ContentPageDisplayTarget.Inline;
        public Func<IEnumerable, IEnumerable> QueryResultDecorator { get; set; }

        public abstract object ViewModelActivator { get; }

        private Lazy<object> _viweModel;

        public object ViewModel => _viweModel.Value;

        public QueryConfigBase()
        {
            ResetViewModel();
        }
        public void ResetViewModel()
        {
            _viweModel = new Lazy<object>(()=>ViewModelActivator);
        }
    }
}