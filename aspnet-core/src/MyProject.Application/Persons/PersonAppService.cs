

using Abp.Domain.Repositories;
using Abp.UI;
using MyProject.Persons.Dto;

namespace MyProject.Persons
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IRepository<Person> _personRepository;

        public PersonAppService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public void CreatePerson(CreatePersonInput input)
        {
            var person = _personRepository.FirstOrDefault(p => p.EmailAdress == input.EmailAddress);
            if (person != null)
            {
                throw new UserFriendlyException("There is already a person with given email address");
            }

            person = new Person{ Name = input.Name, EmailAddress = input.EmailAddress };
            _personRepository.Insert(person);

        }
    }
}
