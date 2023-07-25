using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Alumnos.DataService;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Alumnos.BusinessService
{
    public class InfoLimpiezaBusinessService
    {
        public InfoLimpiezaDataService _InfoLimpiezaDataService;
        public CourseStudentDataService _CourseStudentDataService;
        public StudentDataService _StudentDataService;
        public CourseAssignmentDataService _CourseAssignmentDataService;
        public TeacherDataService _TeacherDataService;
        public ScheduleDataService _ScheduleDataService;


        public InfoLimpiezaBusinessService(InfoLimpiezaDataService infoLimpiezaDataService,
                                           CourseStudentDataService courseStudentDataService,
                                           StudentDataService studentDataService,
                                           CourseAssignmentDataService courseAssignmentDataService,
                                           TeacherDataService teacherDataService,
                                           ScheduleDataService scheduleDataService){
            _InfoLimpiezaDataService= infoLimpiezaDataService;
            _CourseStudentDataService = courseStudentDataService;
            _StudentDataService = studentDataService;
            _CourseAssignmentDataService = courseAssignmentDataService;
            _TeacherDataService = teacherDataService;
            _ScheduleDataService = scheduleDataService;
        }
        //-----------------HttpGet-------------------------
        public List<InfoLimpieza> GetAllInfoLimpiezaByIdSemestre(string IdSemester=null){
            return _InfoLimpiezaDataService.GetAllInfoLimpiezaByIdSemestre(IdSemester);
        }
        public DemoResponse<string>   GenerateInfoCleaning(string Day,string HoraInit,string HoraEnd,string semester = null)
        {   

            //----------------FILTRO A LOS ESTUDIANTES UNICOS CON CARGAS ------------------------
            Console.WriteLine(DateTime.Now.Year);
            InfoLimpieza info = new InfoLimpieza();
            List<CourseStudent> coursesStudents = _CourseStudentDataService.GetAllCourseStudent();
            IEnumerable<Student> students = _StudentDataService.GetAllStudent();

            if(string.IsNullOrEmpty(semester)){
                semester = coursesStudents.Max(cs => cs.IdSemester);
            }
            //limpiamnos la tabla antes de generar nuevamente
            _InfoLimpiezaDataService.DeleteInfoLimpiezaBySemestre(semester);

            var resultStudent = from coursestudent in coursesStudents
                        join student in students on coursestudent.IdStudent equals student.IdStudent
                        where coursestudent.IdSemester == semester
                        select new { coursestudent.IdStudent,student.NameStudent, };

            var resultStudent2 = resultStudent.GroupBy(x => x.IdStudent)
                                           .Select(g => g.First())
                                           .ToList();

            var random = new Random();
            // Mezclamos los elementos de la lista de forma aleatoria
            var shuffledStudents = resultStudent2.OrderBy(x => random.Next()).ToList();
            Console.WriteLine(shuffledStudents.LongCount());
            Console.WriteLine("========================================0");
            

            //----------------FILTRO A LOS DOCENTES unicos  CON CARGAS ------------------------
            List<CourseAssignment> CourseAssignments = _CourseAssignmentDataService.GetAllCourseCourseAssignments();
            List<Teacher> Teachers = _TeacherDataService.GetAllTeacher();
            List<Schedule> Schedules = _ScheduleDataService.GetSAllchedule();

            var resultTeachers = from CourseAssignment in CourseAssignments
                     join Teacher in Teachers on CourseAssignment.IdTeacher equals Teacher.IdTeacher
                     join Schedule in Schedules on CourseAssignment.IdSchedule equals Schedule.IdSchedule
                     where  Schedule.IdCourse.Contains("IF") && Schedule.IdCourse.Contains("IN")
                     select new { CourseAssignment.IdTeacher, Teacher.NameTeacher,Schedule.IdSemester,Schedule.IdCourse};

            Console.WriteLine(resultTeachers.LongCount());
            var resultTeachers2 = resultTeachers.GroupBy(x => x.IdTeacher)
                                           .Select(g => g.First())
                                           .ToList();
            Console.WriteLine(JsonSerializer.Serialize(resultTeachers2[0]));


            //----------------Distribucion alumnos --------

            long resto = shuffledStudents.LongCount()%resultTeachers2.LongCount();
            long median = (shuffledStudents.LongCount())/resultTeachers2.LongCount();
            
            Console.WriteLine($"median: {median} , resto: {resto}");
            if(9<=median && median <=20 ){
                int count = 0;
                int index = 0;
                resultTeachers2.ForEach(teacher =>
                    {
                        while (index < resultStudent2.LongCount())
                        {
                            Console.WriteLine($"_____________________________________valor y {index}");
                            if(count==median){
                                count = 0;
                                break;
                            }
                            InfoLimpieza newInfLim =new InfoLimpieza(){
                                IdStudent = resultStudent2[index].IdStudent,
                                IdCourse = teacher.IdCourse,
                                IdTeacher = teacher.IdTeacher,
                                IdSemester= teacher.IdSemester,
                                Day = Day,
                                Aula =null,
                                HoraInit = HoraInit,
                                HorEnd = HoraEnd
                            };
                            _InfoLimpiezaDataService.AddInfoLimpieza(newInfLim);
                            count ++;
                            index ++;
                            
                            
                        }
                        
                    } 
                
                );
                if(resto>0){
                    int count2 = 0;
                    int i = Convert.ToInt32(resultTeachers2.LongCount() - (resto + 1));
                    foreach (var teacher in resultTeachers2)
    
                    {
                        InfoLimpieza newInfLim = new InfoLimpieza()
                        {
                            IdStudent = resultStudent2[i].IdStudent,
                            IdCourse = teacher.IdCourse,
                            IdTeacher = teacher.IdTeacher,
                            IdSemester = teacher.IdSemester,
                            Day = Day,
                            Aula =null,
                            HoraInit = HoraInit,
                            HorEnd = HoraEnd
                        };

                        if (count2 == resto)
                        {
                            break;
                        }
                        _InfoLimpiezaDataService.AddInfoLimpieza(newInfLim);
                        count2++;
                        i++;
                    }


                }
            }
            // Crear un objeto de respuesta con un mensaje de éxito
            var response = DemoResponse<string>.GetResult(200, "La operación se realizó con éxito", "!!!! Info Limpieza Geberada ¡¡¡ ");

            return response;

        }
        #region ReporteExportToExel
        public byte[] ReporteExportToExel(string semester=null) 
        {
            //-----------------------OBTENEMOS  LA LISTA DE ELEMNTOS DE LA TABLA INFOLIMPIEZA-----------
            List<InfoLimpieza> InfoLimpiezas = GetAllInfoLimpiezaByIdSemestre(semester);
            //---------------------OBTEENMOSLISTA DE DOCENTES ----------
            List<CourseAssignment> CourseAssignments = _CourseAssignmentDataService.GetAllCourseCourseAssignments();
            List<Teacher> Teachers = _TeacherDataService.GetAllTeacher();
            List<Schedule> Schedules = _ScheduleDataService.GetSAllchedule();
            
            var resultTeachers = from CourseAssignment in CourseAssignments
                     join Teacher in Teachers on CourseAssignment.IdTeacher equals Teacher.IdTeacher
                     join Schedule in Schedules on CourseAssignment.IdSchedule equals Schedule.IdSchedule
                     where  Schedule.IdCourse.Contains("IF") && Schedule.IdCourse.Contains("IN")
                     select new { CourseAssignment.IdTeacher, Teacher.NameTeacher,Schedule.IdSemester,Schedule.IdCourse};
            var resultTeachers2 = resultTeachers.GroupBy(x => x.IdTeacher)
                                           .Select(g => g.First())
                                           .ToList();
            
            //----------------------OBTENEMOS LISTA DE ALUMNOS-------------
            List<CourseStudent> coursesStudents = _CourseStudentDataService.GetAllCourseStudent();
            IEnumerable<Student> students = _StudentDataService.GetAllStudent();
            if(string.IsNullOrEmpty(semester)){
                semester = coursesStudents.Max(cs => cs.IdSemester);
            }
            var resultStudent = from coursestudent in coursesStudents
                        join student in students on coursestudent.IdStudent equals student.IdStudent
                        where coursestudent.IdSemester == semester
                        select new { coursestudent.IdStudent,student.NameStudent, };

            var resultStudent2 = resultStudent.GroupBy(x => x.IdStudent)
                                           .Select(g => g.First())
                                           .ToList();
            //-----------------------OBTENEMOS  LA LISTA DE ELEMNTOS DE LA TABLA INFOLIMPIEZA-----------
            //---------------------------------------------------------------
            Dictionary<string, ExcelWorksheet> sheets = new Dictionary<string, ExcelWorksheet>() {
                { "Docentes Informatica", null },
                { "Alumnos Informatica", null },
                { "Reporte Docentes", null }
            };
            byte[] result = null;
            using (var package = new ExcelPackage()) {
                foreach (var sheet in sheets.Keys) {
                    var workSheet = package.Workbook.Worksheets.Add(sheet);
                    sheets[sheet] = workSheet;
                    if(sheet=="Docentes Informatica"){
                        // Establecer formato de letra y estilo de texto para celda H2:N2
                        workSheet.Cells["F2:P2"].Merge = true;
                        workSheet.Cells["F2:P2"].Value = "Docentes De Ingenieria Informatica y de Sistemas";
                        workSheet.Cells["F2:P2"].Style.Font.Size = 14;
                        workSheet.Cells["F2:P2"].Style.Font.Bold = true;
                        workSheet.Cells["F2:P2"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["F2:P2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["F2:P2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));
                        workSheet.Cells["F2:P2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells["F2:P2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        // Ancho de la fila 2
                        workSheet.Row(2).Height = 30;

                        //workSheet.Column("F2:P2").AutoFit(MinimumWidth: 45);

                        //Agregamos el encabezado de las columnas
                        workSheet.Cells["H4"].Value = "Codigo";
                        workSheet.Cells["H4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["H4"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["H4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(186, 240, 85));

                        workSheet.Cells["I4:N4"].Merge=true;
                        workSheet.Cells["I4:N4"].Value = "Nombre";
                        workSheet.Cells["I4:N4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["I4:N4"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["I4:N4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(186, 240, 85));
                        
                        //-------datos de los teaches
                        int row = 5;
                        resultTeachers2.ForEach(teacher => {
                            workSheet.Cells[$"H{row}"].Value = teacher.IdTeacher;
                            
                            workSheet.Cells[$"I{row}:N{row}"].Merge=true;
                            workSheet.Cells[$"I{row}:N{row}"].Value = teacher.NameTeacher;
                            row ++;
                        });

                    }
                    if(sheet=="Alumnos Informatica"){
                        // Establecer formato de letra y estilo de texto para celda H2:N2
                        workSheet.Cells["F2:P2"].Merge = true;
                        workSheet.Cells["F2:P2"].Value = "Alumnos de Informatica y de Sistemas";
                        workSheet.Cells["F2:P2"].Style.Font.Size = 14;
                        workSheet.Cells["F2:P2"].Style.Font.Bold = true;
                        workSheet.Cells["F2:P2"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["F2:P2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["F2:P2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));
                        workSheet.Cells["F2:P2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells["F2:P2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        // Ancho de la fila 2
                        workSheet.Row(2).Height = 30;

                        //workSheet.Column("F2:P2").AutoFit(MinimumWidth: 45);

                        //Agregamos el encabezado de las columnas
                        workSheet.Cells["H4"].Value = "Codigo";
                        workSheet.Cells["H4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["H4"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["H4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(186, 240, 85));

                        workSheet.Cells["I4:N4"].Merge=true;
                        workSheet.Cells["I4:N4"].Value = "Nombre";
                        workSheet.Cells["I4:N4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["I4:N4"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["I4:N4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(186, 240, 85));
                        
                        //-------datos de los teaches
                        int row = 5;
                        Console.WriteLine($" numero de studiantes {resultStudent2.LongCount()}");
                        resultStudent2.ForEach(student   => {
                            
                            Console.WriteLine($"{student.IdStudent}");

                            workSheet.Cells[$"H{row}"].Value = student.IdStudent;
                            
                            workSheet.Cells[$"I{row}:N{row}"].Merge=true;
                            workSheet.Cells[$"I{row}:N{row}"].Value = student.NameStudent;
                            row ++;
                        });
                    }
                    if(sheet=="Reporte Docentes"){
                        // Establecer formato de letra y estilo de texto para celda H2:N2
                        workSheet.Cells["F2:P2"].Merge = true;
                        workSheet.Cells["F2:P2"].Value = $"Reporte Info-Limpieza-{semester}";
                        workSheet.Cells["F2:P2"].Style.Font.Size = 16;
                        workSheet.Cells["F2:P2"].Style.Font.Bold = true;
                        workSheet.Cells["F2:P2"].Style.Font.Color.SetColor(Color.Black);
                        workSheet.Cells["F2:P2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        workSheet.Cells["F2:P2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));
                        workSheet.Cells["F2:P2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells["F2:P2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        // Ancho de la fila 2
                        workSheet.Row(2).Height = 50; 
                        int row1 = 4;
                        Console.WriteLine($" numero de studiantes {resultStudent2.LongCount()}");
                        //-------datos de los teaches
                        resultTeachers2.ForEach(teacher => {
                            //----------------emcabezado docente acargo
                            workSheet.Cells[$"G{row1}:O{row1}"].Merge = true;
                            workSheet.Cells[$"G{row1}:O{row1}"].Value = teacher.NameTeacher;
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.Font.Size = 14;
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.Font.Bold = true;
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.Font.Color.SetColor(Color.Black);
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(22, 172, 177));
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            workSheet.Cells[$"G{row1}:O{row1}"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            row1=row1+1;
                            //-----------------------------------------------------------
                            //Agregamos el encabezado de las columnas
                            workSheet.Cells[$"H{row1}"].Value = "Codigo";
                            workSheet.Cells[$"H{row1}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            workSheet.Cells[$"H{row1}"].Style.Font.Color.SetColor(Color.Black);
                            workSheet.Cells[$"H{row1}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(186, 240, 85 ));

                            workSheet.Cells[$"I{row1}:N{row1}"].Merge=true;
                            workSheet.Cells[$"I{row1}:N{row1}"].Value = "Nombre";
                            workSheet.Cells[$"I{row1}:N{row1}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            workSheet.Cells[$"I{row1}:N{row1}"].Style.Font.Color.SetColor(Color.Black);
                            workSheet.Cells[$"I{row1}:N{row1}"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(186, 240, 85 ));
                            row1=row1+1;
                            //-----------------------------------------------------------
                            List<InfoLimpieza> SubInfoLimpieza = InfoLimpiezas.Where(limpieza => limpieza.IdTeacher == teacher.IdTeacher).ToList();
                            SubInfoLimpieza.ForEach(w  => Console.WriteLine($"teacher:{w.IdTeacher},Idsturnet:{w.IdStudent}"));
                            Console.WriteLine($"{teacher.IdTeacher} ,{SubInfoLimpieza.LongCount()}");
                            SubInfoLimpieza.ForEach(x => {
                                workSheet.Cells[$"H{row1}"].Value = x.IdStudent;
                                workSheet.Cells[$"I{row1}:N{row1}"].Merge=true;
                                workSheet.Cells[$"I{row1}:N{row1}"].Value = (resultStudent2.Find(n=>n.IdStudent==x.IdStudent)).NameStudent;
                                row1 ++;

                            });


                            
                            
                        });
                    }
                }
                result = package.GetAsByteArray();
            }


            // Devuelve el valor que se asignó en el bucle
            return result;
        }
        #endregion

        //---------------------post 
        public InfoLimpieza AddInfoLimpieza(InfoLimpieza InfoLimpieza){
            return _InfoLimpiezaDataService.AddInfoLimpieza(InfoLimpieza);

        }
    }

}