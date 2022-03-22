using Aguila.Core.DTOs;
using Aguila.Core.DTOs.DTOsRespuestas;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Aguila.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //CreateMap<Origen, Destino>();
            CreateMap<Empresas, EmpresasDto>().ReverseMap();

            CreateMap<Sucursales, SucursalDto>().ReverseMap();

            CreateMap<EstacionesTrabajo, EstacionesTrabajoDto>().ReverseMap();

            // Evitamos que se envie el campo password, cuando solicitan datos
            CreateMap<Usuarios, UsuariosDto>().ForMember(dest => dest.Password, act => act.Ignore());
            CreateMap<UsuariosDto, Usuarios >();            

            CreateMap<Modulos, ModulosDto>().ReverseMap();
                                    
            CreateMap<Recursos, RecursosDto>().ForMember(
                 dest => dest.opciones, act => act.MapFrom(o => o.opciones.ToString().Split(",", StringSplitOptions.None).ToList())
                );

            CreateMap<RecursosDto, Recursos>();

            CreateMap<ModulosMnu, ModulosMnuDto>().ReverseMap();

            CreateMap<AsigUsuariosModulos, AsigUsuariosModulosDto>().ReverseMap();

            CreateMap<AsigUsuariosEstacionesTrabajo, AsigUsuariosEstacionesTrabajoDto>().ReverseMap();

            CreateMap<RecursosAtributos, RecursosAtributosDto>().ReverseMap();

            CreateMap<AsigUsuariosRecursosAtributos, AsigUsuariosRecursosAtributosDto>().ReverseMap();

            CreateMap<Roles, RolesDto>().ReverseMap();

            CreateMap<UsuariosRoles, UsuariosRolesDto>().ReverseMap();

            CreateMap<paises, paisesDto>().ReverseMap();

            CreateMap<departamentos, departamentosDto>().ReverseMap();

            CreateMap<municipios, municipiosDto>().ReverseMap();

            CreateMap<ubicaciones, ubicacionesDto>().ReverseMap();

            CreateMap<direcciones, direccionesDto>().ReverseMap();

            CreateMap<entidadComercial, entidadComercialDto>().ReverseMap();

            //CreateMap<entidadesComercialesDirecciones, entidadesComercialesDireccionesDto>().ReverseMap();

            CreateMap<entidadesComercialesDirecciones, entidadesComercialesDireccionesDto>().ForMember(dest => dest.direccion, act => act.Ignore());

            CreateMap<entidadesComercialesDireccionesDto, entidadesComercialesDirecciones>().ForMember(dest => dest.direccion, act => act.Ignore())
                                                                                            .ForMember(dest => dest.entidadComercial, act => act.Ignore());

            CreateMap<tipoClientes, tipoClientesDto>().ReverseMap();

            CreateMap<corporaciones, corporacionesDto>().ReverseMap();

            CreateMap<clientes, clientesDto>().ForMember(dest => dest.direccion, act => act.Ignore())
                                              .ForMember(dest => dest.entidadComercial, act => act.Ignore())
                                              .ForMember(dest => dest.direccionFiscal, act => act.Ignore());

            CreateMap<clientesDto, clientes>().ForMember(dest => dest.direccion, act => act.Ignore())
                                              .ForMember(dest => dest.entidadComercial, act => act.Ignore())                                              
                                              .ForMember(dest => dest.tipoCliente, act => act.Ignore());

            CreateMap<proveedores, proveedoresDto>().ForMember(dest => dest.entidadComercial, act=>act.Ignore())
                                                    .ForMember(dest => dest.direccion, act => act.Ignore())
                                                    .ForMember(dest => dest.direccionFiscal, act => act.Ignore());

            CreateMap<proveedoresDto,proveedores>().ForMember(dest => dest.entidadComercial, act => act.Ignore())
                                                   .ForMember(dest => dest.direccion, act=> act.Ignore())
                                                   .ForMember(dest => dest.tipoProveedor, act => act.Ignore())                                                   
                                                   .ForMember(dest => dest.empresa, act => act.Ignore());

            CreateMap<pilotosTipos, pilotosTiposDto>().ReverseMap();

            CreateMap<tipoMecanicos, tipoMecanicosDto>().ReverseMap();

            CreateMap<llantaTipos, llantaTiposDto>().ReverseMap();

            CreateMap<tipoEquipoRemolque, tipoEquipoRemolqueDto>().ReverseMap();

            CreateMap<tipoVehiculos, tipoVehiculosDto>().ReverseMap();

            CreateMap<tipoGeneradores, tipoGeneradoresDto>().ReverseMap();

            CreateMap<proveedores, proveedoresDto>().ReverseMap();

            CreateMap<transportes, transportesDto>().ReverseMap();

            CreateMap<rutas, rutasDto>().ReverseMap();

            CreateMap<servicios, serviciosDto>().ReverseMap();

            CreateMap<clienteServicios, clienteServiciosDto>().ReverseMap();

            CreateMap<empleados, empleadosDto>().ReverseMap();

            CreateMap<asesores, asesoresDto>().ReverseMap();

            CreateMap<pilotos, pilotosDto>().ReverseMap();

            CreateMap<pilotosDocumentos, pilotosDocumentosDto>().ReverseMap();

            CreateMap<activoOperaciones, activoOperacionesDto>().ReverseMap();

            CreateMap<medidas, medidasDto>().ReverseMap();

            CreateMap<invCategoria, invCategoriaDto>().ReverseMap();

            CreateMap<invSubCategoria, invSubCategoriaDto>().ReverseMap();

            CreateMap<productos, productosDto>().ReverseMap();

            CreateMap<invProductoBodega, invProductoBodegaDto>().ReverseMap();

            CreateMap<equipoRemolque, equipoRemolqueDto>().ReverseMap();

            CreateMap<generadores, generadoresDto>().ReverseMap();

            CreateMap<invUbicacionBodega, invUbicacionBodegaDto>().ReverseMap();

            CreateMap<productosBusqueda, productosBusquedaDto>().ReverseMap();

            CreateMap<estados, estadosDto>().ReverseMap();

            CreateMap<activoEstados, activoEstadosDto>().ReverseMap();

            CreateMap<activoUbicaciones, activoUbicacionesDto>().ReverseMap();

            CreateMap<mecanicos, mecanicosDto>().ReverseMap();

            CreateMap<llantas, llantasDto>().ReverseMap();

            CreateMap<llantaActual, llantaActualDto>().ReverseMap();

            CreateMap<tarifario, tarifarioDto>().ReverseMap();

            CreateMap<clienteTarifas, clienteTarifasDto>().ReverseMap();

            //Se convierte las opcionesAsigandas en una lista de valores separadas por coma
            //CreateMap<UsuariosRecursos, UsuariosRecursosDto>().ReverseMap();

            CreateMap<UsuariosRecursos, UsuariosRecursosDto>().ForMember(
                dest => dest.opcionesAsignadas, act => act.MapFrom(o => o.opcionesAsignadas.ToString().Split(",", StringSplitOptions.None).ToList()))
                                                               .ForPath(
                dest => dest.Recurso.opciones, act => act.MapFrom(o => o.Recurso.opciones.ToString().Split(",",StringSplitOptions.None).ToList())
                                                                )
                                                               .ForPath(
                dest=> dest.Recurso.Nombre, act => act.MapFrom(o =>o.Recurso.Nombre))
                                                               .ForPath(
                dest => dest.Recurso.Id, act => act.MapFrom(o => o.Recurso.Id))
                                                               .ForPath(
                dest => dest.Recurso.Tipo, act => act.MapFrom(o => o.Recurso.Tipo))
                                                               .ForPath(
                dest => dest.Recurso.Controlador, act => act.MapFrom(o => o.Recurso.Controlador))
                                                               .ForPath(
                dest => dest.Recurso.fechaCreacion, act => act.MapFrom(o => o.Recurso.fechaCreacion));
            
            CreateMap<UsuariosRecursosDto, UsuariosRecursos>();

            //se evita que se envie el string de la imagen en base64            
           CreateMap<ImagenRecursoConfiguracion, ImagenRecursoConfiguracionDto>().ForMember(dest => dest.SubirImagenBase64, act => act.Ignore());
           CreateMap<ImagenRecursoConfiguracionDto, ImagenRecursoConfiguracion>();

            CreateMap<tiposLista, tiposListaDto>().ReverseMap();
            CreateMap<listas, listasDto>().ReverseMap();
            CreateMap<tipoReparaciones, tipoReparacionesDto>().ReverseMap();
            CreateMap<reparaciones, reparacionesDto>().ReverseMap();
            CreateMap<tipoActivos, tipoActivosDto>().ReverseMap();
            CreateMap<activoGenerales, activoGeneralesDto>().ReverseMap();
            CreateMap<tipoProveedores, tipoProveedoresDto>().ReverseMap();
            CreateMap<vehiculos, vehiculosDto>().ReverseMap();
            CreateMap<condicionActivos, condicionActivosDto>().ReverseMap();
            CreateMap<activoMovimientos, activoMovimientosDto>().ReverseMap();
            CreateMap<activoMovimientosActual, activoMovimientosActualDto>().ReverseMap();
            CreateMap<condicionCabezal, condicionCabezalDto>().ReverseMap();
            CreateMap<condicionEquipo, condicionEquipoDto>().ReverseMap();
            CreateMap<condicionCisterna, condicionCisternaDto>().ReverseMap();
            CreateMap<condicionGenSet, condicionGenSetDto>().ReverseMap();
            CreateMap<condicionTecnicaGenSet, condicionTecnicaGenSetDto>().ReverseMap();
            CreateMap<controlContratistas, controlContratistasDto>().ReverseMap();
            
            //RESPUESTAS
            //Solo para envio de respuestas personalizadas, colocar los dtos en la carpeta DTOsRespuestas
            //CreateMap<condicionActivos, condicionActivosDto2>();
            CreateMap<Usuarios, UsuariosDto2>();
            CreateMap<activoMovimientos, activoMovimientosDto2>();
            CreateMap<reporteActivoMovimientosQueryFilter, ReporteMovimientosEquiposRemolqueQueryFilter>().ReverseMap();
            CreateMap<reporteActivoMovimientosQueryFilter, ReporteMovimientosVehiculosQueryFilter>().ReverseMap();
            CreateMap<reporteActivoMovimientosQueryFilter, reporteMovimientosGeneradoresQueryFilter>().ReverseMap();
            CreateMap<activoMovimientos, movimientoEquipoRemolqueDto>().ReverseMap();
            CreateMap<activoMovimientos, movimientoVehiculoDto>().ReverseMap();
            CreateMap<activoMovimientos, movimientoGeneradoresDto>().ReverseMap();
            CreateMap<activoOperaciones, ActivoEquipoRemolqueDto>().ReverseMap();
            CreateMap<activoOperaciones, ActivoVehiculoDto>().ReverseMap();
            CreateMap<activoOperaciones, ActivoGeneradoresDto>().ReverseMap();
            CreateMap<equipoRemolque, equipoRemolqueDto2>().ReverseMap();
            CreateMap<vehiculos , vehiculosDto2>().ReverseMap();
            CreateMap<generadores, generadoresDto2>().ReverseMap();
            //Ejemplo 
            //Utilizacion de Automapper desde el controlador u otra clase con nueva instancia

            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<condicionActivos, condicionActivosDto2>();
            //    cfg.CreateMap<Usuarios, UsuariosDto2>();
            //});

            //var xmapper = new Mapper(configuration);

            //var condicionesDto2 = xmapper.Map<IEnumerable<condicionActivosDto2>>(condiciones);

            //Informacion
            //https://www.infoworld.com/article/3192900/how-to-work-with-automapper-in-csharp.html

            CreateMap<condicionFurgon, condicionFurgonDto>().ReverseMap();
            CreateMap<eventosControlEquipo, eventosControlEquipoDto>().ReverseMap();
            CreateMap<controlVisitas, controlVisitasDto>().ReverseMap();
            CreateMap<controlEquipoAjeno, controlEquipoAjenoDto>().ReverseMap();
            CreateMap<empleadosIngresos, empleadosIngresosDto>().ReverseMap();
            CreateMap<condicionTallerVehiculo, condicionTallerVehiculoDto>().ReverseMap();
            CreateMap<detalleCondicion, detalleCondicionDto>().ReverseMap();
            CreateMap<codigoPostal, codigoPostalDto>().ReverseMap();
            CreateMap<condicionContenedor, condicionContenedorDto>().ReverseMap();
        }

    }
}
