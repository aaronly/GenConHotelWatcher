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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Gen_Con_Hotel_Watch
{
    
    public partial class FormSearch : Form
    {
        public static CookieContainer cookieContainer = new CookieContainer();

        public static string key;
        public static int guests;
        public static int rooms;
        public static int children;
        public static bool breakfast;
        public static bool parking;
        public static DateTime checkIn;
        public static DateTime checkOut;
        public static string hotelSite = "https://aws.passkey.com/event/14276138/owner/10909638/rooms/select";
        public static string searchResults;
        public static Hotel[] vacancies;
        public static Hotel[] results;
        public static bool repeat = false;
        public static int maxNotify;
        public static DateTime beginTime;
        public static DateTime endTime;
        public static string searchUnit = "blocks";
        public static decimal searchValue = 1;
        public static int maxCounter = 0;

        public EmailInfo[] emailInfo;

        GMapOverlay hotelMarkers = new GMapOverlay("hotels");
        GMapOverlay iccOverlay = new GMapOverlay("ICC");

        private int sortColumn = -1;

        #region Hotel Amenties
        public static HotelData[] hotelinfo = new HotelData[] {
            new HotelData("Baymont Inn & Suites - Indy Airport- Plainfield Gateway Dr.",true,true),
            new HotelData("Best Western Plus Atrea Airport Inn & Suites",true,true),
            new HotelData("Cambria Suites Indianapolis Airport",true,false),
            new HotelData("Candlewood  Suites Airport",true,false),
            new HotelData("Candlewood Suites Indianapolis City Centre",true,false),
            new HotelData("Candlewood Suites Indianapolis East",true,false),
            new HotelData("Clarion Hotel & Conference Center-Indy Waterfront Pkwy",true,true),
            new HotelData("Clarion Inn and Suites Northwest",true,true),
            new HotelData("Columbia Club / At Monument Circle Downtown",false,false),
            new HotelData("Comfort Suites City Centre",false,true),
            new HotelData("Comfort Suites Indianapolis Airport",true,true),
            new HotelData("Conrad Indianapolis",false,false),
            new HotelData("Country Inns & Suites By Carlson, Indianapolis Airport South",true,true),
            new HotelData("Courtyard by Marriott Airport",true,false),
            new HotelData("Courtyard by Marriott at the Capitol",false,false),
            new HotelData("Courtyard by Marriott Downtown Indianapolis",false,false),
            new HotelData("Courtyard by Marriott Northwest",true,false),
            new HotelData("Crowne Plaza Indianapolis Airport",true,false),
            new HotelData("Crowne Plaza Indianapolis Downtown Union Station",false,false),
            new HotelData("Doubletree Guest Suites- N. Meridian Carmel",true,false),
            new HotelData("Embassy Suites Indianapolis - North",true,true),
            new HotelData("Embassy Suites Indianapolis Downtown",false,true),
            new HotelData("Fairfield Inn & Suites Indianapolis Downtown",false,true),
            new HotelData("Hampton Inn - Northwest",true,true),
            new HotelData("Hampton Inn and Suites - Airport",true,true),
            new HotelData("Hampton Inn Downtown",false,true),
            new HotelData("Hilton Garden Inn - Airport",true,false),
            new HotelData("Hilton Garden Inn Downtown",false,false),
            new HotelData("Hilton Garden Inn Indianapolis- Carmel",true,false),
            new HotelData("Hilton Indianapolis",false,false),
            new HotelData("Holiday Inn Express Hotel & Suites City Centre",false,false),
            new HotelData("Holiday Inn Express Indianapolis West- Airport Area",true,true),
            new HotelData("Holiday Inn Express Northwest - Park 100",true,true),
            new HotelData("Holiday Inn Indianapolis Airport",true,false),
            new HotelData("Holiday Inn Indianapolis-Carmel",true,false),
            new HotelData("Home2 Suites Indianapolis Downton",false,true),
            new HotelData("Homewood Suites by Hilton - Airport/Plainfield",true,true),
            new HotelData("Homewood Suites by Hilton NW",true,true),
            new HotelData("Hotel Indy By Lexington (Ex Radisson Indianapolis Airport)",true,false),
            new HotelData("Hyatt Regency Indianapolis",false,false),
            new HotelData("Indianapolis Marriott Downtown",false,false),
            new HotelData("Indianapolis Marriott East & Conference Center",true,false),
            new HotelData("JW Marriott Indianapolis",false,false),
            new HotelData("Le M‚ridien Indianapolis",false,false),
            new HotelData("Omni Severin Hotel",false,false),
            new HotelData("Renaissance Indianapolis North Hotel",true,false),
            new HotelData("Sheraton Indianapolis City Centre Hotel",false,false),
            new HotelData("Sheraton Indianapolis Hotel at Keystone Crossing",true,false),
            new HotelData("SpringHill Suites Indianapolis Downtown",false,true),
            new HotelData("Staybridge Suites Indianapolis City Centre",false,true),
            new HotelData("The Alexander",false,false),
            new HotelData("The Westin Indianapolis",false,false),
            new HotelData("Wingate NW Indianapolis",true,true),
            new HotelData("Wyndham Indianapolis West",true,false) };
        #endregion

        public FormSearch()
        {
            InitializeComponent();
        }
        private void FormSearch_Load(object sender, EventArgs e)
        {
            comboBoxUnits.Text = "blocks";

            // Specifiying the map provider and center location
            myMap.MapProvider = GMap.NET.MapProviders.OviMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            myMap.Position = new PointLatLng(39.764386, -86.164050); // Center map on Indianapolis
            myMap.ShowCenter = false;

            myMap.Overlays.Add(hotelMarkers);
            myMap.Overlays.Add(iccOverlay);

            // Adding a polygon to the map (The Indiana Convention Center)
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(39.765818, -86.167050));
            points.Add(new PointLatLng(39.765719, -86.161772));
            points.Add(new PointLatLng(39.762684, -86.161900));
            points.Add(new PointLatLng(39.763707, -86.166578));
            points.Add(new PointLatLng(39.764672, -86.167116));
            GMapPolygon polygon = new GMapPolygon(points, "ICC");
            polygon.Fill = new SolidBrush(Color.FromArgb(100, Color.DarkBlue));
            polygon.Stroke = new Pen(Color.DarkBlue, 1);
            iccOverlay.Polygons.Add(polygon);

            myMap.DragButton = MouseButtons.Left;
            myMap.Select();

            comboBoxMap.Items.Add(GMap.NET.MapProviders.OviHybridMapProvider.Instance);
            comboBoxMap.Items.Add(GMap.NET.MapProviders.OviMapProvider.Instance);
            comboBoxMap.SelectedText = myMap.MapProvider.Name;

        }
        public void myMap_OnMapTypeChanged(GMap.NET.MapProviders.GMapProvider provider)
        {
            if (myMap.MapProvider.MaxZoom != null)
                myMap.MaxZoom = (int)myMap.MapProvider.MaxZoom;
            if (myMap.MapProvider.MaxZoom > 5)
                myMap.MinZoom = (int)myMap.MapProvider.MinZoom;
            int zoomDelta = myMap.MaxZoom - myMap.MinZoom;
            trackBarMap.Maximum = zoomDelta;
            trackBarMap.Value = (int)myMap.Zoom - myMap.MinZoom;
        }

        #region Search Functions
        public static async Task RunAsync()
        {
            try
            {
                #region Get Initial Cookies
                string mainSite = string.Format("https://aws.passkey.com/reg/{0}/null/null/1/0/null", key);
                var mainAddress = new Uri(mainSite);
                using (var mainClient = new HttpClient() { BaseAddress = mainAddress })
                {
                    mainClient.Timeout = new System.TimeSpan(0, 2, 0);
                    HttpResponseMessage mainResponse = await mainClient.GetAsync(mainAddress);

                    if (mainResponse.IsSuccessStatusCode)
                    {
                        cookieContainer = ReadCookies(mainResponse);
                    }
                    else
                    {
                        return;
                    }
                }
                #endregion

                #region Get Search Results
                var criteria = new Dictionary<string, string>();
                criteria.Add("hotelId", "0");
                criteria.Add("blockMap.blocks[0].blockId", "0");
                criteria.Add("blockMap.blocks[0].checkIn", checkIn.ToString("yyyy-MM-dd"));
                criteria.Add("blockMap.blocks[0].checkOut", checkOut.ToString("yyyy-MM-dd"));
                criteria.Add("blockMap.blocks[0].numberOfGuests", guests.ToString());
                criteria.Add("blockMap.blocks[0].numberOfRooms", rooms.ToString());
                criteria.Add("blockMap.blocks[0].numberOfChildren", children.ToString());

                var query = new FormUrlEncodedContent(criteria);

                var hotelAddress = new Uri(hotelSite);
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var hotelClient = new HttpClient(handler) { BaseAddress = hotelAddress })
                {
                    hotelClient.Timeout = new System.TimeSpan(0, 2, 0);
                    HttpResponseMessage hotelResponse = await hotelClient.PostAsync(hotelAddress, query);
                    if (!hotelResponse.IsSuccessStatusCode)
                        return;
                    //File.WriteAllText(@"C:\Users\Aaron\Downloads\Testing\hotelResponse.html", await hotelResponse.Content.ReadAsStringAsync());
                    searchResults = await hotelResponse.Content.ReadAsStringAsync();
                }
                #endregion
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public static void FindHotels(string response)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            string results = doc.DocumentNode.Descendants().Single(x => x.Id == "last-search-results").InnerHtml;
            results = @"{""hotels"":" + results + "}";
            Hotels hotelresults = JsonConvert.DeserializeObject<Hotels>(results);

            #region Get the vacancies available
            int i = 0;
            foreach (Hotel hotel in hotelresults.hotels)
            {
                if (hotel.blocks.Length > 0)
                {
                    Array.Resize(ref vacancies, i + 1);
                    vacancies[i] = hotel;
                    vacancies[i].name = vacancies[i].name.Replace("&amp;", "&").Replace("&#x2f;", "/").Replace("&#x2b;", "+").Replace("&#x28;", "(").Replace("&#x29;", ")");
                    i += 1;
                }
            }

            #endregion
        }
        public static CookieContainer ReadCookies(HttpResponseMessage response)
        {
            var pageUri = response.RequestMessage.RequestUri;
            var cookieContainer = new CookieContainer();
            IEnumerable<string> cookies;
            if (response.Headers.TryGetValues("set-cookie", out cookies))
            {
                foreach (var c in cookies)
                {
                    cookieContainer.SetCookies(pageUri, c);
                }
            }
            return cookieContainer;
        }
        #endregion

        /// <summary>
        /// A quick function to send an email
        /// </summary>
        /// <param name="emailInfo">An array of email information including: SMTP host, credentials, and to address.</param>
        /// <param name="messageSubject">The subject of the email message.</param>
        /// <param name="messageBody">The body of the email message. HTML is allowed.</param>
        public void sendEmail(EmailInfo[] emailInfo, string messageSubject, string messageBody)
        {
            foreach (EmailInfo email in emailInfo)
            {
                SmtpClient client = new SmtpClient(email.smtp);

                    client.EnableSsl = true;
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                    client.Credentials = new NetworkCredential(email.from, email.pword);
                    client.Port = 587;

                    MailMessage message = new MailMessage();
                    message.To.Add(email.to);
                    message.From = new MailAddress(email.from);
                    message.IsBodyHtml = true;
                    message.Subject = messageSubject;
                    message.Body = messageBody;

                    client.SendAsync(message, "Gen Con Hotel Watch Notification");
            }
        }
        private static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(String.Format("[{0}] {1}", e.UserState, e.Error.ToString()), "Email Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool getDataFromForm()
        {
            key = textBoxKey.Text;
            guests = (int)numericUpDownGuests.Value;
            rooms = (int)numericUpDownRooms.Value;
            breakfast = checkBoxBreakfast.Checked;
            parking = checkBoxParking.Checked;
            checkIn = monthCalendarSelection.SelectionStart;
            checkOut = monthCalendarSelection.SelectionEnd;
            searchUnit = comboBoxUnits.Text;
            searchValue = numericUpDownSearch.Value;
            maxNotify = (int)numericUpDownMaxNotify.Value;
            if ((checkIn > new DateTime(2016, 8, 4)) | (checkOut < new DateTime(2016, 8, 7)))
            {
                MessageBox.Show("8/4/2016 is the latest available check in date and \r" +
                    "8/7/2016 is the earliest available check out.\r" +
                    "Please reselect your check in and check out dates.", "Not enough nights selected!", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            

            return false;
        }

        private void disableControls()
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            toolStripMenuItemStartSearch.Enabled = false;
            toolStripMenuItemStopSearch.Enabled = true;
        }
        private void enableControls()
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            toolStripMenuItemStartSearch.Enabled = true;
            toolStripMenuItemStopSearch.Enabled = false;
        }

        private void sendNotifications()
        {

            string popup = "";
            string begin = "<html><body><ul>";
            string middle = "";
            string end = String.Format("</ul><a href=\"https://aws.passkey.com/reg/{0}/null/null/1/0/null\">Go to Housing page.</a></body></html>", key);

            for (int i = 0; i < results.Length; i++)
            {
                Hotel result = results[i];
                string distUnit;
                if (result.distanceUnit == 1)
                    distUnit = "blocks";
                else if (result.distanceUnit == 2)
                    distUnit = "yards";
                else if (result.distanceUnit == 3)
                    distUnit = "miles";
                else if (result.distanceUnit == 4)
                    distUnit = "meters";
                else if (result.distanceUnit == 5)
                    distUnit = "kilometers";
                else
                    distUnit = "unknown";

                middle += String.Format("<li><b>{0}</b><ul><li>{1} {2} away</li></ul></li>", result.name, result.distanceFromEvent.ToString(), distUnit);
                popup += String.Format("{0}\r\t{1} {2}", result.name, result.distanceFromEvent.ToString(), distUnit) + "\r\r";
            }
            string message = begin + middle + end;

            // Send email if it is not empty
            if ((checkBoxEmail.Checked) && (emailInfo != null))
                sendEmail(emailInfo, "Vacant rooms found!", message);

            if (checkBoxPopup.Checked)
                MessageBox.Show(new Form { TopMost = true }, popup, "Vacant rooms found!", MessageBoxButtons.OK);
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text == "")
            {
                MessageBox.Show("Please enter your hotel access key.");
                return;
            }
                
            hotelMarkers.Clear();
            listViewDates.Items.Clear();
            listViewHotels.Items.Clear();
            labelRoomType.Text = "";
            textBoxOverallRate.Clear();

            // Get the data from the form
            if (getDataFromForm())
                goto NoAction;

            // Pull the information from the internet
            await RunAsync();

            // Find available vacancies
            FindHotels(searchResults);

            // Add the results to the listview
            listViewHotels.Items.Clear();
            listViewDates.Items.Clear();
            int resultNum = 0;
            results = null;
            foreach (Hotel hotel in vacancies)
            {
                // Get the hotel name and add it to the list
                string hotelname = hotel.name.Replace("&amp;", "&").Replace("&#x2f;", "/").Replace("&#x2b;", "+").Replace("&#x28;", "(").Replace("&#x29;", ")");
                ListViewItem hotelItem = listViewHotels.Items.Add(hotelname, hotelname, 0);

                // Add the distance to the list
                hotelItem.SubItems.Add(hotel.distanceFromEvent.ToString());

                // Add the distance unit to the list
                string distUnit;
                if (hotel.distanceUnit == 1)
                    distUnit = "blocks";
                else if (hotel.distanceUnit == 2)
                    distUnit = "yards";
                else if (hotel.distanceUnit == 3)
                    distUnit = "miles";
                else if (hotel.distanceUnit == 4)
                    distUnit = "meters";
                else if (hotel.distanceUnit == 5)
                    distUnit = "kilometers";
                else
                    distUnit = "unknown";
                hotelItem.SubItems.Add(distUnit);

                // Lookup breakfast and parking and add it to the list
                bool thisParking = false;
                bool thisBreakfast = false;
                foreach( HotelData thisData in hotelinfo)
                {
                    if(thisData.name == hotelname)
                    {
                        thisParking = thisData.parking;
                        thisBreakfast = thisData.breakfast;
                        if (thisBreakfast == true)
                        {
                            hotelItem.SubItems.Add("yes");
                        } else {
                            hotelItem.SubItems.Add("no");
                        }
                        if (thisParking == true)
                        {
                            hotelItem.SubItems.Add("yes");
                        }
                        else {
                            hotelItem.SubItems.Add("no");
                        }
                    }
                }

                // Adding results to the map
                PointLatLng hotelLatLng = new PointLatLng(hotel.latitude, hotel.longitude);
                GMarkerGoogle marker = new GMarkerGoogle(hotelLatLng, GMarkerGoogleType.red);
                marker.ToolTipText = hotel.name;
                hotelMarkers.Markers.Add(marker);

                // Look for hotels that match the distance criteria
                if (((distUnit == comboBoxUnits.Text) || (distUnit == "blocks")) && ((decimal)hotel.distanceFromEvent <= searchValue))
                {
                    // Look for hotels that match the amenities criteria
                    if (((breakfast == thisBreakfast) || (breakfast == false)) && ((parking == thisParking) || (parking == false)))
                    {
                        Array.Resize(ref results, resultNum + 1);
                        results[resultNum] = hotel;
                        resultNum += 1;
                    }
                    
                }
            }
            myMap.Update();

            // If there were hotels found that match the search criteria, send notifications.
            if (results != null)
            {
                if (maxCounter < maxNotify) sendNotifications();
                maxCounter += 1;
            }
            else
            {
                maxCounter = 0;
            }

            if (repeat)
            {
                beginTime = DateTime.Now;
                endTime = beginTime.AddMinutes((double)numericUpDownRepeat.Value);

                this.Text = "Gen Con Hotel Search - " + getTimeString(endTime);

                timer1.Start();

                disableControls();
            }

            NoAction:
            return;

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Text = "Gen Con Hotel Search";
            enableControls();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void listViewHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listViewHotels.SelectedItems.Count == 0)
                return;
            string hotelName = listViewHotels.SelectedItems[0].Name;
            Hotel hotel = vacancies.Single(q => q.name == hotelName);
            BlockInfo selection = hotel.blocks[0];
            textBoxOverallRate.Text = selection.charge.ToString("C2");
            labelRoomType.Text = selection.name.Replace("&amp;", "&").Replace("&#x2f;", "/").Replace("&#x2b;", "+").Replace("&#x28;", "(").Replace("&#x29;", ")"); 

            Inventory[] nights = selection.inventory;
            DateTime[] availableNights = new DateTime[nights.Count()];
            int m = 0;
            listViewDates.Items.Clear();
            foreach (Inventory night in nights)
            {
                availableNights[m] = new DateTime(night.date[0], night.date[1], night.date[2]);
                ListViewItem nightItem = listViewDates.Items.Add(availableNights[m].ToShortDateString());
                nightItem.SubItems.Add(night.available.ToString());
                nightItem.SubItems.Add(night.rate.ToString("C2"));
                m += 1;
            }

            GMarkerGoogle oldMarker = (GMarkerGoogle)hotelMarkers.Markers.SingleOrDefault(n => n.ToolTipText == null);
            hotelMarkers.Markers.Remove(oldMarker);
            GMarkerGoogle newMarker = new GMarkerGoogle(new PointLatLng(hotel.latitude, hotel.longitude), GMarkerGoogleType.green);
            hotelMarkers.Markers.Add(newMarker);
        }
        private void listViewHotels_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listViewHotels.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewHotels.Sorting == SortOrder.Ascending)
                    listViewHotels.Sorting = SortOrder.Descending;
                else
                    listViewHotels.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewHotels.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            listViewHotels.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewHotels.Sorting);
        }
        private void myMap_OnMarkerClick(GMapMarker sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GMarkerGoogle oldMarker = (GMarkerGoogle)hotelMarkers.Markers.SingleOrDefault(n => n.ToolTipText == null);
                hotelMarkers.Markers.Remove(oldMarker);
                PointLatLng newPos = sender.Position;
                GMarkerGoogle newMarker = new GMarkerGoogle(newPos, GMarkerGoogleType.green);
                hotelMarkers.Markers.Add(newMarker);

                float newLat = (float)newPos.Lat;
                float newLng = (float)newPos.Lng;
                Hotel foundHotel = vacancies.Single(x => x.latitude == newLat && x.longitude == newLng);
                ListViewItem[] sel = listViewHotels.Items.Find(foundHotel.name, false);
                if (sel != null)
                {
                    sel[0].Selected = true;
                }
            }
        }

        private void checkBoxRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRepeat.Checked)
            {
                repeat = true;
                numericUpDownRepeat.Enabled = true;
            }
            else
            {
                repeat = false;
                numericUpDownRepeat.Enabled = false;
            }

        }
        private void checkBoxEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEmail.Checked)
                buttonEmailInfo.Enabled = true;
            else
                buttonEmailInfo.Enabled = false;
        }
        private void buttonEmailInfo_Click(object sender, EventArgs e)
        {
            FormEmail emailForm = new FormEmail(emailInfo);
            emailForm.ShowDialog();
        }

        private string getTimeString(DateTime time)
        {
            decimal hrs = time.Subtract(DateTime.Now).Hours;
            decimal min = time.Subtract(DateTime.Now).Minutes;
            decimal sec = time.Subtract(DateTime.Now).Seconds;
            string timeString = "";

            if (hrs > 0)
                timeString += hrs.ToString() + ":";

            if ((min > 0) | (hrs > 0))
            {
                if (min < 10)
                    timeString += "0";
                timeString += min.ToString() + ":";
            }
            else if ((min == 0) & (hrs > 0))
                timeString += "00:";

            if ((sec > 0) | (min > 0) | (hrs > 0))
            {
                if (sec < 10)
                    timeString += "0";
                timeString += sec.ToString();
            }
            else if (sec == 0)
                timeString += "00";
            return timeString;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            this.Text = "Gen Con Hotel Search - " + getTimeString(endTime);
            if (endTime < DateTime.Now)
            {
                if (repeat)
                {
                    beginTime = DateTime.Now;
                    endTime = beginTime.AddMinutes((double)numericUpDownRepeat.Value);
                    this.Text = "Gen Con Hotel Search - " + getTimeString(endTime.AddSeconds(-1));
                    buttonStart_Click(sender, e);
                }
                else
                {
                    timer1.Stop();
                    this.Text = "Gen Con Hotel Search";
                }
            }


        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            if (myMap.Zoom != myMap.MaxZoom)
            {
                double prevZoom = myMap.Zoom;
                myMap.Zoom = prevZoom + 1;
                trackBarMap.Value = (int)myMap.Zoom - myMap.MinZoom;
            }
        }
        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            if (myMap.Zoom != myMap.MinZoom)
            {
                double prevZoom = myMap.Zoom;
                myMap.Zoom = prevZoom - 1;
                trackBarMap.Value = (int)myMap.Zoom - myMap.MinZoom;
            }
        }
        private void trackBarMap_Scroll(object sender, EventArgs e)
        {
            myMap.Zoom = trackBarMap.Value + myMap.MinZoom;
        }
        private void myMap_MouseWheel(object sender, MouseEventArgs e)
        {
            if ((myMap.Zoom == myMap.MaxZoom) || (myMap.Zoom == myMap.MinZoom))
                return;

            GMapControl thisMap = (GMapControl)sender;
            int width = thisMap.Width;
            int height = thisMap.Height;
            if ((e.X > 0) && (e.Y > 0) && (e.X < width) && (e.Y < height))
            {

                int inc = e.Delta;
                if (inc > 0)
                    trackBarMap.Value = (int)myMap.Zoom - myMap.MinZoom + 1;
                if (inc < 0)
                    trackBarMap.Value = (int)myMap.Zoom - myMap.MinZoom - 1;
            }
        }
        private void comboBoxMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            GMap.NET.MapProviders.GMapProvider something = (GMap.NET.MapProviders.GMapProvider)comboBoxMap.SelectedItem;
            myMap.MapProvider = something;
            myMap.Update();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            else
                this.Activate();
        }
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;
                else
                    this.Activate();
            }
        }
        private void toolStripMenuItemStartSearch_Click(object sender, EventArgs e)
        {
            buttonStart_Click(sender, e);
        }
        private void toolStripMenuItemStopSearch_Click(object sender, EventArgs e)
        {
            buttonStop_Click(sender, e);
        }
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            buttonClose_Click(sender, e);
        }

        private void labelKey_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://www.gencon.com/housing");
            Process.Start(sInfo);
        }

    }

    // Implements the manual sorting of items by columns.
    class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            
            // Determine whether the type being compared is a double.
            try
            {
                // Parse the two objects passed as a parameter as a double.
                double firstNumber = Double.Parse(((ListViewItem)x).SubItems[col].Text);
                double secondNumber = Double.Parse(((ListViewItem)y).SubItems[col].Text);
                // Compare the two numbers.
                returnVal = (int)(firstNumber * 10 - secondNumber * 10); //multiplied by 10 to factor in rounding
            }
            // If neither compared object has a valid double format, compare as a string.
            catch
            {
                if (((ListViewItem)x).SubItems.Count != ((ListViewItem)y).SubItems.Count)
                    return 1;
                // Compare the two items as a string.
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                returnVal *= -1; // Invert the value returned by String.Compare.
            return returnVal;
        }
    }

    #region Json Objects
    public class Hotels
    {
        public List<Hotel> hotels { get; set; }
    }
    [JsonObject]
    public class Hotel : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator GetEnumerator()
        {
            foreach (Hotel item in this)
            {
                yield return item;
            }
        }
        public HotelAddress address { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string childPolicy { get; set; }
        public string rewardsPrograms { get; set; }
        public BlockInfo[] blocks { get; set; }
        public float minAvgRate { get; set; }
        public float maxAvgRate { get; set; }
        public int maxGuests { get; set; }
        public bool childrenAffectRate { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int distanceUnit { get; set; }
        public bool marker { get; set; }
        public int multiPayment { get; set; }
        public float distanceFromEvent { get; set; }
        public string phoneNumber2 { get; set; }
        public string tollFreeNumber { get; set; }
        public string faxNumber { get; set; }
        public string faxNumber2 { get; set; }
        public Amenity[] amenities { get; set; }
        public ExtraInfo accomodationType { get; set; }
        public ExtraInfo starRating { get; set; }
        public bool showAccessible { get; set; }
        public string marketingMessage { get; set; }
        public string messageMap { get; set; }
        public bool hqhotel { get; set; }
        public int[] resAccessDate { get; set; }
        public int[] hotelCloseDate { get; set; }
        public int splitFolio { get; set; }
        public string facebookPage { get; set; }
        public int userDefinedRank { get; set; }
        public bool suppressChildrenCount { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
    public class HotelAddress
    {
        public string state { get; set; }
        public HotelCountry country { get; set; }
        public string city { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string zip { get; set; }
    }
    public class HotelCountry
    {
        public string alpha2Code { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
    public class Amenity
    {
        public string name { get; set; }
        public int id { get; set; }
    }
    public class ExtraInfo
    {
        public bool enable { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
    public class BlockInfo
    {
        public string description { get; set; }
        public int roomTypeId { get; set; }
        public int hotelId { get; set; }
        public float averageRate { get; set; }
        public int maxPersons { get; set; }
        public Inventory[] inventory { get; set; }
        public bool taxesIncluded { get; set; }
        public bool childrenAffectRate { get; set; }
        public float charge { get; set; }
        public float upsellAmount { get; set; }
        public float roomRate2nd { get; set; }
        public float roomRate3rd { get; set; }
        public float roomRate4th { get; set; }
        public float roomRate5thPlus { get; set; }
        public string cancelPolicy { get; set; }
        public bool showSmokingPref { get; set; }
        public string marketingMessage { get; set; }
        public int userDefinedRank { get; set; }
        public float averageBasicRate { get; set; }
        public float rating { get; set; }
        public string taxPolicy { get; set; }
        public int minNumberOfRooms { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }
    public class Inventory
    {
        public int[] date { get; set; }
        public int available { get; set; }
        public int wlAvailable { get; set; }
        public bool hideRate { get; set; }
        public float singleRate { get; set; }
        public float rate { get; set; }
        public int minLengthOfStay { get; set; }
    }
    #endregion

    public class EmailInfo : IEnumerable
    {
        public string smtp { get; set; }
        public string from { get; set; }
        public string pword { get; set; }
        public string to { get; set; }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator GetEnumerator()
        {
            foreach (EmailInfo item in this)
            {
                yield return item;
            }
        }
    }

    public class HotelData : IEnumerable
    {
        public HotelData(string Name, bool Parking, bool Breakfast)
        {
            name = Name;
            parking = Parking;
            breakfast = Breakfast;
        }
        public string name { get; set; }
        public bool parking { get; set; }
        public bool breakfast { get; set; }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator GetEnumerator()
        {
            foreach (HotelData item in this)
            {
                yield return item;
            }
        }
    }
}
