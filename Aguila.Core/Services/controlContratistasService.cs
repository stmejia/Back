using Aguila.Core.Interfaces.Services;
using Aguila.Core.Entities;
using Aguila.Core.CustomEntities;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Aguila.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Aguila.Core.DTOs;
using Aguila.Core.Interfaces;

namespace Aguila.Core.Services
{
    public class controlContratistasService : IcontrolContratistasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        
        public controlContratistasService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IAguilaMap aguilaMap,
                                          IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _aguilaMap = aguilaMap;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<controlContratistas>> GetControlContratistas(controlContratistasQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var filtroFechas = false;
            var controlContratistas = _unitOfWork.controlContratistasRepository.GetAllIncludes();

            if (filter.idEstacionTrabajo == null)
                throw new AguilaException("Debe especificar una estacion de trabajo", 404);

            if (filter.fechaInicio != null && filter.fechaFin != null)
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);

                filtroFechas = true;
            }

            //FILTRA LOS RESULTADO POR EL RANGO DE FECHAS ENVIADO EN EL FILTER.
            if (filtroFechas)
            {
                controlContratistas = controlContratistas.Where(e => e.ingreso >= filter.fechaInicio && e.ingreso < filter.fechaFin.Value.AddDays(1));
            }
            else
            {
                controlContratistas = controlContratistas.Where(e => e.ingreso.Date == DateTime.Now.Date);
            }

            //Filtra por estacion de trabajo
            controlContratistas = controlContratistas.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);

            //REPORTE DE CONTRASTISTAS EN PREDIO
            if (filter.enPredio != null)
            {
                if ((bool)filter.enPredio)
                {
                   controlContratistas = controlContratistas.Where(e => e.salida == null)
                                .OrderByDescending(e => e.ingreso);

                    var paginados = PagedList<controlContratistas>.create(controlContratistas, filter.PageNumber, filter.PageSize);

                    // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
                    await _imagenesRecursosService.AsignarUrlImagenesDefault(paginados.Select(e => e.DPI).ToList());
                    return paginados;

                }
            }

            if (filter.empresaVisita != null)
            {
                controlContratistas = controlContratistas.Where(e => e.empresaVisita.ToUpper().Trim().Equals(filter.empresaVisita.ToUpper().Trim()));
            }

            if (filter.nombre != null)
            {
                controlContratistas = controlContratistas.Where(e => e.nombre.ToUpper().Trim().Contains(filter.nombre.ToUpper().Trim()));
            }

            if (filter.identificacion != null)
            {
                controlContratistas = controlContratistas.Where(e => e.identificacion.ToUpper().Trim().Contains(filter.identificacion.ToUpper().Trim()));
            }

            if (filter.empresa != null)
            {
                controlContratistas = controlContratistas.Where(e => e.empresa.ToUpper().Trim().Contains(filter.empresa.ToUpper().Trim()));
            }

            if (filter.vehiculo != null)
            {
                controlContratistas = controlContratistas.Where(e => e.vehiculo.ToUpper().Trim().Contains(filter.empresa.ToUpper().Trim()));
            }

            if (filter.ingreso != null)
            {
                controlContratistas = controlContratistas.Where(v => v.ingreso.Date == filter.ingreso.Value.Date);
            }

            if (filter.idUsuario != null)
            {
                controlContratistas = controlContratistas.Where(e => e.idUsuario == filter.idUsuario);
            }

            if (filter.idEstacionTrabajo != null)
            {
                controlContratistas = controlContratistas.Where(e => e.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            controlContratistas = controlContratistas.OrderByDescending(v => v.ingreso);
            var pagedControlContratista = PagedList<controlContratistas>.create(controlContratistas, filter.PageNumber, filter.PageSize);

            // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedControlContratista.Select(e => e.DPI).ToList());
            return pagedControlContratista;
        }

        public async Task<controlContratistas> GetControlContratistas(long id)
        {
            var currentContratista = await _unitOfWork.controlContratistasRepository.GetByIdIncludes(id);
            if (currentContratista == null)
            {
                throw new AguilaException("Contratista no existente");
            }

            //Manejo de imagenes
            if (currentContratista != null && currentContratista.idImagenRecursoDpi != null && currentContratista.idImagenRecursoDpi != Guid.Empty && currentContratista.DPI == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(currentContratista.idImagenRecursoDpi ?? Guid.Empty);
                currentContratista.DPI = imgRecurso;
            }
            //Fin Imagenes

            return currentContratista;
        }

        public async Task InsertControlContratistas(controlContratistas xControlContratistas)
        {
            //Insertamos la fecha de ingreso del registro
            xControlContratistas.id = 0;
            xControlContratistas.ingreso = DateTime.Now;
            xControlContratistas.fechaCreacion = DateTime.Now;
            xControlContratistas.empresaVisita = xControlContratistas.empresaVisita.ToUpper();

            if (xControlContratistas.vehiculo != null)
            {
                if (xControlContratistas.vehiculo.Length < 1)
                {
                    xControlContratistas.vehiculo = "PEATON";
                }
                else
                {
                    xControlContratistas.vehiculo = xControlContratistas.vehiculo.ToUpper().Trim();
                }
            }
            else
            {
                xControlContratistas.vehiculo = "PEATON";
            }

            //Guardamos el recurso de Imagen
            if (xControlContratistas.DPI != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(xControlContratistas.DPI, "controlContratistas", nameof(xControlContratistas.DPI));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    xControlContratistas.idImagenRecursoDpi = imgRecurso.Id;
            }
            //  Fin de recurso de Imagen  

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.controlContratistasRepository.Add(xControlContratistas);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<bool> UpdateControlContratistas(controlContratistas controlContratistas)
        {
            var currentControlContratistas = await _unitOfWork.controlContratistasRepository.GetByID(controlContratistas.id);
            if (currentControlContratistas == null)
            {
                throw new AguilaException("Contratista no existente...");
            }

            currentControlContratistas.nombre = controlContratistas.nombre;
            currentControlContratistas.identificacion = controlContratistas.identificacion;
            currentControlContratistas.empresa = controlContratistas.empresa;
            currentControlContratistas.vehiculo = controlContratistas.vehiculo;
            currentControlContratistas.ingreso = controlContratistas.ingreso;
            currentControlContratistas.salida = controlContratistas.salida;
            currentControlContratistas.empresaVisita = controlContratistas.empresaVisita;

            // Guardamos el Recurso de Imagen
            if (controlContratistas.DPI != null)
            {
                controlContratistas.DPI.Id = currentControlContratistas.idImagenRecursoDpi ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(controlContratistas.DPI, "controlContratistas", nameof(controlContratistas.DPI));

                if (currentControlContratistas.idImagenRecursoDpi == null || currentControlContratistas.idImagenRecursoDpi == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentControlContratistas.idImagenRecursoDpi = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen   

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.controlContratistasRepository.Update(currentControlContratistas);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();
            }

            return true;
        }

        public async Task<bool> DeleteControlContratistas(long id)
        {
            var currentControlContratistas = await _unitOfWork.controlContratistasRepository.GetByID(id);
            if (currentControlContratistas == null)
            {
                throw new AguilaException("Contratista no existente");
            }

            // Eliminamos el recurso de imagen
            if (currentControlContratistas.idImagenRecursoDpi != null && currentControlContratistas.idImagenRecursoDpi != Guid.Empty)
                await _imagenesRecursosService.EliminarImagenRecurso(currentControlContratistas.idImagenRecursoDpi ?? Guid.Empty);
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

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<controlContratistas> darSalida(string identificacion)
        {
            var currentContratista = await _unitOfWork.controlContratistasRepository.GetByIdentificacion(identificacion);
            if (currentContratista == null)
            {
                throw new AguilaException("Contratista no existente o ya se le dio salida");
            }

            currentContratista.salida = DateTime.Now;
            await UpdateControlContratistas(currentContratista);

            return currentContratista;

        }

        public async Task<controlContratistas> contratistaPorId(string identificacion)
        {
            var currentContratista = await _unitOfWork.controlContratistasRepository.GetByIdentificacion(identificacion);
            if (currentContratista == null)
            {
                throw new AguilaException("Contratista no existente o ya se le dio salida");
            }

            return currentContratista;

        }


        //devuelve una visita por su identificacion
        public async Task<controlContratistas> contratistaPorIdGeneric(string identificacion)
        {
            var currentVisita = await _unitOfWork.controlContratistasRepository.GetByIdentificacionGeneric(identificacion);

            //Manejo de imagenes
            if (currentVisita != null && currentVisita.idImagenRecursoDpi != null && currentVisita.idImagenRecursoDpi != Guid.Empty && currentVisita.DPI == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(currentVisita.idImagenRecursoDpi ?? Guid.Empty);
                currentVisita.DPI = imgRecurso;
            }
            //Fin Imagenes

            return currentVisita;

        }

        //devuelve los contratistas que estan aun en predio en un día especifico
        public IEnumerable<controlContratistas> enPredio(DateTime fecha, int estacionTrabajo)
        {
            var contratistas =  _unitOfWork.controlContratistasRepository.GetAll()
                                .Where(e => e.ingreso.Date == fecha.Date && e.idEstacionTrabajo==estacionTrabajo && e.salida == null)
                                .OrderByDescending(e => e.ingreso);
            

            return contratistas.AsEnumerable();
        }
    }
}
