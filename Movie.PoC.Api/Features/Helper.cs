using Movie.PoC.Api.Entities;
using System;

namespace Movie.PoC.Api.Features
{
    public static class Helper
    {
        public static T ParseEnum<T>(string value) where T : struct, Enum
        {
            if (Enum.TryParse<T>(value, true, out T result))
            {
                return result;
            }
            else
            {
                // Handle unrecognized enum values here
                return default; // Default value for TEnum
            }
        }

        public static List<T> ParseEnum<T>(List<string> value) where T : struct, Enum
        {
            var items = new List<T>();
            foreach (var item in value)
            {
                if(Enum.TryParse<T>(item, true, out T result))
                {
                    items.Add(result);
                }
                else { items.Add(default); }
            }

            return items;
        }
    }
}
