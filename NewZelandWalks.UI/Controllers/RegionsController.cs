using Microsoft.AspNetCore.Mvc;
using NewZelandWalks.UI.Models.DTO;
using System.Linq.Expressions;

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
    }
}
