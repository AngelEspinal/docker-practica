using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Alumnos.DataService
{
    public class StudentDataService
    {
        private Context _context;
        public StudentDataService(Context context)
        {
            _context = context;
        }
        public Student GetStudentById(string Id)
        {  
            var student = _context.Tstudent.Find(Id);
            return student;
        }

        public IEnumerable<Student> GetAllStudent(){

            
            //return _context.Tstudent.Include(b => b.CourseStudent).ToList();
            //var aa = _context.Tstudent.ToList();
            //var cur = aa[0].CourseStudent;
            //Console.WriteLine($"________________________{JsonConvert.SerializeObject(cur)}");
            _context.Set<CourseStudent>().Load();
            _context.Set<Student>().Load();
            _context.Set<Semester>().Load();
            //_context.Set<Course>().Load();
            //_context.Set<Career>().Load();
            //return _context.Tstudent.Where(e=>e.NameStudent.Contains("angel")==true).Include(e=>e.CourseStudent).ToList();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var a = _context.Tstudent.ToList();
            // Obtiene el tiempo transcurrido en milisegundos
            stopwatch.Stop();
            long tiempoTranscurrido1 = stopwatch.ElapsedMilliseconds;
            // Imprime el resultado
            Console.WriteLine($"====={a.Count()}=====>Tiempo  1 de ejecución: " + tiempoTranscurrido1 + " ms");
            return a;
        }
        //---------------------htttp post--------------------------------
        public Student AddStudent(Student student)
        {
            _context.Tstudent.Add(student);
            _context.SaveChanges();
            return student;
        }
    }
    
}