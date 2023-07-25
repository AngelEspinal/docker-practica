using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.BusinessService.Interface;
using Alumnos.DataService;
using Alumnos.models;

namespace Alumnos.BusinessService
{
    public class StudentBusinessService: IStudentBusinessService
    {
        private StudentDataService _StudentDataService;

        public StudentBusinessService(StudentDataService studentDataService)
        {
            _StudentDataService = studentDataService;
        }
        public Student GetStudentById(string Id)
        {  
            return _StudentDataService.GetStudentById(Id);
        }
        public IEnumerable<Student> GetAllStudent(){
            return _StudentDataService.GetAllStudent();
        }
        //---------------------htttp post--------------------------------
        public Student AddStudent(Student student)
        {
            return _StudentDataService.AddStudent(student);
        }
    }
}