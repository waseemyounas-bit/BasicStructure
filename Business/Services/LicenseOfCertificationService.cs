using Business.IServices;
using Data.Entities;
using DataAccess.Repository;
using DataAccess.UoW;

namespace Business.Services
{
    public class LicenseOfCertificationService : ILicenseOfCertificationService
    {
        private readonly IRepository<LicenseOfCertification> repository;
        private readonly IUnitofWork unitofWork;

        public LicenseOfCertificationService(IRepository<LicenseOfCertification> _repository, IUnitofWork _unitofWork)
        {
            repository = _repository;
            unitofWork = _unitofWork;
        }
        public void AddLicenseOfCertification(LicenseOfCertification licenseOfCertification)
        {
            repository.Add(licenseOfCertification);
            unitofWork.saveChanges();
        }

        public void DeleteLicenseOfCertification(Guid id)
        {
            repository.Delete(id);
            unitofWork.saveChanges();
        }

        public List<LicenseOfCertification> GetAllLicenseOfCertification()
        {
            var experiences = repository.GetAll().OrderByDescending(x => x.EndDate).ToList();
            return experiences;
        }

        public LicenseOfCertification GetLicenseOfCertificationById(Guid id)
        {
            var licenseOfCertification = repository.GetById(id);
            return licenseOfCertification;
        }

        public void UpdateLicenseOfCertification(LicenseOfCertification licenseOfCertification)
        {
            repository.Update(licenseOfCertification);
            unitofWork.saveChanges();
        }
        public void UpdateLicenseOfCertificationImage(string ImageUrl, Guid id)
        {
            LicenseOfCertification licenseOfCertification = repository.GetById(id);
            licenseOfCertification.LicenseOfCertificationImage = "https://localhost:44300/" + ImageUrl;
            repository.Update(licenseOfCertification);
            unitofWork.saveChanges();
        }
    }
}
