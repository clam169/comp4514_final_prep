using System.Collections.Generic;
using final_mock.Models;

namespace final_mock.Data
{
    public class SeedData
    {

        public static List<Person> GetPeople(){
            List<Person> people = new List<Person>() {
                new Person {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Occupation = "Student",
                    Gender = "M",
                    PictureUrl = "",
                    Votes = 0
                },
                new Person {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Occupation = "Student",
                    Gender = "F",
                    PictureUrl = "",
                    Votes = 0
                }
            };

            return people;
        }
    }
}