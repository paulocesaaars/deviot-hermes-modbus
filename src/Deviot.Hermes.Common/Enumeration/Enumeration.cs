using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Deviot.Hermes.Common
{
    public abstract class Enumeration : IComparable
    {
        public ushort Id { get; private set; }

        private bool Available { get; set; }

        public string Name { get; private set; }

        protected Enumeration(ushort id, bool available, string name)
        {
            Id = id;
            Available = available;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);


            return fields.Select(f => f.GetValue(null)).Cast<T>().Where(f => f.Available == true);
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Name.Equals(otherValue.Name);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Name.CompareTo(((Enumeration)other).Name);
    }
}
