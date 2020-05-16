using KulpAssessment.Controllers;
using KulpAssessment.Data.Entities;
using KulpAssessment.Managers;
using KulpAssessment.Utilities.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace KulpAssessment.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void DtoShouldMatchEntity()
        {
            Person p = new Person
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1950, 1, 1),
                DateOfDeath = new DateTime(1980, 1, 1),
                AvatarUrl = "/images/blah.png",
                Street1 = "123 Any St",
                Street2 = "Apt 1",
                City = "Cityville",
                State = "ST",
                PostalCode = "00000",
                Interests = "a,b,c"
            };

            var d = KulpAssessment.Data.Dto.PersonDto.FromEntity(p);

            Assert.Equal(p.Id, d.Id);
            Assert.Equal(p.FirstName, d.FirstName);
            Assert.Equal(p.LastName, d.LastName);
            Assert.Equal(p.DateOfBirth, d.DOB);
            Assert.Equal(p.DateOfDeath, d.DOD);
            Assert.Equal(p.AvatarUrl, d.AvatarUrl);
            Assert.Equal(p.Street1, d.Street1);
            Assert.Equal(p.Street2, d.Street2);
            Assert.Equal(p.City, d.City);
            Assert.Equal(p.State, d.State);
            Assert.Equal(p.PostalCode, d.PostalCode);
            Assert.Equal(p.Interests, d.Interests);
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldCrashOnDemand()
        {
            var l = new DummyLogger();
            var r = new MockPersonRepository();

            var c = new PersonController(l, r);
            await Assert.ThrowsAnyAsync<Exception>(() => c.Get("err"));
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldReturnKnownPeople()
        {
            var l = new DummyLogger();
            var r = new MockPersonRepository();

            var c = new PersonController(l, r);
            var p = r.GetAll().First();
            var fname = p.FirstName;
            var lname = p.LastName;

            Assert.NotEmpty(await c.Get(fname));
            Assert.NotEmpty(await c.Get(lname));
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldNotReturnUnknownPeople()
        {
            var l = new DummyLogger();
            var r = new MockPersonRepository();

            var c = new PersonController(l, r);

            Assert.Empty(await c.Get("XXX"));
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldNotCrashOnEmptyQuery()
        {
            var l = new DummyLogger();
            var r = new MockPersonRepository();

            var c = new PersonController(l, r);

            Assert.Empty(await c.Get(""));
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldNotCrashOnNullQuery()
        {
            var l = new DummyLogger();
            var r = new MockPersonRepository();

            var c = new PersonController(l, r);

            Assert.Empty(await c.Get(null));
        }

        [Fact]
        public void ListExtensionShouldDetectNullOrEmpty()
        {
            List<int> coll = null;
            Assert.True(coll.IsNullOrEmpty(), "should return true when null");

            coll = new List<int>();
            Assert.True(coll.IsNullOrEmpty(), "should return true when empty");
        }

        [Fact]
        public void ListExtensionShouldDetectNotNullOrEmpty()
        {
            List<int> coll = new List<int> { 1, 2, 3, 4, };
            Assert.False(coll.IsNullOrEmpty(), "should return false when not empty");
        }

        [Fact]
        public void ListExtensionShouldReturnRandom()
        {
            List<string> coll = null;
            Assert.Null(coll.GetRandom());

            coll = new List<string>();
            Assert.Null(coll.GetRandom());

            coll = new List<string> { "one", "two", "three" };
            Assert.NotNull(coll.GetRandom());
        }
        #region Plumbing
        public class DummyLogger : ILogger<PersonController>
        {
            public List<string> LoggedMessages { get; } = new List<string>();

            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                LoggedMessages.Add(formatter(state, exception));
            }
        }
        #endregion
    }
}
