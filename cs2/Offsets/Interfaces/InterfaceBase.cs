using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal abstract class InterfaceBase
    {
        public InterfaceBase(string name)
        {
            this.Name = name;

            if (string.IsNullOrEmpty(Name))
                Name = this.GetType().Name;
        }

        public void ParseInterface(string data)
        {
            int startIndex = data.IndexOf("class " + Name);
            if (startIndex == -1)
                throw new FormatException($"{Name} not found.");
            foreach(FieldInfo field in this.GetType().GetFields())
            {
                string fieldName = field.Name;
                int valueStartIndex = data.IndexOf(fieldName + " = ", startIndex);
                if (valueStartIndex == -1)
                {
                    Program.Log($"{Name}.{field.Name} not found.", ConsoleColor.Yellow);
                    continue;
                }
                valueStartIndex += fieldName.Length + 3; // " = "
                int valueEndIndex = data.IndexOf(";", valueStartIndex);
                int lenght = valueEndIndex - valueStartIndex;
                string value = data.Substring(valueStartIndex, lenght).Substring(2);
                field.SetValue(this, IntPtr.Parse(value, System.Globalization.NumberStyles.HexNumber));
            }
        }

        public string Name
        {
            get; private set;
        }
    }
}
