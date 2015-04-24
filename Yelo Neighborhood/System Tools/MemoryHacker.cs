using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yelo.Debug;
using System.IO;
using Yelo.Shared;

namespace Yelo.Neighborhood.System_Tools
{
    public partial class MemoryHacker : Form
    {
        public enum ValueTypes
        { 
            Byte,
            Int16,
            Int32,
            Float
        }

        public class MemoryInfoItem
        {
            public uint Offset { get; internal set; }

            public MemoryInfoItem(uint offset)
            { Offset = offset; }
        }

        BindingList<MemoryInfoItem> MemoryInfo = new BindingList<MemoryInfoItem>();

        public MemoryHacker()
        {
            InitializeComponent();
            lstOffsets.DataSource = MemoryInfo;
            cboType.DataSource = Enum.GetValues(typeof(ValueTypes));
        }

        private void cmdBegin_Click(object sender, EventArgs e)
        {
            Enabled = false;
            lstOffsets.DataSource = null;
            
            uint baseAddress = ((ModuleInfo)cboModule.SelectedItem).BaseAddress;
            uint size = ((ModuleInfo)cboModule.SelectedItem).Size;
            
            ValueTypes type = (ValueTypes)cboType.SelectedItem;
            object searchValue = txtValue.Text;

            switch (type)
            {
                case ValueTypes.Byte:
                    searchValue = Convert.ToByte((string)searchValue);
                    break;
                case ValueTypes.Int16:
                    searchValue = Convert.ToInt16((string)searchValue);
                    break;
                case ValueTypes.Int32:
                    searchValue = Convert.ToInt32((string)searchValue);
                    break;
                case ValueTypes.Float:
                    searchValue = Convert.ToSingle((string)searchValue);
                    break;
            }

            const uint BlockSize = 512;
            uint blockCount = size / BlockSize;
            uint offset = 0;
            //for (int i = 0; i < blockCount; i++)
            {
                BinaryReader br = new BinaryReader(new MemoryStream(XBoxIO.XBox.GetMemory(baseAddress + offset, size)));

                //probar.Value = (int)((i * 100) / (blockCount * 100));
                Application.DoEvents();

                while (br.BaseStream.Position < br.BaseStream.Length)//offset < size)
                {
                    probar.Value = (int)(((float)br.BaseStream.Position / (float)br.BaseStream.Length) * 100.0f);

                    switch (type)
                    {
                        case ValueTypes.Byte:
                            {
                                byte data = br.ReadByte();
                                if (data == (byte)searchValue)
                                    MemoryInfo.Add(new MemoryInfoItem(offset));
                                offset += sizeof(byte);
                                break;
                            }
                        case ValueTypes.Int16:
                            {
                                Int16 data = br.ReadInt16();
                                if (data == (Int16)searchValue)
                                    MemoryInfo.Add(new MemoryInfoItem(offset));
                                offset += sizeof(Int16);
                                break;
                            }
                        case ValueTypes.Int32:
                            {
                                Int32 data = br.ReadInt32(); ;
                                if (data == (Int32)searchValue)
                                    MemoryInfo.Add(new MemoryInfoItem(offset));
                                offset += sizeof(Int32);
                                break;
                            }
                        case ValueTypes.Float:
                            {
                                float data = br.ReadSingle();
                                if (data == (float)searchValue)
                                    MemoryInfo.Add(new MemoryInfoItem(offset));
                                offset += sizeof(float);
                                break;
                            }
                    }
                }

                br.Close();

                //WaitForSeconds(10.0f);
            }
            lstOffsets.DataSource = MemoryInfo;
            probar.Value = 0;
            Enabled = true;
        }

        void WaitForSeconds(float seconds)
        {
            TimeSpan start = DateTime.Now.TimeOfDay;
            while (true)
            {
                TimeSpan now = DateTime.Now.TimeOfDay;
                if ((now.Subtract(start)).TotalSeconds < seconds)
                    break;
                Application.DoEvents();
            }
        }

        private void cmdReduceList_Click(object sender, EventArgs e)
        {

        }

        private void MemoryHacker_Shown(object sender, EventArgs e)
        { cboModule.DataSource = XBoxIO.XBox.Modules; }
    }
}
