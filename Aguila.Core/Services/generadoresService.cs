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
using Aguila.Core.Enumeraciones;

namespace Aguila.Core.Services
{
    public class generadoresService : IgeneradoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly IestadosService _estadosService;
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public generadoresService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                  IactivoOperacionesService activoOperacionesService,
                                  IestadosService estadosService,
                                  IactivoMovimientosService activoMovimientosService,
                                  IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoOperacionesService = activoOperacionesService;
            _estadosService = estadosService;
            _activoMovimientosService = activoMovimientosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public PagedList<generadores> GetGeneradores(generadoresQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var generadores = _unitOfWork.generadoresRepository.GetAllIncludes();

            if (filter.codigo != null)
            {
                generadores = generadores.Where(e => e.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa != null)
            {
                generadores = generadores.Where(e => e.activoOperacion.idEmpresa == filter.idEmpresa);
            }

            if (filter.idEstado != null)
            {
                generadores = generadores.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                generadores = generadores.Where(e => e.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    generadores = generadores.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {
                    generadores = generadores.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    generadores = generadores.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    generadores = generadores.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idActivo != null)
            {
                generadores = generadores.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idTipoGenerador != null)
            {
                generadores = generadores.Where(e => e.idTipoGenerador == filter.idTipoGenerador);
            }

            if (filter.capacidadGalones != null)
            {
                generadores = generadores.Where(e => e.capacidadGalones == filter.capacidadGalones);
            }

            if (filter.numeroCilindros != null)
            {
                generadores = generadores.Where(e => e.numeroCilindros == filter.numeroCilindros);
            }

            if (filter.marcaGenerador != null)
            {
                generadores = generadores.Where(e => e.marcaGenerador.ToLower().Contains(filter.marcaGenerador.ToLower()));
            }

            if (filter.tipoInstalacion != null)
            {
                generadores = generadores.Where(e => e.tipoInstalacion.ToLower().Contains(filter.tipoInstalacion.ToLower()));
            }

            if (filter.tipoEnfriamiento != null)
            {
                generadores = generadores.Where(e => e.tipoEnfriamiento.ToLower().Contains(filter.tipoEnfriamiento.ToLower()));
            }

            if (filter.aptoParaCA != null)
            {
                generadores = generadores.Where(e => e.aptoParaCA.ToLower().Contains(filter.aptoParaCA.ToLower()));
            }

            var pagedGeneradores = PagedList<generadores>.create(generadores, filter.PageNumber, filter.PageSize);
            return pagedGeneradores;
        }

        public async Task<generadores> GetGenerador(int id)
        {
            return await _unitOfWork.generadoresRepository.GetByID(id);
        }

        public async Task<generadoresDto> InsertGenerador(generadoresDto generadorDto, int usuario)
        {
            //se verifica que el correlativo enviado NO exista en el tipo de equipo y empresa
            generadoresQueryFilter filtro = new generadoresQueryFilter() { idTipoGenerador = generadorDto.idTipoGenerador };
            var generadores = GetGeneradores(filtro);


            foreach (var g in generadores)
            {
                var activoOper = await _activoOperacionesService.GetActivoOperacion(g.idActivo);

                if (activoOper.correlativo == generadorDto.activoOperacion.correlativo)
                {
                    throw new AguilaException("Corraltivo ya asignado, por favor ingrese uno distinto.", 402);
                }
                
            }

            //se va a traer el PREFIJO del vehiculo para poder armar el codigo de Operaciones
            var currentTipoGenerador = await _unitOfWork.tipoGeneradoresRepository.GetByID(generadorDto.idTipoGenerador);

            //se arma el codigo COC

            switch (currentTipoGenerador.prefijo)
            {
                case "GS01":
                    generadorDto.activoOperacion.coc = generadorDto.tipoInstalacion + generadorDto.marcaGenerador + generadorDto.aptoParaCA+"000"
                        + generadorDto.activoOperacion.flota;
                    break;
            }

            //si no existe el Correlativo , se procede a ingresar el activo de operacion
            var currentActivoOperacion = new activoOperaciones()
            {
                //se arma el codigo utilizando el prefijo + correlativo
                codigo = currentTipoGenerador.prefijo + generadorDto.activoOperacion.correlativo.ToString().PadLeft(4, '0'),
                descripcion = generadorDto.activoOperacion.descripcion.ToUpper(),
                categoria = "G",                
                marca = generadorDto.activoOperacion.marca.ToUpper(),                
                correlativo = generadorDto.activoOperacion.correlativo,                
                idTransporte = generadorDto.activoOperacion.idTransporte,
                flota = generadorDto.activoOperacion.flota,
                fechaCreacion = DateTime.Now,
                coc = generadorDto.activoOperacion.coc.ToUpper(),
                idEmpresa = generadorDto.activoOperacion.idEmpresa,
                Fotos = generadorDto.activoOperacion.Fotos,
                serie = generadorDto.activoOperacion.serie,
                color = generadorDto.activoOperacion.color,
                modeloAnio = generadorDto.activoOperacion.modeloAnio
            };

            //Guardamos el recurso de Imagen
            if (currentActivoOperacion.idImagenRecursoFotos == null && currentActivoOperacion.Fotos != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(currentActivoOperacion.Fotos, "activoOperaciones", nameof(currentActivoOperacion.Fotos));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    currentActivoOperacion.idImagenRecursoFotos = imgRecurso.Id;
            }
            //  Fin de recurso de Imagen  

            try
            {
                await _unitOfWork.activoOperacionesRepository.Add(currentActivoOperacion);
                await _unitOfWork.SaveChangeAsync();

                var currentGenerador = new generadores() {
                    idActivo = currentActivoOperacion.id,
                    idTipoGenerador = generadorDto.idTipoGenerador,
                    capacidadGalones = generadorDto.capacidadGalones,
                    numeroCilindros = generadorDto.numeroCilindros,
                    marcaGenerador = estandarizarMayuscula(generadorDto.marcaGenerador),
                    tipoInstalacion = estandarizarMayuscula(generadorDto.tipoInstalacion),
                    tipoEnfriamiento = estandarizarMayuscula(generadorDto.tipoEnfriamiento),
                    aptoParaCA = estandarizarMayuscula(generadorDto.aptoParaCA),
                    codigoAnterior = estandarizarMayuscula(generadorDto.codigoAnterior),
                    tipoMotor = estandarizarMayuscula(generadorDto.tipoMotor),
                    noMotor = estandarizarMayuscula(generadorDto.noMotor),
                    velocidad = estandarizarMayuscula(generadorDto.velocidad),
                    potenciaMotor = estandarizarMayuscula(generadorDto.potenciaMotor),
                    modeloGenerador = estandarizarMayuscula(generadorDto.modeloGenerador),
                    serieGenerador = estandarizarMayuscula(generadorDto.serieGenerador),
                    tipoGeneradorGen = estandarizarMayuscula(generadorDto.tipoGeneradorGen),
                    potenciaGenerador = estandarizarMayuscula(generadorDto.potenciaGenerador),
                    tensionGenerador = estandarizarMayuscula(generadorDto.tensionGenerador),
                    tipoTanque = estandarizarMayuscula(generadorDto.tipoTanque),
                    tipoAceite = estandarizarMayuscula(generadorDto.tipoAceite),

                    fechaCreacion = DateTime.Now
                };               


                //se guarda el generador
                await _unitOfWork.generadoresRepository.Add(currentGenerador);
                await _unitOfWork.SaveChangeAsync();

                //Se Registra el primer Movimiento del equipo recien creado en activo Movimiento

                activoMovimientosDto movimientoDto = new activoMovimientosDto()
                {
                    idActivo = currentGenerador.idActivo,
                    //idEstado = getStatusNuevo(currentActivoOperacion.idEmpresa, "activoEstados", 0),
                    idEstacionTrabajo = generadorDto.idEstacion,
                    //TODO: definir como asignar la ubicacion
                    //ubicacionId = 
                    lugar = "Reciente Ingreso",
                    idUsuario = usuario,
                    observaciones = "CREACION",
                    //fecha = DateTime.Now,
                    fechaCreacion = DateTime.Now,
                    evento = ControlActivosEventos.Creacion,
                    idEmpresa = currentGenerador.activoOperacion.idEmpresa
                };
                
                var estacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(generadorDto.idEstacion);
                if (estacion != null) movimientoDto.lugar = estacion.Nombre;
                //activoMovimientos movimiento = new activoMovimientos()
                //{
                //    idActivo = currentGenerador.idActivo,
                //    idEstado = getStatusNuevo(currentActivoOperacion.idEmpresa, "activoEstados", 0),
                //    idEstacionTrabajo = generadorDto.idEstacion,
                //    //TODO: definir como asignar la ubicacion
                //    //ubicacionId = 
                //    lugar = "Reciente Ingreso",
                //    idUsuario = usuario,
                //    observaciones = "Reciente Ingreso",
                //    fecha = DateTime.Now,
                //    fechaCreacion = DateTime.Now
                //};

                //activoMovimientosActual movimientoActual = new activoMovimientosActual()
                //{
                //    idActivo = movimiento.idActivo,
                //    idEstado = (int)movimiento.idEstado,
                //    idEstacionTrabajo = movimiento.idEstacionTrabajo,
                //    //TODO: definir como asignar la ubicacion
                //    //ubicacionId = 
                //    lugar = "Reciente Ingreso",
                //    idUsuario = usuario,
                //    observaciones = "Reciente Ingreso",
                //    fecha = movimiento.fecha,
                //    fechaCreacion = movimiento.fechaCreacion
                //};

                //await _unitOfWork.activoMovimientosRepository.Add(movimiento);
                //await _unitOfWork.SaveChangeAsync();
                //await _unitOfWork.activoMovimientosActualRepository.Add(movimientoActual);
                //await _unitOfWork.SaveChangeAsync();

                await _activoMovimientosService.InsertMovimientoPorEvento(movimientoDto);

                //se asignan al DTo los valores generados durante las inserciones a la base de datos para poderlos retornar
                generadorDto.idActivo = currentGenerador.idActivo;
                generadorDto.activoOperacion.id = currentActivoOperacion.id;
                generadorDto.activoOperacion.codigo = currentActivoOperacion.codigo;
                generadorDto.activoOperacion.categoria = currentActivoOperacion.categoria;
                generadorDto.fechaCreacion = currentGenerador.fechaCreacion;
                generadorDto.activoOperacion.fechaCreacion = currentActivoOperacion.fechaCreacion;
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();//si existe alguna exception se descartan los cambios realizados a la base de datos
                throw new AguilaException("Ocurrio un error, por favor verifique sus datos:.." + ex.InnerException.Message);
            }

            return generadorDto;
        }

        public async Task<bool> UpdateGenerador(generadoresDto generador)
        {
            var currentGenerador = await _unitOfWork.generadoresRepository.GetByID(generador.idActivo);
            if (currentGenerador == null)
            {
                throw new AguilaException("Generador no existente...");
            }
      
            
            currentGenerador.capacidadGalones = generador.capacidadGalones;
            currentGenerador.numeroCilindros = generador.numeroCilindros;
            currentGenerador.marcaGenerador = generador.marcaGenerador;
            currentGenerador.tipoInstalacion = generador.tipoInstalacion;
            currentGenerador.tipoEnfriamiento = generador.tipoEnfriamiento;
            currentGenerador.capacidadGalones = generador.capacidadGalones;
            currentGenerador.numeroCilindros = generador.numeroCilindros;
            currentGenerador.marcaGenerador = estandarizarMayuscula(generador.marcaGenerador);
            currentGenerador.tipoInstalacion = estandarizarMayuscula(generador.tipoInstalacion);
            currentGenerador.tipoEnfriamiento = estandarizarMayuscula(generador.tipoEnfriamiento);
            currentGenerador.aptoParaCA = estandarizarMayuscula(generador.aptoParaCA);
            currentGenerador.codigoAnterior = estandarizarMayuscula(generador.codigoAnterior);
            currentGenerador.tipoMotor = estandarizarMayuscula(generador.tipoMotor);
            currentGenerador.noMotor = estandarizarMayuscula(generador.noMotor);
            currentGenerador.velocidad = estandarizarMayuscula(generador.velocidad);
            currentGenerador.potenciaMotor = estandarizarMayuscula(generador.potenciaMotor);
            currentGenerador.modeloGenerador = estandarizarMayuscula(generador.modeloGenerador);
            currentGenerador.serieGenerador = estandarizarMayuscula(generador.serieGenerador);
            currentGenerador.tipoGeneradorGen = estandarizarMayuscula(generador.tipoGeneradorGen);
            currentGenerador.potenciaGenerador = estandarizarMayuscula(generador.potenciaGenerador);
            currentGenerador.tensionGenerador = estandarizarMayuscula(generador.tensionGenerador);
            currentGenerador.tipoTanque = estandarizarMayuscula(generador.tipoTanque);
            currentGenerador.tipoAceite = estandarizarMayuscula(generador.tipoAceite);


            var currentActivoOper = await _unitOfWork.activoOperacionesRepository.GetByID(generador.idActivo);
            if (currentActivoOper == null)
            {
                throw new AguilaException("Activo Operacion No Existente!...");
            }


            currentActivoOper.descripcion = estandarizarMayuscula(generador.activoOperacion.descripcion);
            currentActivoOper.fechaBaja = generador.activoOperacion.fechaBaja;            
            currentActivoOper.marca = estandarizarMayuscula(generador.activoOperacion.marca);         
            currentActivoOper.idTransporte = generador.activoOperacion.idTransporte;
            currentActivoOper.flota = generador.activoOperacion.flota;
            currentActivoOper.serie = generador.activoOperacion.serie;
            currentActivoOper.color = generador.activoOperacion.color;
            currentActivoOper.modeloAnio = generador.activoOperacion.modeloAnio;


            // Guardamos el Recurso de Imagen
            if (generador.activoOperacion.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                generador.activoOperacion.Fotos.Id = currentActivoOper.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(generador.activoOperacion.Fotos, "activoOperaciones", nameof(generador.activoOperacion.Fotos));

                if (currentActivoOper.idImagenRecursoFotos == null || currentActivoOper.idImagenRecursoFotos == Guid.Empty)
                {
                    if (imgRecursoOp.Id != null && imgRecursoOp.Id != Guid.Empty)
                        currentActivoOper.idImagenRecursoFotos = imgRecursoOp.Id;
                }
            }
            //  Fin de recurso de Imagen   

            //se va a traer el PREFIJO del vehiculo para poder armar el codigo de Operaciones
            var currentTipoGenerador = await _unitOfWork.tipoGeneradoresRepository.GetByID(generador.idTipoGenerador);

            //se arma el codigo COC

            switch (currentTipoGenerador.prefijo)
            {
                case "GS01":
                    currentGenerador.activoOperacion.coc = currentGenerador.tipoInstalacion + currentGenerador.marcaGenerador + currentGenerador.aptoParaCA + "000"
                        + currentGenerador.activoOperacion.flota;
                    break;
            }


            try
            {
                _unitOfWork.generadoresRepository.Update(currentGenerador);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.activoOperacionesRepository.Update(currentActivoOper);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                throw new AguilaException("Ocurrio un error, por favor verifique sus datos:.." + ex.InnerException.Message);
            }


           
            return true;
        }

        public async Task<bool> DeleteGenerador(long id)
        {
            var currentGenerador = await _unitOfWork.generadoresRepository.GetByID(id);
            if (currentGenerador == null)
            {
                throw new AguilaException("Generador no existente...");
            }

            await _unitOfWork.generadoresRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();
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

        public string estandarizarMayuscula(string str)
        {            

            if (!String.IsNullOrEmpty(str))
            {                
                return str.ToUpper().Trim();
            }
            else
            {
                return str;
            }
        }
    }
}
