using Plugin.LocalNotifications;
using Railminder.Models;

namespace Railminder.Services
{
    public class NotificationService
    {
        public void ScheduleTrainNotification(TrainInfo train)
        {
            var notifyTime = train.ArrivalTime.Subtract(Config.TimeToReachStation);
            CrossLocalNotifications.Current.Show("Time to go!", $"There's a train coming into {train.Stationfullname} at {train.Exparrival} heading {train.Direction}. If you go now you should make it.", 1, notifyTime);
        }

        public void NotifyNoTrainsAvailable(Station station)
        {
            CrossLocalNotifications.Current.Show("No trains right now", $"There aren't any trains coming into {station.Description} that you can catch.");
        }
    }
}
