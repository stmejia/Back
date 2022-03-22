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

namespace Aguila.Core.Services
{
    public class activoOperacionesService : IactivoOperacionesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IestadosService _estadosService;
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;
        

        public activoOperacionesService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IactivoMovimientosService activoMovimientosService,
                                        IestadosService estadosService, IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoMovimientosService = activoMovimientosService;
            _estadosService = estadosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<activoOperaciones>> GetActivoOperaciones(activoOperacionesQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var activoOperaciones = _unitOfWork.activoOperacionesRepository.GetAllIncludes();

            if (filter.codigo != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.codigoInit != null)//filtra por el inicio del codigo , es decir el prefijo del codigo definino en el tipo de vehiculo
            {
                activoOperaciones = activoOperaciones.Where(e => e.codigo.ToLower().StartsWith(filter.codigoInit.ToLower()));
            }

            if (filter.descripcion != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.descripcion.ToLower().Contains(filter.descripcion));
            }

            //if (filter.fechaBaja != null)
            //{
            //    activoOperaciones = activoOperaciones.Where(e => e.fechaBaja == filter.fechaBaja);
            //}

            if (filter.categoria != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.categoria.ToLower().Contains(filter.categoria.ToLower()));
            }

            if (filter.color != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.color.ToLower().Contains(filter.color));
            }

            if (filter.marca != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.marca.ToLower().Contains(filter.marca));
            }

            if (filter.vin != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.vin.ToLower().Contains(filter.vin));
            }

            if (filter.correlativo != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.correlativo == filter.correlativo);
            }

            if (filter.serie != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.serie.ToLower().Contains(filter.serie));
            }

            if (filter.modeloAnio != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.modeloAnio == filter.modeloAnio);
            }

            if (filter.idActivoGenerales != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.idActivoGenerales == filter.idActivoGenerales);
            }

            if (filter.idTransporte != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.idTransporte == filter.idActivoGenerales);
            }

            if (filter.flota != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.flota.ToLower().Contains(filter.flota));
            }

            if (filter.idEmpresa != null)
            {
                activoOperaciones = activoOperaciones.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            var pagedActivoOperaciones = PagedList<activoOperaciones>.create(activoOperaciones, filter.PageNumber, filter.PageSize);

            // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedActivoOperaciones.Select(e => e.Fotos).ToList());
            return pagedActivoOperaciones;
        }

        public async Task<activoOperaciones> GetActivoOperacion(int id)
        {
            var currentActivo = await _unitOfWork.activoOperacionesRepository.GetByID(id);
            if (currentActivo == null)
                throw new AguilaException("Activo no existente");

            //Manejo de imagenes
            if (currentActivo != null && currentActivo.idImagenRecursoFotos != null && currentActivo.idImagenRecursoFotos != Guid.Empty && currentActivo.Fotos == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(currentActivo.idImagenRecursoFotos ?? Guid.Empty);
                currentActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes

            //return await _unitOfWork.activoOperacionesRepository.GetByID(id);
            return currentActivo;
        }

        public async Task<ingresoDto> GetActivoOperacionCodigo(activoOperaciones activo)
        {
            string placa = null;
            int idPiloto = 0;
            string piloto = null;
            long condicion = 0;
            string transporte = null;
            

            switch (activo.categoria.ToLower())
            {
                case "v":
                    var vehiculo = await _unitOfWork.vehiculosRepository.GetByID(activo.id);
                    placa = vehiculo.placa;
                    var movimiento = await _unitOfWork.activoMovimientosActualRepository.GetByID(activo.id);
                    var pilot = await _unitOfWork.empleadosRepository.GetByID(movimiento.idEmpleado);
                    piloto = pilot.nombres;
                    idPiloto = pilot.id;
                    condicion = (long)movimiento.documento;
                    var transP = await _unitOfWork.transportesRepository.GetByID(activo.idTransporte);
                    transporte = transP.nombre;
                    break;
                case "e":
                    var equipo = await _unitOfWork.equipoRemolqueRepository.GetByID(activo.id);
                    placa = equipo.placa;
                    movimiento = await _unitOfWork.activoMovimientosActualRepository.GetByID(activo.id);
                    pilot = await _unitOfWork.empleadosRepository.GetByID(movimiento.idEmpleado);
                    piloto = pilot.nombres;
                    idPiloto = pilot.id;
                    condicion = (long)movimiento.documento;
                    transP = await _unitOfWork.transportesRepository.GetByID(activo.idTransporte);
                    transporte = transP.nombre;
                    break;
                case "g":
                    movimiento = await _unitOfWork.activoMovimientosActualRepository.GetByID(activo.id);
                    pilot = await _unitOfWork.empleadosRepository.GetByID(movimiento.idEmpleado);
                    piloto = pilot.nombres;
                    idPiloto = pilot.id;
                    condicion = (long)movimiento.documento;
                    transP = await _unitOfWork.transportesRepository.GetByID(activo.idTransporte);
                    transporte = transP.nombre;
                    break;
                default:
                    break;
            }

            var ingreso = new ingresoDto() {
                idActivo = activo.id,
                idEmpresa = activo.idEmpresa,
                placa = placa,
                piloto = piloto,
                condicion = condicion,
                tipoEquipo = activo.descripcion,
                flota = activo.flota,
                transporte = transporte,
                idPiloto = idPiloto
            };

            return ingreso;
        }

        public async Task InsertActivoOperacion(activoOperaciones activoOperacion)
        {
            //Insertamos la fecha de ingreso del registro
            activoOperacion.id = 0;
            activoOperacion.fechaCreacion = DateTime.Now;
            activoOperacion.color = activoOperacion.color.ToUpper().Trim();
            activoOperacion.vin = activoOperacion.vin.ToUpper().Trim();
            activoOperacion.serie = activoOperacion.serie.ToUpper().Trim();
            activoOperacion.modeloAnio = activoOperacion.modeloAnio;
            activoOperacion.idActivoGenerales = activoOperacion.idActivoGenerales;
            activoOperacion.idImagenRecursoFotos = activoOperacion.idImagenRecursoFotos;

            //Guardamos el recurso de Imagen
            if (activoOperacion.idImagenRecursoFotos == null && activoOperacion.Fotos != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(activoOperacion.Fotos, "activoOperaciones", nameof(activoOperacion.Fotos));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    activoOperacion.idImagenRecursoFotos = imgRecurso.Id;
            }
            //  Fin de recurso de Imagen  

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.activoOperacionesRepository.Add(activoOperacion);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.RollbackTransaction();
            }
            
        }

        //public async Task<ingresoDto> InsertActivoOperacionCodigo(ingresoDto ingreso, int usuario)
        //{

        //    activoMovimientos movimiento = new activoMovimientos()
        //    {
        //        idActivo = ingreso.idActivo,
        //        idEstacionTrabajo = ingreso.idEstacionTrabajo,
        //        idPiloto = ingreso.idPiloto,
        //        idUsuario = usuario,
        //        cargado = ingreso.cargado,
        //        fecha = DateTime.Now,
        //        fechaCreacion = DateTime.Now
        //    };

        //    //Evaluar si viene el Id del estado


        //    estadosQueryFilter filter = new estadosQueryFilter()
        //    {
        //        idEmpresa = ingreso.idEmpresa,
        //        tipo = "activoEstados"
        //    };

        //    if (ingreso.tipoMovimiento)
        //    {
        //        //Ingreso de equipo
        //        filter.evento = "INGRESO";
        //        movimiento.tipoDocumento = "INGRESO";
        //        movimiento.observaciones = "INGRESO A PREDIO POR: " + ingreso.guardiaNombre + " - Observaciones: " + ingreso.observaciones;
        //        movimiento.documento = ingreso.condicion;
        //    }
        //    else
        //    {
        //        //Salida de equipo
        //        filter.evento = "SALIDA";
        //        movimiento.tipoDocumento = "SALIDA";
        //        movimiento.observaciones = "SALIDA DE PREDIO POR: " + ingreso.guardiaNombre + " - Observaciones: " + ingreso.observaciones;
        //        movimiento.documento = ingreso.condicion;
        //    }

        //    var nombreEstacion = (await _unitOfWork.EstacionesTrabajoRepository.GetByID(ingreso.idEstacionTrabajo)).Nombre;
        //    var estado = _estadosService.GetEstados(filter);

        //    var xestadoId = _estadosService.GetEstadoByEvento(ingreso.idEmpresa, "activoEstados", ingreso.tipoEquipo.ToString()).Id;


        //    //movimiento.observaciones += nombreEstacion;
        //    movimiento.idEstado = estado.FirstOrDefault().id;
        //    movimiento.lugar = nombreEstacion;

        //    await _activoMovimientosService.InsertActivoMovimiento(movimiento);
        //    return ingreso;
        //}

        public async Task<bool> UpdateActivoOperacion(activoOperaciones activoOperacion)
        {
            var currentActivo = await _unitOfWork.activoOperacionesRepository.GetByID(activoOperacion.id);
            if (currentActivo == null)
            {
                throw new AguilaException("Activo no existente...");
            }

            currentActivo.descripcion = activoOperacion.descripcion;
            currentActivo.fechaBaja = activoOperacion.fechaBaja;
            currentActivo.categoria = activoOperacion.categoria;
            currentActivo.color = activoOperacion.color;
            currentActivo.marca = activoOperacion.marca;            
            currentActivo.vin = activoOperacion.vin;
            currentActivo.serie = activoOperacion.serie;
            currentActivo.modeloAnio = activoOperacion.modeloAnio;
            currentActivo.idActivoGenerales = activoOperacion.idActivoGenerales;
            currentActivo.idTransporte = activoOperacion.idTransporte;

            // Guardamos el Recurso de Imagen
            if (activoOperacion.Fotos != null)
            {
                activoOperacion.Fotos.Id = currentActivo.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(activoOperacion.Fotos, "activoOperaciones", nameof(activoOperacion.Fotos));

                if (currentActivo.idImagenRecursoFotos == null || currentActivo.idImagenRecursoFotos == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentActivo.idImagenRecursoFotos = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen   

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.activoOperacionesRepository.Update(currentActivo);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                _unitOfWork.RollbackTransaction();
            }

            return true;
        }

        public async Task<bool> DeleteActivoOperacion(int id)
        {
            var currentActivo = await _unitOfWork.activoOperacionesRepository.GetByID(id);
            if (currentActivo == null)
            {
                throw new AguilaException("Activo no existente...");
            }

            // Eliminamos el recurso de imagen
            if (currentActivo.idImagenRecursoFotos != null && currentActivo.idImagenRecursoFotos != Guid.Empty)
                await _imagenesRecursosService.EliminarImagenRecurso(currentActivo.idImagenRecursoFotos ?? Guid.Empty);
            // fin recurso imagen

            _unitOfWork.BeginTransaction();
            try
            {
                await _unitOfWork.activoOperacionesRepository.Delete(id);
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

        public int getStatusNuevo(byte empresa, string tipoE, int orden)
        {
            estadosQueryFilter filter = new estadosQueryFilter()
            {
                idEmpresa = empresa,
                tipo = tipoE,
                numeroOrden = orden
            };
            var estado = _estadosService.GetEstados(filter);
            return estado.FirstOrDefault().id;
        }
    }
}
