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
            #region Device DTOs
            CreateMap<Device, GetDeviceDTO>().ForMember(e => e.CategoryName, opt => opt.MapFrom(e => e.Category.Name));


            CreateMap<Device, GetDetailedDeviceDTO>().ForMember(e => e.CategoryName, opt => opt.MapFrom(e => e.Category.Name))
                .ForMember(e => e.Properties, opt => opt.MapFrom(e => e.DeviceProperties));
            #endregion



            #region DeviceProperty DTOs
            CreateMap<DeviceProperty, GetDevicePropertiesDTO>().ForMember(e => e.PropertyName, opt => opt.MapFrom(e => e.Property.Description));
            #endregion

            #region Category DTO

            CreateMap<Category, GetCategoryDTO>();
            CreateMap<Category, GetCategoryForSelectionDTO>();
            #endregion
            #region Property DTO
            CreateMap<Property, GetPropertyDTO>();
            #endregion
        }
    }
}
