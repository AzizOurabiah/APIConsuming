using Microsoft.AspNetCore.Mvc;

namespace NewZelandWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpContextFactory;

        public RegionsController(IHttpClientFactory httpContextFactory)
        {
            _httpContextFactory = httpContextFactory;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                //On crée a new HttpClient
                var client = _httpContextFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7096/api/regions");

                //Ensure the response is succes
                httpResponseMessage.EnsureSuccessStatusCode();

                //read the response
                var stringResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                ViewBag.Response = stringResponseBody;
            }
            catch (Exception ex)
            {
                //Log the exception 
            }
            return View();
        }
    }
}
