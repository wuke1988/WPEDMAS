using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomato.Mvvm;
using WPEDMAS.Client.Core.ViewModels;

namespace WPEDMAS.QueryClient.ViewModels
{
    public class MultiQueryContainerViewModel:BindableBase
    {
        public object QueryKinds { get; private set; }
        public string Name { get; private set; }

        private ObservableCollection<GroupedNavigationItemViewModel> _openQueries = new ObservableCollection<GroupedNavigationItemViewModel>();
        /// <summary>
        /// 打开的查询选项卡
        /// </summary>
        public ObservableCollection<GroupedNavigationItemViewModel> OpenQueries
        {
            get { return _openQueries; }
        }

        private GroupedNavigationItemViewModel _selectedQuery;
        /// <summary>
        /// 当前选中的查询
        /// </summary>
        public GroupedNavigationItemViewModel SelectedQuery
        {
            get { return _selectedQuery; }
            set
            {
                if (SetProperty(ref _selectedQuery, value))
                {
                    OpenQuery(_selectedQuery);
                }
            }
        }
        public MultiQueryContainerViewModel(SecondLevelMenuViewModel model)
        {
            Name = model.Name;
            QueryKinds = model.Content;
        }

        
        /// <summary>
        /// 关闭当前查询界面
        /// </summary>
        /// <param name="item"></param>
        public void CloseQuery(GroupedNavigationItemViewModel item)
        {
            int index = OpenQueries.IndexOf(item);
            if (index > 0)
            {
                SelectedQuery = OpenQueries[index - 1];
                OpenQueries.Remove(item);
            }
            else
            {
                OpenQueries.Remove(item);
                SelectedQuery = OpenQueries.FirstOrDefault();
            }
            item.OnClose();
        }
        /// <summary>
        /// 打开新的查询界面
        /// </summary>
        /// <param name="item"></param>
        public void OpenQuery(GroupedNavigationItemViewModel item)
        {
            if (item != null && !_openQueries.Contains(item))
            {
                OpenQueries.Add(item);
            }
        }

    }
}
