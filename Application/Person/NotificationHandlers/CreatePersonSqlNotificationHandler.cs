using Application.Person.Notifications;
using AutoMapper;
using Infrastructure;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Application.Person.NotificationHandlers
{
    public class CreatePersonSqlNotificationHandler : object, IConsumer<CreatePersonNotification>
    {
        protected IUnitOfWork _unitOfWork { get; }
        protected IMapper _mapper { get; }
        public CreatePersonSqlNotificationHandler(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreatePersonNotification> context)
        {
            var person = _mapper.Map<Core.Models.Person>(context.Message);

            await _unitOfWork.Person.InsertPersonAsync(person);
        }
    }
}
