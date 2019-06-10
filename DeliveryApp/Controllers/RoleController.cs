using DeliveryApp.Attributes;
using DeliveryApp.Constants;
using DeliveryApp.DTOs;
using DeliveryApp.Enums;
using DeliveryApp.Exceptions;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public sealed class RoleController : SecureController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
            : base(new Dictionary<AccessOption, string> {
                { AccessOption.Create,  ActivityNames.RoleCr },
                { AccessOption.Delete,  ActivityNames.RoleDel },
                { AccessOption.Details, ActivityNames.RoleDet },
                { AccessOption.Index,   ActivityNames.RoleInd },
                { AccessOption.Update,  ActivityNames.RoleEd },
            })
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.RoleInd)]
        public async Task<IEnumerable<Role>> Index()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.RoleDet)]
        public async Task<Role> Get(string id)
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
        [AuthorizeActivity(ActivityNames.RoleCr)]
        public async Task<IActionResult> Create([FromBody]Role dto)
        {
            Role createdDto = await _service.CreateAsync(dto, ModelState.AddModelError);
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
        [AuthorizeActivity(new string[] { ActivityNames.RoleCr, ActivityNames.RoleEd })]
        public async Task<IActionResult> Update([FromBody]Role dto)
        {
            try
            {
                Role updatedDto = await _service.UpdateAsync(dto, ModelState.AddModelError);
                if (updatedDto == null)
                {
                    return BadRequest(ModelState);
                }

                return Ok(updatedDto);
            }
            catch (EntityNotFoundException<Role, string>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeActivity(ActivityNames.RoleDel)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                Role deletedDto = await _service.DeleteAsync(id, ModelState.AddModelError);
                if (deletedDto == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(deletedDto);
            }
            catch (EntityNotFoundException<Role, string>)
            {
                return NotFound();
            }
        }
    }
}