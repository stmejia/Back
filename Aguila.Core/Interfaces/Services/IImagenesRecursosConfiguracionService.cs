using Aguila.Core.CustomEntities;
using Aguila.Core.Entities;
using Aguila.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aguila.Core.Interfaces.Services
{
    public interface IImagenesRecursosConfiguracionService
    {
        Task insertImagenRecursosConfiguracion(ImagenRecursoConfiguracion imagenRecursoConfiguracion, string imagenBase64);
        PagedList<ImagenRecursoConfiguracion> GetImagenRecursoConfiguracion(ImagenRecursoConfiguracionQueryFilter filter);
        Task<bool> UpdateImagenRecursosConfiguracion(ImagenRecursoConfiguracion imagenRecursoConf, string imagenBase64);
        Task<bool> DeleteImagenRecursoConfiguracion(long id);
        Task<ImagenRecursoConfiguracion> GetImagen(long id);
        Task<Recursos> GetRecursoByControlador(string controladorNombre);
    }
}