using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lib.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> ToErrorList(this ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
        }
    }
}