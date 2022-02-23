using Core.Models;
using FluentAssertions;
using Infrastructure.Repositories.Person;
using Infrastructure.Test.Utils;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Infrastructure.Test
{
    [TestCaseOrderer("Infrastructure.Test.Utils.PriorityOrderer", "Infrastructure.Test")]
    public class PersonRepositoryTest : IClassFixture<TestFixture<Person>>
    {
        /// <summary>
        /// Fixture
        /// </summary>
        protected TestFixture<Person> _fixture { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public PersonRepositoryTest(TestFixture<Person> fixture, ITestOutputHelper output)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            _fixture.Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        [Fact, TestPriority(1)]
        public async Task GetAll_ShouldResultCountBe_2()
        {
            var people = await _fixture.PersonRepository.GetAll();

            foreach (var person in people)
                _fixture.Output.WriteLine(System.Text.Json.JsonSerializer.Serialize(person));

            people.Should().NotBeNull();
            people.Count.Should().Be(2);
        }

        [Fact, TestPriority(2)]
        public async Task InsertPerson_ShouldReturn_Person()
        {
            // Arrange
            Person person = new Person() { Age = 33, FirstName = "David", LastName = "Broke", Id = Guid.NewGuid() };

            // Act
            var result = await _fixture.PersonRepository.InsertPersonAsync(person);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(person.Id);
            result.FirstName.Should().Be(person.FirstName);
            result.LastName.Should().Be(person.LastName);
            result.Age.Should().Be(person.Age);
            _fixture.PersonId = result.Id;
        }


        [Fact, TestPriority(3)]
        public async Task FindById_ShouldReturn_PersonInserted()
        {
            var person = await _fixture.PersonRepository.FindById(_fixture.PersonId);

            person.Should().NotBeNull();
            person.Id.Should().Be(_fixture.PersonId);
            person.FirstName.Should().Be("David");
            person.LastName.Should().Be("Broke");
            person.Age.Should().Be(33);
        }

        [Fact, TestPriority(4)]
        public async Task Update_ShouldUpdate_PersonAdded()
        {
            var person = await _fixture.PersonRepository.FindById(_fixture.PersonId);
            person.FirstName = "Alice";
            person.LastName = "Jane";
            person.Age = 19;

            var updatetd = await _fixture.PersonRepository.Update(person);

            updatetd.Should().NotBeNull();
            updatetd.Id.Should().Be(_fixture.PersonId);
            updatetd.FirstName.Should().Be("Alice");
            updatetd.LastName.Should().Be("Jane");
            updatetd.Age.Should().Be(19);
        }

        [Fact, TestPriority(5)]
        public async Task Delete_ShouldRemove_PersonAdded()
        {
            await _fixture.PersonRepository.Delete(_fixture.PersonId);

            var deletedPerson = await _fixture.PersonRepository.FindById(_fixture.PersonId);
            deletedPerson.Should().BeNull();
        }


    }

}

