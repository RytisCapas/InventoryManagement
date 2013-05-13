using System;
using System.Collections.Generic;
using System.Linq;

using WarehouseInventoryManagement.DataEntities.Enums.Extensions;

namespace WarehouseInventoryManagement.Tools.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            var type = value.GetType();

            var fieldInfo = type.GetField(value.ToString());

            var attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attribs != null && attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public static List<T> GetAllCombinations<T>(this T flagsEnum, bool sumPermissions = true)
        {
            var attributes = flagsEnum.GetType().GetCustomAttributes(typeof(FlagsAttribute), false);

            if (!attributes.Any())
            {
                throw new ArgumentOutOfRangeException("flagsEnum", "Must have [Flags] attribute");
            }

            var values = Enum.GetValues(typeof(T)).Cast<int>().ToArray();
            var valuesInverted = values.Select(v => ~v).ToArray();
            var result = new List<T>();

            var max = 0;

            for (var i = 0; i < values.Length; i++)
            {
                max |= values[i];
            }

            for (var i = 0; i <= max; i++)
            {
                var unaccountedBits = i;

                for (int j = 0; j < valuesInverted.Length; j++)
                {
                    unaccountedBits &= valuesInverted[j];

                    if (unaccountedBits == 0)
                    {
                        if (sumPermissions)
                        {
                            if (i >= (int)(object)flagsEnum)
                            {
                                result.Add((T)(object)i);
                            }
                        }
                        else
                        {
                            if ((i & (int)(object)flagsEnum) != 0)
                            {
                                result.Add((T)(object)i);
                            }
                        }

                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(Enum.GetName(typeof(T), (T)(object)0)))
            {
                result.Remove((T)(object)0);
            }

            return result;
        }
    }
}
