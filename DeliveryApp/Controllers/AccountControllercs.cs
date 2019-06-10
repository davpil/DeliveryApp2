using DeliveryApp.Attributes;
using DeliveryApp.Constants;
using DeliveryApp.DTOs;
using DeliveryApp.Enums;
using DeliveryApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class AccountController : SecureController
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public AccountController(SignInManager<UserEntity> signInManager
            , UserManager<UserEntity> userManager)
            : base(new Dictionary<AccessOption, string> {
                { AccessOption.Create,  ActivityNames.UserCr },
                { AccessOption.Delete,  ActivityNames.UserDel },
                { AccessOption.Details, ActivityNames.UserDet },
                { AccessOption.Index,   ActivityNames.UserInd },
                { AccessOption.Update,  ActivityNames.UserEd },
            })
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.UserInd)]
        public async Task<IEnumerable<UserShort>> Index()
        {
            return await _userManager.Users
                    .Where(u => !u.Predefined)
                    .Include(u => u.PersonEntity)
                    .Select(u => new UserShort { ID = u.Id, Name = u.PersonEntity.FullName })
                    .ToListAsync();
        }

        [HttpGet]
        [Route("GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.UserDet)]
        public async Task<IActionResult> GetUser(string id)
        {
            UserEntity user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NoContent();
            }

            Update update = new Update
            {
                ID = user.Id,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user),
            };

            return Ok(update);
        }

        [HttpPost]
        [ValidateModelFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]Login login)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return Ok();
                }
                if (result.RequiresTwoFactor)
                {
                    //return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    //return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError("Login", "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("Logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return Ok();
        }

        [HttpGet]
        [Route("GetUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetUserName()
        {
            UserEntity user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NoContent();
            }

            return Ok(user.UserName);
        }

        [HttpPost]
        [Route("Register")]
        [ValidateModelFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.UserCr, true)]
        public async Task<IActionResult> Register([FromBody]Register register)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = new UserEntity
                {
                    UserName = register.Email,
                    Email = register.Email,
                    PersonEntityID = register.PersonID
                };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");
                    if (register.RoleNames?.Count != null)
                    {
                        await _userManager.AddToRolesAsync(user, register.RoleNames);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/api/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(register.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("General", error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }

        [HttpPut]
        [ValidateModelFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [AuthorizeActivity(ActivityNames.UserEd)]
        public async Task<IActionResult> Update([FromBody]Update update)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = await _userManager.FindByIdAsync(update.ID);
                if (user == null)
                {
                    return NotFound();
                }

                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRolesAsync(user, update.Roles);

                await _userManager.SetEmailAsync(user, update.Email);
                await _userManager.SetUserNameAsync(user, update.Email);
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}
