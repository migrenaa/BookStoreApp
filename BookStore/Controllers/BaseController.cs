using AutoMapper;
using BookStore.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;



namespace BookStore.Controllers
{
    public abstract class BaseController : ApiController
    {
        
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}
