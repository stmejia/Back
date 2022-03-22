using Aguila.Interfaces.Trafico.Data;
using Aguila.Interfaces.Trafico.Model;
using Aguila.Interfaces.Trafico.QueryFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;


namespace Aguila.Interfaces.Trafico.Services
{
    public class SolicitudesMovimientoService  
    {
        private readonly TraficoDBContext _context;

        public SolicitudesMovimientoService(TraficoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<SolicitudesMovimientosIntegracion> MovimientosPendietesReservaEquipoRemolque(SolicitudesMovimientosQueryFilter soliFilter)
        {
            //Devuelve un listado de Movimientos de solicitid pendientes de Reservar Equipo de Remolque
            //Podemos devolver unicamente un registro en la coleccion utilizando el Filtro SolicitudMovimientoIntegracionID

            var soliMovs = _context.SolicitudesmovimientosIntegracion
                .Include(i => i.solicitudMovimiento).ThenInclude(s => s.clientes)
                .Include(i => i.solicitudMovimiento).ThenInclude(s => s.SectorOrigen)
                .Include(i => i.solicitudMovimiento).ThenInclude(s => s.SectorDestino)
                .Include(i => i.solicitudMovimiento).ThenInclude(s => s.TipoMovimiento)
                .Include(i => i.solicitudMovimiento).ThenInclude(s => s.TipoDocumento)
                .Include(i => i.solicitudMovimiento).ThenInclude(s => s.Usuario)
                .Include(i => i.Servicio)
                .Include(i => i.tipoContenedor)
                .Include(i => i.EquiposEstatus).ThenInclude(e => e.equipoRemolque)
                .AsQueryable();

            //Filtarmos unicamente el registro solicitado y retornamos el registro
            if (soliFilter.solicituMovimientoIntegracionId != null)
            {
                return soliMovs.Where(i => i.SlMIID == soliFilter.solicituMovimientoIntegracionId);
            }

            //Establecemos filtro obligatorio
            soliMovs = soliMovs.Where(i =>
                    i.solicitudMovimiento.EstaID == soliFilter.estacionTrabajoId
                    && i.solicitudMovimiento.SoliFin == false
                    && i.SlMIAut == true
                    && i.solicitudMovimiento.SoliAnul == false
                    && i.solicitudMovimiento.SoliFch >= soliFilter.fechaDel
                    && i.solicitudMovimiento.SoliFch <= soliFilter.fechaAl
                    && i.solicitudMovimiento.SoliAut == true
                    ).AsQueryable();

            //Filtro Opcional                       

            if (soliFilter.ClienteNombre != null)
                soliMovs = soliMovs.Where(i => i.solicitudMovimiento.clientes.ClieNombre.ToUpper().Trim().Contains(soliFilter.ClienteNombre.ToUpper().Trim()));

            if (soliFilter.clienteCodigo != null)
                soliMovs = soliMovs.Where(i => i.solicitudMovimiento.clientes.ClieCodigo == soliFilter.clienteCodigo);

            if (soliFilter.contenedorPrefijo != null)
                soliMovs = soliMovs.Where(i => i.SlMIContPref.ToUpper().Trim().Contains(soliFilter.contenedorPrefijo.ToUpper().Trim()));

            if (soliFilter.contenedorNumero != null)
                soliMovs = soliMovs.Where(i => i.SlMIContNumero.ToUpper().Trim().Contains(soliFilter.contenedorNumero.ToUpper().Trim()));

            if (soliFilter.solicitudDel != null)
                soliMovs = soliMovs.Where(i => Convert.ToInt32(i.solicitudMovimiento.SoliCodigo) >= (int)soliFilter.solicitudDel);

            if (soliFilter.solicitudAl != null)
                soliMovs = soliMovs.Where(i => Convert.ToInt32(i.solicitudMovimiento.SoliCodigo) <= (int)soliFilter.solicitudAl);

            if (soliFilter.booking != null)
                soliMovs = soliMovs.Where(i => i.solicitudMovimiento.SoliBooking.ToUpper().Trim().Contains(soliFilter.booking.ToUpper().Trim()));

            // Exists  --> Any   --Que el item de solicitud no tenga movimiento
            if (soliFilter.documento == SolicitudesMovimientosQueryFilter.Documento.Envio)
                soliMovs = soliMovs.Where(i => i.EquiposEstatus == null && !i.Movimientos.Any(m => m.SlMIID == i.SlMIID));


            return soliMovs;
        }

        public bool reservarEquipo(int? solicitudMovimientoIntegracionID, string equipoCodigo, int empresaID, bool quitarReserva = false)
        {

            //Rectificar igual ancho de caracteres para la busqueda
            //CH200003   aguila   8 caracteres
            //CH20085    altas    7 caracteres

            int xCorrel = 0;

            if (!int.TryParse(equipoCodigo.Substring(4, equipoCodigo.Length - 4), out xCorrel))
            {
                return false;
            }

            string xCodigo = "";

            try
            {
                xCodigo = equipoCodigo.Substring(0, 3) + xCorrel.ToString().Trim().PadLeft(4, '0');
            }
            catch (IOException e)
            {
                return false;
            }
            //Fin de la rectificacion 

            var equipo = _context.EquipoRemolque.Where(r => r.EmprID == empresaID && r.EqRmCodigo.ToUpper().Trim() == xCodigo.Trim().ToUpper()).FirstOrDefault();
            if (equipo == null)
            {
                return false;
            }

            var equipoStatus = _context.EquiposEstatus.Where(e => e.TipoEq.ToUpper().Trim() == "EQUIPO" && e.EquiID == equipo.EqRmID).FirstOrDefault();

            if (equipoStatus == null)
                return false;

            //Reservar
            if (quitarReserva == false)
            {
                equipoStatus.SlMIID = solicitudMovimientoIntegracionID;
            }
            //Quitar Reserva
            else
            {
                equipoStatus.SlMIID = null;
            }

            _context.EquiposEstatus.Update(equipoStatus);

            _context.SaveChanges();

            return true;
        }
    }
}
