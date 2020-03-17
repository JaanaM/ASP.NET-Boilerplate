

using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.Extensions.Logging;
using MyProject.Persons.Dto;

namespace MyProject.Persons
{
    public class PersonAppService : IPersonAppService
    {
        public ILogger Logger { get; set; }
        //private IRepository<Person> _personRepository;

        ////constructor injection. 
        ////PersonAppService does not know which classes implement IPersonRepository or how it is created. 
        ////When an PersonAppService is needed, we first create an IPersonRepository and pass it to the constructor of the PersonAppService
        //public PersonAppService(IRepository<Person> personRepository)
        //{
        //    _personRepository = personRepository;
        //}

        public void CreatePerson(CreatePersonInput input)
        {
        //    Logger.Debug("IChecking if person excists with email address...");
        //    var person = _personRepository.FirstOrDefault(p => p.EmailAdress == input.EmailAddress);
        //    if (person != null)
        //    {
        //        throw new UserFriendlyException("There is already a person with given email address");
        //    }
        //    Logger.Debug("Inserting a new person with email address" + input.EmailAddress);
        //    person = new Person{ Name = input.Name, EmailAddress = input.EmailAddress };
        //    _personRepository.Insert(person);
        //    Logger.Debug("Person succefully added");

        }
    }
}
