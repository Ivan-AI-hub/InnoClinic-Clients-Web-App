using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Domain.Services
{
    public record UpdateServiceModel(string Name, int Price, bool Status, Guid SpecializationId, string CategoryName);
}
