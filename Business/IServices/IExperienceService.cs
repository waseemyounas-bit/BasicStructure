using Data.Entities;

namespace Business.IServices
{
    public interface IExperienceService
    {
        void AddExperience(Experience experience);
        void UpdateExperience(Experience experience);
        void DeleteExperience(Guid id);
        Experience GetExperienceById(Guid id);
        List<Experience> GetAllExperience();
        void UpdateExperienceImage(string ImageUrl, Guid id);
    }
}
