﻿using System.Web.Http;

namespace smapi.Controllers
{
  public class AuthenticationController : BaseController {
    [Authorize]
    [HttpGet]
    public IHttpActionResult TestAuthentication() {
      return Ok(new { Id = CurrentUserId, Username = CurrentUsername, Role = CurrentRole });
    }
  }
}
