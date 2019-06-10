using DeliveryApp.DTOs;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicMenuController: ControllerBase
    {
        protected readonly IDynamicMenuService _service;

        public DynamicMenuController(IDynamicMenuService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IEnumerable<DynamicManu>> GetDynamicManuItems()
        {
            return await _service.GetAllAsync();
        }
    }
}
