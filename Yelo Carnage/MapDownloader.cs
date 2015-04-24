using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yelo.Carnage
{
    public partial class MapDownloader : Form
    {
        BindingSource MapList = new BindingSource();

        public MapDownloader()
        {
            InitializeComponent();
            FillFilterLists();

            MapList.DataSource = Server.MapList;
            gridMaps.DataSource = MapList;

            Server.MapList.Add(new MapInfo());
            Server.MapList.Add(new MapInfo());
            Server.MapList.Add(new MapInfo());
        }

        public void UpdateConnectionInfo()
        { Text = Program.DebugConnectionInfo + " - Map Downloader v" + Program.Version; }

        private void MapDownloader_Shown(object sender, EventArgs e)
        { UpdateConnectionInfo(); }

        private void Filter_MouseUp(object sender, MouseEventArgs e)
        { Server.UpdateMapList(lstTags.CheckedItems, lstSupportedGametypes.CheckedItems, lstBaseMap.CheckedItems); }

        void EnumList_Format(object sender, ListControlConvertEventArgs e)
        { e.Value = e.Value.ToString().Replace('_', ' '); }

        private void FillFilterLists()
        {
            lstTags.FormattingEnabled = true;
            lstSupportedGametypes.FormattingEnabled = true;
            lstBaseMap.FormattingEnabled = true;

            lstTags.Format += new ListControlConvertEventHandler(EnumList_Format);
            lstSupportedGametypes.Format += new ListControlConvertEventHandler(EnumList_Format);
            lstBaseMap.Format += new ListControlConvertEventHandler(EnumList_Format);

            foreach (object o in Enum.GetValues(typeof(MapFilterTags)))
                lstTags.Items.Add(o);
            foreach (object o in Enum.GetValues(typeof(MapFilterGametypes)))
                lstSupportedGametypes.Items.Add(o);
            foreach (object o in Enum.GetValues(typeof(MapFilterBaseMap)))
                lstBaseMap.Items.Add(o);
        }
    }
}
