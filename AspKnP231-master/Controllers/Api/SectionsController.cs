using AspKnP231.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspKnP231.Controllers.Api
{
    [Route("api/sections")]
    [ApiController]
    public class SectionsController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;
        [HttpGet]
        public Object DoGet()
        {
            //LINQ - language integrated queries
            var sections = _dataContext
    .ShopSections
    .Where(s => s.DeletedAt == null)
    .AsEnumerable()
    .Select(s => s with { ImageUrl = 
    $"{Request.Scheme}://{Request.Host}/Storage/Item/" + s.ImageUrl });

            return sections;
        }
    }
}

/*
    MVC                         API
    GET /Home   | один          GET /Home   | різні
    POST /Home  | ресурс        POST /Home  | ресурси

    маршрутизація -             за методом запиту
    за адресою

    тип повернення методів контролера
    IActionResult               Object
*/