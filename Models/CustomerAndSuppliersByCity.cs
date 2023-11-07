﻿using System;
using System.Collections.Generic;

namespace Lab_10_Anropa_databasen.Models
{
    public partial class CustomerAndSuppliersByCity
    {
        public string? City { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string Relationship { get; set; } = null!;
    }
}
