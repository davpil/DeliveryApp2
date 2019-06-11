using DeliveryApp.Attributes;
using DeliveryApp.Constants;
using DeliveryApp.DTOs;
using DeliveryApp.Enums;
using DeliveryApp.Exceptions;
using DeliveryApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class PositionController : SecureController
    {
        private readonly IPositionService _service;

        public PositionController(IPositionService service)
            : base(new Dictionary<AccessOption, string> {
                { AccessOption.Create,  ActivityNames.PositionCr },
                { AccessOption.Delete,  ActivityNames.PositionDel },
                { AccessOption.Details, ActivityNames.PositionDet },
                { AccessOption.Index,   ActivityNames.PositionInd },
                { AccessOption.Update,  ActivityNames.PositionEd },
            })
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.PositionInd)]
        public async Task<IEnumerable<Position>> Index()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.EmployeeDet)]
        public async Task<Position> Get(int id)
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
        [AuthorizeActivity(ActivityNames.PositionCr)]
        public async Task<IActionResult> Create([FromBody]Position dto)
        {
            Position createdDto = await _service.CreateAsync(dto, ModelState.AddModelError);
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
        [AuthorizeActivity(new string[] { ActivityNames.PositionCr, ActivityNames.PositionEd })]
        public async Task<IActionResult> Update([FromBody]Position dto)
        {
            try
            {
                Position updatedDto = await _service.UpdateAsync(dto, ModelState.AddModelError);
                if (updatedDto == null)
                {
                    return BadRequest(ModelState);
                }

                return Ok(updatedDto);
            }
            catch (EntityNotFoundException<Position, int>)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AuthorizeActivity(ActivityNames.PositionDel)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Position deletedDto = await _service.DeleteAsync(id, ModelState.AddModelError);
                if (deletedDto == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(deletedDto);
            }
            catch (EntityNotFoundException<Position, int>)
            {
                return NotFound();
            }
        }
    }
}
