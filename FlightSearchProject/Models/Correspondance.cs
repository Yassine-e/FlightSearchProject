namespace FlightSearchProject.Models
{
    public class Keys
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

    public class Paramms
    {
        public string iata_code { get; set; }
        public string lang { get; set; }
    }

    public class Karmas
    {
        public bool is_blocked { get; set; }
        public bool is_crawler { get; set; }
        public bool is_bot { get; set; }
        public bool is_friend { get; set; }
        public bool is_regular { get; set; }
    }

    public class Clients
    {
        public string ip { get; set; }
        public string geo { get; set; }
        public string connection { get; set; }
        public string device { get; set; }
        public string agent { get; set; }
        public Karma karma { get; set; }
    }

    public class Requests
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

    public class Responses
    {
        public string icao_code { get; set; }
        public string country_code { get; set; }
        public string iata_code { get; set; }
        public double lng { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
    }

    public class Correspondance
    {
        public Requests request { get; set; }
        public List<Responses> response { get; set; }
        public string terms { get; set; }
    }


}
