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
    public class condicionGenSetService : IcondicionGenSetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionGenSetService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IAguilaMap aguilaMap,
                                      IcondicionActivosService condicionActivosService, IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _aguilaMap = aguilaMap;
            _condicionActivosService = condicionActivosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<condicionGenSet>> GetCondicionGenSet(condicionGenSetQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            //Se valida que vengan los filtros de fechas
            if (!filter.ignorarFechas)
            {
                if (filter.fechaInicio == null || filter.fechaFin == null)
                {
                    throw new AguilaException("Debe Especificar una Fecha de Inicio y una Fecha Final para Obtener los datos.", 404);
                }
                else
                {
                    var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                    if (dias > 31) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor de 31 dias.", 404);
                }
            }

            var condicionGenSet = _unitOfWork.condicionGenSetRepository.GetAllIncludes();

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionGenSet = condicionGenSet.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.idCondicionActivo != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.idCondicionActivo == filter.idCondicionActivo);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.movimiento != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.condicionActivo.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.disponible != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.condicionActivo.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.condicionActivo.cargado == filter.cargado);
            }

            if (filter.inspecVeriOrden != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.galonesRequeridos != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.galonesRequeridos == filter.galonesRequeridos);
            }

            if (filter.galonesGenSet != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.galonesGenSet == filter.galonesGenSet);
            }

            if (filter.galonesCompletar != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.galonesCompletar == filter.galonesCompletar);
            }

            if (filter.dieselEntradaSalida != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.dieselEntradaSalida == filter.dieselEntradaSalida);
            }

            if (filter.dieselConsumido != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.dieselConsumido == filter.dieselConsumido);
            }

            if (filter.horasTrabajadas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.horasTrabajadas == filter.horasTrabajadas);
            }

            if (filter.estExPuertasGolpeadas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.estExPuertasGolpeadas == filter.estExPuertasGolpeadas);
            }

            if (filter.estExPuertasQuebradas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.estExPuertasQuebradas == filter.estExPuertasQuebradas);
            }

            if (filter.estExPuertasFaltantes != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.estExPuertasFaltantes == filter.estExPuertasFaltantes);
            }

            if (filter.estExPuertasSueltas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.estExPuertasSueltas == filter.estExPuertasSueltas);
            }

            if (filter.estExBisagrasQuebradas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.estExBisagrasQuebradas == filter.estExBisagrasQuebradas);
            }

            if (filter.panelGolpes != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.panelGolpes == filter.panelGolpes);
            }

            if (filter.panelTornillosFaltantes != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.panelTornillosFaltantes == filter.panelTornillosFaltantes);
            }

            if (filter.panelOtros != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.panelOtros == filter.panelOtros);
            }

            if (filter.soporteGolpes != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteGolpes == filter.soporteGolpes);
            }

            if (filter.soporteTornillosFaltantes != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteTornillosFaltantes == filter.soporteTornillosFaltantes);
            }

            if (filter.soporteMarcoQuebrado != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteMarcoQuebrado == filter.soporteMarcoQuebrado);
            }

            if (filter.soporteMarcoFlojo != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteMarcoFlojo == filter.soporteMarcoFlojo);
            }

            if (filter.soporteBisagrasQuebradas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteBisagrasQuebradas == filter.soporteBisagrasQuebradas);
            }

            if (filter.soporteBisagrasQuebradas != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteBisagrasQuebradas == filter.soporteBisagrasQuebradas);
            }

            if (filter.soporteSoldaduraEstado != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.soporteSoldaduraEstado == filter.soporteSoldaduraEstado);
            }

            if (filter.revIntCablesQuemados != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.revIntCablesQuemados == filter.revIntCablesQuemados);
            }

            if (filter.revIntCablesSueltos != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.revIntCablesSueltos == filter.revIntCablesSueltos);
            }

            if (filter.revIntReparacionesImpropias != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.revIntReparacionesImpropias == filter.revIntReparacionesImpropias);
            }

            if (filter.tanqueAgujeros != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.tanqueAgujeros == filter.tanqueAgujeros);
            }

            if (filter.tanqueSoporteDanado != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.tanqueSoporteDanado == filter.tanqueSoporteDanado);
            }

            if (filter.tanqueMedidorDiesel != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.tanqueMedidorDiesel == filter.tanqueMedidorDiesel);
            }

            if (filter.tanqueCodoQuebrado != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.tanqueCodoQuebrado == filter.tanqueCodoQuebrado);
            }

            if (filter.tanqueTapon != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.tanqueTapon == filter.tanqueTapon);
            }

            if (filter.tanqueTuberia != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.tanqueTuberia == filter.tanqueTuberia);
            }

            if (filter.pFaltMedidorAceite != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.pFaltMedidorAceite == filter.pFaltMedidorAceite);
            }

            if (filter.pFaltTapaAceite != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.pFaltTapaAceite == filter.pFaltTapaAceite);
            }

            if (filter.pFaltTaponRadiador != null)
            {
                condicionGenSet = condicionGenSet.Where(e => e.pFaltTaponRadiador == filter.pFaltTaponRadiador);
            }

            var pagedCondicionGenerador = PagedList<condicionGenSet>.create(condicionGenSet, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionGenerador.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionGenerador.Select(e => e.condicionActivo.Fotos).ToList());
            return pagedCondicionGenerador;
        }

        public async Task<condicionGenSet> GetCondicionGenSet(long idCondicion)
        {
            var condicionGen = _unitOfWork.condicionGenSetRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();

            //Manejo de imagenes
            if (condicionGen != null && condicionGen.condicionActivo.idImagenRecursoFirma != null && condicionGen.condicionActivo.idImagenRecursoFirma != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionGen.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionGen.condicionActivo.ImagenFirmaPiloto = imgRecurso;
            }

            if (condicionGen != null && condicionGen.condicionActivo.idImagenRecursoFotos != null && condicionGen.condicionActivo.idImagenRecursoFotos != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionGen.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionGen.condicionActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes
            
            return condicionGen;
        }

        public async Task<condicionGenSetDto> InsertCondicionGenSet(condicionGenSetDto condicionGenSetDto)
        {
            //TODO: convencion de serie siempre A
            condicionGenSetDto.condicionActivo.tipoCondicion = "GENERADOR";
            condicionGenSetDto.condicionActivo.serie = "A";

            var xCondicionGenSet = _aguilaMap.Map<condicionGenSet>(condicionGenSetDto);

            var xCondicionActivo = xCondicionGenSet.condicionActivo;

            xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);

            if (xCondicionActivo.id == 0)
                return null;

            await _unitOfWork.condicionGenSetRepository.Add(xCondicionGenSet);
            await _unitOfWork.SaveChangeAsync();

            var xCondicionActivoDto = _aguilaMap.Map<condicionActivosDto>(xCondicionActivo);

            condicionGenSetDto.idCondicionActivo = xCondicionActivo.id;
            condicionGenSetDto.condicionActivo = xCondicionActivoDto;

            //llenarCondicionLlantas(xCondicionCabezal,ref condicionCabezalDto);

            return condicionGenSetDto;
        }

        public async Task<bool> UpdateCondicionGenSet(condicionGenSet condicionGenSet)
        {
            var currentCondicionGenSet = await _unitOfWork.condicionGenSetRepository.GetByID(condicionGenSet.idCondicionActivo);
            if (currentCondicionGenSet == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            currentCondicionGenSet.idCondicionActivo = condicionGenSet.idCondicionActivo;
            currentCondicionGenSet.galonesRequeridos = condicionGenSet.galonesRequeridos;

            var currentCondicionActivo = await _unitOfWork.condicionActivosRepository.GetByID(condicionGenSet.idCondicionActivo);
            if (currentCondicionActivo == null)
            {
                throw new AguilaException("Condicion No Existente!...");
            }

            // Guardamos el Recurso de Imagen
            if (condicionGenSet.condicionActivo.Fotos != null)
            {
                //Obligatorio enviar el id de imagen recurso guardado en la tabla
                condicionGenSet.condicionActivo.Fotos.Id = currentCondicionActivo.idImagenRecursoFotos ?? Guid.Empty;

                var imgRecursoOp = await _imagenesRecursosService.GuardarImagenRecurso(condicionGenSet.condicionActivo.Fotos, "condicionActivos", nameof(condicionGenSet.condicionActivo.Fotos));

                if (currentCondicionActivo.idImagenRecursoFotos == null || currentCondicionActivo.idImagenRecursoFotos == Guid.Empty)
                {
                    if (imgRecursoOp.Id != null && imgRecursoOp.Id != Guid.Empty)
                        currentCondicionActivo.idImagenRecursoFotos = imgRecursoOp.Id;
                }
            }
            //  Fin de recurso de Imagen   

            _unitOfWork.BeginTransaction();
            try
            {
                _unitOfWork.condicionGenSetRepository.Update(currentCondicionGenSet);
                await _unitOfWork.SaveChangeAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
            }

            return true;
        }

        public async Task<bool> DeleteCondicionGenSet(int id)
        {
            var currentCondicionGenSet = await _unitOfWork.condicionGenSetRepository.GetByID(id);
            if (currentCondicionGenSet == null)
            {
                throw new AguilaException("Condicion no existente");
            }

            await _unitOfWork.condicionGenSetRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<condicionActivos> prepararCondicionActivo(condicionActivosDto condicion)
        {
            var filter = new condicionActivosQueryFilter
            {
                idEstacionTrabajo = condicion.idEstacionTrabajo,
                serie = condicion.serie,
                tipoCondicion = condicion.tipoCondicion,
                anio = condicion.fecha.Year
            };

            var condicionActivos = new condicionActivos
            {
                tipoCondicion = condicion.tipoCondicion,
                idActivo = condicion.idActivo,
                idEstacionTrabajo = condicion.idEstacionTrabajo,
                idEmpleado = condicion.idEmpleado,
                idReparacion = condicion.idReparacion,
                idImagenRecursoFirma = condicion.idImagenRecursoFirma,
                idImagenRecursoFotos = condicion.idImagenRecursoFotos,
                ubicacionIdEntrega = condicion.ubicacionIdEntrega,
                idUsuario = condicion.idUsuario,
                movimiento = condicion.movimiento,
                disponible = condicion.disponible,
                cargado = condicion.cargado,
                serie = condicion.serie,
                numero = condicion.numero,
                observaciones = condicion.observaciones,
                fecha = condicion.fecha,
                fechaCreacion = DateTime.Now
            };

            await _unitOfWork.condicionActivosRepository.Add(condicionActivos);
            await _unitOfWork.SaveChangeAsync();
            return condicionActivos;
        }

        public condicionGenSet ultima(int idActivo)
        {
            var condicionGenSet = _unitOfWork.condicionGenSetRepository.GetUltima(idActivo);
            return condicionGenSet;
        }

        //REPORTE DE CONDICIONES DE EQUIPOS
        public IEnumerable<condicionActivos> reporteCondicionesGeneradores(reporteCondicionesGeneradoresQueryFilter filter, int usuario)
        {
            if (filter.idEmpresa is null)
                throw new AguilaException("Debe Especificar una empresa", 404);

            if (filter.fechaInicio == null || filter.fechaFin == null)
            {
                throw new AguilaException("Debe Especificar una Fecha de Inicio y una Fecha Final para Obtener los datos.", 404);
            }
            else
            {
                var dias = (filter.fechaFin - filter.fechaInicio).Value.Days;
                if (dias > 92) throw new AguilaException("Debe Especificar un Rango de Fechas No Mayor a 91 días.", 404);
            }

            var condicionesQuery = _unitOfWork.condicionActivosRepository.reporteCondicionesGeneradores((int)filter.idEmpresa, usuario).AsQueryable();

            if (filter.idEstado != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.idEstado == filter.idEstado);
            }

            if (filter.movimiento != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.movimiento.ToLower().Trim().Equals(filter.movimiento.ToLower().Trim()));
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.listaIdEstados != null)
            {
                var xEstados = filter.listaIdEstados.Split(",").Select(Int32.Parse).ToList();
                if (xEstados.Count > 0)
                    condicionesQuery = condicionesQuery.Where(x => xEstados.Contains((int)x.idEstado));
            }

            if (filter.codigo != null)
            {
                condicionesQuery = condicionesQuery.Where(x => x.activoOperacion.codigo.ToLower().Trim().StartsWith(filter.codigo.ToLower().Trim()));
            }

            //FILTRADO DE FECHAS

            condicionesQuery = condicionesQuery.Where(x => x.fecha >= filter.fechaInicio && x.fecha < Convert.ToDateTime(filter.fechaFin).AddDays(1));

            //ordenar por fecha
            condicionesQuery = condicionesQuery.OrderByDescending(x => x.fecha);

            var condiciones = condicionesQuery.ToList();
            return condiciones;
        }
    }
}
