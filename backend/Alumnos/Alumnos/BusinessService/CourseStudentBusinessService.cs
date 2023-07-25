using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.DataService;
using Alumnos.models;

namespace Alumnos.BusinessService
{
    public class CourseStudentBusinessService
    {
        private CourseStudentDataService _CourseStudentDataService;

        public CourseStudentBusinessService(CourseStudentDataService CourseStudentDataService)
        {
            _CourseStudentDataService = CourseStudentDataService;
        }
        public List<CourseStudent> GetAllCourseStudent(){
            return _CourseStudentDataService.GetAllCourseStudent();
        }
        
    }
}