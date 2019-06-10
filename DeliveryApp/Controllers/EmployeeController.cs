using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DeliveryApp.Attributes;
using DeliveryApp.Constants;
using DeliveryApp.DTOs;
using DeliveryApp.Enums;
using DeliveryApp.Exceptions;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : SecureController
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
            : base(new Dictionary<AccessOption, string> {
                { AccessOption.Create,  ActivityNames.EmployeeCr },
                { AccessOption.Delete,  ActivityNames.EmployeeDel },
                { AccessOption.Details, ActivityNames.EmployeeDet },
                { AccessOption.Index,   ActivityNames.EmployeeInd},
                { AccessOption.Update,  ActivityNames.EmployeeEd },
            })
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        //[HttpPost]
        //[Route("CreateEmployee")]
        //public async Task<IActionResult> CreateEmployee([FromBody]Employee dto)
        //{
        //    Employee createdDto = await _service.CreateAsync(dto, ModelState.AddModelError);

        //    //if (createdDto == null)
        //    //{
        //    //    return null;
        //    //}

        //    return null;
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.EmployeeInd)]
        public async Task<IEnumerable<Employee>> Index()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.EmployeeDet)]
        public async Task<Employee> Get(Guid id)
        {
            // 200(OK) if found an id
            // 204(No Content) if there is no such id
            return await _service.GetAsync(id);
        }

        [HttpPost]
        [ValidateModelFilter]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.EmployeeCr)]
        public async Task<IActionResult> Create([FromBody] Employee dto)
        {
            Employee createdDto = await _service.CreateAsync(dto, ModelState.AddModelError);
            if (createdDto == null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtAction(
                nameof(Get),
                new { id = createdDto.ID },
                createdDto
                );
        }

        [HttpPut]
        [ValidateModelFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeActivity(new string[] { ActivityNames.EmployeeCr, ActivityNames.EmployeeEd })]
        public async Task<IActionResult> Update([FromBody] Employee dto)
        {
            try
            {
                Employee updatedDto = await _service.UpdateAsync(dto, ModelState.AddModelError);
                if (updatedDto == null)
                {
                    return BadRequest(ModelState);
                }

                return Ok(updatedDto);
            }
            catch (EntityNotFoundException<Employee, Guid>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeActivity(ActivityNames.EmployeeDel)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Employee deletedDto = await _service.DeleteAsync(id, ModelState.AddModelError);
                if (deletedDto == null)
                {
                    return BadRequest(ModelState);
                }

                return Ok(deletedDto);
            }
            catch(EntityNotFoundException<Employee, Guid>)
            {
                return NotFound();
            }
        }
    }
}