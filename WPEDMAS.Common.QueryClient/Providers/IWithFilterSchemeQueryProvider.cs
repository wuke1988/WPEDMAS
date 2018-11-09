using Orc.FilterBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDMAS.Common.QueryClient.Providers
{
    public interface IWithFilterSchemeQueryProvider:IQueryProvider
    {
        FilterScheme CurrentScheme { get;  }
    }
}
