using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;

namespace Alumnos.DataService
{
    public class ScheduleDataService
    {
        public Context _Context;
        public ScheduleDataService(Context context){
            _Context = context;
        }
        //-------get----
        public List<Schedule> GetSAllchedule(){
            return _Context.Tschedule.ToList();
        }

    }
}