namespace EddActividad1.Models.Entitys;

public class GestorInventario<T>
{
    private List<T> elementos = new List<T>();

    public void Agregar(T elemento) => elementos.Add(elemento);
    public List<T> ObtenerTodos() => elementos;
    public T ObtenerElemento(int indice) => elementos[indice];
}