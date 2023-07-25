using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialization(this IApplicationBuilder app){
            using(var serviceScope = app.ApplicationServices.CreateScope()){
                var serviceDb = serviceScope.ServiceProvider.GetService<Context>();
                serviceDb.Database.Migrate();
            }
        }
    }
}