﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Domain.Specializations
{
    public record ChangeSpecializationStatusModel(Guid Id, bool IsActive);
}
