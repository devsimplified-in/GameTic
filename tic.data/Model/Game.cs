﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace tic.data.Model
{
    public partial class Game
    {
        public Game()
        {
            PlayerGame = new HashSet<PlayerGame>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Guid RowGuid { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<PlayerGame> PlayerGame { get; set; }
    }
}