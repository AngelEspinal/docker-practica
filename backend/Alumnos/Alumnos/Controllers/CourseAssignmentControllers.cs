using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.BusinessService;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;

namespace Alumnos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CourseAssignmentControllers:ControllerBase
    {
        public CourseAssignmentBusinessService _CourseAssignmentBusinessService;
         
        public CourseAssignmentControllers(CourseAssignmentBusinessService courseAssignmentBusinessService){
            _CourseAssignmentBusinessService = courseAssignmentBusinessService;
        }
        //---------------------------get-------------
        [HttpGet("GetAllStudents")]
        public List<CourseAssignment> GetAllCourseCourseAssignments(){
            return _CourseAssignmentBusinessService.GetAllCourseCourseAssignments();
        }
    }
}