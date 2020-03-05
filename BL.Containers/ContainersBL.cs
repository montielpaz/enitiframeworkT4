using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Containers
{
    public class ContainersBL
    {
        Contexto _contexto;
        public BindingList<Container> ListaContainers { get; set; }

        public ContainersBL()
        {
            _contexto = new Contexto();
            ListaContainers = new BindingList<Container>();

           

        }

        public BindingList<Container> ObtenerContainers()
        {
            _contexto.container.Load();
            ListaContainers = _contexto.container.Local.ToBindingList();

            return ListaContainers;
        }

        //Instancia

        public Resultado GuardarProducto(Container container)
        {
            var resultado = Validar(container);

            if (resultado.Exitoso == false)
            {
                return resultado; 
            }

            _contexto.SaveChanges();
          
            resultado.Exitoso = true;
            return resultado;
        }
        public void AgregarProducto()
        {
            var nuevoContainer = new Container();
            ListaContainers.Add(nuevoContainer);
        }
        public bool EliminarProducto(int id)
        {
            foreach (var container in ListaContainers)
            {
                if (container.Id == id)
                {
                    ListaContainers.Remove(container);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        //Validacion

        private Resultado Validar(Container container)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (container.PesoNeto < 0) 
            {
                resultado.Mensaje = "Ingrese un peso neto mayor que cero";
                resultado.Exitoso = false;

            }
            if (container.VolumenInterno < 0)
            {
                resultado.Mensaje = "El volumen interno debe ser mayor que cero";
                resultado.Exitoso = false;
            }
            if (container.MasaBrutaMaxima < 0)
            {
                resultado.Mensaje = "La masa bruta maxima debe ser mayor que cero";
                resultado.Exitoso = false;
            }
            if (container.AvisoAltura < 0)
            {
                resultado.Mensaje = "La altura maxima debe ser mayor que cero";
                resultado.Exitoso = false;
            }
            return resultado;
        }
    }

    public class Container
    {
        public int Id { get; set; }
        public string NumeroSerie { get; set; }
        public int MasaBrutaMaxima { get; set; }
        public int PesoNeto { get; set; }
        public int AvisoAltura { get; set; }
        public int VolumenInterno { get; set; }
        public bool Activo { get; set; }

    }
    public class Resultado
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }
}

