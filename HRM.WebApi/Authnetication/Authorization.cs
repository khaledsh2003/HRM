using HRM.DAL.EF;
using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    namespace MvcApplication.HowTo.Attributes
    {
        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
        public class AuthorizeEnumAttribute : AuthorizeAttribute
        {
            public AuthorizeEnumAttribute(params object[] roles)
            {
                if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                    throw new ArgumentException("roles");

                this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
            }
        }
    }
}
