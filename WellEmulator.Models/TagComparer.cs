using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    class TagComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            if (x.Id == y.Id) return true;
            return x.Name.Equals(y.Name) && x.WellName.Equals(y.WellName);
        }

        public int GetHashCode(Tag obj)
        {
            if (ReferenceEquals(obj, null)) return 0;
            int name = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int id = obj.Id.GetHashCode();
            return name ^ id;
        }
    }
}
