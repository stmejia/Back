using Aguila.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IImagenesRecursosService
    {
        Task<ImagenRecurso> GetByID(Guid id);
        Task<ImagenRecursoConfiguracion> GetConfiguracion(string ControladorNombre, string propiedad);
        Task<ImagenRecurso> GuardarImagenRecurso(ImagenRecurso imagenRecurso, string controlador, string propiedad);
        Task<bool> GuardarImagenes(ImagenRecurso imagenRecurso);
        Task<bool> EliminarImagenRecurso(Guid id);
        string GetImagenDefault(ImagenRecursoConfiguracion config);
        Task<bool> AsignarUrlImagenesDefault(List<ImagenRecurso> imagenesRecursos);

    }
}
