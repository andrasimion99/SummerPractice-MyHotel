﻿using System;
using System.Collections.Generic;

namespace MyHotel.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime ReservationDate { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
