using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.BusinessService;
using Alumnos.BusinessService.Interface;
using Alumnos.DataService;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alumnos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentControllers : ControllerBase
    {
        private IStudentBusinessService _IStudentBusinessService;


        public StudentControllers(IStudentBusinessService IstudentBusinessService)
        {
            _IStudentBusinessService = IstudentBusinessService;
        }
        //---------------------htttp get--------------------------------
        [HttpGet("/api/GetAllStudents")]
        public IEnumerable<Student> GetAllStudent(){
            return _IStudentBusinessService.GetAllStudent();
        }
        [HttpGet("/api/GetStudentById/{Id}")]
        public Student  GetStudentById(string Id)
        {
            return _IStudentBusinessService.GetStudentById(Id);
        }
        //---------------------htttp post--------------------------------
        [HttpPost("/api/SaveStudent")]
        public Student AddStudent(Student student)
        {
            return _IStudentBusinessService.AddStudent(student);
        }

        
    }
}