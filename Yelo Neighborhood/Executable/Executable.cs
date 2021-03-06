using System;
using System.Collections.Generic;

namespace Yelo.Neighborhood
{
    public partial class Executable : IEqualityComparer<Executable>
    {
        public string Name { get; set; }
        public string Filename { get; set; }

        public List<Script> Scripts { get { return _scripts; } }
        List<Script> _scripts = new List<Script>();

        public override string ToString()
        { return Name; }

        #region IEqualityComparer<Executable> Members
        public bool Equals(Executable x, Executable y)
        { return x.Filename == y.Filename; }

        public int GetHashCode(Executable obj)
		{ return obj.Filename.GetHashCode(); }
        #endregion
    };
}