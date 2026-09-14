namespace EddActividad1.Models.Interfaces;

public interface ITransporte
{
    void RegistrarPasajeros(int cantidad);
    string ObtenerResumen();
}