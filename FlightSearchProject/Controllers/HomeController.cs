using FlightSearchProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FlightsBooking.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> search()
        {
            string authToken = "W4b7AmkA8i3tM0zBKusNbKjllbw3";
            DataFlight flightInfo = null;

            string type = Request.Form["add"];
            string airportDeparture = Request.Form["from"];
            string airportArrival = Request.Form["to"];
            string nbrPassengers = Request.Form["nbrpassengers"];
            string departureDate = Request.Form["departureDate"];
            string returnDate = Request.Form["returnDate"];

            string airportstart = airportDeparture.Substring(0, Math.Min(airportDeparture.Length, 23));
            string airportend = airportArrival.Substring(0, Math.Min(airportArrival.Length, 23));
            string APIdeparture = "https://airlabs.co/api/v9/suggest?q=" + airportstart + "&api_key=3f8357d2-88d8-436c-b499-393dc237a7f6";
            string APIarrival = "https://airlabs.co/api/v9/suggest?q=" + airportend + "&api_key=3f8357d2-88d8-436c-b499-393dc237a7f6";
            string iataDeparture;
            string iataArrival;
            using (var client = new HttpClient())
            {
                AirportTitle airportName = null;
                HttpResponseMessage response1 = await client.GetAsync(APIdeparture);
                if (response1.IsSuccessStatusCode)
                {
                    var EmpResponse = response1.Content.ReadAsStringAsync().Result;
                    airportName = JsonConvert.DeserializeObject<AirportTitle>(EmpResponse);
                }
                iataDeparture = airportName.response.airports[0].iata_code;

                HttpResponseMessage response2 = await client.GetAsync(APIarrival);
                if (response2.IsSuccessStatusCode)
                {
                    var EmpResponse = response2.Content.ReadAsStringAsync().Result;
                    airportName = JsonConvert.DeserializeObject<AirportTitle>(EmpResponse);
                }
                iataArrival = airportName.response.airports[0].iata_code;
            }

            string val = "From : " + airportDeparture + " To : " + airportArrival + " number of places: " + nbrPassengers + " Flight Date : " + departureDate + " Flight return date : " + returnDate;
            ViewBag.val = val;
            ViewBag.airport1 = iataDeparture;
            ViewBag.airport2 = iataArrival;

            using (var client = new HttpClient())
            {
                if (type == "oneway")
                {
                    string api = "https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode=" + iataDeparture + "&destinationLocationCode=" + iataArrival + "&departureDate=" + departureDate + "&adults=" + nbrPassengers + "&max=8";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    HttpResponseMessage response = await client.GetAsync(api);
                    if (response.IsSuccessStatusCode)
                    {
                        var EmpResponse = response.Content.ReadAsStringAsync().Result;
                        flightInfo = JsonConvert.DeserializeObject<DataFlight>(EmpResponse);
                    }
                    foreach (Datum flight in flightInfo.data)
                    {
                        foreach (Itinerary itinirary in flight.itineraries)
                        {
                            foreach (Segment correspondance in itinirary.segments)
                            {
                                if (correspondance.arrival.iataCode != iataArrival && correspondance.arrival.iataCode != iataDeparture)
                                {
                                    var apiName = "https://airlabs.co/api/v9/airports?iata_code=" + correspondance.arrival.iataCode + "&api_key=3f8357d2-88d8-436c-b499-393dc237a7f6";
                                    using (var clients = new HttpClient())
                                    {
                                        Correspondance correspondances = null;
                                        HttpResponseMessage responsename = await clients.GetAsync(apiName);
                                        if (responsename.IsSuccessStatusCode)
                                        {
                                            var EmpResponse = responsename.Content.ReadAsStringAsync().Result;
                                            correspondances = JsonConvert.DeserializeObject<Correspondance>(EmpResponse);
                                        }
                                        correspondance.arrival.iataCode = correspondances.response[0].name;
                                    }

                                }
                            }
                        }
                    }
                    ViewBag.FlightInfo = flightInfo;
                }
                else
                {
                    string api = "https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode=" + iataDeparture + "&destinationLocationCode=" + iataArrival + "&departureDate=" + departureDate + "&returnDate=" + returnDate + "&adults=" + nbrPassengers + "&max=8";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    HttpResponseMessage response = await client.GetAsync(api);
                    if (response.IsSuccessStatusCode)
                    {
                        var EmpResponse = response.Content.ReadAsStringAsync().Result;
                        flightInfo = JsonConvert.DeserializeObject<DataFlight>(EmpResponse);
                    }
                    foreach (Datum flight in flightInfo.data)
                    {
                        foreach (Itinerary itinirary in flight.itineraries)
                        {
                            foreach (Segment correspondance in itinirary.segments)
                            {
                                if (correspondance.arrival.iataCode != iataArrival && correspondance.arrival.iataCode != iataDeparture)
                                {
                                    var apiName = "https://airlabs.co/api/v9/airports?iata_code=" + correspondance.arrival.iataCode + "&api_key=3f8357d2-88d8-436c-b499-393dc237a7f6";
                                    using (var clients = new HttpClient())
                                    {
                                        Correspondance correspondances = null;
                                        HttpResponseMessage responsename = await clients.GetAsync(apiName);
                                        if (responsename.IsSuccessStatusCode)
                                        {
                                            var EmpResponse = responsename.Content.ReadAsStringAsync().Result;
                                            correspondances = JsonConvert.DeserializeObject<Correspondance>(EmpResponse);
                                        }
                                        correspondance.arrival.iataCode = correspondances.response[0].name;
                                    }

                                }
                            }
                        }
                    }

                    ViewBag.FlightInfo = flightInfo;
                }
                return View(flightInfo);
            }
        }
        [HttpPost]
        public JsonResult Index(string Prefix)
        {
            //Note : you can bind same list from database
            List<AirportTitleAuto> ObjList = new List<AirportTitleAuto>()
            {

                new AirportTitleAuto {Id=1,name="Paris Charles de Gaulle Airport"},
                new AirportTitleAuto {Id=2,name="Paris Orly Airport"},
                new AirportTitleAuto {Id=3,name="Blue Grass Airport"},
                new AirportTitleAuto {Id=4,name="Gran Canaria Airport"},
                new AirportTitleAuto {Id=5,name="Tenerife North Airport"},
                new AirportTitleAuto {Id=6,name="Tenerife South Airport"},
                new AirportTitleAuto {Id=7,name="Punta Cana International Airport"},
                new AirportTitleAuto {Id=1,name="General Servando Canales International Airport"},
                new AirportTitleAuto {Id=2,name="Mohammed V International Airport"},
                new AirportTitleAuto {Id=3,name="Turin Airport"},
                new AirportTitleAuto {Id=4,name="East Midlands Airport"},
                new AirportTitleAuto {Id=5,name="Brindisi - Salento Airport"},
                new AirportTitleAuto {Id=6,name="Casper-Natrona County International Airport"},
                new AirportTitleAuto {Id=7,name="Tangier Ibn Battouta Airport"},
                new AirportTitleAuto {Id=1,name="Agadir-Al Massira Airport"},
                new AirportTitleAuto {Id=2,name="Fes-Saiss Airport"},
                new AirportTitleAuto {Id=3,name="Menara Airport"},
                new AirportTitleAuto {Id=4,name="Nice Cote d'Azur International Airport"},
                new AirportTitleAuto {Id=5,name="Lyon-Saint Exupery Airport"},
                new AirportTitleAuto {Id=6,name="Marseille Provence Airport"},
                new AirportTitleAuto {Id=7,name="Chicago O'Hare International Airport"},
                new AirportTitleAuto {Id=1,name="Hartsfield-Jackson Atlanta International Airport"},
                new AirportTitleAuto {Id=2,name="Dallas/Fort Worth International Airport"},
                new AirportTitleAuto {Id=3,name="Charlotte Douglas International Airport"},
                new AirportTitleAuto {Id=4,name="Los Angeles International Airport"},
                new AirportTitleAuto {Id=5,name="Brindisi - Salento Airport"},
                new AirportTitleAuto {Id=6,name="Casper-Natrona County International Airport"}

        };


            var Name = (from N in ObjList
                        where N.name.ToLower().Contains(Prefix.ToLower())
                        select new
                        {
                            N.name
                        });


            return Json(Name);

        }
    }
}