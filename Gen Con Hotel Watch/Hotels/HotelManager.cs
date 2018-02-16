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
using System.Collections.Generic;

namespace Gen_Con_Hotel_Watch.Hotels
{
    public class HotelManager {
        public static string GetPrettyName(string input)
        {
            return input.Replace("&amp;", "&").Replace("&#x2f;", "/").Replace("&#x2b;", "+").Replace("&#x28;", "(").Replace("&#x29;", ")");
        }

        public static List<Data> HotelList = new List<Data> {
            new Data("Baymont Inn & Suites - Indy Airport- Plainfield Gateway Dr.",true,true),
            new Data("Best Western Plus Atrea Airport Inn & Suites",true,true),
            new Data("Cambria Suites Indianapolis Airport",true,false),
            new Data("Candlewood  Suites Airport",true,false),
            new Data("Candlewood Suites Indianapolis City Centre",true,false),
            new Data("Candlewood Suites Indianapolis East",true,false),
            new Data("Clarion Hotel & Conference Center-Indy Waterfront Pkwy",true,true),
            new Data("Clarion Inn and Suites Northwest",true,true),
            new Data("Columbia Club / At Monument Circle Downtown",false,false),
            new Data("Comfort Suites City Centre",false,true),
            new Data("Comfort Suites Indianapolis Airport",true,true),
            new Data("Conrad Indianapolis",false,false),
            new Data("Country Inns & Suites By Carlson, Indianapolis Airport South",true,true),
            new Data("Courtyard by Marriott Airport",true,false),
            new Data("Courtyard by Marriott at the Capitol",false,false),
            new Data("Courtyard by Marriott Downtown Indianapolis",false,false),
            new Data("Courtyard by Marriott Northwest",true,false),
            new Data("Crowne Plaza Indianapolis Airport",true,false),
            new Data("Crowne Plaza Indianapolis Downtown Union Station",false,false),
            new Data("Doubletree Guest Suites- N. Meridian Carmel",true,false),
            new Data("Embassy Suites Indianapolis - North",true,true),
            new Data("Embassy Suites Indianapolis Downtown",false,true),
            new Data("Fairfield Inn & Suites Indianapolis Downtown",false,true),
            new Data("Hampton Inn - Northwest",true,true),
            new Data("Hampton Inn and Suites - Airport",true,true),
            new Data("Hampton Inn Downtown",false,true),
            new Data("Hilton Garden Inn - Airport",true,false),
            new Data("Hilton Garden Inn Downtown",false,false),
            new Data("Hilton Garden Inn Indianapolis- Carmel",true,false),
            new Data("Hilton Indianapolis",false,false),
            new Data("Holiday Inn Express Hotel & Suites City Centre",false,false),
            new Data("Holiday Inn Express Indianapolis West- Airport Area",true,true),
            new Data("Holiday Inn Express Northwest - Park 100",true,true),
            new Data("Holiday Inn Indianapolis Airport",true,false),
            new Data("Holiday Inn Indianapolis-Carmel",true,false),
            new Data("Home2 Suites Indianapolis Downton",false,true),
            new Data("Homewood Suites by Hilton - Airport/Plainfield",true,true),
            new Data("Homewood Suites by Hilton NW",true,true),
            new Data("Hotel Indy By Lexington (Ex Radisson Indianapolis Airport)",true,false),
            new Data("Hyatt Regency Indianapolis",false,false),
            new Data("Indianapolis Marriott Downtown",false,false),
            new Data("Indianapolis Marriott East & Conference Center",true,false),
            new Data("JW Marriott Indianapolis",false,false),
            new Data("Le M‚ridien Indianapolis",false,false),
            new Data("Omni Severin Hotel",false,false),
            new Data("Renaissance Indianapolis North Hotel",true,false),
            new Data("Sheraton Indianapolis City Centre Hotel",false,false),
            new Data("Sheraton Indianapolis Hotel at Keystone Crossing",true,false),
            new Data("SpringHill Suites Indianapolis Downtown",false,true),
            new Data("Staybridge Suites Indianapolis City Centre",false,true),
            new Data("The Alexander",false,false),
            new Data("The Westin Indianapolis",false,false),
            new Data("Wingate NW Indianapolis",true,true),
            new Data("Wyndham Indianapolis West",true,false) };
    }



}
