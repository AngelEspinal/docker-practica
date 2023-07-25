using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.BusinessService;
using Alumnos.BusinessService.Interface;
using Alumnos.DataService;

namespace Alumnos.configuration
{
    public static class ServiceConfiguration
    {
        public static void AddBaseServices(this IServiceCollection Services){

        //services AddTranscient 
        Services.AddTransient<IStudentBusinessService,StudentBusinessService>();
        //bussines Service
        Services.AddTransient<TeacherBusinessService>();
        Services.AddTransient<CourseStudentBusinessService>();
        Services.AddTransient<InfoLimpiezaBusinessService>();
        Services.AddTransient<CourseAssignmentBusinessService>();
        Services.AddTransient<ScheduleBusinessService>();
        // data service
        Services.AddTransient<TeacherDataService>();

        Services.AddTransient<CourseStudentDataService>();

        Services.AddTransient<InfoLimpiezaDataService>();

        Services.AddTransient<CourseAssignmentDataService>();
        Services.AddTransient<StudentDataService>();

        Services.AddTransient<ScheduleDataService>();

        }
    }
}