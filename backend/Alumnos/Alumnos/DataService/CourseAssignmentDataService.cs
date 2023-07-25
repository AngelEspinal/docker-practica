using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;

namespace Alumnos.DataService
{
    public class CourseAssignmentDataService
    {
        public Context _Context;

        public CourseAssignmentDataService(Context context){
            _Context = context;
        }
        public List<CourseAssignment> GetAllCourseCourseAssignments(){
            return _Context.TcourseAssignment.ToList();
        }

    }
}