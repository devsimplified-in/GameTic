﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace tic.data.Model
{
    public partial class Player
    {
        public Player()
        {
            PlayerGame = new HashSet<PlayerGame>();
            PlayerQueue = new HashSet<PlayerQueue>();
        }

        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Guid RowGuid { get; set; }

        public virtual ICollection<PlayerGame> PlayerGame { get; set; }
        public virtual ICollection<PlayerQueue> PlayerQueue { get; set; }
    }
}