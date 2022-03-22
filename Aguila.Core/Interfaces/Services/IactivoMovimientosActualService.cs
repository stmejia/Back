using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Threading.Tasks;
using Aguila.Core.CustomEntities;
using Aguila.Interfaces.Trafico.Model;
using Aguila.Interfaces.Trafico.QueryFilters;
using Aguila.Core.Enumeraciones;
using System.Collections;
using System.Collections.Generic;

namespace Aguila.Core.Interfaces.Services
{
    public interface IactivoMovimientosActualService
    {
        PagedList<activoMovimientosActual> GetActivoMovimientosActual(activoMovimientosActualQueryFilter filter);
        Task<activoMovimientosActual> GetActivoMovimientoActual(int id);
        activoMovimientosActual GetActivoMovimientoActualByCodigo(string codigo, int empresaId);
        equipoRemolque GetEquipoByCodigo(string codigo, int idEmpresa);
        vehiculos GetVehiculoByCodigo(string codigo, int idEmpresa);
        generadores GetGeneradorByCodigo(string codigo, int idEmpresa);
        Task InsertActivoMovimientoActual(activoMovimientosActual activoMovimientoActual);
        Task actualizarMovimientosActual(activoMovimientos movimiento, ControlActivosEventos evento);
        Task<bool> UpdateActivoMovimientoActual(activoMovimientosActual activoMovimientoActual);
        Task<bool> DeleteActivoMovimientoActual(int id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
        PagedList<SolicitudesMovimientosIntegracion> SolicitudesReservasAltas(SolicitudesMovimientosQueryFilter filter);
        PagedList<vehiculos> GetVehiculoEstadoActual(vehiculosQueryFilter filter);
        PagedList<generadores> GetGeneradoresEstadoActual(generadoresQueryFilter filter);
        vehiculos GetVehiculoByID(int idActivo);
        generadores GetGeneradorByID(int idActivo);
        PagedList<equipoRemolque> GetEquiposEstadoActual(equipoRemolqueQueryFilter filter);
        equipoRemolque GetEquipoByID(int idActivo);
        IEnumerable<vehiculos> reporteInventarioVehiculos(vehiculosQueryFilter filter, int usuario);
        IEnumerable<equipoRemolque> reporteInventarioEquipo(equipoRemolqueQueryFilter filter, int usuario);
        IEnumerable<generadores> reporteInventarioGeneradores(generadoresQueryFilter filter, int usuario);
        
    }
}
