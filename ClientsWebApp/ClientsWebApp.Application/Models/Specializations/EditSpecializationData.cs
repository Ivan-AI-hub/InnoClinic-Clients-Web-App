using ClientsWebApp.Domain.Specializations;

namespace ClientsWebApp.Application.Models.Specializations
{
    public class EditSpecializationData
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public EditSpecializationData(Specialization specialization)
        {
            Name = specialization.Name;
            IsActive = specialization.IsActive;
        }
    }
}
