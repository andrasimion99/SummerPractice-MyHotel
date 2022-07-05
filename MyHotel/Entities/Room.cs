﻿using System.Collections.Generic;

namespace MyHotel.Entities
{
    public class Room : BaseEntity
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int Price { get; set; }
        public string Facilities { get; set; }
        public string Status { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
