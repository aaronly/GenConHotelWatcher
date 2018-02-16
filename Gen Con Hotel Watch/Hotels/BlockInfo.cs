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

namespace Gen_Con_Hotel_Watch.Hotels
{
    public class BlockInfo
    {
        public string Description { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public float AverageRate { get; set; }
        public int MaxPersons { get; set; }
        public Inventory[] Inventory { get; set; }
        public bool TaxesIncluded { get; set; }
        public bool ChildrenAffectRate { get; set; }
        public float Charge { get; set; }
        public float UpsellAmount { get; set; }
        public float RoomRate2nd { get; set; }
        public float RoomRate3rd { get; set; }
        public float RoomRate4th { get; set; }
        public float RoomRate5thPlus { get; set; }
        public string CancelPolicy { get; set; }
        public bool ShowSmokingPref { get; set; }
        public string MarketingMessage { get; set; }
        public int UserDefinedRank { get; set; }
        public float AverageBasicRate { get; set; }
        public float Rating { get; set; }
        public string TaxPolicy { get; set; }
        public int MinNumberOfRooms { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
