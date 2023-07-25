using System.IO;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IO;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Alumnos.DataService
{
    public class TeacherDataService
    {
        private Context _context;
         
        public TeacherDataService(Context context)
        {
            _context = context;

        }
        //---------------------htttp get--------------------------------
        
        public Teacher GetStudentByIdTeacher(string IdTeacher)
        {
            //Teacher teacher = _context.Tteacher.Include(t => t.student).FirstOrDefault(t => t.IdTeacher == IdTeacher);
            //Teacher teacher = _context.Tteacher.Include(t => t.student).FirstOrDefault(t => t.IdTeacher == IdTeacher);
            var teacher = _context.Tteacher.Find(IdTeacher);
            
            return teacher;
        }

        public List<Teacher> GetAllTeacher( )
        {
            return _context.Tteacher.ToList();
        }
        
        public Teacher AddTeacher(Teacher teacher)
        {
            _context.Tteacher.Add(teacher);
            _context.SaveChanges();
            return teacher;
        }
        

    }
}