using System;
using System.Collections.Generic;
using System.Text;
using InterfaceTrafico.Data;
using InterfaceTrafico.Model;

namespace InterfaceTrafico.Services
{
    public class Solicitudes
    {
        public readonly TraficoDBContext _context; 

        public Solicitudes(TraficoDBContext context)
        {
            _context = context;
        }


        public void  Prueba()
        {
            //     var query = context.Set<DbDocument>()
            //.Where(t => partnerIds.Contains(t.SenderId))
            //.Select(t => t.SenderId).Distinct() // <--
            //.SelectMany(key => context.Set<DbDocument>().Where(t => t.SenderId == key) // <--
            //    .OrderByDescending(t => t.InsertedDateTime).Take(10)
            //);

            var x = _context.SolicitudesMovimientos.Find(125);


            //using (TraficoDBContext xcontext = new TraficoDBContext() )
            //{
            //    var Solicitud = xcontext.SolicitudesMovimientos.Find(125);                 
                    
                    
            //        .Profesores //Indicamos la tabla
            //                          .Include(x => x.Cursos) //Incluimos los resultados coincidentes de la tabla cursos (inner join)
            //                          .ThenInclude(x => x.Alumnos) //Incluimos los resultados coincidentes de la tabla alumnos (inner join)
            //                          .First(); //Seleccionamos el primero

            //}







        }





    }
}
