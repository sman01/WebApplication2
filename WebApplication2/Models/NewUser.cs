using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NewUser
    {
        public string uname { get; set; }
        public string pass { get; set; }
        public int access { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
   
    }
    public enum Access
    {
        admin,
        superadmin,
        user
    }
}