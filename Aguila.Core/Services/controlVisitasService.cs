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
    public class controlVisitasService : IcontrolVisitasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public controlVisitasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                     IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<controlVisitas>> GetVisitas(controlVisitasQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var filtroFechas = false;
            var visitas = _unitOfWork.controlVisitasRepository.GetAllIncludes();

            if(filter.idEstacionTrabajo == null)
                throw new AguilaException("Debe Especificar una estacion de trabajo.", 404);

            if (filter.fechaInicio != null && filter.fechaFin != null)
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);

                filtroFechas = true;
            }

            //FILTRA LOS RESULTADO POR EL RANGO DE FECHAS ENVIADO EN EL FILTER.
            if (filtroFechas)           {
                
                visitas = visitas.Where(e => e.ingreso >= filter.fechaInicio && e.ingreso < filter.fechaFin.Value.AddDays(1));
            }
            else
            {
                visitas = visitas.Where(e => e.ingreso.Date == DateTime.Now.Date);
            }

            //Filtra por estacion de trabajo
            visitas = visitas.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);

            //REPORTE DE VISITAS EN PREDIO
            if (filter.enPredio != null)
            {
                if ((bool)filter.enPredio)
                {
                    visitas = visitas.Where(e => e.salida == null)
                                 .OrderByDescending(e => e.ingreso);

                    var paginados = PagedList<controlVisitas>.create(visitas, filter.PageNumber, filter.PageSize);

                    // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
                    await _imagenesRecursosService.AsignarUrlImagenesDefault(paginados.Select(e => e.DPI).ToList());
                    return paginados;

                }
            }

            if (filter.empresaVisita != null)
            {
                visitas = visitas.Where(e=>e.empresaVisita.ToLower().Trim().Equals(filter.empresaVisita.ToLower().Trim()));
            }

            if (filter.nombre != null)
            {
                visitas = visitas.Where(v => v.nombre.ToLower().Trim().Contains(filter.nombre.ToLower().Trim()));
            }

            if (filter.identificacion != null)
            {
                visitas = visitas.Where(v => v.identificacion.ToLower().Trim().Contains(filter.identificacion.ToLower().Trim()));
            }

            if (filter.motivoVisita != null)
            {
                visitas = visitas.Where(v => v.motivoVisita.ToLower().Trim().Contains(filter.motivoVisita.ToLower().Trim()));
            }

            if (filter.vehiculo != null)
            {
                visitas = visitas.Where(v => v.vehiculo.ToLower().Trim().Contains(filter.vehiculo.ToLower().Trim()));
            }

            if (filter.ingreso != null)
            {
                visitas = visitas.Where(v => v.ingreso.Date == filter.ingreso.Value.Date);
            }

            if (filter.idUsuario != null)
            {
                visitas = visitas.Where(v => v.idUsuario == filter.idUsuario);
            }

            if (filter.areaVisita != null)
            {
                visitas = visitas.Where(v => v.areaVisita.ToLower().Trim().Contains(filter.areaVisita.ToLower().Trim()));
            }

            if (filter.nombreQuienVisita != null)
            {
                visitas = visitas.Where(v => v.nombreQuienVisita.ToLower().Trim().Contains(filter.nombreQuienVisita.ToLower().Trim()));
            }

            visitas = visitas.OrderByDescending(v => v.ingreso);
            var pagedVisitas = PagedList<controlVisitas>.create(visitas, filter.PageNumber, filter.PageSize);

            // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedVisitas.Select(e => e.DPI).ToList());
            return pagedVisitas;
        }


        public async Task<controlVisitas> GetVisita(long id)
        {
            var currentVisita = await _unitOfWork.controlVisitasRepository.GetByIdIncludes(id);
            if (currentVisita == null)
            {
                throw new AguilaException("Visita no existente...");
            }

            //Manejo de imagenes
            if (currentVisita != null && currentVisita.idImagenRecursoDpi != null && currentVisita.idImagenRecursoDpi != Guid.Empty && currentVisita.DPI == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(currentVisita.idImagenRecursoDpi ?? Guid.Empty);
                currentVisita.DPI = imgRecurso;
            }
            //Fin Imagenes

            return currentVisita;
        }

        public async Task InsertVisita(controlVisitas visita)
        {
            visita.id = 0;
            visita.fechaCreacion = DateTime.Now;
            visita.ingreso = DateTime.Now;
            visita.nombre = visita.nombre.ToUpper().Trim();
            visita.identificacion = visita.identificacion.ToUpper().Trim();
            visita.motivoVisita = visita.motivoVisita.ToUpper().Trim();
            visita.areaVisita = visita.areaVisita.ToUpper().Trim();
            visita.nombreQuienVisita = visita.nombreQuienVisita.ToUpper().Trim();
            visita.empresaVisita = visita.empresaVisita.ToUpper().Trim();
            visita.idImagenRecursoDpi = visita.idImagenRecursoDpi;


            if (visita.vehiculo!= null)
            {
                if (visita.vehiculo.Length < 1)
                {
                    visita.vehiculo = "PEATON";
                }
                else
                {
                    visita.vehiculo = visita.vehiculo.ToUpper().Trim();
                }

            }
            else
            {
                visita.vehiculo = "PEATON";
            }

            //Guardamos el recurso de iamgen
            if (visita.idImagenRecursoDpi == null && visita.DPI != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(visita.DPI, "controlVisitas", nameof(visita.DPI));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    visita.idImagenRecursoDpi = imgRecurso.Id;
            }
            //  Fin de recurso de Imagen  


            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.controlVisitasRepository.Add(visita);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                _unitOfWork.RollbackTransaction();
            }

        }

        public async Task<bool> UpdateVisita(controlVisitas visita)
        {
            var currentVisita = await _unitOfWork.controlVisitasRepository.GetByID(visita.id);
            if (currentVisita == null)
            {
                throw new AguilaException("Visita no existente...");
            }

            currentVisita.nombre = visita.nombre.ToUpper().Trim();
            currentVisita.identificacion = visita.identificacion.ToUpper().Trim();
            currentVisita.motivoVisita = visita.motivoVisita.ToUpper().Trim();
            currentVisita.areaVisita = visita.areaVisita.ToUpper().Trim();
            currentVisita.nombreQuienVisita = visita.nombreQuienVisita.ToUpper().Trim();
            currentVisita.vehiculo = visita.vehiculo.ToUpper().Trim();
            currentVisita.empresaVisita = visita.empresaVisita.ToUpper().Trim();
            currentVisita.ingreso = visita.ingreso;
            currentVisita.salida = visita.salida;

            // Guardamos el Recurso de Imagen
            if (visita.DPI != null)
            {
                visita.DPI.Id = currentVisita.idImagenRecursoDpi ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(visita.DPI, "controlVisitas", nameof(visita.DPI));

                if (currentVisita.idImagenRecursoDpi == null || currentVisita.idImagenRecursoDpi == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentVisita.idImagenRecursoDpi = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen     

            _unitOfWork.BeginTransaction();

            try
            {
                _unitOfWork.controlVisitasRepository.Update(currentVisita);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                _unitOfWork.RollbackTransaction();
            }

            return true;
        }

        public async Task<bool> DeleteVisita(long id)
        {
            var currentVisita = await _unitOfWork.controlVisitasRepository.GetByID(id);
            if (currentVisita == null)
            {
                throw new AguilaException("Visita no existente");
            }

            // Eliminamos el recurso de imagen
            if (currentVisita.idImagenRecursoDpi != null && currentVisita.idImagenRecursoDpi != Guid.Empty)
                await _imagenesRecursosService.EliminarImagenRecurso(currentVisita.idImagenRecursoDpi ?? Guid.Empty);
            // fin recurso imagen

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.controlVisitasRepository.Delete(id);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                _unitOfWork.RollbackTransaction();
            }

            return true;
        }

        public async Task<controlVisitas> darSalida(string identificacion)
        {


            var currentVisita = await _unitOfWork.controlVisitasRepository.GetByIdentificacion(identificacion);
            if (currentVisita == null)
            {
                throw new AguilaException("Visita no existente o ya se le dio salida");
            }

            currentVisita.salida = DateTime.Now;
            await UpdateVisita(currentVisita);

            return currentVisita;

        }

        // //devuelve una visita por su identificacion siempre y cuando tenga la salida pendiente
        public async Task<controlVisitas> visitaPorId(string identificacion)
        {
            var currentVisita = await _unitOfWork.controlVisitasRepository.GetByIdentificacion(identificacion);
            if (currentVisita == null)
            {
                throw new AguilaException("Visita no existente o ya se le dio salida");
            }

            //Manejo de imagenes
            if (currentVisita != null && currentVisita.idImagenRecursoDpi != null && currentVisita.idImagenRecursoDpi != Guid.Empty && currentVisita.DPI == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(currentVisita.idImagenRecursoDpi ?? Guid.Empty);
                currentVisita.DPI = imgRecurso;
            }
            //Fin Imagenes

            return currentVisita;

        }

        //devuelve una visita por su identificacion
        public async Task<controlVisitas> visitaPorIdGeneric(string identificacion)
        {
            var currentVisita = await _unitOfWork.controlVisitasRepository.GetByIdentificacionGeneric(identificacion);

            //Manejo de imagenes
            if (currentVisita != null && currentVisita.idImagenRecursoDpi != null && currentVisita.idImagenRecursoDpi != Guid.Empty && currentVisita.DPI == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(currentVisita.idImagenRecursoDpi ?? Guid.Empty);
                currentVisita.DPI = imgRecurso;
            }
            //Fin Imagenes

            return currentVisita;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        //devuelve las visitas que estan aun en predio en un día especifico
        public IEnumerable<controlVisitas> enPredio(DateTime fecha, int estacionTrabajo)
        {
            var visitas = _unitOfWork.controlVisitasRepository.GetAll()
                                .Where(e => e.ingreso.Date == fecha.Date && e.idEstacionTrabajo == estacionTrabajo && e.salida == null)
                                .OrderByDescending(e => e.ingreso);


            return visitas.AsEnumerable();
        }

    }
}
