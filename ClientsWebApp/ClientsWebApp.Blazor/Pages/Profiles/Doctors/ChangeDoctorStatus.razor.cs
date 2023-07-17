using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain.Profiles.Doctor;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    public partial class ChangeDoctorStatus
    {
        [Parameter] public DoctorDTO Doctor { get; set; }

        [Parameter] public EventCallback OnStatusChanged { get; set; }

        private bool IsDateChanges { get; set; }

        private bool IsLoading { get; set; } = false;
        private IEnumerable<WorkStatus> Statuses { get; set; }

        private ChangeDoctorStatusData Data { get; set; }

        protected override void OnInitialized()
        {
            Statuses = Enum.GetValues<WorkStatus>();
            Data = new ChangeDoctorStatusData()
            {
                Status = Doctor.Status
            };
        }

        private void StartChanging()
        {
            IsDateChanges = true;
        }

        private void StopChanging()
        {
            IsDateChanges = false;
        }

        private async Task ChangeAsync()
        {
            IsLoading = true;

            await DoctorManager.ChangeStatusAsync(Doctor.Id, Data, _cts.Token);
            await OnStatusChanged.InvokeAsync();
            StopChanging();

            IsLoading = false;
        }
    }
}