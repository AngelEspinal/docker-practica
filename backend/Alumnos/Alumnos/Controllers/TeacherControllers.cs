using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.BusinessService;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace Alumnos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TeacherControllers : ControllerBase
    {
        private TeacherBusinessService _TeacherBusinessService;


        public TeacherControllers(TeacherBusinessService teacherBusinessService)
        {
            _TeacherBusinessService = teacherBusinessService;
        }
        //---------------------htttp get--------------------------------
        [HttpGet("[action]")]
        public Teacher GetStudentByIdTeacher(string IdTeacher)
        {

            return _TeacherBusinessService.GetStudentByIdTeacher(IdTeacher);
        }
        [HttpGet("[action]")]
        public List<Teacher> GetAllTeacher( )
        {
            
            return _TeacherBusinessService.GetAllTeacher( );
        }
        [HttpGet("export")]  
        public IActionResult ExportToExcel(string IdTeacher)
        {
            var fileBytes = new MemoryStream(_TeacherBusinessService.ExportUsersToExcel(IdTeacher));
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UserList.xlsx"); // Devolver el archivo generado como una respuesta de archivo del controlador
        }


        //----------------------post---------
        [HttpPost("[action]")]
        public Teacher AddTeacher(Teacher teacher)
        {
            return _TeacherBusinessService.AddTeacher(teacher);
        }






    }
}