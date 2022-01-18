using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Productos : ICN_Productos
    {
        
        public CN_Productos()
        {
            objetoCD = new CD_Productos();
        }
        private ICD_Productos objetoCD;
        public CN_Productos(ICD_Productos paramobjetoCD)
        {

            if (paramobjetoCD == null)
            {
                objetoCD = new CD_Productos();
            }
            else
            {
                objetoCD = paramobjetoCD;
            }
        }
        public virtual DataTable MostrarProd()
        {
            
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public void InsertarPRod(string nombre, string desc, string marca, string precio, string stock)
        {

            objetoCD.Insertar(nombre, desc, marca, Convert.ToDouble(precio), Convert.ToInt32(stock));
        }

        public void EditarProd(string nombre, string desc, string marca, string precio, string stock, string id)
        {
            objetoCD.Editar(nombre, desc, marca, Convert.ToDouble(precio), Convert.ToInt32(stock), Convert.ToInt32(id));
        }
        public void EliminarPRod(string id)
        {

            objetoCD.Eliminar(Convert.ToInt32(id));
        }

    }
}
