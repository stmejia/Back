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
    public class condicionCisternaService : IcondicionCisternaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public condicionCisternaService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IAguilaMap aguilaMap,
                                        IcondicionActivosService condicionActivosService,
                                         IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _condicionActivosService = condicionActivosService;
            _aguilaMap = aguilaMap;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<condicionCisterna>> GetCondicionCisterna(condicionCisternaQueryFilter filter)
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

            var condicionCisterna = _unitOfWork.condicionCisternaRepository.GetAllIncludes();

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionCisterna = condicionCisterna.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.idCondicionActivo != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.idCondicionActivo == filter.idCondicionActivo);
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.movimiento != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.condicionActivo.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.disponible != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.condicionActivo.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.condicionActivo.cargado == filter.cargado);
            }

            if (filter.inspecVeriOrden != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.luzLateral != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.luzLateral.ToLower().Contains(filter.luzLateral.ToLower()));
            }

            if (filter.luzTrasera != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.luzTrasera.ToLower().Contains(filter.luzTrasera.ToLower()));
            }

            if (filter.guardaFango != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.guardaFango.ToLower().Contains(filter.guardaFango.ToLower()));
            }

            if (filter.cintaReflectiva != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.cintaReflectiva.ToLower().Contains(filter.cintaReflectiva.ToLower()));
            }

            if (filter.manitas != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.manitas.ToLower().Contains(filter.manitas.ToLower()));
            }

            if (filter.bumper != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.bumper == filter.bumper);
            }

            if (filter.patas != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.patas == filter.patas);
            }

            if (filter.ganchos != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.ganchos == filter.ganchos);
            }

            if (filter.fricciones != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.fricciones == filter.fricciones);
            }

            if (filter.escalera != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.escalera.ToLower().Contains(filter.escalera.ToLower()));
            }

            if (filter.señalizacion != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.señalizacion.ToLower().Contains(filter.señalizacion.ToLower()));
            }

            if (filter.taponValvulas != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.taponValvulas.ToLower().Contains(filter.taponValvulas.ToLower()));
            }

            if (filter.manHole != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.manHole.ToLower().Contains(filter.manHole.ToLower()));
            }

            if (filter.platinas != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.platinas.ToLower().Contains(filter.platinas.ToLower()));
            }

            if (filter.placaPatin != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.placaPatin==filter.placaPatin);
            }

            if (filter.llanta1 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta1.ToLower().Contains(filter.llanta1.ToLower()));
            }

            if (filter.llanta2 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta2.ToLower().Contains(filter.llanta2.ToLower()));
            }

            if (filter.llanta3 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta3.ToLower().Contains(filter.llanta3.ToLower()));
            }

            if (filter.llanta4 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta4.ToLower().Contains(filter.llanta4.ToLower()));
            }

            if (filter.llanta5 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta5.ToLower().Contains(filter.llanta5.ToLower()));
            }

            if (filter.llanta6 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta6.ToLower().Contains(filter.llanta6.ToLower()));
            }

            if (filter.llanta7 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta7.ToLower().Contains(filter.llanta7.ToLower()));
            }

            if (filter.llanta8 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta8.ToLower().Contains(filter.llanta8.ToLower()));
            }

            if (filter.llanta9 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta9.ToLower().Contains(filter.llanta9.ToLower()));
            }

            if (filter.llanta10 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta10.ToLower().Contains(filter.llanta10.ToLower()));
            }

            if (filter.llanta11 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta11.ToLower().Contains(filter.llanta11.ToLower()));
            }

            if (filter.llanta12 != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llanta12.ToLower().Contains(filter.llanta12.ToLower()));
            }

            if (filter.llantaR != null)
            {
                condicionCisterna = condicionCisterna.Where(e => e.llantaR.ToLower().Contains(filter.llantaR.ToLower()));
            }

            var pagedCondicionCisterna = PagedList<condicionCisterna>.create(condicionCisterna, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionCisterna.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionCisterna.Select(e => e.condicionActivo.Fotos).ToList());
            return pagedCondicionCisterna;
        }

        public async Task<condicionCisterna> GetCondicionCisterna(long idCondicion)
        {
            var condicionCisterna = _unitOfWork.condicionCisternaRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();
            //Manejo de imagenes
            if (condicionCisterna != null && condicionCisterna.condicionActivo.idImagenRecursoFirma != null && condicionCisterna.condicionActivo.idImagenRecursoFirma != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionCisterna.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionCisterna.condicionActivo.ImagenFirmaPiloto = imgRecurso;
            }

            if (condicionCisterna != null && condicionCisterna.condicionActivo.idImagenRecursoFotos != null && condicionCisterna.condicionActivo.idImagenRecursoFotos != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionCisterna.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionCisterna.condicionActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes

            return condicionCisterna;
        }

        public async Task<condicionCisternaDto> InsertCondicionCisterna(condicionCisternaDto condicionCisternaDto)
        {
            //TODO: convencion de serie siempre A
            condicionCisternaDto.condicionActivo.tipoCondicion = "CISTERNA";
            condicionCisternaDto.condicionActivo.serie = "A";

            var xCondicionCisterna = _aguilaMap.Map<condicionCisterna>(condicionCisternaDto);

            var xNumeroLlantas = condicionCisternaDto.condicionesLlantas.Count;
            var xNumeroLlantaRepuesto = condicionCisternaDto.condicionesLlantasRepuesto.Count;

            xCondicionCisterna.llanta1 = xNumeroLlantas >= 1 ? condicionCisternaDto.condicionesLlantas.ElementAt(0).ToString() : null;
            xCondicionCisterna.llanta2 = xNumeroLlantas >= 2 ? condicionCisternaDto.condicionesLlantas.ElementAt(1).ToString() : null;
            xCondicionCisterna.llanta3 = xNumeroLlantas >= 3 ? condicionCisternaDto.condicionesLlantas.ElementAt(2).ToString() : null;
            xCondicionCisterna.llanta4 = xNumeroLlantas >= 4 ? condicionCisternaDto.condicionesLlantas.ElementAt(3).ToString() : null;
            xCondicionCisterna.llanta5 = xNumeroLlantas >= 5 ? condicionCisternaDto.condicionesLlantas.ElementAt(4).ToString() : null;
            xCondicionCisterna.llanta6 = xNumeroLlantas >= 6 ? condicionCisternaDto.condicionesLlantas.ElementAt(5).ToString() : null;
            xCondicionCisterna.llanta7 = xNumeroLlantas >= 7 ? condicionCisternaDto.condicionesLlantas.ElementAt(6).ToString() : null;
            xCondicionCisterna.llanta8 = xNumeroLlantas >= 8 ? condicionCisternaDto.condicionesLlantas.ElementAt(7).ToString() : null;
            xCondicionCisterna.llanta9 = xNumeroLlantas >= 9 ? condicionCisternaDto.condicionesLlantas.ElementAt(8).ToString() : null;
            xCondicionCisterna.llanta10 = xNumeroLlantas >= 10 ? condicionCisternaDto.condicionesLlantas.ElementAt(9).ToString() : null;
            xCondicionCisterna.llanta11 = xNumeroLlantas >= 11 ? condicionCisternaDto.condicionesLlantas.ElementAt(10).ToString() : null;
            xCondicionCisterna.llanta12 = xNumeroLlantas >= 12 ? condicionCisternaDto.condicionesLlantas.ElementAt(11).ToString() : null;

            xCondicionCisterna.llantaR = xNumeroLlantaRepuesto >= 1 ? condicionCisternaDto.condicionesLlantasRepuesto.ElementAt(0).ToString() : null;
            xCondicionCisterna.llantaR2 = xNumeroLlantaRepuesto >= 2 ? condicionCisternaDto.condicionesLlantasRepuesto.ElementAt(1).ToString() : null;

            var xCondicionActivo = xCondicionCisterna.condicionActivo;

            xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);

            if (xCondicionActivo.id == 0)
                return null;

            await _unitOfWork.condicionCisternaRepository.Add(xCondicionCisterna);
            await _unitOfWork.SaveChangeAsync();

            var xCondicionActivoDto = _aguilaMap.Map<condicionActivosDto>(xCondicionActivo);

            condicionCisternaDto.idCondicionActivo = xCondicionActivo.id;
            condicionCisternaDto.condicionActivo = xCondicionActivoDto;

            //llenarCondicionLlantas(xCondicionCabezal,ref condicionCabezalDto);

            return condicionCisternaDto;
        }

        public async Task<bool> UpdateCondicionCisterna(condicionCisterna condicionCisterna)
        {
            var currentCondicionCisterna = await _unitOfWork.condicionCisternaRepository.GetByID(condicionCisterna.idCondicionActivo);
            if (currentCondicionCisterna == null)
            {
                throw new AguilaException("Condicion no existente...");
            }

            currentCondicionCisterna.idCondicionActivo = condicionCisterna.idCondicionActivo;
            currentCondicionCisterna.luzLateral = condicionCisterna.luzLateral;
            currentCondicionCisterna.luzTrasera = condicionCisterna.luzTrasera;
            currentCondicionCisterna.guardaFango = condicionCisterna.guardaFango;
            currentCondicionCisterna.cintaReflectiva = condicionCisterna.cintaReflectiva;
            currentCondicionCisterna.manitas = condicionCisterna.manitas;
            currentCondicionCisterna.bumper = condicionCisterna.bumper;
            currentCondicionCisterna.patas = condicionCisterna.patas;
            currentCondicionCisterna.ganchos = condicionCisterna.ganchos;
            currentCondicionCisterna.fricciones = condicionCisterna.fricciones;
            currentCondicionCisterna.escalera = condicionCisterna.escalera;
            currentCondicionCisterna.señalizacion = condicionCisterna.señalizacion;
            currentCondicionCisterna.taponValvulas = condicionCisterna.taponValvulas;
            currentCondicionCisterna.manHole = condicionCisterna.manHole;
            currentCondicionCisterna.platinas = condicionCisterna.platinas;
            currentCondicionCisterna.placaPatin = condicionCisterna.placaPatin;
            currentCondicionCisterna.llanta1 = condicionCisterna.llanta1;
            currentCondicionCisterna.llanta2 = condicionCisterna.llanta2;
            currentCondicionCisterna.llanta3 = condicionCisterna.llanta3;
            currentCondicionCisterna.llanta4 = condicionCisterna.llanta4;
            currentCondicionCisterna.llanta5 = condicionCisterna.llanta5;
            currentCondicionCisterna.llanta6 = condicionCisterna.llanta6;
            currentCondicionCisterna.llanta7 = condicionCisterna.llanta7;
            currentCondicionCisterna.llanta8 = condicionCisterna.llanta8;
            currentCondicionCisterna.llanta9 = condicionCisterna.llanta9;
            currentCondicionCisterna.llanta10 = condicionCisterna.llanta10;
            currentCondicionCisterna.llanta11 = condicionCisterna.llanta11;
            currentCondicionCisterna.llanta12 = condicionCisterna.llanta12;
            currentCondicionCisterna.llantaR = condicionCisterna.llantaR;

            _unitOfWork.condicionCisternaRepository.Update(currentCondicionCisterna);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteCondicionCisterna(int id)
        {
            var currentCondicionCisterna = await _unitOfWork.condicionCisternaRepository.GetByID(id);
            if (currentCondicionCisterna == null)
            {
                throw new AguilaException("Condicion no existente");
            }

            await _unitOfWork.condicionCisternaRepository.Delete(id);
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

        public condicionCisterna ultima(int idActivo)
        {
            var condicionCisterna = _unitOfWork.condicionCisternaRepository.GetUltima(idActivo);
            return condicionCisterna;
        }

        public void llenarCondicionLlantas(condicionCisterna condicionCisterna, ref condicionCisternaDto condicionCisternaDto)
        {
            Func<List<condicionLlantaDto>, int, string, bool> agregarLlanta = (lista, id, llantaString) => {
                var xLlanta = new condicionLlantaDto(llantaString);
                if (!string.IsNullOrEmpty(xLlanta.codigo))
                {
                    xLlanta.id = id;
                    lista.Add(xLlanta);
                }
                return false;
            };

            var xLlantas = new List<condicionLlantaDto>();

            agregarLlanta(xLlantas, 1, condicionCisterna.llanta1);
            agregarLlanta(xLlantas, 2, condicionCisterna.llanta2);
            agregarLlanta(xLlantas, 3, condicionCisterna.llanta3);
            agregarLlanta(xLlantas, 4, condicionCisterna.llanta4);
            agregarLlanta(xLlantas, 5, condicionCisterna.llanta5);
            agregarLlanta(xLlantas, 6, condicionCisterna.llanta6);
            agregarLlanta(xLlantas, 7, condicionCisterna.llanta7);
            agregarLlanta(xLlantas, 8, condicionCisterna.llanta8);
            agregarLlanta(xLlantas, 9, condicionCisterna.llanta9);
            agregarLlanta(xLlantas, 10, condicionCisterna.llanta10);
            agregarLlanta(xLlantas, 11, condicionCisterna.llanta11);
            agregarLlanta(xLlantas, 12, condicionCisterna.llanta12);

            condicionCisternaDto.condicionesLlantas = xLlantas;

            var xLlantasRepuesto = new List<condicionLlantaDto>();

            agregarLlanta(xLlantasRepuesto, 1, condicionCisterna.llantaR);
            agregarLlanta(xLlantasRepuesto, 2, condicionCisterna.llantaR2);

            condicionCisternaDto.condicionesLlantasRepuesto = xLlantasRepuesto;
        }
    }
}
