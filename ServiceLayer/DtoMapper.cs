using AutoMapper;
using CoreLayer.DTOs;
using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    internal  class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, ProductDto>();
            CreateMap<UserAppDto, UserApp>();
        }
    }
}
