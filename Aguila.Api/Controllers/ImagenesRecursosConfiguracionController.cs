using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesRecursosConfiguracionController : ControllerBase
    {
        private readonly IImagenesRecursosConfiguracionService _imagenesRecursosConfiguracionService;
        private readonly IMapper _mapper;

        public ImagenesRecursosConfiguracionController(IImagenesRecursosConfiguracionService imagenesRecursosConfiguracionService,
                                                        IMapper mapper)
        {
            _imagenesRecursosConfiguracionService = imagenesRecursosConfiguracionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Extraccion total de Configuracion de Imagenes, enviar filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<ImagenRecursoConfiguracion>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetImagenesRecursosConfiguracion([FromQuery] ImagenRecursoConfiguracionQueryFilter filter)
        {
            var imagenesConf = _imagenesRecursosConfiguracionService.GetImagenRecursoConfiguracion(filter);
            var imagenesConfDto = _mapper.Map<IEnumerable<ImagenRecursoConfiguracionDto>>(imagenesConf);

            var metadata = new Metadata
            {
                TotalCount = imagenesConf.TotalCount,
                PageSize = imagenesConf.PageSize,
                CurrentPage = imagenesConf.CurrentPage,
                TotalPages = imagenesConf.TotalPages,
                HasNextPage = imagenesConf.HasNextPage,
                HasPreviousPage = imagenesConf.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<ImagenRecursoConfiguracionDto>>(imagenesConfDto)
            {
                Meta = metadata
            };

            return Ok(response);            
        }

        /// <summary>
        /// Consulta de Configuracion Imagen, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<RolesDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImagenConfiguracion(long id)
        {
            var imagenConf = await _imagenesRecursosConfiguracionService.GetImagen(id);
            var imagenConfDTo = _mapper.Map<ImagenRecursoConfiguracionDto>(imagenConf);

            var response = new AguilaResponse<ImagenRecursoConfiguracionDto>(imagenConfDTo);
            return Ok(response);
        }

        /// <summary>
        /// Crear Imagen Configuracion Nueva
        /// </summary>
        /// <param name="imagenRecursosConfDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)] 
        public async Task<IActionResult> Post(ImagenRecursoConfiguracionDto imagenRecursosConfDto)
        {
            var imagenConf = _mapper.Map<ImagenRecursoConfiguracion>(imagenRecursosConfDto);

            await _imagenesRecursosConfiguracionService.insertImagenRecursosConfiguracion(imagenConf, imagenRecursosConfDto.SubirImagenBase64);
            var imagenConfDto = _mapper.Map<ImagenRecursoConfiguracionDto>(imagenConf);

            var response = new AguilaResponse<ImagenRecursoConfiguracionDto>(imagenConfDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualizacion de Imagen Configuracion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imagenConfDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(long id, ImagenRecursoConfiguracionDto imagenConfDto)
        {
            var imagenConf = _mapper.Map<ImagenRecursoConfiguracion>(imagenConfDto);
            imagenConf.Id = id;

            var result = await _imagenesRecursosConfiguracionService.UpdateImagenRecursosConfiguracion(imagenConf, imagenConfDto.SubirImagenBase64);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Eliminar Imagen Configuracion, enviar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(long id)
        {

            var result = await _imagenesRecursosConfiguracionService.DeleteImagenRecursoConfiguracion(id);
            var response = new AguilaResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Devuelve el recurso asignado al EndPoint
        /// </summary>       
        /// <returns></returns>
        [HttpGet("/api/ImagenesRecursosConfiguracion/Recurso")]
        [HttpOptions]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<Recursos>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecurso()
        {
            var controlador = ControllerContext.ActionDescriptor.ControllerName;

            var recurso = await _imagenesRecursosConfiguracionService.GetRecursoByControlador(controlador);

            var response = new AguilaResponse<Recursos>(recurso);
            return Ok(response);
        }
    }
}
