using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMate.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CreatedByUserProfileId { get; set; }
        public Player Player { get; set; }

        public List<Player> Players { get; set; }

    }
}