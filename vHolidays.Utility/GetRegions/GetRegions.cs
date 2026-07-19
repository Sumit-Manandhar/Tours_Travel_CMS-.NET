using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace vHolidays.Utility.GetRegions
{
    public static class GetRegions
    {

        public async static Task<CountriesApiModel> GetCountryData()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://countriesnow.space/api/v0.1/countries/info?returns=name,states," +
                    "currency,flag,unicodeFlag,dialCode,cities,iso2,iso3");

                client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CountriesApiModel>(dataObjects);
                }
            }
            return null;
        }
        public  async static Task<StatesApiModel> GetStateData()
        {
            using (HttpClient statesClient = new HttpClient())
            {
                statesClient.BaseAddress = new Uri("https://countriesnow.space/api/v0.1/countries/states");

                statesClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = statesClient.GetAsync("").Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<StatesApiModel>(dataObjects);
                }
            }
            return null;
        }

    }

    public class Datum
    {
        public string name { get; set; }
        public string currency { get; set; }
        public string unicodeFlag { get; set; }
        public string flag { get; set; }
        public string dialCode { get; set; }
        public List<string> cities { get; set; }
        public string iso2 { get; set; }
        public string iso3 { get; set; }
    }

    public class CountriesApiModel
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public List<Datum> data { get; set; }
    }
    public class StatesApiModel
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public List<StatesData> data { get; set; }
    }
    public class StatesData
    {
        public string name { get; set; }
        public string iso3 { get; set; }
        public List<States> states { get; set; }
    }
    public class States
    {
        public string name { get; set; }
        public string state_code { get; set; }
    }
}
