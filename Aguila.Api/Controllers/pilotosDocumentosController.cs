using Aguila.Api.Responses;
using Aguila.Core.CustomEntities;
using Aguila.Core.DTOs;
using Aguila.Core.Entities;
using Aguila.Core.Exceptions;
using Aguila.Core.Interfaces.Services;
using Aguila.Core.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aguila.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class pilotosDocumentosController : ControllerBase
    {
        private readonly IpilotosDocumentosService _pilotosDocumentosService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public pilotosDocumentosController(IpilotosDocumentosService pilotosDocumentosService, IMapper mapper, IPasswordService password)
        {
            _pilotosDocumentosService = pilotosDocumentosService;
            _mapper = mapper;
            _passwordService = password;
        }

        /// <summary>
        /// Obtiene los documentos asignados a pilotos
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AguilaResponse<IEnumerable<pilotosDocumentosDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPilotosDocumentos([FromQuery] pilotosDocumentosQueryFilter filter)
        {
            var pilotosDocs = _pilotosDocumentosService.GetPilotosDocumentos(filter);
            var pilotosDocsDto = _mapper.Map<IEnumerable<pilotosDocumentosDto>>(pilotosDocs);

            var metadata = new Metadata
            {
                TotalCount = pilotosDocs.TotalCount,
                PageSize = pilotosDocs.PageSize,
                CurrentPage = pilotosDocs.CurrentPage,
                TotalPages = pilotosDocs.TotalPages,
                HasNextPage = pilotosDocs.HasNextPage,
                HasPreviousPage = pilotosDocs.HasPreviousPage,
            };

            var response = new AguilaResponse<IEnumerable<pilotosDocumentosDto>>(pilotosDocsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }
    }
}
