using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDev_Practice1.Pages
{
    public class WeatherModel : PageModel
    {
        [BindProperty]
        public double? Latitude {  get; set; }
        [BindProperty]
        public double? Longtitude { get; set; }
        [BindProperty]
        public double? City { get; set; }

        public class DefinedCity
        {
            public string Name { get; set; }
            public double? Latitude { get; set; }
            public double? Longtitude { get; set; }
        }

        private Dictionary<string, DefinedCity> DefinedCities = new Dictionary<string, DefinedCity>()
        {
            new DefinedCity()
        };

        public WeatherResponse? Result { get; set; }

        public class WeatherResponse
        {
            CurrentWeather? current_weather {  get; set; }
            public string? timezone { get; set; }
        }

        public class CurrentWeather
        {
            public double temperature {  get; set; }
            public double wind_direction {  get; set; }

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (string.IsNullOrEmpty(City)) {

                string? lat;
                string? lon;


                var url = $"";

                var client = new HttpClient();

            return Page();
        }
    }
}
