using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("/")]
public class ControladorIncial : Controller
{
    public IActionResult PaginaInicial()
    {
        return View("PaginaInicial");
    }
}
