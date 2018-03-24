using System;
using System.Linq;
using Railminder.Models;

namespace Railminder.Services
{
    public class ScheduleService
    {
        public DateTime GetArrivalTime(TrainInfo train)
        {
            var arrivalHour = int.Parse(train.Exparrival.Split(':').First());
            var arrivalMinute = int.Parse(train.Exparrival.Split(':').Last());
            var currentTime = DateTime.Parse(train.Servertime);
            var arrivalDate = currentTime.Date;

            if (arrivalHour < currentTime.Hour)
            {
                arrivalDate = arrivalDate.AddDays(1); // Handle getting a train around midnight
            }

            return arrivalDate.AddHours(arrivalHour).AddMinutes(arrivalMinute);
        }
    }
}
