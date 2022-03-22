using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Enumeraciones;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using Aguila.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aguila.Core.Services
{
    public class equipoRemolqueService : IequipoRemolqueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly IestadosService _estadosService;
        private readonly ItransportesService _transportesService;
        private readonly ItipoEquipoRemolqueService _tipoEquipoRemolqueService;
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public equipoRemolqueService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                     IactivoOperacionesService activoOperacionesService,
                                     IestadosService estadosService,
                                     ItransportesService transportesService,
                                     ItipoEquipoRemolqueService tipoEquipoRemolqueService,
                                     IactivoMovimientosService activoMovimientosService,
                                     IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoOperacionesService = activoOperacionesService;
            _estadosService = estadosService;
            _transportesService = transportesService;
            _tipoEquipoRemolqueService = tipoEquipoRemolqueService;
            _activoMovimientosService = activoMovimientosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<equipoRemolque>> GetEquipoRemolque(equipoRemolqueQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var equipoRemolque = _unitOfWork.equipoRemolqueRepository.GetAllIncludes();

            if (filter.codigo != null)
            {
                equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));
            }

            if (filter.idEmpresa != null)
            {
                equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.idEmpresa == filter.idEmpresa);
            }

            if (filter.idEstado != null)
            {
                equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);
            }

            if (filter.flota != null)
            {
                equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.flota.ToLower().Equals(filter.flota.ToLower()));
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {
                    equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {
                    equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                {
                    equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {
                    equipoRemolque = equipoRemolque.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idActivo != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.idActivo == filter.idActivo);
            }

            if (filter.idTipoEquipoRemolque != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.idTipoEquipoRemolque == filter.idTipoEquipoRemolque);
            }

            if (filter.tarjetaCirculacion != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.placa.ToLower().Contains(filter.placa.ToLower()));
            }

            if (filter.noEjes != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.noEjes == filter.noEjes);
            }

            if (filter.tandemCorredizo != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.tandemCorredizo.ToLower().Contains(filter.tandemCorredizo.ToLower()));
            }

            if (filter.chasisExtensible != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.chasisExtensible.ToLower().Contains(filter.chasisExtensible.ToLower()));
            }

            if (filter.tipoCuello != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.tipoCuello.ToLower().Contains(filter.tipoCuello.ToLower()));
            }

            if (filter.acopleGenset != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.acopleGenset.ToLower().Contains(filter.acopleGenset.ToLower()));
            }

            if (filter.acopleDolly != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.acopleDolly.ToLower().Contains(filter.acopleDolly.ToLower()));
            }

            if (filter.capacidadCargaLB != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.capacidadCargaLB.ToLower().Contains(filter.capacidadCargaLB.ToLower()));
            }

            if (filter.medidaLB != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.medidaLB.ToLower().Contains(filter.medidaLB.ToLower()));
            }

            if (filter.medidaPlataforma != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.medidaPlataforma.ToLower().Contains(filter.medidaPlataforma.ToLower()));
            }

            if (filter.pechera != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.pechera.ToLower().Contains(filter.pechera.ToLower()));
            }

            if (filter.alturaContenedor != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.alturaContenedor.ToLower().Contains(filter.alturaContenedor.ToLower()));
            }

            if (filter.marcaUR != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.marcaUR.ToLower().Contains(filter.marcaUR.ToLower()));
            }

            if (filter.largoFurgon != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.largoFurgon.ToLower().Contains(filter.largoFurgon.ToLower()));
            }

            if (filter.suspension != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.suspension.ToLower().Contains(filter.suspension.ToLower()));
            }

            if (filter.rieles != null)
            {
                equipoRemolque = equipoRemolque.Where(e => e.rieles.ToLower().Contains(filter.rieles.ToLower()));
            }

            equipoRemolque = equipoRemolque.OrderByDescending(e=>e.fechaCreacion);

            var pagedEquipoRemolque = PagedList<equipoRemolque>.create(equipoRemolque, filter.PageNumber, filter.PageSize);
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedEquipoRemolque.Select(e => e.imagenTarjetaCirculacion).ToList());

            return pagedEquipoRemolque;
        }

        public async Task<equipoRemolque> GetEquipoRemolque(int id)
        {
            var equipoRemolque =  await _unitOfWork.equipoRemolqueRepository.GetByID(id);
            //Manejo de imagenes
            if (equipoRemolque != null && equipoRemolque.idImagenRecursoTarjetaCirculacion  != null && equipoRemolque.idImagenRecursoTarjetaCirculacion != Guid.Empty && equipoRemolque.imagenTarjetaCirculacion == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(equipoRemolque.idImagenRecursoTarjetaCirculacion ?? Guid.Empty);
                equipoRemolque.imagenTarjetaCirculacion = imgRecurso;
            }
            //Fin Imagenes

            return equipoRemolque;
        }

        public async Task<equipoRemolqueDto> InsertEquipoRemolque(equipoRemolqueDto equipoDto, int usuario)
        {

            //se verifica que el correlativo enviado NO exista en el tipo de equipo y empresa
            equipoRemolqueQueryFilter filtro = new equipoRemolqueQueryFilter() { idTipoEquipoRemolque = equipoDto.idTipoEquipoRemolque };
            var equipos = await GetEquipoRemolque(filtro);            

            foreach (var e in equipos)
            {
                var activoOper = await _activoOperacionesService.GetActivoOperacion(e.idActivo);

                if (activoOper.correlativo == equipoDto.activoOperacion.correlativo)
                {
                    throw new AguilaException("Corraltivo ya asignado, por favor ingrese uno distinto.", 402);
                }

                if (equipoDto.placa.ToLower().Equals(e.placa.ToLower()))
                {
                    throw new AguilaException("Placa ya asignada, por favor ingrese una distinta.", 402);
                }
            }

            //se va a traer el PREFIJO del vehiculo para poder armar el codigo de Operaciones
            var currentTipoEquipo = await _unitOfWork.tipoEquipoRemolqueRepository.GetByID(equipoDto.idTipoEquipoRemolque);

            //se arma el codigo COC

            switch (currentTipoEquipo.prefijo)
            {
                case "CH20":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + equipoDto.tandemCorredizo + equipoDto.chasisExtensible
                        + equipoDto.tipoCuello+ equipoDto.acopleGenset+ equipoDto.acopleDolly
                        + equipoDto.activoOperacion.flota;
                    break;

                case "CH40":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + equipoDto.tandemCorredizo + equipoDto.chasisExtensible
                        + equipoDto.tipoCuello + equipoDto.acopleGenset + equipoDto.acopleDolly
                        + equipoDto.activoOperacion.flota;
                    break;

                case "CH24":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + equipoDto.tandemCorredizo + equipoDto.chasisExtensible
                        + equipoDto.tipoCuello + equipoDto.acopleGenset + equipoDto.acopleDolly
                        + equipoDto.activoOperacion.flota;
                    break;

                case "LB01":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString()+ equipoDto.medidaLB+equipoDto.capacidadCargaLB+
                        equipoDto.chasisExtensible + "00"+ equipoDto.activoOperacion.flota;
                    break;

                case "DL01":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + "00000" +
                        equipoDto.activoOperacion.flota;
                    break;

                case "CN20":
                    equipoDto.activoOperacion.coc = equipoDto.alturaContenedor + equipoDto.tipoContenedor +
                        equipoDto.marcaUR + "000" + equipoDto.activoOperacion.flota;
                    break;

                case "CN40":
                    equipoDto.activoOperacion.coc = equipoDto.alturaContenedor + equipoDto.tipoContenedor +
                        equipoDto.marcaUR + "000" + equipoDto.activoOperacion.flota;
                    break;

                case "FUSE":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + equipoDto.tandemCorredizo +
                        equipoDto.largoFurgon + equipoDto.medidaPlataforma + equipoDto.suspension +
                        equipoDto.rieles + equipoDto.activoOperacion.flota;
                    break;

                case "FURE":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + equipoDto.tandemCorredizo +
                        equipoDto.largoFurgon + equipoDto.medidaPlataforma + equipoDto.suspension +
                        equipoDto.rieles + equipoDto.activoOperacion.flota;
                    break;

                case "PL40":
                    equipoDto.activoOperacion.coc = equipoDto.noEjes.ToString() + equipoDto.tandemCorredizo +
                        equipoDto.chasisExtensible + equipoDto.medidaPlataforma + equipoDto.pechera + equipoDto.acopleDolly +
                        equipoDto.activoOperacion.flota;
                    break;

            }

           

            //si no existe el Correlativo , se procede a ingresar el activo de operacion
            var currentActivoOperacion = new activoOperaciones()
            {
                //se arma el codigo utilizando el prefijo + correlativo
                codigo = currentTipoEquipo.prefijo + equipoDto.activoOperacion.correlativo.ToString().PadLeft(4, '0'),
                descripcion = equipoDto.activoOperacion.descripcion.ToUpper(),
                categoria = "E",
                color = equipoDto.activoOperacion.color.ToUpper(),          
                marca = equipoDto.activoOperacion.marca.ToUpper(),
                vin = equipoDto.activoOperacion.vin.ToUpper(),
                correlativo = equipoDto.activoOperacion.correlativo,
                serie = equipoDto.activoOperacion.serie.ToUpper(),
                modeloAnio = equipoDto.activoOperacion.modeloAnio,
                idTransporte = equipoDto.activoOperacion.idTransporte,
                flota = equipoDto.activoOperacion.flota,
                fechaCreacion = DateTime.Now,
                coc = equipoDto.activoOperacion.coc.ToUpper(),
                idEmpresa = equipoDto.activoOperacion.idEmpresa,
                Fotos = equipoDto.activoOperacion.Fotos
            };

            if (equipoDto.activoOperacion.vin != null) { currentActivoOperacion.vin = equipoDto.activoOperacion.vin.ToUpper(); }

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
                //se aguarda el activo operacion
                await _unitOfWork.activoOperacionesRepository.Add(currentActivoOperacion);
                await _unitOfWork.SaveChangeAsync();

                var equipo = new equipoRemolque()
                {
                    idActivo = currentActivoOperacion.id,
                    idTipoEquipoRemolque = equipoDto.idTipoEquipoRemolque,
                    noEjes = equipoDto.noEjes,
                    tandemCorredizo = equipoDto.tandemCorredizo,
                    chasisExtensible = equipoDto.chasisExtensible,
                    tipoCuello = equipoDto.tipoCuello,
                    acopleGenset = equipoDto.acopleGenset,
                    acopleDolly = equipoDto.acopleDolly,
                    capacidadCargaLB = equipoDto.capacidadCargaLB,
                    medidaLB = equipoDto.medidaLB,
                    medidaPlataforma = equipoDto.medidaPlataforma,
                    tarjetaCirculacion = equipoDto.tarjetaCirculacion,
                    //placa = equipoDto.placa.ToUpper(),
                    pechera = equipoDto.pechera,
                    alturaContenedor = equipoDto.alturaContenedor,
                    tipoContenedor = equipoDto.tipoContenedor,
                    marcaUR = equipoDto.marcaUR,
                    largoFurgon = equipoDto.largoFurgon,
                    suspension = equipoDto.suspension,
                    rieles = equipoDto.rieles,
                    fechaCreacion = DateTime.Now
                };

                //valida que venga el valor de la placa del equipo, en caso tenga
                if (equipoDto.placa != null) { equipo.placa = equipoDto.placa.ToUpper(); }

                //Guardamos el recurso de iamgen
                if (equipoDto.imagenTarjetaCirculacion != null)
                {
                    var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(equipoDto.imagenTarjetaCirculacion, "equipoRemolque", nameof(equipoDto.imagenTarjetaCirculacion));

                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        equipo.idImagenRecursoTarjetaCirculacion = imgRecurso.Id;
                }
                //  Fin de recurso de Imagen          


                //se guarda el equipo de operacion
                await _unitOfWork.equipoRemolqueRepository.Add(equipo);
                await _unitOfWork.SaveChangeAsync();

                //Se Registra el primer Movimiento del equipo recien creado en activo Movimiento

                activoMovimientosDto movimientoDto = new activoMovimientosDto()
                {
                    idActivo = equipo.idActivo,
                    //idEstado = getStatusNuevo(currentActivoOperacion.idEmpresa, "activoEstados", 0),
                    idEstacionTrabajo = equipoDto.idEstacion,
                    //TODO: definir como asignar la ubicacion
                    //ubicacionId = 
                    lugar = "Reciente Ingreso",
                    idUsuario = usuario,
                    observaciones = "CREACION",
                    //fecha = DateTime.Now,
                    fechaCreacion = DateTime.Now,
                    evento = ControlActivosEventos.Creacion,
                    idEmpresa = equipo.activoOperacion.idEmpresa
                };

                var estacion = await _unitOfWork.EstacionesTrabajoRepository.GetByID(equipoDto.idEstacion);
                if (estacion != null) movimientoDto.lugar = estacion.Nombre;
                //activoMovimientos movimiento = new activoMovimientos()
                //{
                //    idActivo = currentEquipo.idActivo,
                //    idEstado = getStatusNuevo(currentActivoOperacion.idEmpresa, "activoEstados", 0),
                //    idEstacionTrabajo = equipoDto.idEstacion,
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
                equipoDto.idActivo = equipo.idActivo;
                equipoDto.activoOperacion.id = currentActivoOperacion.id;
                equipoDto.activoOperacion.codigo = currentActivoOperacion.codigo;
                equipoDto.activoOperacion.categoria = currentActivoOperacion.categoria;
                equipoDto.fechaCreacion = equipo.fechaCreacion;
                equipoDto.activoOperacion.fechaCreacion = currentActivoOperacion.fechaCreacion;
            }
            catch (Exception ex)
            {
                if (currentActivoOperacion.id > 0)
                {
                    await _activoOperacionesService.DeleteActivoOperacion(currentActivoOperacion.id);
                    
                }

                _unitOfWork.Dispose();//si existe alguna exception se descartan los cambios realizados a la base de datos
                throw new AguilaException("Ocurrio un error, por favor verifique sus datos:.." + ex.InnerException.Message);
            }

            return equipoDto;
        }

        public async Task<bool> UpdateEquipoRemolque(equipoRemolqueDto equipoRemolque)
        {
            var currentEquipoRemolque = await _unitOfWork.equipoRemolqueRepository.GetByID(equipoRemolque.idActivo);
            if (currentEquipoRemolque == null)
            {
                throw new AguilaException("Equipo no existente...");
            }

            if (!equipoRemolque.placa.ToLower().Equals(currentEquipoRemolque.placa.ToLower()))
            {

                if (await existePlaca(equipoRemolque.placa))
                {
                    throw new AguilaException("Placa Ya Existente!...");
                }
            }

            currentEquipoRemolque.noEjes = equipoRemolque.noEjes;
            currentEquipoRemolque.tandemCorredizo = equipoRemolque.tandemCorredizo;
            currentEquipoRemolque.chasisExtensible = equipoRemolque.chasisExtensible;
            currentEquipoRemolque.tipoCuello = equipoRemolque.tipoCuello;
            currentEquipoRemolque.acopleGenset = equipoRemolque.acopleGenset;
            currentEquipoRemolque.acopleDolly = equipoRemolque.acopleDolly;
            currentEquipoRemolque.capacidadCargaLB = equipoRemolque.capacidadCargaLB;
            currentEquipoRemolque.medidaLB = equipoRemolque.medidaLB;
            currentEquipoRemolque.medidaPlataforma = equipoRemolque.medidaPlataforma;

            if (currentEquipoRemolque.tarjetaCirculacion != null) { currentEquipoRemolque.tarjetaCirculacion = equipoRemolque.tarjetaCirculacion.ToUpper(); }
            if (currentEquipoRemolque.placa!=null) { currentEquipoRemolque.placa = equipoRemolque.placa.ToUpper(); }
            
            currentEquipoRemolque.pechera = equipoRemolque.pechera;
            currentEquipoRemolque.alturaContenedor = equipoRemolque.alturaContenedor;
            currentEquipoRemolque.tipoContenedor = equipoRemolque.tipoContenedor;
            currentEquipoRemolque.marcaUR = equipoRemolque.marcaUR;
            currentEquipoRemolque.largoFurgon = equipoRemolque.largoFurgon;
            //currentEquipoRemolque.rielesHorizontales = equipoRemolque.rielesHorizontales;
            //currentEquipoRemolque.rielesVerticales = equipoRemolque.rielesVerticales;
            currentEquipoRemolque.suspension = equipoRemolque.suspension;
            currentEquipoRemolque.rieles = equipoRemolque.rieles;


            var currentActivoOper = await _unitOfWork.activoOperacionesRepository.GetByID(equipoRemolque.idActivo);
            if (currentActivoOper == null)
            {
                throw new AguilaException("Activo Operacion No Existente!...");
            }

            if (currentActivoOper.descripcion!=null) { currentActivoOper.descripcion = equipoRemolque.activoOperacion.descripcion.ToUpper(); }
            
            currentActivoOper.fechaBaja = equipoRemolque.activoOperacion.fechaBaja;
            currentActivoOper.categoria = equipoRemolque.activoOperacion.categoria;

            if (currentActivoOper.color!=null) { currentActivoOper.color = equipoRemolque.activoOperacion.color.ToUpper(); }
            if (currentActivoOper.marca!=null) { currentActivoOper.marca = equipoRemolque.activoOperacion.marca.ToUpper(); }           
            if (currentActivoOper.vin != null) { currentActivoOper.vin = equipoRemolque.activoOperacion.vin.ToUpper(); }
            if (currentActivoOper.serie!=null) { currentActivoOper.serie = equipoRemolque.activoOperacion.serie.ToUpper(); }
            
            currentActivoOper.modeloAnio = equipoRemolque.activoOperacion.modeloAnio;
            currentActivoOper.idTransporte = equipoRemolque.activoOperacion.idTransporte;
            currentActivoOper.flota = equipoRemolque.activoOperacion.flota;

            // Guardamos el Recurso de Imagen
            if (equipoRemolque.activoOperacion.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                equipoRemolque.activoOperacion.Fotos.Id = currentActivoOper.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(equipoRemolque.activoOperacion.Fotos, "activoOperaciones", nameof(equipoRemolque.activoOperacion.Fotos));

                if (currentActivoOper.idImagenRecursoFotos == null || currentActivoOper.idImagenRecursoFotos == Guid.Empty)
                {
                    if (imgRecursoOp.Id != null && imgRecursoOp.Id != Guid.Empty)
                        currentActivoOper.idImagenRecursoFotos = imgRecursoOp.Id;
                }
            }
            //  Fin de recurso de Imagen   

            //SE ARMA NUEVAMENTE EL COC CON LOS CAMBIOS ENVIADOS

            var currentTipoEquipo = await _unitOfWork.tipoEquipoRemolqueRepository.GetByID(equipoRemolque.idTipoEquipoRemolque);

            switch (currentTipoEquipo.prefijo)
            {
                case "CH20":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.tandemCorredizo + currentEquipoRemolque.chasisExtensible
               + currentEquipoRemolque.tipoCuello + currentEquipoRemolque.acopleGenset + currentEquipoRemolque.acopleDolly
               + currentActivoOper.flota;
                    break;

                case "CH40":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.tandemCorredizo + currentEquipoRemolque.chasisExtensible
               + currentEquipoRemolque.tipoCuello + currentEquipoRemolque.acopleGenset + currentEquipoRemolque.acopleDolly
               + currentActivoOper.flota;
                    break;

                case "CH24":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.tandemCorredizo + currentEquipoRemolque.chasisExtensible
               + currentEquipoRemolque.tipoCuello + currentEquipoRemolque.acopleGenset + currentEquipoRemolque.acopleDolly
               + currentActivoOper.flota;
                    break;

                case "LB01":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.medidaLB+currentEquipoRemolque.capacidadCargaLB +
               currentEquipoRemolque.chasisExtensible + "00" + currentActivoOper.flota;
                    break;

                case "DL01":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + "00000" +
               currentActivoOper.flota;
                    break;

                case "CN20":
                    currentActivoOper.coc = currentEquipoRemolque.alturaContenedor + currentEquipoRemolque.tipoContenedor +
               currentEquipoRemolque.marcaUR + "000" + currentActivoOper.flota;
                    break;

                case "CN40":
                    currentActivoOper.coc = currentEquipoRemolque.alturaContenedor + currentEquipoRemolque.tipoContenedor +
                    currentEquipoRemolque.marcaUR + "000" + currentActivoOper.flota;
                    break;

                case "FUSE":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.tandemCorredizo +
                    currentEquipoRemolque.largoFurgon + currentEquipoRemolque.medidaPlataforma + currentEquipoRemolque.suspension +
                    currentEquipoRemolque.rieles + currentActivoOper.flota;
                    break;

                case "FURE":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.tandemCorredizo +
                    currentEquipoRemolque.largoFurgon + currentEquipoRemolque.medidaPlataforma + currentEquipoRemolque.suspension +
                    currentEquipoRemolque.rieles + currentActivoOper.flota;
                    break;

                case "PL40":
                    currentActivoOper.coc = currentEquipoRemolque.noEjes.ToString() + currentEquipoRemolque.tandemCorredizo +
                        currentEquipoRemolque.chasisExtensible + currentEquipoRemolque.medidaPlataforma + currentEquipoRemolque.pechera + currentEquipoRemolque.acopleDolly +
                        currentEquipoRemolque.activoOperacion.flota;
                    break;
            }

            // Guardamos el Recurso de Imagen
            if (equipoRemolque.imagenTarjetaCirculacion != null)
            {
                equipoRemolque.imagenTarjetaCirculacion.Id = currentEquipoRemolque.idImagenRecursoTarjetaCirculacion ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(equipoRemolque.imagenTarjetaCirculacion, "equipoRemolque", nameof(equipoRemolque.imagenTarjetaCirculacion));

                if (currentEquipoRemolque.idImagenRecursoTarjetaCirculacion == null || currentEquipoRemolque.idImagenRecursoTarjetaCirculacion == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentEquipoRemolque.idImagenRecursoTarjetaCirculacion = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen            


            try
            {
                _unitOfWork.equipoRemolqueRepository.Update(currentEquipoRemolque);
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

        public async Task<bool> DeleteEquipoRemolque(int id)
        {
            var currentEquipoRemolque = await _unitOfWork.equipoRemolqueRepository.GetByID(id);
            if (currentEquipoRemolque == null)
            {
                throw new AguilaException("Equipo no existente...");
            }

            await _unitOfWork.equipoRemolqueRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<bool> existePlaca(string placa)
        {
            var response = false;
            equipoRemolqueQueryFilter filter = new equipoRemolqueQueryFilter() { placa = placa };

            var equipo = await GetEquipoRemolque(filter);

            if (equipo.Count > 0)
            {
                response = true;
            }

            return response;
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
