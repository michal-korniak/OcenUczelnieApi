using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace OcenUczelnie.Api.Controllers
{
    [Route("[controller]")]
    public abstract class BaseApiController: Controller
    {
        public Guid GetCurrentUserId()
        {
            Claim guidClaim = User.Claims.SingleOrDefault(x => x.Type.EndsWith("nameidentifier"));
            if(guidClaim==null)
                throw new Exception("No one is logged in.");
            var id = Guid.Parse(guidClaim.Value);
            return id;
        }
    }
}