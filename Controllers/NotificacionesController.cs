using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using ms_notificaciones.Models;
namespace ms_notificaciones.Controllers;


[ApiController]
[Route("[controller]")]
public class NotificacionesController : ControllerBase
{
    [Route("correo")]
    [HttpPost]
    public async Task<ActionResult> EnviarCorreo(ModeloCorreo datos)
    {

        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("jesus.1701912773@ucaldas.edu.co", "jesus salazar");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress(datos.correoDestino, datos.nombreDestino);
        var plainTextContent = "plainTextContent";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("correo enviado a la direccion: " + datos.correoDestino);
        }
        else
        {
            return BadRequest("error al enviar el correo a la direccion" + datos.correoDestino);
        }
    }
}
