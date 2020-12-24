using System;
using System.Net;
using System.Text;
using System.Text.Json;

/*
Написать онлайн пррограмму прогноз погоды.
Пользователь вбивает любой город и программадолжна показать прогноз погоду на текущую дату
*/
namespace Weather_
{
    class Program
    {
        static public decimal ConverCelvinToCelsius(decimal celvin) //Формула для перевода: °C = °К — 273,15  или наоборот  °K = °C + 273,15
        {
            decimal tmp = 273.15m;

            decimal celsius = celvin - tmp;

            return celsius;
        }
        static public string Qeolocation(string city)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("http://api.openweathermap.org/data/2.5/weather?q=");
            strBuilder.Append(city);
            strBuilder.Append("&appid=6eecea7626cdf4f4d025750897990bee");

            return strBuilder.ToString();
        }

        static void Main(string[] args)
        {
            Menyu action;
            WebClient web = new WebClient();
            var result = new Rootobject();
            bool check;
            string locality;



            do
            {
                Console.WriteLine("0 - Exit\n" +
                                  "1 - Weather information");


                action = (Menyu)Enum.Parse(typeof(Menyu), Console.ReadLine());

                Console.Clear();
                if (action == Menyu.EXIT)
                {
                    Console.WriteLine("You left program");
                    break;
                }
                else if (action == Menyu.WEATHER)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();
                    Console.WriteLine("Enter locality to display weather forecast ?\n");
                    locality = Console.ReadLine();
                    check = false;

                    try
                    {
                        var url = Qeolocation(locality);
                        string str = web.DownloadString(url);
                        result = JsonSerializer.Deserialize<Rootobject>(str);
                        check = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (check)
                    {
                        // ya vivel osnovnie xarakteristiki  poqodi a ne vse podryad

                        result.main.temp = ConverCelvinToCelsius(result.main.temp);
                        result.main.temp_min = ConverCelvinToCelsius(result.main.temp_min);
                        result.main.temp_max = ConverCelvinToCelsius(result.main.temp_max);
                        Console.WriteLine(" -----------------------------------------\n" +
                                          "|                                         |\n" +
                                          "|  !!Welcome to World Weather Forecast!!  |\n" +
                                          "|                                         |\n" +
                                          "|-----------------------------------------|\n" +
                                          $"|     Locality    :{result.name.PadLeft(15)}        |\n" +
                                          $"|     Id          :{result.sys.country.PadLeft(15)}        |\n" +
                                          $"|-----------------------------------------|\n" +
                                          $"|     Coord                               |\n" +
                                          $"|     Longitude   : {result.coord.lon.ToString().PadLeft(15)}       |\n" +
                                          $"|     Latitude    : {result.coord.lat.ToString().PadLeft(15)}       |\n" +
                                          $"|-----------------------------------------|\n" +
                                          $"|     Clouds      : {result.weather[0].description.ToString().PadLeft(20)}  |\n" +
                                          $"|     Temperature : {result.main.temp.ToString().PadLeft(15)}°      |\n" +
                                          $"|     Min temp    : {result.main.temp_min.ToString().PadLeft(15)}°      |\n" +
                                          $"|     Max temp    : {result.main.temp_max.ToString().PadLeft(15)}°      |\n" +
                                          $"|     Pressure    : {result.main.pressure.ToString().PadLeft(15)} p     |\n" +
                                          $"|     Wind speed  : {result.wind.speed.ToString().PadLeft(15)}km / h |\n" +
                                          $"|_________________________________________|\n");

                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                else
                {
                    Console.WriteLine("Incorrect choice\n");
                }

            } while (true);

        }
    }

}

