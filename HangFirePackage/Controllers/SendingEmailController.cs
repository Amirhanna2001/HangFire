using Hangfire;
using HangFirePackage.DTOs;
using HangFirePackage.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangFirePackage.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SendingEmailController : ControllerBase
{

    private readonly IEmailServices _emailServices;

    public SendingEmailController(IEmailServices emailServices)
    {
        _emailServices = emailServices;
    }
    [HttpGet]
    public IActionResult SendEmailImmediately(string email)
    {
        EmailDto dto = new()
        {
            Body = "Hello this is the report just a test message .",
            To = email,
        };
        BackgroundJob.Enqueue(()=>_emailServices.SendEmail(dto));
        return Ok("Mail sent successfully");
    }
    [HttpGet("afterMins")]
    public IActionResult SendEmailAfter5Min(string email)
    {
        EmailDto dto = new()
        {
            Body = $"Hello this is the report just a test message time when call this endpoint is {DateTime.Now} .",
            To = email,
        };
        BackgroundJob.Schedule(()=>_emailServices.SendEmail(dto),TimeSpan.FromMinutes(5));
        return Ok("Mail will be sent after 5 mins");
    }
    [HttpGet("everyDay")]
    public IActionResult SendEmailEveryDay(string email)
    {
        EmailDto dto = new()
        {
            Body = $"Hello this is the report just a test message time when call this endpoint is {DateTime.Now} .",
            To = email,
        };
        RecurringJob.AddOrUpdate(() => _emailServices.SendEmail(dto), Cron.Daily(20));
        return Ok("Mail will be sent every day at 8 PM");
    }
}