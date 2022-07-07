﻿using Microsoft.EntityFrameworkCore;
using MyHotel.Domain.Entities;

namespace MyHotel.Persistance.Data.Mappings
{
    internal abstract class ReservationMapping
    {
        internal static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .Property(s => s.Id)
                .HasColumnName("Id")
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(s => s.CheckIn)
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(s => s.CheckOut)
                .IsRequired();
        }
    }
}
