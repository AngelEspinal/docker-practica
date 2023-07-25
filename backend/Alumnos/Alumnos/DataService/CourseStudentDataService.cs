using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;

namespace Alumnos.DataService
{
    public class CourseStudentDataService
    {
        private Context _context;
         
        public CourseStudentDataService(Context context)
        {
            _context = context;

        }
        public List<CourseStudent> GetAllCourseStudent(){
            return _context.TcourseStudents.ToList();
        }
    }
}