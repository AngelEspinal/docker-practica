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
    public class ScheduleControllers
    {
        private ScheduleBusinessService _ScheduleBusinessService;


        public ScheduleControllers(ScheduleBusinessService scheduleBusinessService)
        {
            _ScheduleBusinessService = scheduleBusinessService;
        }
        //--------------httpGet-----------
        [HttpGet("[action]")]
        public List<Schedule> GetSAllchedule(){
            return _ScheduleBusinessService.GetSAllchedule();
        }
    }
}