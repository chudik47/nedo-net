﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NedoNet.API.Entities;
using NedoNet.API.Services;

namespace NedoNet.API.Areas.v1.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUsersService _usersService;

        private static readonly string ModelIsNotValidErrorMessage = "Model is not valid";

        public UsersController( IUsersService usersService ) {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUserAsync([FromQuery] Guid id) {

            var result = await _usersService.GetUserAsync( id );

            if ( result.IsSuccess ) {
                return Ok( result.Result );
            }

            return BadRequest( result.Result );
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersPageAsync([FromQuery] int page) {

            var result = await _usersService.GetPageAsync( page );

            if ( result.IsSuccess ) {
                return Ok( result.Result );
            }

            return BadRequest( result.Result );
        }

        [HttpPost]
        public IActionResult CreateUser( [FromBody] CreateUserEntity userEntity ) {
            if ( !ModelState.IsValid ) {
                return BadRequest( ModelIsNotValidErrorMessage );
            }

            var result = _usersService.CreateUser( userEntity );

            if ( result.IsSuccess ) {
                return Ok( result.Result );
            }

            return BadRequest( result.Result );
        }

        [HttpPut]
        public IActionResult UpdateUser([FromQuery] Guid? id, [FromBody] UpdateUserEntity userEntity) {
            if ( !id.HasValue ) {
                return BadRequest("Id must be set");
            }

            if ( !ModelState.IsValid ) {
                return BadRequest( ModelIsNotValidErrorMessage );
            }

            var result = _usersService.UpdateUser(id.Value, userEntity);

            if ( result.IsSuccess ) {
                return Ok( result.Result );
            }

            return BadRequest( result.Result );
        }


    }
}