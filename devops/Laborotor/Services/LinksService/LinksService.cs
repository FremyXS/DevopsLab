using Laborotor.Database;
using Laborotor.Models.Requests;
using Laborotor.Services.LinksService.Common;
using Laborotor.Services.Rabbit;
using Microsoft.EntityFrameworkCore;

namespace Laborotor.Services.LinksService
{
    public class LinksService : ILinksService
    {
        private readonly LinksDbContext _linksDbContext;
        private readonly IRabbitMqService _rabbitMqService;
        public LinksService(LinksDbContext linksDbContext, IRabbitMqService rabbitMqService)
        {
            _linksDbContext = linksDbContext;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<Database.Entity.Link> Get(long id)
        {
            var link = await _linksDbContext.Links.FirstOrDefaultAsync(x => x.Id == id);

            if (link == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return link;
        }

        public async Task Create(LinkCreateRequest linkRequest)
        {
            if (linkRequest == null)
            {
                throw new ArgumentNullException(nameof(linkRequest));
            }

            if (linkRequest.Url == null)
            {
                throw new ArgumentNullException(nameof(linkRequest.Url));
            }

            var link = new Database.Entity.Link();
            link.Url = linkRequest.Url;

            await _linksDbContext.Links.AddAsync(link);

            await _linksDbContext.SaveChangesAsync();

            _rabbitMqService.SendMessage(link);
        }

        public async Task UpdateStatus(long id, bool status, int code)
        {
            var link = await _linksDbContext.Links.FirstOrDefaultAsync(x => x.Id == id);

            if (link == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            link.Status = status;
            link.Code = code;

            //_linksDbContext.Links.Update(link);

            await _linksDbContext.SaveChangesAsync();

        }
    }
}
