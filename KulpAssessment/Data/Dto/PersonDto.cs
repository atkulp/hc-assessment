using System;

namespace KulpAssessment.Data.Dto
{
    // Much more valuable in a real app to avoid circular dependencies and have
    // tighter control over what gets returned in different scenarios
    public class PersonDto
    {
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime DOB {get; set;}
        public DateTime? DOD {get; set;}
        public string AvatarUrl {get; set;}
        public string Interests {get; set;}
        public string Street1 {get; set;}
        public string Street2 {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public string PostalCode {get; set;}

        public static PersonDto FromEntity(Entities.Person src)
        {
            return new PersonDto{
                Id = src.Id,
                FirstName = src.FirstName,
                LastName = src.LastName,
                DOB = src.DateOfBirth,
                DOD = src.DateOfDeath,
                AvatarUrl = src.AvatarUrl,
                Interests = src.Interests,
                Street1 = src.Street1,
                Street2 = src.Street2,
                City = src.City,
                State = src.State,
                PostalCode = src.PostalCode
            };
        }
    }
}
