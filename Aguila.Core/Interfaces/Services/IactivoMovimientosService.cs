using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Enumeraciones;
using System.Collections.Generic;
using Aguila.Core.DTOs.DTOsRespuestas;

namespace Aguila.Core.Interfaces.Services
{
    public interface IactivoMovimientosService
    {
        PagedList<activoMovimientos> GetActivoMovimientos(activoMovimientosQueryFilter filter);
        Task<activoMovimientos> GetActivoMovimiento(int id);
        Task InsertActivoMovimiento(activoMovimientos activoMovimiento, ControlActivosEventos evento);
        Task<activoMovimientos> InsertMovimientoPorEvento(activoMovimientosDto evento);
        Task<bool> UpdateActivoMovimiento(activoMovimientos activoMovimiento);
        Task<bool> DeleteActivoMovimiento(int id);
        Task<bool> Reserva(int? solicitudMovimientoIntegracionId, int idEquipo, bool reservar = false);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        Task<string> validarMovimiento(int idActivo, ControlActivosEventos evento);
        IEnumerable<activoMovimientos> reporteMovimientos(reporteActivoMovimientosQueryFilter filter);
        IEnumerable<movimientoEquipoRemolqueDto> reporteMovimientosEquipos(ReporteMovimientosEquiposRemolqueQueryFilter filter);
        IEnumerable<movimientoVehiculoDto> reporteMovimientosVehiculos(ReporteMovimientosVehiculosQueryFilter filter);
        IEnumerable<movimientoGeneradoresDto> reporteMovimientosGeneradores(reporteMovimientosGeneradoresQueryFilter filter);
        Task<bool> verificarEvento(activoMovimientosDto movimiento);

    }
}
