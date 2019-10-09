using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jiesen.WebApi.Models.Dto
{
    public class EntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}