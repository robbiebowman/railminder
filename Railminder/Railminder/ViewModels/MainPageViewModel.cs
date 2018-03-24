using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Railminder.Models;
using Railminder.Services;

namespace Railminder.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private List<Station> _stations;
        private readonly ApiService _apiService;
        private int _selectedStation;
        private List<string> _directions;
        private int _selectedDirection;
        private readonly ScheduleService _scheduleService;
        private readonly NotificationService _notificationService;
        private string _checkResult;

        public MainPageViewModel()
        {
            _stations = new List<Station>();
            _apiService = new ApiService();
            _scheduleService = new ScheduleService();
            _notificationService = new NotificationService();
            UpdateStations();
        }


        public string CheckResult
        {
            get => _checkResult;
            set
            {
                if (_checkResult != value)
                {
                    _checkResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedStation
        {
            get => _selectedStation;
            set
            {
                _selectedStation = value;
                CheckResult = string.Empty;
                OnPropertyChanged();
                UpdateDirections();
            }
        }

        public int SelectedDirection
        {
            get => _selectedDirection;
            set
            {
                _selectedDirection = value;
                CheckResult = string.Empty;
            }
        }

        public List<string> Directions
        {
            get => _directions;
            set
            {
                if (_directions != value)
                {
                    _directions = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> StationNames =>
            _stations.Select(s => string.IsNullOrEmpty(s.Alias) ? s.Description : s.Alias).ToList();


        private async void UpdateDirections()
        {
            Directions = new List<string>();
            Directions = await _apiService.GetDirections(_stations[SelectedStation]);
        }

        public async void CheckForUpcomingTrains()
        {
            var now = DateTime.Now;
            var station = _stations[_selectedStation];
            var upcomingTrains =
                await _apiService.GetUpcomngTrains(station, _directions[_selectedDirection]);
            foreach (var train in upcomingTrains)
            {
                train.ArrivalTime = _scheduleService.GetArrivalTime(train);
            }

            var viableTrains = upcomingTrains.Where(t =>
                t.ArrivalTime - now >= Config.TimeToReachStation
                && t.ArrivalTime - now <= Config.TimeBeforeNotInterestedInTrain).ToList();

            if (viableTrains.Any())
            {
                var bestTrain = viableTrains.OrderBy(t => t.ArrivalTime).First();
                _notificationService.ScheduleTrainNotification(bestTrain);

                CheckResult = $"Success! Scheduling a notification for the {bestTrain.Exparrival} train at {bestTrain.Stationfullname} heading {bestTrain.Direction}.";
            }
            else
            {
                _notificationService.NotifyNoTrainsAvailable(station);

                CheckResult = $"Can't find any viable trains at {station.Description} heading {_directions[_selectedDirection]}.";
            }
        }

        private async void UpdateStations()
        {
            _stations = await _apiService.GetStations();

            OnPropertyChanged(nameof(StationNames));

            if (!string.IsNullOrWhiteSpace(Config.FavouriteStation))
            {
                var favouriteStationIndex = _stations.FindIndex(s => s.Description == Config.FavouriteStation);
                if (favouriteStationIndex != -1)
                {
                    SelectedStation = favouriteStationIndex;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}