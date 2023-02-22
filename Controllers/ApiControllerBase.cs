namespace NETCoreDemo.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]s")]
public abstract class ApiControllerBase : ControllerBase
{
}