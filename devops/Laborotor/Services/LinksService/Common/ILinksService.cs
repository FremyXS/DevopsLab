using Laborotor.Models.Requests;

namespace Laborotor.Services.LinksService.Common
{
    public interface ILinksService
    {
        Task<Database.Entity.Link> Get(long id);
        Task Create(LinkCreateRequest linkRequest);
        Task UpdateStatus(long id, bool status, int code);
    }
}
