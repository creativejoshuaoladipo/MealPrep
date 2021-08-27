using AutoMapper;
using MealPrepApp.Data.Models.Domain;
using MealPrepApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPrepApp.Mapper
{
    public class MapperConfig
    {

        public static MapperConfiguration RegristerMapper()
        {

            var mapConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
            });

            return mapConfig;
        }
    }
}
