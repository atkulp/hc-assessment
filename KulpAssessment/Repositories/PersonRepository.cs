using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using KulpAssessment.Data;
using KulpAssessment.Data.Entities;

namespace KulpAssessment.Managers
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AssessmentDbContext _db;

        public PersonRepository(AssessmentDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<Person> FindPeopleByName(string name, int? take = null)
        {
            // Don't hit database if we know it's a blank search
            if( string.IsNullOrWhiteSpace(name))
            {
                return Enumerable.Empty<Person>();
            }
            else
            {
                // Play it safe
                var q = name.Trim();

                // string.Contains also works, but isn't case-insensitive (which matters depending on collation)
                // Using the LIKE syntax is more flexible here.  Even better would be to use fuzzy search of some sort...
                var results = _db.People
                    .Where( p => EF.Functions.Like(p.FirstName, $"%{q}%") || EF.Functions.Like(p.LastName, $"%{q}%"));

                // It seems like Take(0) should return all, but that's not the case... Do it right.
                return take == null ? results : results.Take((int)take);
            }
        }

        public Person GetPerson(int id)
        {
            // Moderate benefits over FirstOrDefault() due to 1) better intent, 2) potential caching
            // in context if tracked
            return _db.People.Find(id);
        }
    }
}