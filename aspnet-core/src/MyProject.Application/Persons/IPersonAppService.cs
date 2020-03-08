using System.Threading.Tasks;
using Abp.Application.Services;
using MyProject.Persons.Dto;

namespace MyProject.Persons
{
    public interface IPersonAppService : IApplicationService
    {
        void CreatePerson(CreatePersonInput input);
    }
}
