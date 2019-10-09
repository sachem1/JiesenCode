using Epass.Vue.WebApi.Controllers;
using Epass.Vue.WebApi.Models;
using Jiesen.WebApi.Models.Dto;
using Jiesen.WebApi.Models.Entitys;
using Jiesen.WebApi.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jiesen.WebApi.Controllers
{
    public class ProductController : Base1Controller<Product,ProductDto,ProductSearch>
    {
        public ReturnResult<ProductDto> Create(ProductDto dto)
        {


            return new ReturnResult<ProductDto>();
        }


    }
}
