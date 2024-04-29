﻿using eKhaya.Domain.ENums;
using eKhaya.Domain.Units;
using eKhaya.Domain.Users;
using eKhaya.Domain.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class CreateMaintenanceRequestDto
    {
        public MaintenanceType Type { get; set; }
        public MaintenanceRequestStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DateCompleted { get; set; }

        public Resident Tenant { get; set; }

        public Unit UnitID { get; set; }

        public Worker Worker { get; set; }
    }
}
