using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDMAS.Data
{
    public static class ConvertEx
    {
        public static object SafeCastValue(object value,Type desiredType)
        {
            Exception exception;
            return SafeCastValue(value,desiredType,out exception);
        }
        public static object SafeCastValue(object value, Type desiredType, out Exception error)
        {
            error = new Exception();
            return null;
        }
    }
}
