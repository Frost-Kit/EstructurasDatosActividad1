using EddActividad1.Models.Interfaces;

namespace EddActividad1.Models.Entitys;

public sealed class Micro : UnidadTransporte, ITransporte
{
    public int PasajerosAtendidos { get; private set; }
    public double TarifaPasaje { get; private set; }

    public Micro(string placa, string linea, int capacidad, double tarifa) 
        : base(placa, linea, capacidad)
    {
        TarifaPasaje = tarifa;
        PasajerosAtendidos = 0;
    }

    public void RegistrarPasajeros(int cantidad)
    {
        PasajerosAtendidos += cantidad;
    }

    public override string ObtenerResumen()
    {
        return $"Micro Placa: {Placa} | Línea: {Linea} | Pasajeros: {PasajerosAtendidos} | Recaudación: BS {PasajerosAtendidos * TarifaPasaje}";
    }

    // --- Sobrecarga de Operadores ---
    // Operador 1 (+): Suma los pasajeros atendidos de dos micros
    public static int operator +(Micro m1, Micro m2)
    {
        return m1.PasajerosAtendidos + m2.PasajerosAtendidos;
    }

    // Operador 2 (== y !=): Compara si dos micros tienen la misma capacidad
    public static bool operator ==(Micro m1, Micro m2)
    {
        if (ReferenceEquals(m1, null) && ReferenceEquals(m2, null)) return true;
        if (ReferenceEquals(m1, null) || ReferenceEquals(m2, null)) return false;
        return m1.Capacidad == m2.Capacidad;
    }

    public static bool operator !=(Micro m1, Micro m2)
    {
        return !(m1 == m2);
    }

    public override bool Equals(object obj) => obj is Micro micro && Capacidad == micro.Capacidad;
    public override int GetHashCode() => Capacidad.GetHashCode();

    // --- Recursividad ---
    // Suma secuencialmente los minutos de cada parada en la ruta de Tarija
    public int CalcularTiempoRutaRecursivo(int[] tiemposParadas, int indice = 0)
    {
        if (indice >= tiemposParadas.Length)
            return 0; // Caso base

        return tiemposParadas[indice] + CalcularTiempoRutaRecursivo(tiemposParadas, indice + 1);
    }
}
