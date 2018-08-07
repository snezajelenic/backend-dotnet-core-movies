﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoviesBackend.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        [Range(1950, 2025, ErrorMessage = "Please put in a valid year!")]
        public int Year { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
}
