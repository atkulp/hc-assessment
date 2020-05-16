using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KulpAssessment.Data.Entities
{
    [Table("People")]
    public class Person
    {
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime DateOfBirth {get; set;}
        public DateTime? DateOfDeath {get; set;}
        public string AvatarUrl {get; set;}
        
        // Should be one-to-many in a real app
        public string Interests {get; set;}

        // Should be one-to-many with addresses (billing, mailing, etc.)
        public string Street1 {get; set;}
        public string Street2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string PostalCode {get; set;}
    }
}
