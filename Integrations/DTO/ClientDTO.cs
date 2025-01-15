namespace Integrations.DTO
{
    public record ClientDTO<T>
    (
        T Entity,
        string Url,
        string Token
    );

    public record ClientDTO
    (
        string Url,
        string Token
    );

    public static class ClientExtensions
    {
        public static ClientDTO<T> MapperDTO<T>(T entity, string url,string token)
        {
            return new ClientDTO<T>(entity,url,token);
        }
    }
}