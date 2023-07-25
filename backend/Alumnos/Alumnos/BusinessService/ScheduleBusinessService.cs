using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.DataService;
using Alumnos.models;

namespace Alumnos.BusinessService
{
    public class ScheduleBusinessService
    {
        private ScheduleDataService _ScheduleDataService;


        public ScheduleBusinessService(ScheduleDataService scheduleDataService)
        {
            _ScheduleDataService = scheduleDataService;
        }
        //---------------get-----------
        public List<Schedule> GetSAllchedule(){
            return _ScheduleDataService.GetSAllchedule();
        }
    }
}