using System;

namespace Railminder.Services
{
    public static class Config
    {
        // The notification should be shown 10 minutes before the train arrives on the station, because that is how much time I spend
        // to walk from the office to the station.
        public static readonly TimeSpan TimeToReachStation = TimeSpan.FromMinutes(10);

        // If there are no trains in the next 20 minutes or any error happened, I would like to be warned, otherwise I  can waste my  time
        // waiting for the notification.
        public static readonly TimeSpan TimeBeforeNotInterestedInTrain = TimeSpan.FromMinutes(20);

        // we would like tohave an appwhere wecan check the real time information of trains arrivinginBlackrock station.
        public const string FavouriteStation = "Blackrock";
    }
}
