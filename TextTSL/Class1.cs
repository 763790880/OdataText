using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextTSL
{
    public static class Class1
    {
        public static T GetDescriptionToValue<T>(this string value) where T : Enum
        {
                return Enum.GetValues(typeof(T))
                             .Cast<T>()
                             .FirstOrDefault(v => v.GetDescription() == value);


        }
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return "";
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)?
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
    public enum TypeA
    {
        [Description("集中采购")]
        No=2,
    }
}
