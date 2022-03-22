using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class empleadosIngresosService : IempleadosIngresosService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IempleadosService _empleadosService;

        public empleadosIngresosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,IempleadosService empleadosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _empleadosService = empleadosService;
        }

        public PagedList<empleadosIngresos> getIngresos(empleadosIngresosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var filtroFechas = false;
            var ingresos = _unitOfWork.empleadosIngresosRepository.GetAllIncludes();


            if (filter.idEstacionTrabajo == null)
                throw new AguilaException("Debe Especificar un predio...", 404);

            if (filter.fechaInicio != null && filter.fechaFin != null)
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);

                filtroFechas = true;
            }

            //FILTRA LOS RESULTADO POR EL RANGO DE FECHAS ENVIADO EN EL FILTER.
            if (filtroFechas)
            {

                ingresos = ingresos.Where(e => e.fechaEvento >= filter.fechaInicio && e.fechaEvento < filter.fechaFin.Value.AddDays(1));
            }
            else
            {
                ingresos = ingresos.Where(e => e.fechaEvento.Date == DateTime.Now.Date);
            }

            //Filtra por estacion de trabajo
            ingresos = ingresos.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);

            if (filter.cui != null)
            {
                ingresos = ingresos.Where(e => e.cui.ToLower().Trim().Contains(filter.cui.ToLower().Trim()));
            }

            if (filter.evento != null)
            {
                ingresos = ingresos.Where(e => e.evento.ToLower().Trim().Equals(filter.evento.ToLower().Trim()));
            }

            if (filter.vehiculo != null)
            {
                ingresos = ingresos.Where(e => e.vehiculo.ToLower().Trim().Contains(filter.vehiculo.ToLower().Trim()));
            }

            //REPORTE DE HORA DE MARCAJE (EMPLEADOS QUE ENTRARON ANTES O DESPUES DE UNA HORA ESPECIFICA EN UN DIA DETERMINADO)

            if (filter.hora != null)
            {
                if (filter.antesDe == null)
                {
                    throw new AguilaException("Para poder filtrar por hora debe indicar antes de o despues de...");
                }

                if (filter.evento == null)
                {
                    throw new AguilaException("Para poder filtrar por hora debe indicar el evento a reportar...");
                }

                DateTime horaEvento = new DateTime();
                horaEvento = DateTime.Now;
                string[] valores = filter.hora.Split(':');

                var hora = valores[0];
                var minutos = valores[1];

                TimeSpan ts = new TimeSpan(int.Parse(hora), int.Parse(minutos), 0);
                horaEvento = horaEvento.Date + ts;

                if (!(bool)filter.antesDe)
                {
                    ingresos = ingresos.Where(e => e.fechaEvento.TimeOfDay >= horaEvento.TimeOfDay);
                }
                else { ingresos = ingresos.Where(e => e.fechaEvento.TimeOfDay <= horaEvento.TimeOfDay); }
            }
            //            

            ingresos = ingresos.OrderByDescending(v => v.fechaEvento);

            var pagedIngresos = PagedList<empleadosIngresos>.create(ingresos, filter.PageNumber, filter.PageSize);
            return pagedIngresos;
        }

        ////REPORTE DE AUSENCIAS
        //public IEnumerable<empleados> getAusencias(reporteAusenciasQueryFilter filter)
        //{                

        //    var fecha = new DateTime(2022,02,11);

        //    var listadoIngresos = _unitOfWork.empleadosIngresosRepository.GetAll().Where(e => e.fechaEvento >= fecha && e.fechaEvento < fecha.AddDays(1));
        //    var listadoEmpleados = _unitOfWork.empleadosRepository.GetAll().Where(e=>e.idEmpresa == filter.idEmpresa && e.localidad.Trim().ToLower().Equals(filter.localidad.Trim().ToLower()));

        //    if (filter.cui != null)
        //    {
        //        listadoEmpleados = listadoEmpleados.Where(e=>e.codigo.Contains(filter.cui));
        //    }

        //    if (filter.evento != null)
        //    {
        //        listadoIngresos = listadoIngresos.Where(e=>e.evento.Equals(filter.evento.Trim().ToUpper()));
        //    }


        //    var query = from empleado in listadoEmpleados.ToList()
        //                join ingreso in listadoIngresos.ToList() on empleado equals ingreso.empleado into myLeftJoin
        //                from subIngreso in myLeftJoin.DefaultIfEmpty()
        //                select new { empleado.id, empleado.codigo, empleado.nombres, ingresa = subIngreso?.evento ?? String.Empty };

        //    var empleadosAusentes = new List<empleados>();
        //    query = query.Where(e=>string.IsNullOrEmpty(e.ingresa));
            

        //    foreach(var emp in query)
        //    {              
        //            var employee = new empleados() { id = emp.id, codigo=emp.codigo, nombres = emp.nombres };
        //            empleadosAusentes.Add(employee);           
        //    }
                     
        //   return empleadosAusentes.AsEnumerable();
        //}

        public async Task<empleadosIngresos> GetIngreso(long id)
        {
            var currenIngreso = await _unitOfWork.empleadosIngresosRepository.GetByIdIncludes(id);
            if (currenIngreso == null)
            {
                throw new AguilaException("Ingreso no existente...");
            }

            return currenIngreso;
        }

        public async Task InsertIngreso(empleadosIngresos ingreso, Boolean validar)
        {
            //valida si hay que verificar el evento
            if (validar)
            {
                var msgError = "";
                var result = validarEvento(ingreso.evento.ToUpper().Trim(), ingreso.idEmpleado);
                if (!result)
                {
                    switch (ingreso.evento.ToUpper().Trim())
                    {
                        case "INGRESO": msgError = "EL USUARIO NO TIENE REGISTRADA UNA SALIDA.";

                            break;

                        case "SALIDA":
                            msgError = "EL USUARIO NO TIENE REGISTRADO UN INGRESO.";
                            break;
                    }

                    throw new AguilaException(msgError);
                }
            }

            ingreso.id = 0; 
           
            ingreso.fechaEvento = DateTime.Now;
            ingreso.fechaCreacion = DateTime.Now;
            ingreso.evento = ingreso.evento.ToUpper().Trim();
            if (ingreso.vehiculo == null)
                ingreso.vehiculo = "PEATON";
            else
                ingreso.vehiculo = ingreso.vehiculo.ToUpper().Trim();

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.empleadosIngresosRepository.Add(ingreso);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new AguilaException("Error al Registrar Evento: " + ex.InnerException.Message);

            }
        }

        public async Task<bool> UpdateIngreso(empleadosIngresos ingreso)
        {
            var currentIngreso = await _unitOfWork.empleadosIngresosRepository.GetByID(ingreso.id);
            if (currentIngreso == null)
            {
                throw new AguilaException("Ingreso no existente...");
            }

            currentIngreso.evento = ingreso.evento.ToUpper().Trim();

            if (!String.IsNullOrEmpty(ingreso.vehiculo)) {
                currentIngreso.vehiculo = ingreso.vehiculo.ToUpper().Trim();
            }
            else
            {
                currentIngreso.vehiculo = null;
            }
            

            currentIngreso.fechaEvento = ingreso.fechaEvento;



            _unitOfWork.BeginTransaction();

            try
            {
                _unitOfWork.empleadosIngresosRepository.Update(currentIngreso);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {

                _unitOfWork.RollbackTransaction();
                throw new AguilaException("Error al Actualizar Evento: " + ex.InnerException.Message);
            }

            return true;
        }

        public async Task<bool> DeleteVisita(long id)
        {
            var currentIngreso = await _unitOfWork.empleadosIngresosRepository.GetByID(id);
            if (currentIngreso == null)
            {
                throw new AguilaException("Ingreso no existente");
            }

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.controlVisitasRepository.Delete(id);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {

                _unitOfWork.RollbackTransaction();
                throw new AguilaException("Error al Eliminar Evento: " + ex.InnerException.Message);
            }

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public Boolean validarEvento(string evento, int idEmpleado )
        {
            var ultimoEvento = _unitOfWork.empleadosIngresosRepository.GetAll()
                                    .Where(e=>e.idEmpleado == idEmpleado && e.fechaEvento.Date == DateTime.Now.Date)
                                    .OrderByDescending(e=>e.fechaEvento)
                                    .FirstOrDefault();

            if (ultimoEvento == null )
            {
                if (evento.Equals("SALIDA")) { return false; }
                else { return true; }

                
            }else if(ultimoEvento.evento.Equals("INGRESO") && evento.Equals("SALIDA"))
                    {
                         return true;
            }else if (ultimoEvento.evento.Equals("SALIDA") && evento.Equals("INGRESO"))
                    {
                        return true;
                    }

            return false;

        }
    }
}
