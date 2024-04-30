using Movie.PoC.Api.Entities;
using System;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt.ClassInstances;
using LanguageExt.Common;

namespace Movie.PoC.Api.Features
{
    public static class Helper
    {
        public static T ParseEnum<T>(string value) where T : struct, Enum
        {
            if (Enum.TryParse(value, true, out T result))
            {
                return result;
            }
            else
            {
                return default; // Default value for TEnum
            }
        }

        public static List<T> ParseEnum<T>(List<string> value) where T : struct, Enum
        {
            var items = new List<T>();
            foreach (var item in value)
            {
                if(Enum.TryParse(item, true, out T result))
                {
                    items.Add(result);
                }
            }

            return items;
        }
    }
}
