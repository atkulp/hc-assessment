using System;
using System.Collections.Generic;
using System.Linq;
using KulpAssessment.Data.Entities;
using KulpAssessment.Utilities.Extensions;

namespace KulpAssessment.Managers
{
    // Use in-memory dummy collection of people
    public class MockPersonRepository : IPersonRepository
    {
        #region Dummy data init
        private static readonly List<String> FirstNames = new List<string>
        {
            "John", "Bob", "Chris", "Paul", "Cindy", "Janet", "Julie", "Diane", "Wilma", "Betty"
        }, LastNames = new List<string>
        {
            "Smith", "Johnson", "Jones", "Peters", "Anderson", "Brown", "Gonzalez", "Davis", "Wilson", "Miller"
        },
        Streets = new List<string>
        {
            "Elm", "Birch", "Pine", "Lincoln", "1st", "2nd", "Main"
        },
        StreetTypes = new List<string>
        {
            "St", "Ave", "Blvd", "Ln", "Rd"
        },
        Cities = new List<string>
        {
            "Springfield", "Whoville", "Anytown"
        },
        States = new List<string>
        {
            "AZ", "AK", "AL", "AR"
        },
        Interests = new List<string>
        {
            "snorkeling", "breathing", "blinking", "staring", "eating", "sitting"
        };
        #endregion

        // Fake database of people
        private static Person[] people = null;

        static MockPersonRepository()
        {
            var rng = new Random();

            // Ugly randomization to create different people
            people = Enumerable.Range(1, 30).Select(index => new Person
            {
                Id = index,
                DateOfBirth = DateTime.Now.AddYears(-rng.Next(5, 40)),
                DateOfDeath = rng.Next(10) == 1 ? DateTime.Now.AddYears(-rng.Next(5)) : (DateTime?)null,
                FirstName = FirstNames.GetRandom(),
                LastName = LastNames.GetRandom(),
                Street1 = $"{rng.Next(999)} {Streets.GetRandom()} {StreetTypes.GetRandom()}",
                Street2 = rng.Next(10) < 3 ? $"Apt {rng.Next(1, 30)}" : null,
                City = Cities.GetRandom(),
                State = States.GetRandom(),
                PostalCode = $"{rng.Next(99999):00000}",
                Interests = rng.Next(10) < 5 ? Interests.GetRandom() : null,
                AvatarUrl = $"/images/{(index % 2 == 0 ? "m" : "f")}/{index}.png"
            }).ToArray();
        }

        public IEnumerable<Person> FindPeopleByName(string name, int? take = null)
        {
            //We can return all or nothing with a blank query.  Nothing seems better...
            if( string.IsNullOrWhiteSpace(name))
            {
                return Enumerable.Empty<Person>();
            }
            else{
                var q = name.Trim();

                var results = people
                    .Where( p => p.FirstName.Contains(q, StringComparison.OrdinalIgnoreCase)
                        || p.LastName.Contains(q, StringComparison.OrdinalIgnoreCase));

                return take == null ? results : results.Take((int)take);
            }
        }

        public Person GetPerson(int id)
        {
            return people.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Get all created people.  Useful in testing or seeding
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Person> GetAll()
        {
            return people;
        }
    }
}