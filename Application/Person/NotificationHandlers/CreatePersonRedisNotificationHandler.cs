using Application.Person.Notifications;
using AutoMapper;
using Infrastructure.NoSql.Redis;
using MassTransit;
using System.Threading.Tasks;

namespace Application.Person.NotificationHandlers
{
    public class CreatePersonRedisNotificationHandler : object, IConsumer<CreatePersonNotification>
    {
        protected IMapper _mapper { get; }
        protected IRedisStore<Core.Models.Person> _redisStore { get; }
        public CreatePersonRedisNotificationHandler(
            IMapper mapper,
            IRedisStore<Core.Models.Person> redisStore)
        {
            _mapper = mapper;
            _redisStore = redisStore;
        }
        public async Task Consume(ConsumeContext<CreatePersonNotification> context)
        {
            var person = _mapper.Map<Core.Models.Person>(context.Message);
            await _redisStore.Save($"person:{person.Id}", person);
        }
    }
}
