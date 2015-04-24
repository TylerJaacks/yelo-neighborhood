using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;

namespace Yelo.Carnage
{
    public static class Server
    {
        static Uri RemnantMods = new Uri("http://www.remnantmods.com/files/xzodia/");
        static Uri MapListRequest = new Uri(RemnantMods, "MapList.php");

        public static BindingList<MapInfo> MapList { get { return _mapList; } }
        static BindingList<MapInfo> _mapList = new BindingList<MapInfo>();

        public static void UpdateMapList(CheckedListBox.CheckedItemCollection tags, CheckedListBox.CheckedItemCollection gametypes, CheckedListBox.CheckedItemCollection basemaps)
        {
            using(WebClient request = new WebClient())
            {
                request.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                request.UploadString(MapListRequest, "");
                request.DownloadString(MapListRequest);
            }
        }
    }
}
