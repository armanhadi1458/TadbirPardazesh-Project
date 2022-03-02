using Application.Person;
using Application.Person.NotificationHandlers;
using Application.Person.Notifications;
using AutoMapper;
using FluentAssertions;
using Infrastructure;
using Infrastructure.NoSql.Redis;
using Infrastructure.Repositories.Person;
using MassTransit;
using Moq;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Person
{
    public class CreatePersonRedisNotificationHandlerTest
    {
        protected CreatePersonRedisNotificationHandler _personRedisHandler { get; }
        protected IMapper _mapper { get; }
        protected Mock<IRedisStore<Core.Models.Person>> _redisStoreMock { get; }
        protected Mock<ConsumeContext<CreatePersonNotification>> _consumeContextMock { get; }

        public CreatePersonRedisNotificationHandlerTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(PersonProfile).Assembly));
            _mapper = config.CreateMapper();

            _consumeContextMock = new Mock<ConsumeContext<CreatePersonNotification>>();

            _redisStoreMock = new Mock<IRedisStore<Core.Models.Person>>();
            _redisStoreMock.Setup(x => x.Save(It.IsAny<string>(), It.IsAny<Core.Models.Person>()));

            _personRedisHandler = new CreatePersonRedisNotificationHandler(_mapper, _redisStoreMock.Object);
        }


        [Fact]
        public async void Consume_ShouldReturnSuccess_WithCorrectMessage()
        {
            //Arrange
            _consumeContextMock.Setup(x => x.Message).Returns(new CreatePersonNotification());

            //Act
            await _personRedisHandler.Consume(_consumeContextMock.Object);

            //Assert
            _redisStoreMock.Verify(x => x.Save(It.IsAny<string>(), It.IsAny<Core.Models.Person>()), Times.Once);
        }

        [Fact]
        public async void Consume_ShouldReturnFailed_WhenMessageNull()
        {
            //Act
            Func<Task> act = () => _personRedisHandler.Consume(_consumeContextMock.Object);

            //Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("Message");

            _redisStoreMock.Verify(x => x.Save(It.IsAny<string>(), It.IsAny<Core.Models.Person>()), Times.Never);
        }

        [Fact]
        public async void Consume_ShouldReturnFailed_WhenMapperReturnNull()
        {
            //Arrange
            _consumeContextMock.Setup(x => x.Message).Returns(new CreatePersonNotification());
            //_mapperMock.Setup(m => m.Map<Core.Models.Person>(It.IsAny<CreatePersonNotification>())).Returns<Core.Models.Person>(null);

            //Act
            Func<Task> act = () => _personRedisHandler.Consume(_consumeContextMock.Object);

            //Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("person");

            //_mapperMock.Verify(x => x.Map<Core.Models.Person>(It.IsAny<CreatePersonNotification>()), Times.Once);
            _redisStoreMock.Verify(x => x.Save(It.IsAny<string>(), It.IsAny<Core.Models.Person>()), Times.Never);
        }

    }
}
