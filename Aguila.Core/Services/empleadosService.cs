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
    public class empleadosService : IempleadosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly IImagenesRecursosService _imagenesRecursosService;

        public empleadosService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,
                                IImagenesRecursosService imagenesRecursosService)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _imagenesRecursosService = imagenesRecursosService;
        }

        public async Task<PagedList<empleados>> GetEmpleados(empleadosQueryFilter filter)
        
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            var empleados = _unitOfWork.empleadosRepository.GetAllIncludes();
           

            if (filter.codigo != null)
            {
                empleados = empleados.Where(e => e.codigo.ToLower().Contains(filter.codigo.ToLower()));
            }

            if (filter.codigoAnterior != null)
            {
                empleados = empleados.Where(e => e.codigoAnterior.ToLower().Contains(filter.codigoAnterior.ToLower()));
            }

            //if (filter.idEmpresa is null)
            //{
            //    throw new AguilaException("Debe ingresar un id de Empresa");
            //}
            //else
            //{
            //    empleados = empleados.Where(e => e.idEmpresa == filter.idEmpresa);
            //}

            if (filter.idEmpresa != null)
            {
                empleados = empleados.Where(e => e.idEmpresa == filter.idEmpresa);
            }

            if (filter.nombres != null)
            {
                empleados = empleados.Where(e => e.nombres.ToLower().Contains(filter.nombres.ToLower()));
            }

            if (filter.apellidos != null)
            {
                empleados = empleados.Where(e => e.apellidos.ToLower().Contains(filter.apellidos.ToLower()));
            }

            if (filter.dpi != null)
            {
                empleados = empleados.Where(e => e.dpi.ToLower().Contains(filter.dpi.ToLower()));
            }

            if (filter.nit != null)
            {
                empleados = empleados.Where(e => e.nit.ToLower().Contains(filter.nit.ToLower()));
            }

            if (filter.idDireccion != null)
            {
                empleados = empleados.Where(e => e.idDireccion == filter.idDireccion);
            }

            if (filter.telefono != null)
            {
                empleados = empleados.Where(e => e.telefono == filter.telefono);
            }

            if (filter.fechaAlta != null)
            {
                empleados = empleados.Where(e => e.fechaAlta == filter.fechaAlta);
            }

            if (filter.licenciaConducir != null)
            {
                empleados = empleados.Where(e => e.licenciaConducir.ToLower().Contains(filter.licenciaConducir.ToLower()));
            }

            if (filter.fechaNacimiento != null)
            {
                empleados = empleados.Where(e => e.fechaNacimiento == filter.fechaNacimiento);
            }

            if (filter.fechaBaja != null)
            {
                empleados = empleados.Where(e => e.fechaBaja == filter.fechaBaja);
            }

            //if (filter.puesto != null)
            //{
            //    empleados = empleados.Where(e => e.puesto.ToLower().Contains(filter.puesto.ToLower()));
            //}

            if (filter.pais != null)
            {
                empleados = empleados.Where(e => e.pais.ToLower().Contains(filter.pais.ToLower()));
            }

            

            if (filter.correlativo != null)
            {
                empleados = empleados.Where(e => e.correlativo.ToLower().Contains(filter.correlativo.ToLower()));
            }

            if (filter.area != null)
            {
                empleados = empleados.Where(e => e.area.ToLower().Contains(filter.area.ToLower()));
            }

            if (filter.subArea != null)
            {
                empleados = empleados.Where(e => e.subArea.ToLower().Contains(filter.subArea.ToLower()));
            }

            if (filter.puesto != null)
            {
                empleados = empleados.Where(e => e.puesto.ToLower().Contains(filter.puesto.ToLower()));
            }

            if (filter.categoria != null)
            {
                empleados = empleados.Where(e => e.categoria.ToLower().Contains(filter.categoria.ToLower()));
            }

            if (filter.localidad != null)
            {
                empleados = empleados.Where(e => e.localidad.ToLower().Contains(filter.localidad.ToLower()));
            }

            if (filter.idEmpresaEmpleador != null)
            {
                empleados = empleados.Where(e => e.idEmpresaEmpleador == filter.idEmpresaEmpleador);
            }

            if (filter.estado != null)
            {
                empleados = empleados.Where(e => e.estado.ToLower().Contains(filter.estado.ToLower()));
            }

            if (filter.dependencia != null)
            {
                empleados = empleados.Where(e => e.dependencia.ToLower().Contains(filter.dependencia.ToLower()));
            }

            var Empleados = empleados.ToList();

            if (filter.fPuesto != null) {

                Boolean filtroValido = false;

                if (filter.fPuesto.ToLower().Equals("pilotos"))
                {
                    filtroValido = true;
                    var pilotos = _unitOfWork.pilotosRepository.GetAll();
                    var joinEmpleados = (from e in empleados.ToList()
                                         join p in pilotos.ToList() on e.id equals p.idEmpleado
                                         select new { e }).ToList();

                    List<empleados> listaEmpleados = new List<empleados>();
                    foreach (var emp in joinEmpleados)
                    {
                        listaEmpleados.Add(emp.e);
                    }
                    //IEnumerable<empleados> enumEmpleados = listaEmpleados;
                    Empleados = listaEmpleados;
                }

                if (filter.fPuesto.ToLower().Equals("asesores"))
                {
                    filtroValido = true;
                    var asesores = _unitOfWork.asesoresRepository.GetAll();
                    var joinEmpleados = (from e in empleados.ToList()
                                         join a in asesores.ToList() on e.id equals a.idEmpleado
                                         select new { e }).ToList();

                    List<empleados> listaEmpleados = new List<empleados>();
                    foreach (var emp in joinEmpleados)
                    {
                        listaEmpleados.Add(emp.e);
                    }
                    //IEnumerable<empleados> enumEmpleados = listaEmpleados;
                    Empleados = listaEmpleados;
                }


                if (filter.fPuesto.ToLower().Equals("mecanicos"))
                {
                    filtroValido = true;
                    var mecanicos = _unitOfWork.mecanicosRepository.GetAll();
                    var joinEmpleados = (from e in empleados.ToList()
                                         join m in mecanicos.ToList() on e.id equals m.idEmpleado
                                         select new { e }).ToList();

                    List<empleados> listaEmpleados = new List<empleados>();
                    foreach (var emp in joinEmpleados)
                    {
                        listaEmpleados.Add(emp.e);
                    }
                    //IEnumerable<empleados> enumEmpleados = listaEmpleados;
                    Empleados = listaEmpleados;
                }

                if (!filtroValido) 
                {
                    throw new AguilaException("Filtro de Tipo Empleado Invalido, revise sus datos...");
                }


            }

            empleados = empleados.OrderByDescending(e=>Convert.ToInt32(e.correlativo));

            var pagedEmpleados = PagedList<empleados>.create(empleados, filter.PageNumber, filter.PageSize);

            // Manejo de Imagenes, colocar la Url a las imagenes por defecto para toda la lista
            await _imagenesRecursosService.AsignarUrlImagenesDefault(pagedEmpleados.Select(e => e.Fotos).ToList());
            return pagedEmpleados;
        }

        public async Task<empleados> GetEmpleado(int id)
        {
            var empleado = await _unitOfWork.empleadosRepository.GetByIdIncludes(id);

            //Manejo de imagenes
            if (empleado != null && empleado.idImagenRecursoFotografias != null && empleado.idImagenRecursoFotografias != Guid.Empty && empleado.Fotos == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(empleado.idImagenRecursoFotografias ?? Guid.Empty);
                empleado.Fotos = imgRecurso;
            }
            //Fin Imagenes

            return empleado;
        }

        public async Task InsertEmpleado(empleados empleado)
        {
            //Insertamos la fecha de ingreso del registro
            empleado.id = 0;
            empleado.fechaCreacion = DateTime.Now;
            empleado.pais = empleado.pais.ToUpper().Trim();
           
            empleado.departamento = empleado.departamento.ToUpper().Trim();
            empleado.subArea = empleado.subArea.ToUpper().Trim();
            empleado.categoria = empleado.categoria.ToUpper().Trim();
            empleado.localidad = empleado.localidad.ToUpper().Trim();
            

            if (empleado.codigoAnterior != null) { empleado.codigoAnterior = empleado.codigoAnterior.ToUpper().Trim(); }
            if (empleado.dependencia != null) { empleado.dependencia = empleado.dependencia.ToUpper().Trim(); }

            var currentEmpleadoRazonSocial = await _unitOfWork.EmpresaRepository.GetByID(empleado.idEmpresa);
            var currentEmpleadoEmpleador = await _unitOfWork.EmpresaRepository.GetByID(empleado.idEmpresaEmpleador);

            //Agregamos ceros a la izquierda para completar la longitud del correlativo
            //if (empleado.correlativo.Length < 4)
            //    empleado.correlativo = empleado.correlativo.PadLeft(4,'0');

            //Se arma el codigo COP/CUI
            if (currentEmpleadoEmpleador == null)
                throw new AguilaException("Empleador no existente");

            var cop = empleado.departamento + empleado.area + empleado.subArea + empleado.puesto +
                      empleado.categoria + "-" + empleado.localidad + "-" + currentEmpleadoEmpleador.Abreviatura + empleado.estado;
            empleado.cop = cop.ToUpper().Trim();

            if (currentEmpleadoRazonSocial == null)
                throw new AguilaException("Razon social no existente");

            var currentCorrelativo = obtenerCorrelativo(empleado.pais, empleado.idEmpresa);

            var cui = empleado.pais + "-" + currentEmpleadoRazonSocial.Abreviatura + currentCorrelativo;
            empleado.codigo = cui.ToUpper().Trim();
            //Fin COP/CUI

            empleado.correlativo = currentCorrelativo;

            //Guardamos el recurso de imagen
            if (empleado.Fotos != null)
            {
                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(empleado.Fotos, "empleados", nameof(empleado.Fotos));

                if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                    empleado.idImagenRecursoFotografias = imgRecurso.Id;
            }
            //  Fin de recurso de Imagen  

            await _unitOfWork.empleadosRepository.Add(empleado);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<bool> UpdateEmpleado(empleados empleado)
        {
            var currentEmpleado = await _unitOfWork.empleadosRepository.GetByID(empleado.id);
            if (currentEmpleado == null)
            {
                throw new AguilaException("Empleado no existente...");
            }

            currentEmpleado.nombres = empleado.nombres;
            currentEmpleado.apellidos = empleado.apellidos;
            currentEmpleado.dpi = empleado.dpi;
            currentEmpleado.nit = empleado.nit;
            currentEmpleado.idDireccion = empleado.idDireccion;
            currentEmpleado.telefono = empleado.telefono;
            currentEmpleado.fechaAlta = empleado.fechaAlta;
            currentEmpleado.fechaBaja = empleado.fechaBaja;
            currentEmpleado.licenciaConducir = empleado.licenciaConducir;
            currentEmpleado.fechaNacimiento = empleado.fechaNacimiento;
            currentEmpleado.idEmpresa = empleado.idEmpresa;
            currentEmpleado.pais = empleado.pais.ToUpper().Trim();

            currentEmpleado.correlativo = empleado.correlativo;
            currentEmpleado.departamento = empleado.departamento.ToUpper().Trim();
            currentEmpleado.area = empleado.area;
            currentEmpleado.subArea = empleado.subArea.ToUpper().Trim();
            currentEmpleado.puesto = empleado.puesto;
            currentEmpleado.categoria = empleado.categoria.ToUpper().Trim();
            currentEmpleado.localidad = empleado.localidad.ToUpper().Trim();
            currentEmpleado.idEmpresaEmpleador = empleado.idEmpresaEmpleador;
            currentEmpleado.estado = empleado.estado;
            currentEmpleado.placas = empleado.placas;

            if (currentEmpleado.codigoAnterior != null) { currentEmpleado.codigoAnterior = empleado.codigoAnterior.ToUpper().Trim(); }
            if (currentEmpleado.dependencia != null){ currentEmpleado.dependencia = empleado.dependencia.ToUpper().Trim(); }

            //var currentEmpleadoRazonSocial = await _unitOfWork.EmpresaRepository.GetByID(empleado.idEmpresa);
            var currentEmpleadoEmpleador = await _unitOfWork.EmpresaRepository.GetByID(empleado.idEmpresaEmpleador);

            //Agregamos ceros a la izquierda para completar la longitud del correlativo
            if (empleado.correlativo.Length < 4)
                empleado.correlativo = empleado.correlativo.PadLeft(4, '0');

            //Se arma el codigo COP/CUI
            var cop = empleado.departamento.ToUpper().Trim() + empleado.area + empleado.subArea.ToUpper().Trim() + empleado.puesto + 
                      empleado.categoria.ToUpper().Trim() + "-" + empleado.localidad.ToUpper().Trim() + "-" + currentEmpleadoEmpleador.Abreviatura.ToUpper().Trim() + empleado.estado;
            currentEmpleado.cop = cop;

            //var cui = empleado.pais + "-" + currentEmpleadoRazonSocial.Abreviatura + empleado.correlativo;
            //currentEmpleado.codigo = cui.ToUpper().Trim();
            //Fin COP/CUI

            // Guardamos el Recurso de Imagen
            if (empleado.Fotos != null)
            {
                empleado.Fotos.Id = currentEmpleado.idImagenRecursoFotografias ?? Guid.Empty;

                var imgRecurso = await _imagenesRecursosService.GuardarImagenRecurso(empleado.Fotos, "empleados", nameof(empleado.Fotos));

                if (currentEmpleado.idImagenRecursoFotografias == null || currentEmpleado.idImagenRecursoFotografias == Guid.Empty)
                {
                    if (imgRecurso.Id != null && imgRecurso.Id != Guid.Empty)
                        currentEmpleado.idImagenRecursoFotografias = imgRecurso.Id;
                }
            }
            //  Fin de recurso de Imagen     

            try
            {
                _unitOfWork.empleadosRepository.Update(currentEmpleado);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                throw new AguilaException("Ocurrio un error, por favor verifique sus datos:.." + ex.InnerException.Message);
            }

            

            return true;
        }

        public async Task<bool> DeleteEmpleado(int id)
        {
            var currentEmpleado = await _unitOfWork.empleadosRepository.GetByID(id);
            if (currentEmpleado == null)
            {
                throw new AguilaException("Empleado no existente...");
            }

            // Eliminamos el recurso de imagen
            if (currentEmpleado.idImagenRecursoFotografias != null && currentEmpleado.idImagenRecursoFotografias != Guid.Empty)
                await _imagenesRecursosService.EliminarImagenRecurso(currentEmpleado.idImagenRecursoFotografias ?? Guid.Empty);
            // fin recurso imagen

            await _unitOfWork.empleadosRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public  async Task<bool> existeCodigo(empleadosDto empleadoDto)
        {
            if (empleadoDto.correlativo.Length < 4)
                empleadoDto.correlativo = empleadoDto.correlativo.PadLeft(4, '0');

            var currentEmpleadoRazonSocial = await  _unitOfWork.EmpresaRepository.GetByID(empleadoDto.idEmpresa);

            var xCui = empleadoDto.pais.ToUpper().Trim() + "-" + currentEmpleadoRazonSocial.Abreviatura + empleadoDto.correlativo;

            var xEmpleado = _unitOfWork.empleadosRepository.GetAll()
                    .Where(e => e.codigo.ToUpper().Trim() == xCui.ToUpper().Trim() & e.idEmpresa == empleadoDto.idEmpresa);

            if (xEmpleado != null && xEmpleado.Count() == 0)
                return false;

            return true;
        }

        public async Task<Recursos> GetRecursoByControlador(string controladorNombre)
        {
            return await _unitOfWork.RecursosRepository.GetByControladorNombre(controladorNombre);
        }

        public async Task<empleados> GetEmpleadoByCui(string cui)
        {
            var empleado = await _unitOfWork.empleadosRepository.GetEmpleadoByCuiIncludes(cui);

            //Manejo de imagenes
            if (empleado != null && empleado.idImagenRecursoFotografias != null && empleado.idImagenRecursoFotografias != Guid.Empty && empleado.Fotos == null)
            {
                var imgRecurso = await _imagenesRecursosService.GetByID(empleado.idImagenRecursoFotografias ?? Guid.Empty);
                empleado.Fotos = imgRecurso;
            }
            //Fin Imagenes

            return empleado;
        }

        //REPORTE DE AUSENCIAS
        public IEnumerable<empleados> getAusencias(reporteAusenciasQueryFilter filter)
        {

            if (filter.fecha == null)
            {
                throw new AguilaException("Debe indicar fecha para generar el reporte...");
            }

            //if (filter.evento == null)
            //{
            //    throw new AguilaException("Debe indicar evento de entrada o salida para generar el reporte...");
            //}

            if(filter.localidad == null)
            {
                throw new AguilaException("Debe indicar una localidad de empleados para generar el reporte...");
            }

            if (filter.idEstacionTrabajo == null)
            {
                throw new AguilaException("Debe indicar un predio para generar el reporte...");
            }

            if (filter.idEmpresa == null)
            {
                throw new AguilaException("Debe indicar la unidad de negocio para generar el reporte...");
            }


            //var fecha = new DateTime(2022, 02, 11);
            var fecha = filter.fecha;

            var listadoIngresos = _unitOfWork.empleadosIngresosRepository.GetAll().Where(e =>e.idEstacionTrabajo==filter.idEstacionTrabajo && e.fechaEvento.Date >= fecha.Value.Date && e.fechaEvento.Date < fecha.Value.Date.AddDays(1));
            var listadoEmpleados = _unitOfWork.empleadosRepository.GetAll().Where(e => e.idEmpresa == filter.idEmpresa && e.localidad.Trim().ToLower().Equals(filter.localidad.Trim().ToLower()));

            if (filter.cui != null)
            {
                listadoEmpleados = listadoEmpleados.Where(e => e.codigo.Contains(filter.cui));
            }

            if (filter.evento != null)
            {
                listadoIngresos = listadoIngresos.Where(e => e.evento.Equals(filter.evento.Trim().ToUpper()));
            }


            var query = from empleado in listadoEmpleados.ToList()
                        join ingreso in listadoIngresos.ToList() on empleado equals ingreso.empleado into myLeftJoin
                        from subIngreso in myLeftJoin.DefaultIfEmpty()
                        select new { empleado.id, empleado.codigo, empleado.nombres, empleado.apellidos, ingresa = subIngreso?.evento ?? String.Empty };

            var empleadosAusentes = new List<empleados>();
            query = query.Where(e => string.IsNullOrEmpty(e.ingresa));


            foreach (var emp in query)
            {
                var employee = new empleados() { id = emp.id, codigo = emp.codigo, nombres = emp.nombres , apellidos=emp.apellidos };
                empleadosAusentes.Add(employee);
            }

            return empleadosAusentes.AsEnumerable();
        }

        //OBTIENE CORRELATIVO
        public string obtenerCorrelativo(string pais, byte idEmpresa)
        {
            var empleados = _unitOfWork.empleadosRepository.GetAll().Where(e => e.pais.Trim().ToLower().Equals(pais.Trim().ToLower()) && e.idEmpresa == idEmpresa);

            var maxCorr = 0;

            if (empleados.Count() > 0) {
                 maxCorr = (from max in empleados
                               select Convert.ToInt32(max.correlativo)).Max() + 1;
            }
            else
            {
                maxCorr = 1;
            }
           

            var correlativo = maxCorr.ToString().PadLeft(4,'0');            

            return correlativo;
        }

    }
}
