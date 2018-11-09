using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDMAS.Client.Core.ViewModels
{
    /// <summary>
    /// 分组导航项视图模型
    /// </summary>
    public class GroupedNavigationItemViewModel
    {
        public string Title { get; set; }
        public string Group { get; set; }
        public string Group2 { get; set; }
        public string Group3 { get; set; }
        public virtual void OnClose()
        {

        }
    }
}
