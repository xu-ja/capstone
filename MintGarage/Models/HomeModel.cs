﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using MintGarage.Models.HomeTab.Reviews;
using MintGarage.Models.HomeTab.HomeContents;
using MintGarage.Models.HomeTab.Suppliers;

namespace MintGarage.Models
{
    public class HomeModel
    {
        public IEnumerable<Review> Review { get; set; }
        public IEnumerable<Supplier> Supplier { get; set; }
        public IEnumerable<HomeContent> HomeContent { get; set; }
    }
}

