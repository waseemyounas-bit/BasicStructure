
using Data.Entities;

namespace Business.IServices
{
    public interface IEducationService
    {
        void AddEducation(Education education);
        void UpdateEducation(Education education);
        void DeleteEducation(Guid id);
        Education GetEducationById(Guid id);
        List<Education> GetAllEducation();
        void UpdateEducationImage(string ImageUrl, Guid id);
    }
}
