using CapaDatos;
using CapaNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace TestProject
{
    [TestClass]
    public class CN_ProductosTest
    {
        [TestMethod]
        public void MostrarProdTest()
        {
            //declarar datatable colocarla en returns
            DataTable tabla = new DataTable();
            tabla.Columns.Add("nombre", typeof(string));
            tabla.Columns.Add("desc", typeof(string));
            tabla.Columns.Add("marca", typeof(string));
            tabla.Columns.Add("precio", typeof(string));
            tabla.Columns.Add("stock", typeof(string));

            tabla.Rows.Add("Papas", "tamaño mediano", "Lays", "1", "1");

            Mock<CD_Productos> mockProductos = new Mock<CD_Productos>();
            mockProductos.Setup(x => x.Mostrar()).Returns(tabla);
            CN_Productos productos = new CN_Productos(mockProductos.Object);

            var producto = productos.MostrarProd();

            Assert.IsNotNull(producto);

            //debe tener alguna logica
        }

        //[TestMethod]
        //public void InsertarPRodTest()
        //{
        //    Mock<CN_Productos> mockProductos = new Mock<CN_Productos>();
        //    CD_Productos productos = new CD_Productos(mockProductos.Object);

        //    string nombre = "Nombre";
        //    string desc = "Descripcion";
        //    string marca = "Marca";
        //    double precio = 1;
        //    int stock = 1;
        //    //mockProductos.Setup(x => x.Insertar(nombre, desc, marca, precio, stock));

        //    CN_Productos producto = productos.Insertar(nombre, desc, marca, precio, stock);


        //}


    }
}
