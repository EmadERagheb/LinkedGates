
//using API.Responses;
//using Data.Contracts;
//using Data.Repositories;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Extensions
//{
//    public static class ApplicationServicesExtension
//    {
//        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
//        {
//            #region CORS Policy
//            services.AddCors(setupAction => setupAction.AddDefaultPolicy(
//                policy => policy.AllowAnyMethod().AllowAnyHeader().WithOrigins(config["ClientOrgins"]!.Split(","))));
//            #endregion

//            #region IOC
//            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
//            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
//            services.AddAutoMapper(typeof(AutoMapperConfiguration));
//            #endregion

//            #region Flat  Validation Error Response
//            services.Configure<ApiBehaviorOptions>(options =>
//            options.InvalidModelStateResponseFactory = context =>
//            {
//                var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage));
//                return new BadRequestObjectResult(new APIValidationErrorResponse(errors));
//            });
//            #endregion

//            return services;

//        }
//    }
//}
