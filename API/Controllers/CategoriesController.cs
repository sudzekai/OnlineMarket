using API.Controllers.Controller;
using BLL.Services.Categories;
using BLL.Services.Clients;
using BLL.Services.Service;
using DAL.Efcore.Models;
using DTO.Models.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller<CategoryFullDto, CategoryActionDto, CategoryActionDto>
    {
        public CategoriesController(ICategoriesService service, IClientsService clientsService) : base(service, clientsService)
        {
        }
    }
}
