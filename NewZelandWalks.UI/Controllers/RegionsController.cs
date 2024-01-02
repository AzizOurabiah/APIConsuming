using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NewZelandWalks.UI.Models;
using NewZelandWalks.UI.Models.DTO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NewZelandWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpContextFactory;

        public RegionsController(IHttpClientFactory httpContextFactory)
        {
            _httpContextFactory = httpContextFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                //On crée a new HttpClient
                var client = _httpContextFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7096/api/regions");

                //Ensure the response is succes
                httpResponseMessage.EnsureSuccessStatusCode();

                //read the response
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

                //ViewBag.Response = stringResponseBody;
            }
            catch (Exception ex)
            {
                //Log the exception 
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            //We create a new client
            var client = _httpContextFactory.CreateClient();

            //We create httpRequest
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7096/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
            };
            //Send the request
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            //Sassuer que la réponse est réussi
            httpResponseMessage.EnsureSuccessStatusCode();

            //And after that we can read the body the same way as we did before.
            var response =  await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return BadRequest(response);
            //return View();
        }
    }
}
