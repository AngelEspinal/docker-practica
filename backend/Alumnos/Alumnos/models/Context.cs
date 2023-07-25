using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.models
{
    public class Context:DbContext
    {
        
        public Context(DbContextOptions<Context>options):base (options){}
        public DbSet<Career> Tcareer { get; set; }
        public DbSet<CourseAssignment> TcourseAssignment { get; set; }
        public DbSet<Course> Tcourse { get; set; }
        public DbSet<CourseStudent> TcourseStudents { get; set; }
        public DbSet<InfoLimpieza> TinfoLimpieza { get; set; }
        public DbSet<ProfessionalSchool> TprofessionalSchool { get; set; }
        public DbSet<Schedule> Tschedule { get; set; }
        public DbSet<Semester> Tsemester { get; set; }
        public DbSet<Student> Tstudent { get; set; }
        public DbSet<Teacher> Tteacher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.IdStudent,cs.IdCourse, cs.IdSemester,cs.IdTeacher });

            modelBuilder.Entity<Semester>()
                .HasKey(cs => new { cs.IdSemester,cs.IdCourse});

            //---------------one-to-one----Semester-to-InfoLimpieza
            /*modelBuilder.Entity<CourseStudent>()
                .HasOne(e => e.InfoLimpieza)
                .WithOne(e => e.CourseStudent)
                .HasForeignKey<InfoLimpieza>(e => new { e.IdStudent,e.IdCourse,e.IdTeacher, e.IdSemester})
                .IsRequired();*/
            /*
            //---------------one-to-many----student-to-courseStudent
             modelBuilder.Entity<Student>()
            .HasMany(e => e.CourseStudent)
            .WithOne(e => e.Student)
            .IsRequired();
            //===================TSemesters====================

            //---------------one-to-many----Courses-to-Tsemester

            modelBuilder.Entity<Semester>()
            .HasOne(e => e.Course)
            .WithMany(e => e.Semester)
            .IsRequired();
            */
            

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.CourseAssignment)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(c => c.Schedule)
                .WithOne(sq => sq.CourseAssignments)
                .OnDelete(DeleteBehavior.Restrict);

        }
        
        
    }
}