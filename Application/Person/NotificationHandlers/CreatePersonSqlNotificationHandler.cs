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
            if (context?.Message is null)
                throw new ArgumentNullException(nameof(context.Message));

            var person = _mapper.Map<Core.Models.Person>(context.Message);

            if (person is null)
                throw new ArgumentNullException(nameof(person));

            await _unitOfWork.Person.InsertPersonAsync(person);
        }
    }
}
