using Microsoft.AspNetCore.Mvc;
using EddActividad1.Models.Entitys;

namespace EddActividad1.Controllers;

public class MicroController : Controller
{
    // Lista estática para conservar los datos mientras la app está corriendo
    private static GestorInventario<Micro> gestor = new();
    private static bool datosInicialesCargados = false;

    public MicroController()
    {
        if (!datosInicialesCargados)
        {
            var micro1 = new Micro("1234-ABC", "Línea 1", 30, 2.00);
            var micro2 = new Micro("5678-DEF", "Línea 15", 30, 2.00);
            
            micro1.RegistrarPasajeros(45);
            micro2.RegistrarPasajeros(60);

            gestor.Agregar(micro1);
            gestor.Agregar(micro2);
            datosInicialesCargados = true;
        }
    }

    [HttpGet]
    public IActionResult Index()
    {
        return CargarVista();
    }

    [HttpPost]
    public IActionResult AgregarMicro(string placa, string linea, int capacidad, double tarifa)
    {
        if (!string.IsNullOrEmpty(placa) && !string.IsNullOrEmpty(linea))
        {
            var nuevoMicro = new Micro(placa, linea, capacidad, tarifa);
            gestor.Agregar(nuevoMicro);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult SumarPasajeros(string placa, int cantidad)
    {
        var micro = gestor.ObtenerTodos().Find(m => m.Placa == placa);
        if (micro != null && cantidad > 0)
        {
            micro.RegistrarPasajeros(cantidad);
        }
        return RedirectToAction("Index");
    }

    private IActionResult CargarVista()
    {
        Micro[] flotaArray = gestor.ObtenerTodos().ToArray();
        int totalPasajerosFlota = 0;

        for (int i = 0; i < flotaArray.Length; i++)
        {
            totalPasajerosFlota += flotaArray[i].PasajerosAtendidos;
        }

        // Sobrecarga de operadores y cálculo recursivo con los datos actuales
        int pasajerosM1yM2 = flotaArray.Length >= 2 ? (flotaArray[0] + flotaArray[1]) : 0;
        bool mismaCapacidad = flotaArray.Length >= 2 ? (flotaArray[0] == flotaArray[1]) : false;
        
        int[] tiemposEntreParadas = new int[] { 8, 12, 5, 15 };
        int tiempoTotalRuta = flotaArray.Length > 0 ? flotaArray[0].CalcularTiempoRutaRecursivo(tiemposEntreParadas) : 0;

        ViewBag.TotalPasajerosFlota = totalPasajerosFlota;
        ViewBag.SumaM1M2 = pasajerosM1yM2;
        ViewBag.MismaCapacidad = mismaCapacidad;
        ViewBag.TiempoTotalRuta = tiempoTotalRuta;

        return View("Index", flotaArray);
    }
    
    // Vista interactiva para probar la Sobrecarga de Operadores
    [HttpGet]
    public IActionResult Operadores()
    {
        ViewBag.Micros = gestor.ObtenerTodos();
        return View();
    }

    [HttpPost]
    public IActionResult CalcularOperadores(string placa1, string placa2)
    {
        var micros = gestor.ObtenerTodos();
        var m1 = micros.Find(m => m.Placa == placa1);
        var m2 = micros.Find(m => m.Placa == placa2);

        if (m1 != null && m2 != null)
        {
            // Uso explícito de los operadores sobrecargados
            ViewBag.SumaPasajeros = m1 + m2;          // Operador +
            ViewBag.MismaCapacidad = (m1 == m2);      // Operador ==
            ViewBag.Micro1 = m1;
            ViewBag.Micro2 = m2;
            ViewBag.Calculado = true;
        }

        ViewBag.Micros = micros;
        return View("Operadores");
    }

    // Vista interactiva para probar la Recursividad
    [HttpGet]
    public IActionResult Recursividad()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CalcularRutaRecursiva(string paradasInput)
    {
        if (!string.IsNullOrEmpty(paradasInput))
        {
            // Convierte el texto "8, 12, 5, 15" en un arreglo de enteros
            int[] tiempos = Array.ConvertAll(paradasInput.Split(','), int.Parse);

            var microAuxiliar = new Micro("TEST", "Línea Test", 30, 2.0);
        
            // Llamada al método recursivo
            int tiempoTotal = microAuxiliar.CalcularTiempoRutaRecursivo(tiempos);

            ViewBag.TiemposIngresados = tiempos;
            ViewBag.TiempoTotal = tiempoTotal;
            ViewBag.Calculado = true;
        }

        return View("Recursividad");
    }
}