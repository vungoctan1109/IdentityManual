using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityManual.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}