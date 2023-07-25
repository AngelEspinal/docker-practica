using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.DataService;
using Alumnos.models;

namespace Alumnos.BusinessService
{
    public class CourseAssignmentBusinessService
    {
        public CourseAssignmentDataService _CourseAssignmentDataService;
         
        public CourseAssignmentBusinessService(CourseAssignmentDataService courseAssignmentDataService){
            _CourseAssignmentDataService = courseAssignmentDataService;
        }
        //---------------------get---------------------
        public List<CourseAssignment> GetAllCourseCourseAssignments(){
            return _CourseAssignmentDataService.GetAllCourseCourseAssignments();
        }
    }
}