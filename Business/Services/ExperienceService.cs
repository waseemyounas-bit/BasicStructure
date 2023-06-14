using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;

namespace Business.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IRepository<Experience> repository;
        private readonly IUnitofWork unitofWork;

        public ExperienceService(IRepository<Experience> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public void AddExperience(Experience experience)
        {
            repository.Add(experience);
            unitofWork.saveChanges();
        }

        public void DeleteExperience(Guid id)
        {
            repository.Delete(id);
            unitofWork.saveChanges();
        }

        public List<Experience> GetAllExperience()
        {
            var experiences = repository.GetAll().OrderByDescending(x => x.EndDate).ToList();
            return experiences;
        }

        public Experience GetExperienceById(Guid id)
        {
            var experience = repository.GetById(id);
            return experience;
        }

        public void UpdateExperience(Experience experience)
        {
            repository.Update(experience);
            unitofWork.saveChanges();
        }
        public void UpdateExperienceImage(string ImageUrl, Guid id)
        {
            Experience experience = repository.GetById(id);
            experience.MediaUrl = "https://localhost:44300/" + ImageUrl;
            repository.Update(experience);
            unitofWork.saveChanges();
        }
    }
}
