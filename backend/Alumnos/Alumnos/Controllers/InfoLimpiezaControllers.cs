using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.BusinessService;
using Alumnos.models;
using Microsoft.AspNetCore.Mvc;

namespace Alumnos.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class InfoLimpiezaControllers: ControllerBase
    {
        public InfoLimpiezaBusinessService _InfoLimpiezaBusinessService;
         
        public InfoLimpiezaControllers(InfoLimpiezaBusinessService InfoLimpiezaBusinessService){
            _InfoLimpiezaBusinessService = InfoLimpiezaBusinessService;
        }
        //------------------------HttpGet---------------------
        [HttpGet("[action]")]
        public IActionResult GenerateInfoCleaning([FromQuery] string Day,
                                                [FromQuery] string HoraInit,
                                                [FromQuery] string HoraEnd,
                                                [FromQuery] string semester = null)
        {

            
            return Ok(_InfoLimpiezaBusinessService.GenerateInfoCleaning(Day , HoraInit,HoraEnd, semester ));
        }
        [HttpGet("[action]")]
        public IActionResult ReporteExportToExel(string semester=null){
            var fileBytes = new MemoryStream(_InfoLimpiezaBusinessService.ReporteExportToExel(semester));
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Evalucion-InnovarPeru.xlsx"); // Devolver el archivo generado como una respuesta de archivo del controlador
        }
        [HttpGet("[action]")]
        public List<InfoLimpieza> GetAllInfoLimpiezaByIdSemestre(string IdSemester = null){
            return _InfoLimpiezaBusinessService.GetAllInfoLimpiezaByIdSemestre(IdSemester);
        }
        
    }
}