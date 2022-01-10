namespace FlightSearchProject.Models
{

    public class Key
    {
        public int id { get; set; }
        public string api_key { get; set; }
        public string type { get; set; }
        public object trial_price { get; set; }
        public object expired { get; set; }
        public DateTime registered { get; set; }
        public int limits_by_hour { get; set; }
        public int limits_by_minute { get; set; }
        public int limits_by_month { get; set; }
        public int limits_total { get; set; }
    }

    public class Params
    {
        public string q { get; set; }
        public string lang { get; set; }
    }

    public class Geo
    {
        public string country_code { get; set; }
        public string country { get; set; }
        public string continent { get; set; }
        public string city { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string timezone { get; set; }
    }

    public class Connection
    {
        public string type { get; set; }
        public int isp_code { get; set; }
        public string isp_name { get; set; }
    }

    public class Device
    {
    }

    public class Agent
    {
    }

    public class Karma
    {
        public bool is_blocked { get; set; }
        public bool is_crawler { get; set; }
        public bool is_bot { get; set; }
        public bool is_friend { get; set; }
        public bool is_regular { get; set; }
    }

    public class Client
    {
        public string ip { get; set; }
        public Geo geo { get; set; }
        public Connection connection { get; set; }
        public Device device { get; set; }
        public Agent agent { get; set; }
        public Karma karma { get; set; }
    }

    public class Request
    {
        public string lang { get; set; }
        public string currency { get; set; }
        public int time { get; set; }
        public string id { get; set; }
        public string server { get; set; }
        public string host { get; set; }
        public int pid { get; set; }
        public Key key { get; set; }
        public Params @params { get; set; }
        public int version { get; set; }
        public string method { get; set; }
        public Client client { get; set; }
    }

    public class Airport
    {
        public string name { get; set; }
        public string iata_code { get; set; }
        public string icao_code { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string slug { get; set; }
        public string country_code { get; set; }
        public int popularity { get; set; }
        public string city_code { get; set; }
    }

    public class CitiesByAirport
    {
        public string name { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string slug { get; set; }
        public string country_code { get; set; }
        public int popularity { get; set; }
        public string city_code { get; set; }
    }

    public class Response
    {
        public List<object> countries { get; set; }
        public List<object> cities { get; set; }
        public List<Airport> airports { get; set; }
        public List<CitiesByAirport> cities_by_airports { get; set; }
        public List<object> cities_by_countries { get; set; }
        public List<object> airports_by_cities { get; set; }
        public List<object> airports_by_countries { get; set; }
    }

    public class AirportTitle
    {
        public Request request { get; set; }
        public Response response { get; set; }
        public string terms { get; set; }
    }
}