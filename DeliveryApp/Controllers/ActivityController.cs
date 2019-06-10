using DeliveryApp.Attributes;
using DeliveryApp.Constants;
using DeliveryApp.DTOs;
using DeliveryApp.Enums;
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
    [Route("api/controller")]
    [ApiController]
    [Authorize]
    public sealed class ActivityController:SecureController
    {
        private readonly IActivityService _service;

        public ActivityController(IActivityService service)
            :base(new Dictionary<AccessOption, string> {
                { AccessOption.Create,  ActivityNames.UserCr },
                { AccessOption.Delete,  ActivityNames.UserDel },
                { AccessOption.Details, ActivityNames.UserDet },
                { AccessOption.Index,   ActivityNames.UserInd },
                { AccessOption.Update,  ActivityNames.UserEd },
            })
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(new string[] { ActivityNames.UserDel, ActivityNames.UserDet, ActivityNames.UserEd, ActivityNames.UserCr })]
        public async Task<IEnumerable<Activity>> Index()
        {
            return await _service.GetAllAsync();
        }
    }
}
