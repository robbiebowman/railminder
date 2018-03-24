using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Railminder.Models;

namespace Railminder.Services
{
    public class ApiService
    {
        public async Task<List<Station>> GetStations()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri("http://api.irishrail.ie/realtime/realtime.asmx/getAllStationsXML"));
                var stationList = await GetFromXmlContent<StationList>("ArrayOfObjStation", response.Content);
                return stationList.Stations;
            }
        }

        private async Task<TrainInfoList> GetTrainInfoForStation(Station station)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri($"http://api.irishrail.ie/realtime/realtime.asmx/getStationDataByCodeXML?StationCode={station.Code}"));
                return await GetFromXmlContent<TrainInfoList>("ArrayOfObjStationData", response.Content);
            }
        }

        public async Task<List<string>> GetDirections(Station station)
        {
            return (await GetTrainInfoForStation(station)).InfoList.Select(i => i.Direction).Distinct().ToList();
        }

        public async Task<List<TrainInfo>> GetUpcomngTrains(Station station, string direction)
        {
            return (await GetTrainInfoForStation(station)).InfoList.Where(ti => ti.Direction == direction).ToList();
        }

        private async Task<T> GetFromXmlContent<T>(string rootName, HttpContent content)
        {
            var xmlReader = XmlReader.Create(await content.ReadAsStreamAsync());
            var xmlSerializer = new XmlSerializer(typeof(T), GetRootXmlAttbibute(rootName));
            return (T) xmlSerializer.Deserialize(xmlReader);
        }

        private XmlRootAttribute GetRootXmlAttbibute(string rootName) => new XmlRootAttribute
        {
            ElementName = rootName,
            Namespace = "http://api.irishrail.ie/realtime/",
            IsNullable = true
        };
    }
}
