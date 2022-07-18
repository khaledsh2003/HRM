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
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Mvc;


    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeEnumAttribute : AuthorizeAttribute
    {
        public AuthorizeEnumAttribute(UserType usertype)
        {
            Roles = UserType.manager.ToString();
        }
    }
}
           