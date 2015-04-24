using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Yelo.Carnage
{
    public class MapInfo : INotifyPropertyChanged
    {
        [Browsable(false)]
        public int ID { get; private set; }

        [Browsable(false)]
        public bool IsDownloaded { get; internal set; }

        [Browsable(false)]
        public bool IsOnXBox { get; internal set; }

        public string Name { get; private set; }

        public bool PC { get; set; }
        public bool XBox
        {
            get { return xbox; }
            set
            {
                if (value)
                {
                    PC = true;
                    NotifyPropertyChanged("PC");
                }
                xbox = value;
            } 
        }
        bool xbox;

        //string Description;
        //Image Preview;

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
