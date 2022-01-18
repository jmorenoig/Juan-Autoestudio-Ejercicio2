using System.Data;

namespace CapaDatos
{
    public interface ICD_Productos
    {
        void Editar(string nombre, string desc, string marca, double precio, int stock, int id);
        void Eliminar(int id);
        void Insertar(string nombre, string desc, string marca, double precio, int stock);
        DataTable Mostrar();
    }
}