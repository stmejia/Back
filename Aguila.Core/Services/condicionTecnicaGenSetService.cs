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
    public class condicionTecnicaGenSetService : IcondicionTecnicaGenSetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IcondicionActivosService _condicionActivosService;
        private readonly IAguilaMap _aguilaMap;
        private readonly IImagenesRecursosService _imagenesRecursosService;


        public condicionTecnicaGenSetService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options, IAguilaMap aguilaMap,
                                             IcondicionActivosService condicionActivosService,
                                             IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _aguilaMap = aguilaMap;
            _condicionActivosService = condicionActivosService;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<condicionTecnicaGenSet>> GetCondicionTecnicaGenSet(condicionTecnicaGenSetQueryFilter filter)
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

            var condicionTecnicaGenSet = _unitOfWork.condicionTecnicaGenSetRepository.GetAllIncludes();

            //if (filter.idActivo != null)
            //{
            //    condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.idActivo == filter.idActivo);
            //}

            //Filtrado por fechas
            if (!filter.ignorarFechas)
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.condicionActivo.fecha >= filter.fechaInicio && e.condicionActivo.fecha < filter.fechaFin.Value.AddDays(1));

            if (filter.movimiento != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.condicionActivo.movimiento.ToLower().Contains(filter.movimiento.ToLower()));
            }

            if (filter.idEstacionTrabajo != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.condicionActivo.idEstacionTrabajo == filter.idEstacionTrabajo);
            }

            if (filter.disponible != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.condicionActivo.disponible == filter.disponible);
            }

            if (filter.cargado != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.condicionActivo.cargado == filter.cargado);
            }

            if (filter.inspecVeriOrden != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.condicionActivo.inspecVeriOrden == filter.inspecVeriOrden);
            }

            if (filter.bateriaCodigo != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bateriaCodigo.ToLower().Contains(filter.bateriaCodigo.ToLower()));
            }

            if (filter.bateriaNivelAcido != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bateriaNivelAcido == filter.bateriaNivelAcido);
            }

            if (filter.bateriaArnes != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bateriaArnes == filter.bateriaArnes);
            }

            if (filter.bateriaTerminales != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bateriaTerminales == filter.bateriaTerminales);
            }

            if (filter.bateriaGolpes != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bateriaGolpes == filter.bateriaGolpes);
            }

            if (filter.bateriaCarga != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bateriaCarga == filter.bateriaCarga);
            }

            if (filter.combustibleDiesel != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.combustibleDiesel == filter.combustibleDiesel);
            }

            if (filter.combustibleAgua != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.combustibleAgua == filter.combustibleAgua);
            }

            if (filter.combustibleAceite != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.combustibleAceite == filter.combustibleAceite);
            }

            if (filter.combustibleFugas != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.combustibleFugas == filter.combustibleFugas);
            }

            if (filter.filtroAceite != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.filtroAceite == filter.filtroAceite);
            }

            if (filter.filtroDiesel != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.filtroDiesel == filter.filtroDiesel);
            }

            if (filter.bombaAguaEstado != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.bombaAguaEstado == filter.bombaAguaEstado);
            }

            if (filter.escapeAgujeros != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.escapeAgujeros == filter.escapeAgujeros);
            }

            if (filter.escapeDañado != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.escapeDañado == filter.escapeDañado);
            }

            if (filter.cojinetesEstado != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.cojinetesEstado == filter.cojinetesEstado);
            }

            if (filter.arranqueFuncionamiento != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.arranqueFuncionamiento == filter.arranqueFuncionamiento);
            }

            if (filter.fajaAlternador != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.fajaAlternador == filter.fajaAlternador);
            }

            if (filter.enfriamientoAire != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.enfriamientoAire == filter.enfriamientoAire);
            }

            if (filter.enfriamientoAgua != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.enfriamientoAgua == filter.enfriamientoAgua);
            }

            if (filter.cantidadGeneradaVolts != null)
            {
                condicionTecnicaGenSet = condicionTecnicaGenSet.Where(e => e.cantidadGeneradaVolts == filter.cantidadGeneradaVolts);
            }

            var pagedCondicionTecnicaGenSet = PagedList<condicionTecnicaGenSet>.create(condicionTecnicaGenSet, filter.PageNumber, filter.PageSize);

            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionTecnicaGenSet.Select(e => e.condicionActivo.ImagenFirmaPiloto).ToList());
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedCondicionTecnicaGenSet.Select(e => e.condicionActivo.Fotos).ToList());
            return pagedCondicionTecnicaGenSet;
        }

        public async Task<condicionTecnicaGenSet> GetCondicionTecnicaGenSet(long idCondicion)
        {
            var condicionTecnica = _unitOfWork.condicionTecnicaGenSetRepository.GetAllIncludes().Where(c => c.idCondicionActivo == idCondicion).FirstOrDefault();
            //Manejo de imagenes
            if (condicionTecnica != null && condicionTecnica.condicionActivo.idImagenRecursoFirma != null && condicionTecnica.condicionActivo.idImagenRecursoFirma != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionTecnica.condicionActivo.idImagenRecursoFirma ?? Guid.Empty);
                condicionTecnica.condicionActivo.ImagenFirmaPiloto = imgRecurso;
            }

            if (condicionTecnica != null && condicionTecnica.condicionActivo.idImagenRecursoFotos != null && condicionTecnica.condicionActivo.idImagenRecursoFotos != Guid.Empty)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(condicionTecnica.condicionActivo.idImagenRecursoFotos ?? Guid.Empty);
                condicionTecnica.condicionActivo.Fotos = imgRecurso;
            }
            //Fin Imagenes

            return condicionTecnica;
        }

        public async Task<condicionTecnicaGenSetDto> InsertCondicionTecnicaGenSet(condicionTecnicaGenSetDto condicionTecnicaGenSetDto)
        {
            //TODO: convencion de serie siempre A
            condicionTecnicaGenSetDto.condicionActivo.tipoCondicion = "TECNICA";
            condicionTecnicaGenSetDto.condicionActivo.serie = "A";

            var xCondicionTecnicaGenSet = _aguilaMap.Map<condicionTecnicaGenSet>(condicionTecnicaGenSetDto);

            var xCondicionActivo = xCondicionTecnicaGenSet.condicionActivo;

            xCondicionActivo = await _condicionActivosService.insert(xCondicionActivo);

            if (xCondicionActivo.id == 0)
                return null;

            await _unitOfWork.condicionTecnicaGenSetRepository.Add(xCondicionTecnicaGenSet);
            await _unitOfWork.SaveChangeAsync();

            var xCondicionActivoDto = _aguilaMap.Map<condicionActivosDto>(xCondicionActivo);

            condicionTecnicaGenSetDto.idCondicionActivo = xCondicionActivo.id;
            condicionTecnicaGenSetDto.condicionActivo = xCondicionActivoDto;

            //llenarCondicionLlantas(xCondicionCabezal,ref condicionCabezalDto);

            return condicionTecnicaGenSetDto;
        }

        public async Task<bool> UpdateCondicionTecnicaGenSet(condicionTecnicaGenSet condicionTecnicaGenSet)
        {
            var currentCondicionTecnicaGenSet = await _unitOfWork.condicionTecnicaGenSetRepository.GetByID(condicionTecnicaGenSet.idCondicionActivo);
            if (currentCondicionTecnicaGenSet == null)
            {
                throw new AguilaException("Condicion Tecnica no existente...");
            }

            currentCondicionTecnicaGenSet.idCondicionActivo = condicionTecnicaGenSet.idCondicionActivo;
            currentCondicionTecnicaGenSet.bateriaCodigo = condicionTecnicaGenSet.bateriaCodigo;

            _unitOfWork.condicionTecnicaGenSetRepository.Update(currentCondicionTecnicaGenSet);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteCondicionTecnicaGenSet(int id)
        {
            var currentCondicionTecnicaGenSet = await _unitOfWork.condicionTecnicaGenSetRepository.GetByID(id);
            if (currentCondicionTecnicaGenSet == null)
            {
                throw new AguilaException("Condicion Tecnica no existente");
            }

            await _unitOfWork.condicionTecnicaGenSetRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public condicionTecnicaGenSet ultima(int idActivo)
        {
            var condicionTecnicaGenSet = _unitOfWork.condicionTecnicaGenSetRepository.GetUltima(idActivo);
            return condicionTecnicaGenSet;
        }
    }
}
