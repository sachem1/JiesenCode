using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jiesen.WebApi.Models
{
    public class BaseEntity:Entity<long>
    {       
    }

    public class Entity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}