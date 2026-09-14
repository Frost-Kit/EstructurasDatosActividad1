namespace EddActividad1.Models.Entitys;

public abstract class UnidadTransporte
{
    public string Placa { get; protected set; }
    public string Linea { get; protected set; }
    public int Capacidad { get; protected set; }

    protected UnidadTransporte(string placa, string linea, int capacidad)
    {
        Placa = placa;
        Linea = linea;
        Capacidad = capacidad;
    }

    public abstract string ObtenerResumen();
}