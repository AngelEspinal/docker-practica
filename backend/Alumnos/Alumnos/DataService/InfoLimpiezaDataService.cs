using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;

namespace Alumnos.DataService
{
    public class InfoLimpiezaDataService
    {
        public Context _Context;
         
        public InfoLimpiezaDataService(Context Context){
            _Context= Context;
        }
        //-----------------------HttpGet
        public List<InfoLimpieza> GetAllInfoLimpiezaData(){
            return _Context.TinfoLimpieza.ToList();
        }
        public List<InfoLimpieza> GetAllInfoLimpiezaByIdSemestre(string IdSemester=null){
            string semester = null;
            if(string.IsNullOrEmpty(IdSemester)){
                var infoLimpieza = GetAllInfoLimpiezaData();
                semester = infoLimpieza.Max(cs => cs.IdSemester);
            }
            return _Context.TinfoLimpieza.Where(si => si.IdSemester == semester).ToList();
        }
        //-----------------------post ----------------
        public InfoLimpieza AddInfoLimpieza(InfoLimpieza InfoLimpieza){
            _Context.TinfoLimpieza.Add(InfoLimpieza);
            _Context.SaveChanges();
            return InfoLimpieza;

        }
        public void DeleteInfoLimpiezaBySemestre(string IdSemester){
            var studentsToDelete = _Context.TinfoLimpieza.Where(s => s.IdSemester == IdSemester);
            _Context.TinfoLimpieza.RemoveRange(studentsToDelete);
            _Context.SaveChanges();
            // Crear un objeto de respuesta con un mensaje de éxito

        }


    }
}