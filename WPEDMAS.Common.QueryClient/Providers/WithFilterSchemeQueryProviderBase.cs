using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Fody;
using Orc.FilterBuilder.Models;

namespace WPEDMAS.Common.QueryClient.Providers
{
    public abstract class WithFilterSchemeQueryProviderBase<T> : QueryProviderBase<T>, IWithFilterSchemeQueryProvider
    {
        private FilterScheme _currentScheme;
        public FilterScheme CurrentScheme
        {
            get
            {
                return _currentScheme;
            }
        }
        public WithFilterSchemeQueryProviderBase(int pageIndex, int pageSize)
            :this(new FilterScheme(typeof(T),"Default"),pageIndex,pageSize)
        {

        }
        public WithFilterSchemeQueryProviderBase([NotNull]FilterScheme filterScheme,int pageIndex,int pageSize)
            :base(pageIndex,pageSize)
        {
            _currentScheme = filterScheme;
            _currentScheme.Updated += _currentScheme_Updated;
        }

        private void _currentScheme_Updated(object sender, EventArgs e)
        {
            OnSchemeUpdated();
        }
        protected virtual void OnSchemeUpdated()
        {
            if (PageIndex == 0)
                RefreshIfNotSuspended();
            else
                PageIndex = 0;
        }        
    }
}
