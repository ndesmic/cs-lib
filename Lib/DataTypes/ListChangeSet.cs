using System.Collections.Generic;

namespace Lib.DataTypes
{
    public class ListChangeSet<T>
    {
        public ListChangeSet()
        {
            Added = new List<T>();
            Removed = new List<T>();
            Same = new List<T>();
        }
        public List<T> Added { get; set; }
        public List<T> Removed { get; set; }
        public List<T> Same { get; set; }
    }
}
