﻿using Abp.Application.Services.Dto;
using eKhaya.Domain.ENums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKhaya.Services.Dtos
{
    public class UnitsDto : EntityDto<Guid>
    {
        public int Size { get; set; }

        public UnitType Type { get; set; }

        public Guid AgentID { get; set; }

        public string UnitNumber { get; set; }

        public int Level { get; set; }

        public bool Availability { get; set; }

        public Guid PropertyID { get; set; }

        public List<Guid> AmenityIds { get; set; }
    }
    }
