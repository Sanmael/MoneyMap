using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.DTO
{
    public record ClientDTO<T>
    (
        string Url,
        T Entity
    );    

    public record ClientDTO
    (
        string Url
    );

    public static class ClientExtensions
    {
        public static ClientDTO<T> MapperDTO<T>(T entity, string Url)
        {
            return new ClientDTO<T>(Url, entity);
        }
    }
}