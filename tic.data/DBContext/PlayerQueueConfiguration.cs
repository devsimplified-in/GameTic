﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using tic.data.Model;

namespace tic.data.DBContext
{
    public class PlayerQueueConfiguration : IEntityTypeConfiguration<PlayerQueue>
    {
        public void Configure(EntityTypeBuilder<PlayerQueue> entity)
        {
            entity.Property(e => e.PlayerQueueId).HasColumnName("PlayerQueueID");

            entity.Property(e => e.CreatedBy)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

            entity.Property(e => e.RowGuid).HasColumnName("RowGUID");

            entity.Property(e => e.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.Player)
                .WithMany(p => p.PlayerQueue)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlayerQueue_Player");
        }
    }
}
