using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.DataService;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Alumnos.BusinessService
{
    public class TeacherBusinessService
    {
        private TeacherDataService _TeacherDataService;

        public TeacherBusinessService(TeacherDataService teacherDataService)
        {
            _TeacherDataService = teacherDataService;
        }
        //---------------------htttp get--------------------------------
        public Teacher GetStudentByIdTeacher(string IdTeacher)
        {
            return _TeacherDataService.GetStudentByIdTeacher(IdTeacher);
        }
        
        public List<Teacher> GetAllTeacher( )
        {
            
            return _TeacherDataService.GetAllTeacher( );
        }
        public byte[] ExportUsersToExcel(string IdTeacher)
        {
            Teacher teacher = _TeacherDataService.GetStudentByIdTeacher(IdTeacher);
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Reporte");
                    
                // Combinar celdas para crear titulo de reporte
                workSheet.Cells[1, 1, 1, 3].Merge = true;
                workSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[1, 1].Value = "Reporte de info Limpieza";
                // Docente a Crago
                workSheet.Cells[2, 1].Value = "Docente : ";
                workSheet.Cells[2, 2].Value=teacher.NameTeacher;
                //Agregamos el encabezado de las columnas
                workSheet.Cells[3, 1].Value = "Codigo";
                workSheet.Cells[3, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(22, 172, 177));

                workSheet.Cells[3, 2,3,3].Merge=true;
                workSheet.Cells[3, 2].Value = "Nombre";
                workSheet.Cells[3, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[3, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(22, 172, 177));
                // Ajustar estilo de la celda combinada
                workSheet.Cells[1, 1].Style.Font.Bold = true;


                //Iteramos sobre los estudiantes del profesor
                /*int row = 4;
                foreach (var student in teacher.student)
                {
                    workSheet.Cells[row, 1].Value = student.IdStudent;
                    workSheet.Cells[row, 2, row, 3].Merge=true;
                    workSheet.Cells[row, 2].Value = student.NameStudent;
                    row++;
                }
                workSheet.Column(2).AutoFit(MinimumWidth: 45);*/



                //workSheet.Cells.LoadFromCollection(teacher, true);
                var workSheet1 = package.Workbook.Worksheets.Add("Reporte2");
                    
                // Combinar celdas para crear titulo de reporte
                workSheet1.Cells[1, 1, 1, 3].Merge = true;
                workSheet1.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet1.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 0));
                workSheet1.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet1.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet1.Cells[1, 1].Value = "Reporte de info Limpieza";
                // Docente a Crago
                workSheet1.Cells[2, 1].Value = "Docente : ";
                workSheet1.Cells[2, 2].Value=teacher.NameTeacher;
                //Agregamos el encabezado de las columnas
                workSheet1.Cells[3, 1].Value = "Codigo";
                workSheet1.Cells[3, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet1.Cells[3, 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(22, 172, 177));

                workSheet1.Cells[3, 2,3,3].Merge=true;
                workSheet1.Cells[3, 2].Value = "Nombre";
                workSheet1.Cells[3, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet1.Cells[3, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(22, 172, 177));
                // Ajustar estilo de la celda combinada
                workSheet1.Cells[1, 1].Style.Font.Bold = true;
                return package.GetAsByteArray();
            }
        } 

       
        //---------------------post-------------
        public Teacher AddTeacher(Teacher teacher)
        {
            return _TeacherDataService.AddTeacher(teacher);
        }
        
    }
}