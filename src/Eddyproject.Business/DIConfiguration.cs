using Eddyproject.Business.Services;
using Eddyproject.Business.Validation;
using Eddyproject.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddyproject.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();

        services.AddScoped<AddressCreateValidator>();
        services.AddScoped<AddressUpdateValidator>();
        services.AddScoped<BudgetCreateValidator>();
        services.AddScoped<BudgetUpdateValidator>();
        services.AddScoped<CourseCreateValidator>();
        services.AddScoped<CourseUpdateValidator>();
        services.AddScoped<StudentCreateValidator>();
        services.AddScoped<StudentUpdateValidator>();
    }
}
