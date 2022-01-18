using System.Data;

namespace CapaNegocio
{
    public interface ICN_Productos
    {
        void EditarProd(string nombre, string desc, string marca, string precio, string stock, string id);
        void EliminarPRod(string id);
        void InsertarPRod(string nombre, string desc, string marca, string precio, string stock);
        DataTable MostrarProd();
    }
}