using Core.Models;
using FluentAssertions;
using Infrastructure.Test.Utils;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test
{
    [TestCaseOrderer("Infrastructure.Test.Utils.PriorityOrderer", "Infrastructure.Test")]
    public class RedisStoreTest: IClassFixture<TestFixture<Person>>
    {
        protected TestFixture<Person> _testFixture { get; }
        public RedisStoreTest(TestFixture<Person> testFixture)
        {
            _testFixture = testFixture ?? throw new ArgumentNullException(nameof(testFixture));
        }

        [Fact, TestPriority(1)]
        public async Task SetAsync_ShouldSaveTo_Redis()
        {
            //Arrange
            Person person = new Person() { Age = 33, FirstName = "David", LastName = "Broke", Id = Guid.NewGuid() };
            string key = $"peron:{person.Id}";
            //Act
            await _testFixture.RedisStore.Save(key, person);

            //Assert
            Person result = await _testFixture.RedisStore.GetValue(key);

            result.Should().NotBeNull();
            result.Id.Should().Be(person.Id);
            result.FirstName.Should().Be(person.FirstName);
            result.LastName.Should().Be(person.LastName);
            result.Age.Should().Be(person.Age);

            await _testFixture.RedisStore.Remove(key);
        }

    }
}
