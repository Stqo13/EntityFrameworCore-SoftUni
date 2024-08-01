﻿using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models
{
    public class Writer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        public string? Pseudonym { get; set; }

        public virtual List<Song> Songs { get; set; } = new List<Song>();
    }
}
