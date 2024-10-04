using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public class AutoMapperConfiguration:Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Device, GetDeviceDTO>().ForMember(e => e.CategoryName, opt => opt.MapFrom(e => e.Category.Name));
        }
    }
}
