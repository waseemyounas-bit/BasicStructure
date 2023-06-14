using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;

namespace Business.Services
{
    public class EducationService : IEducationService
    {
        private readonly IRepository<Education> repository;
        private readonly IUnitofWork unitofWork;
        public EducationService(IRepository<Education> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public void AddEducation(Education education)
        {
            repository.Add(education);
            unitofWork.saveChanges();
        }

        public void DeleteEducation(Guid id)
        {
            repository.Delete(id);
            unitofWork.saveChanges();
        }

        public List<Education> GetAllEducation()
        {
            var educations = repository.GetAll().OrderByDescending(x => x.EndDate).ToList();
            return educations;
        }

        public Education GetEducationById(Guid id)
        {
            var education = repository.GetById(id);
            return education;
        }

        public void UpdateEducation(Education education)
        {
            repository.Update(education);
            unitofWork.saveChanges();
        }
        public void UpdateEducationImage(string ImageUrl, Guid id)
        {
            Education education = repository.GetById(id);
            education.MediaUrl = "https://localhost:44300/" + ImageUrl;
            repository.Update(education);
            unitofWork.saveChanges();
        }
    }
}
