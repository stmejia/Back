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
    public class vehiculosService : IvehiculosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IactivoOperacionesService _activoOperacionesService;
        private readonly ItransportesService _transportesService;
        private readonly ItipoVehiculosService _tipoVehiculosService;
        private readonly IestadosService _estadosService;
        private readonly IactivoMovimientosService _activoMovimientosService;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public vehiculosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                IactivoOperacionesService activoOperacionesService,
                                ItipoVehiculosService tipoVehiculosService,
                                ItransportesService transportesService,
                                IestadosService estadosService,
                                IactivoMovimientosService activoMovimientosService,
                                IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _activoOperacionesService = activoOperacionesService;
            _tipoVehiculosService = tipoVehiculosService;
            _transportesService = transportesService;
            _estadosService = estadosService;
            _activoMovimientosService = activoMovimientosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<vehiculos>> GetVehiculos(vehiculosQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var vehiculo = _unitOfWork.vehiculosRepository.GetAllIncludes();            

            if (filter.codigo != null)
            {
                vehiculo = vehiculo.Where(x => x.activoOperacion.codigo.ToLower().Equals(filter.codigo.ToLower()));                
            }

            if (filter.idEmpresa != null)
            {                
                vehiculo = vehiculo.Where(x => x.activoOperacion.idEmpresa==filter.idEmpresa);            
            }

            if (filter.idEstado != null)
            {               
                vehiculo = vehiculo.Where(x => x.activoOperacion.movimientoActual.idEstado == filter.idEstado);                
            }

            if (filter.flota != null)
            {
                
                vehiculo = vehiculo.Where(x => x.activoOperacion.flota.ToLower().Equals( filter.flota.ToLower()));
                
            }

            if (filter.propio != null)
            {
                if (filter.propio == true)
                {                    
                    vehiculo = vehiculo.Where(x => x.activoOperacion.transporte.propio == true);
                }
                else
                {
                   
                    vehiculo = vehiculo.Where(x => x.activoOperacion.transporte.propio == false);
                }
            }

            if (filter.equipoActivo != null)
            {
                if (filter.equipoActivo == true)
                { 
                    vehiculo = vehiculo.Where(x => x.activoOperacion.fechaBaja == null);
                }
                else
                {                    
                    vehiculo = vehiculo.Where(x => x.activoOperacion.fechaBaja != null);
                }
            }

            if (filter.idTipoVehiculo != null)
            {
                vehiculo = vehiculo.Where(x => x.idTipoVehiculo == filter.idTipoVehiculo);
            }

            if (filter.motor != null)
            {
                vehiculo = vehiculo.Where(x => x.motor.ToLower().Contains(filter.motor.ToLower()));
            }

            if (filter.ejes != null)
            {
                vehiculo = vehiculo.Where(x => x.ejes == filter.ejes);
            }

            if (filter.tarjetaCirculacion != null)
            {
                vehiculo = vehiculo.Where(x => x.tarjetaCirculacion.ToLower().Contains(filter.tarjetaCirculacion.ToLower()));
            }

            if (filter.placa != null)
            {
                vehiculo = vehiculo.Where(x => x.placa.ToLower().Contains(filter.placa.ToLower()));
            }

            if (filter.tamanoMotor != null)
            {
                vehiculo = vehiculo.Where(x => x.tamanoMotor == filter.tamanoMotor);
            }

            if (filter.llantas != null)
            {
                vehiculo = vehiculo.Where(x => x.llantas == filter.llantas);
            }

            if (filter.distancia != null)
            {
                vehiculo = vehiculo.Where(x => x.distancia.ToLower().Equals(filter.distancia.ToLower()));
            }

            if (filter.potencia != null)
            {
                vehiculo = vehiculo.Where(x => x.potencia.ToLower().Equals(filter.potencia.ToLower()));
            }

            if (filter.tornamesaGraduable != null)
            {
                vehiculo = vehiculo.Where(x => x.tornamesaGraduable.ToLower().Equals(filter.tornamesaGraduable.ToLower()));
            }

            if (filter.capacidadCarga != null)
            {                
                vehiculo = vehiculo.Where(x => x.capacidadCarga.ToLower().Equals(filter.capacidadCarga.ToLower()));                
            }

            if (filter.carroceria != null)
            {
                vehiculo = vehiculo.Where(x => x.carroceria.ToLower().Equals(filter.carroceria.ToLower()));
            }

            if (filter.tipoCarga != null)
            {
                vehiculo = vehiculo.Where(x => x.tipoCarga.ToLower().Equals(filter.tipoCarga.ToLower()));
                
            }

            if (filter.tipoVehiculo != null)
            {
                vehiculo = vehiculo.Where(x => x.tipoVehiculo.ToLower().Equals(filter.tipoVehiculo.ToLower()));
            }

            if (filter.tipoMotor != null)
            {
                vehiculo = vehiculo.Where(x => x.tipoMotor.ToLower().Equals(filter.tipoMotor.ToLower()));
            }

            if (filter.polizaSeguro != null)
            {
                vehiculo = vehiculo.Where(x => x.polizaSeguro.ToLower().Equals(filter.polizaSeguro.ToLower()));
            }

            if (filter.tipoMovimiento != null)
            {
                vehiculo = vehiculo.Where(x=>x.tipoMovimiento.ToLower().Equals(filter.tipoMovimiento.ToLower()));
            }

            if (filter.dobleRemolque != null)
            {
                vehiculo = vehiculo.Where(x => x.dobleRemolque.ToLower().Equals(filter.dobleRemolque.ToLower()));
            }

            if (filter.aptoParaCentroAmerica != null)
            {
                vehiculo = vehiculo.Where(x => x.aptoParaCentroAmerica.ToLower().Equals(filter.aptoParaCentroAmerica.ToLower()));
            }

            if (filter.medidaFurgon != null)
            {
                vehiculo = vehiculo.Where(x => x.medidaFurgon.ToLower().Equals(filter.medidaFurgon.ToLower()));
            }

            vehiculo = vehiculo.OrderByDescending(e=>e.fechaCreacion);
            var pagedVehiculos = PagedList<vehiculos>.create(vehiculo, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedVehiculos.Select(e => e.imagenTarjetaCirculacion).ToList());
            return pagedVehiculos;
        }

        public async Task<vehiculos> GetVehiculo(int id)
        {
            var vehiculo = await _unitOfWork.vehiculosRepository.GetByID(id);
            //Manejo de imagenes
            if (vehiculo != null && vehiculo.idImagenRecursoTarjetaCirculacion != null && vehiculo.idImagenRecursoTarjetaCirculacion != Guid.Empty && vehiculo.imagenTarjetaCirculacion == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(vehiculo.idImagenRecursoTarjetaCirculacion ?? Guid.Empty);
                vehiculo.imagenTarjetaCirculacion = imgRecurso;
            }
            //Fin Imagenes
                        
            return vehiculo;
        }

        public async Task<vehiculosDto> InsertVehiculo(vehiculosDto vehiculoDto, int usuario)
        {
            //se verifica que el correlativo enviado NO exista en el tipo de vehiculo y empresa
            vehiculosQueryFilter filtro = new vehiculosQueryFilter() { idTipoVehiculo = vehiculoDto.idTipoVehiculo };
            var vehiculos = await  GetVehiculos(filtro);

            var existePlaca = vehiculos.Where(e => e.placa.ToLower().Equals(vehiculoDto.placa.ToLower()));
            if (existePlaca.Count() > 0)
            {
                throw new AguilaException("Placa ya asignada, por favor ingrese una distinta.", 402);
            }

            var existeCorrelativo = vehiculos.Where(e=>e.activoOperacion.correlativo==vehiculoDto.activoOperacion.correlativo && e.activoOperacion.idEmpresa==vehiculoDto.activoOperacion.idEmpresa);
            if (existeCorrelativo.Count() > 0)
            {
                throw new AguilaException("Correlativo ya asignado, por favor ingrese uno nuevo.", 402);
            }


            //se va a traer el PREFIJO del vehiculo para poder armar el codifo de Operaciones
            var currentTipoVehiculo = await _unitOfWork.tipoVehiculosRepository.GetByID(vehiculoDto.idTipoVehiculo);
            //var currentTipoVehiculo = vehiculos.FirstOrDefault().tipoVehiculos.prefijo;

            //se arma el codigo COC

            switch (currentTipoVehiculo.prefijo)
            {
                case "CA01":

                    vehiculoDto.activoOperacion.coc = vehiculoDto.distancia + vehiculoDto.potencia + vehiculoDto.tornamesaGraduable + vehiculoDto.tipoMovimiento + vehiculoDto.dobleRemolque + vehiculoDto.aptoParaCentroAmerica
                        + vehiculoDto.activoOperacion.flota;
                    break;

                case "CA02":

                    vehiculoDto.activoOperacion.coc =  vehiculoDto.distancia + vehiculoDto.potencia + vehiculoDto.tornamesaGraduable + vehiculoDto.tipoMovimiento + vehiculoDto.dobleRemolque + vehiculoDto.aptoParaCentroAmerica
                        + vehiculoDto.activoOperacion.flota;
                    break;
                

                case "CM01":
                    vehiculoDto.activoOperacion.coc = vehiculoDto.capacidadCarga + vehiculoDto.carroceria + vehiculoDto.tipoCarga + vehiculoDto.medidaFurgon + "00"
                        + vehiculoDto.activoOperacion.flota;
                    break;

                case "MC01":
                    vehiculoDto.activoOperacion.coc = vehiculoDto.capacidadCarga + vehiculoDto.tipoMotor + "0000"
                        + vehiculoDto.activoOperacion.flota;
                    break;

                case "VELI":
                    vehiculoDto.activoOperacion.coc = vehiculoDto.tipoVehiculo + "00000"
                        + vehiculoDto.activoOperacion.flota;
                    break;

                case "MA01":
                    vehiculoDto.activoOperacion.coc = vehiculoDto.tipoVehiculo + "00000"
                        + vehiculoDto.activoOperacion.flota;
                    break;
            }

            //si no existe el Correlativo , se procede a ingresar el activo de operacion
            var currentActivoOperacion = new activoOperaciones() {
                //se arma el codigo utilizando el prefijo + correlativo
                codigo = currentTipoVehiculo.prefijo + vehiculoDto.activoOperacion.correlativo.ToString().PadLeft(4, '0'), 
                descripcion = vehiculoDto.activoOperacion.descripcion.ToUpper(),
                categoria = "V",
                color = vehiculoDto.activoOperacion.color.ToUpper(),
                marca = vehiculoDto.activoOperacion.marca.ToUpper(),
                vin = vehiculoDto.activoOperacion.vin.ToUpper(),
                correlativo = vehiculoDto.activoOperacion.correlativo,
                serie = vehiculoDto.activoOperacion.serie.ToUpper(),
                modeloAnio = vehiculoDto.activoOperacion.modeloAnio,
                idTransporte = vehiculoDto.activoOperacion.idTransporte,
                flota = vehiculoDto.activoOperacion.flota,
                fechaCreacion = DateTime.Now,
                coc=vehiculoDto.activoOperacion.coc.ToUpper(),
                idEmpresa = vehiculoDto.activoOperacion.idEmpresa,
                Fotos = vehiculoDto.activoOperacion.Fotos
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
                //se aguarda el activo operacion
                await _unitOfWork.activoOperacionesRepository.Add(currentActivoOperacion);
                await _unitOfWork.SaveChangeAsync();               

                var currentVehiculo = new vehiculos() {
                    idActivo = currentActivoOperacion.id,
                    idTipoVehiculo = vehiculoDto.idTipoVehiculo,
                    motor = vehiculoDto.motor,
                    ejes = vehiculoDto.ejes,
                    tarjetaCirculacion = vehiculoDto.tarjetaCirculacion.ToUpper(),
                    placa = vehiculoDto.placa.ToUpper(),
                    tamanoMotor = vehiculoDto.tamanoMotor,
                    llantas = vehiculoDto.llantas,
                    fechaCreacion = DateTime.Now,
                    distancia = vehiculoDto.distancia,
                    potencia=vehiculoDto.potencia,
                    tornamesaGraduable =vehiculoDto.tornamesaGraduable,
                    tipoMovimiento = vehiculoDto.tipoMovimiento,
                    dobleRemolque = vehiculoDto.dobleRemolque,
                    aptoParaCentroAmerica = vehiculoDto.aptoParaCentroAmerica,
                    capacidadCarga=vehiculoDto.capacidadCarga,
                    carroceria=vehiculoDto.carroceria,
                    tipoCarga = vehiculoDto.tipoCarga,
                    medidaFurgon = vehiculoDto.medidaFurgon,
                    tipoVehiculo = vehiculoDto.tipoVehiculo,
                    tipoMotor = vehiculoDto.tipoMotor,
                    polizaSeguro = vehiculoDto.polizaSeguro
                };


                //Guardamos el recurso de iamgen
                if (vehiculoDto.imagenTarjetaCirculacion != null)
                {
                    var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(vehiculoDto.imagenTarjetaCirculacion, "vehiculos", nameof(vehiculoDto.imagenTarjetaCirculacion));

                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentVehiculo.idImagenRecursoTarjetaCirculacion = imgRecurso.Id;
                }
                //  Fin de recurso de Imagen          



                //se guarda el vehiculo
                await _unitOfWork.vehiculosRepository.Add(currentVehiculo);
                await _unitOfWork.SaveChangeAsync();

                //se Registra el primer Movimiento del vehiculo recien creado en activo Movimiento

                activoMovimientosDto movimientoDto = new activoMovimientosDto() {
                    idActivo = currentVehiculo.idActivo,
                    //idEstado = getStatusNuevo(currentActivoOperacion.idEmpresa, "activoEstados", 0),
                    idEstacionTrabajo = vehiculoDto.idEstacion,
                    //TODO: definir como asignar la ubicacion
                    //ubicacionId = 
                    lugar = "Reciente Ingreso",
                    idUsuario = usuario,
                    observaciones = "CREACION",
                    //fecha = DateTime.Now,
                    fechaCreacion = DateTime.Now,
                    evento = ControlActivosEventos.Creacion,
                    idEmpresa= vehiculoDto.activoOperacion.idEmpresa
                };

                var estacion =await _unitOfWork.EstacionesTrabajoRepository.GetByID(vehiculoDto.idEstacion);
                if (estacion != null) movimientoDto.lugar = estacion.Nombre;
               
                await _activoMovimientosService.InsertMovimientoPorEvento(movimientoDto);              


                //se asignan al DTo los valores generados durante las inserciones a la base de datos para poderlos retornar
                vehiculoDto.idActivo = currentVehiculo.idActivo;
                vehiculoDto.activoOperacion.id = currentActivoOperacion.id;
                vehiculoDto.activoOperacion.codigo = currentActivoOperacion.codigo;
                vehiculoDto.activoOperacion.categoria = currentActivoOperacion.categoria;


            }catch(Exception ex)
            {
                _unitOfWork.Dispose();//si existe alguna exception se descartan los cambios realizados a la base de datos
                throw new AguilaException("Ocurrio un error, por favor verifique sus datos:.." + ex.InnerException.Message);
            }

            return vehiculoDto;           
        }

        public async Task<bool> UpdateVehiculo(vehiculosDto vehiculo)
        {
            var currentVehiculo = await _unitOfWork.vehiculosRepository.GetByID(vehiculo.idActivo);
            if (currentVehiculo == null)
            {
                throw new AguilaException("Vehiculo No Existente!...");
            }

            if (!vehiculo.placa.ToLower().Equals(currentVehiculo.placa.ToLower())){

                if (await existePlaca(vehiculo.placa))
                {
                    throw new AguilaException("Placa Ya Existente!...");
                }
            }

            //valida que el tipo de vehiculo no se pueda cambiar

            if (currentVehiculo.idTipoVehiculo != vehiculo.idTipoVehiculo)
            {
                throw new AguilaException("No es permito cambiar el tipo de vehiculo!...revise sus datos.",400);
            }
            
            currentVehiculo.motor = vehiculo.motor;
            currentVehiculo.ejes = vehiculo.ejes;
            currentVehiculo.tarjetaCirculacion = vehiculo.tarjetaCirculacion;
            currentVehiculo.placa = vehiculo.placa;
            currentVehiculo.tamanoMotor = vehiculo.tamanoMotor;
            currentVehiculo.llantas = vehiculo.llantas;
            currentVehiculo.distancia = vehiculo.distancia;
            currentVehiculo.potencia = vehiculo.potencia;
            currentVehiculo.tornamesaGraduable = vehiculo.tornamesaGraduable;
            currentVehiculo.tipoMovimiento = vehiculo.tipoMovimiento;
            currentVehiculo.dobleRemolque = vehiculo.dobleRemolque;
            currentVehiculo.aptoParaCentroAmerica = vehiculo.aptoParaCentroAmerica;
            currentVehiculo.capacidadCarga = vehiculo.capacidadCarga;
            currentVehiculo.carroceria = vehiculo.carroceria;
            currentVehiculo.tipoCarga = vehiculo.tipoCarga;
            currentVehiculo.medidaFurgon = vehiculo.medidaFurgon;
            currentVehiculo.tipoMotor = vehiculo.tipoMotor;
            currentVehiculo.polizaSeguro = vehiculo.polizaSeguro;

            //var currentActivoOper = await _activoOperacionesService.GetActivoOperacion(vehiculo.idActivo);
            var currentActivoOper = await _unitOfWork.activoOperacionesRepository.GetByID(vehiculo.idActivo);
            if (currentActivoOper == null)
            {
                throw new AguilaException("Activo Operacion No Existente!...");
            }

            currentActivoOper.descripcion = vehiculo.activoOperacion.descripcion;
            currentActivoOper.fechaBaja = vehiculo.activoOperacion.fechaBaja;
            currentActivoOper.categoria = vehiculo.activoOperacion.categoria;
            currentActivoOper.color = vehiculo.activoOperacion.color;
            currentActivoOper.marca = vehiculo.activoOperacion.marca;
            currentActivoOper.vin = vehiculo.activoOperacion.vin;
            currentActivoOper.serie = vehiculo.activoOperacion.serie;
            currentActivoOper.modeloAnio = vehiculo.activoOperacion.modeloAnio;
            currentActivoOper.idTransporte = vehiculo.activoOperacion.idTransporte;
            currentActivoOper.flota = vehiculo.activoOperacion.flota;

            // Guardamos el Recurso de Imagen
            if (vehiculo.activoOperacion.Fotos != null)
            {            
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                vehiculo.activoOperacion.Fotos.Id = currentActivoOper.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(vehiculo.activoOperacion.Fotos, "activoOperaciones", nameof(vehiculo.activoOperacion.Fotos));

                if (currentActivoOper.idImagenRecursoFotos == null || currentActivoOper.idImagenRecursoFotos == Guid.Empty)
                {
                    if (imgRecursoOp.Id != null && imgRecursoOp.Id != Guid.Empty)
                        currentActivoOper.idImagenRecursoFotos = imgRecursoOp.Id;
                }
            }
            //  Fin de recurso de Imagen   


            //----------------------------se arma el COC en base a los cambios (Codigo de Operacion)-----------------------

            //se va a traer el PREFIJO del vehiculo para poder armar el codifo de Operaciones
            var currentTipoVehiculo = await _unitOfWork.tipoVehiculosRepository.GetByID(currentVehiculo.idTipoVehiculo);

            //se arma el codigo COC

            switch (currentTipoVehiculo.prefijo)
            {
                case "CA01":

                    currentActivoOper.coc = currentVehiculo.distancia + currentVehiculo.potencia + currentVehiculo.tornamesaGraduable + currentVehiculo.tipoMovimiento + currentVehiculo.dobleRemolque + currentVehiculo.aptoParaCentroAmerica
                        + currentActivoOper.flota;
                    break;

                case "CA02":

                    currentActivoOper.coc = currentVehiculo.distancia + currentVehiculo.potencia + currentVehiculo.tornamesaGraduable + currentVehiculo.tipoMovimiento + currentVehiculo.dobleRemolque + currentVehiculo.aptoParaCentroAmerica
                        + currentActivoOper.flota;
                    break;

                case "CA03":

                    currentActivoOper.coc = currentVehiculo.distancia + currentVehiculo.potencia + currentVehiculo.tornamesaGraduable + "000"
                        + currentActivoOper.flota;
                    break;

                case "CM01":
                    currentActivoOper.coc = currentVehiculo.capacidadCarga + currentVehiculo.carroceria + currentVehiculo.tipoCarga + currentVehiculo.medidaFurgon +"00"
                        + currentActivoOper.flota;
                    break;

                case "MC01":
                    currentActivoOper.coc = currentVehiculo.capacidadCarga + currentVehiculo.tipoMotor + "0000"
                        + currentActivoOper.flota;
                    break;

                case "VELI":
                    currentActivoOper.coc = currentVehiculo.tipoVehiculo + "00000"
                        + currentActivoOper.flota;
                    break;

                case "MA01":
                    currentActivoOper.coc = currentVehiculo.tipoVehiculo + "00000"
                        + currentActivoOper.flota;
                    break;
            }

            //-------------------------------------------------------------------------------------------

            // Guardamos el Recurso de Imagen
            if (vehiculo.imagenTarjetaCirculacion != null)
            {
                vehiculo.imagenTarjetaCirculacion.Id = currentVehiculo.idImagenRecursoTarjetaCirculacion ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(vehiculo.imagenTarjetaCirculacion, "vehiculos", nameof(vehiculo.imagenTarjetaCirculacion));

                if (currentVehiculo.idImagenRecursoTarjetaCirculacion == null || currentVehiculo.idImagenRecursoTarjetaCirculacion == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentVehiculo.idImagenRecursoTarjetaCirculacion = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen            


            try
            {
                _unitOfWork.activoOperacionesRepository.Update(currentActivoOper);
                await _unitOfWork.SaveChangeAsync();

                _unitOfWork.vehiculosRepository.Update(currentVehiculo);
                await _unitOfWork.SaveChangeAsync();
            }
            catch(Exception ex)
            {
                _unitOfWork.Dispose();
                throw new AguilaException("Ocurrio un error, por favor verifique sus datos:.." + ex.InnerException.Message);
            }
           

            return true;
        }

        public async Task<bool> DeleteVehiculo(int id)
        {
            var currentVehiculo = await _unitOfWork.vehiculosRepository.GetByID(id);
            if (currentVehiculo == null)
            {
                throw new AguilaException("Vehiculo no existente...");
            }

            await _unitOfWork.vehiculosRepository.Delete(id);
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
            vehiculosQueryFilter filter = new vehiculosQueryFilter() { placa=placa};

            var vehiculo = await GetVehiculos(filter);

            if (vehiculo.Count > 0)
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
