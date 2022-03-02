using Application.Person.NotificationHandlers;
using Application.Person.Notifications;
using AutoMapper;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositories.Person;
using MassTransit;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.Person
{
    public class CreatePersonSqlNotificationHandlerTest
    {
        protected CreatePersonSqlNotificationHandler _personSqlHandler { get; }
        protected Mock<IUnitOfWork> _uowMock { get; }
        protected Mock<IMapper> _mapperMock { get; }
        protected Mock<IPersonRepository> _personRepositoryMock { get; }
        protected Mock<ConsumeContext<CreatePersonNotification>> _consumeContextMock { get; }
        protected Core.Models.Person _person { get; }
        protected CreatePersonNotification _personDto { get; }
        public CreatePersonSqlNotificationHandlerTest()
        {
            _personDto = new CreatePersonNotification() { FirstName = "Joe", LastName = "Solivan", Age = 27 };
            _person = new Core.Models.Person() { Id = Guid.NewGuid(), FirstName = _personDto.FirstName, LastName = _personDto.LastName, Age = _personDto.Age };

            _mapperMock = new Mock<IMapper>();

            _consumeContextMock = new Mock<ConsumeContext<CreatePersonNotification>>();

            _personRepositoryMock = new Mock<IPersonRepository>();

            _uowMock = new Mock<IUnitOfWork>();
            _uowMock.Setup(uow => uow.Person).Returns(_personRepositoryMock.Object);

            _personSqlHandler = new CreatePersonSqlNotificationHandler(_uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void Consume_ShouldReturnSuccess_WithCorrectMessage()
        {
            //Arrange
            _consumeContextMock.Setup(x => x.Message).Returns(_personDto);
            _mapperMock.Setup(x => x.Map<Core.Models.Person>(_personDto)).Returns(_person);
            _personRepositoryMock.Setup(x => x.InsertPersonAsync(_person)).ReturnsAsync(_person);

            //Act
            await _personSqlHandler.Consume(_consumeContextMock.Object);

            //Assert
            _mapperMock.Verify(x => x.Map<Core.Models.Person>(_personDto), Times.Once);
            _personRepositoryMock.Verify(x => x.InsertPersonAsync(_person), Times.Once);
        }

        [Fact]
        public async void Consume_ShouldReturnFailed_WhenMessageNull()
        {
            //Act
            Func<Task> act = () => _personSqlHandler.Consume(_consumeContextMock.Object);

            //Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("Message");

            _mapperMock.Verify(x => x.Map<Core.Models.Person>(_personDto), Times.Never);
            _personRepositoryMock.Verify(x => x.InsertPersonAsync(It.IsAny<Core.Models.Person>()), Times.Never);
        }

        [Fact]
        public async void Consume_ShouldReturnFailed_WhenMapperReturnNull()
        {
            //Arrange
            _consumeContextMock.Setup(x => x.Message).Returns(_personDto);
            _mapperMock.Setup(m => m.Map<Core.Models.Person>(_personDto)).Returns<Core.Models.Person>(null);

            //Act
            Func<Task> act = () => _personSqlHandler.Consume(_consumeContextMock.Object);

            //Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("person");

            _mapperMock.Verify(x => x.Map<Core.Models.Person>(_personDto), Times.Once);
            _personRepositoryMock.Verify(x => x.InsertPersonAsync(It.IsAny<Core.Models.Person>()), Times.Never);
        }

    }
}
