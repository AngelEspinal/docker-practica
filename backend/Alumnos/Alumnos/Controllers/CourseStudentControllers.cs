using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Alumnos.BusinessService;
using Alumnos.models;

namespace Alumnos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CourseStudentControllers: ControllerBase
    {
        public CourseStudentBusinessService _CourseStudentBusinessService;
         
        public CourseStudentControllers(CourseStudentBusinessService courseStudentBusinessService){
            _CourseStudentBusinessService = courseStudentBusinessService;
        }
        [HttpGet("[action]")]
        public List<CourseStudent> GetAllCourseStudent(){
            return _CourseStudentBusinessService.GetAllCourseStudent();
        }

        
    }
}