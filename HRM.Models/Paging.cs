﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Models
{
    public class Paging
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public Paging(int page,int itemsPerPage)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
       
    }
}
