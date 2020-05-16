using System.Collections.Generic;
using KulpAssessment.Data.Entities;

namespace KulpAssessment.Managers
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Get zero or one person matching id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single person matching the id.  Null if no match</returns>
        Person GetPerson(int id);

        /// <summary>
        /// Get zero or more people with first or last name matching search term
        /// </summary>
        /// <param name="name">Search term to compare to first or last name of people in database</param>
        /// <param name="take">Maximum number of results to return, defaults to all.  Optional.</param>
        /// <returns>Collection of zero or more matches on given name parameter</returns>
        IEnumerable<Person> FindPeopleByName(string name, int? take = null);
    }
}