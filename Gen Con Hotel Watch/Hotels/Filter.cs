/*  A small windows form application to watch for vacancies in Gen Con's hotel block
 *  Copyright (C) 2017 Aaron Echols (aaronly@gmail.com)
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using Gen_Con_Hotel_Watch.Hotels.Info;
using System;
using System.Collections.Generic;

namespace Gen_Con_Hotel_Watch.Hotels
{
    class Filter
    {
        public int NumOfGuests { get; set; }
        public int NumOfRooms { get; set; }
        public bool FreeBreakfast { get; set; }
        public bool FreeParking { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DistanceUnit DistanceUnits { get; set; } = DistanceUnit.blocks;
        public float Distance { get; set; } = 1;
        public int MaximumNotifications { get; set; }

        public List<Hotel> GetMatchingHotels(List<Hotel> hotels)
        {
            List<Hotel> results = new List<Hotel>();

            foreach (Hotel hotel in hotels)
            {
                string distanceUnit = hotel.DistanceUnit.ToString();
                float distance = hotel.DistanceFromEvent;
                Data data = HotelManager.HotelList.Find(x => x.Name.Equals(hotel.Name));
                bool breakfast = data.Breakfast;
                bool parking = data.Parking;

                DistanceUnit desiredDistanceUnit = DistanceUnits;
                float desiredDistance = Distance;
                bool desiredBreakfast = FreeBreakfast;
                bool desiredParking = FreeParking;

                // Look for hotels that match the distance criteria
                if (
                    ((distanceUnit == desiredDistanceUnit.ToString()) || (distanceUnit == "blocks"))
                    &&
                    (distance <= desiredDistance)
                    )
                {
                    // Look for hotels that match the amenities criteria
                    if (
                        ((desiredBreakfast == breakfast) || (desiredBreakfast == false))
                        &&
                        ((desiredParking == parking) || (desiredParking == false))
                        )
                        results.Add(hotel);
                }
            }

            return results;
        }
    }
}
