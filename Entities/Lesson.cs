﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace UniversityProject.Entities
{
    public class Lesson
    {
        [Key]
        public Guid Id { get; set; }

        public  string Name { get; set; }

        public  string Description { get; set; }

        public bool  IsDeleted { get; set; }
        public bool  IsActive { get; set; }
        public  DateTime  CreatedOn { get; set; }
    }
}
