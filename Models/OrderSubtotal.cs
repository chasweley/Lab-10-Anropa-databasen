﻿using System;
using System.Collections.Generic;

namespace Lab_10_Anropa_databasen.Models
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
