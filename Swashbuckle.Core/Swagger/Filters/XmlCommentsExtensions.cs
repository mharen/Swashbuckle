﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swashbuckle.Swagger.Filters
{
    public static class XmlCommentsExtensions
    {
        public static string XmlCommentsQualifier(this Type type)
        {
            var builder = new StringBuilder(type.FullNameSansTypeParameters());
            builder.Replace("+", ".");

            if (type.IsGenericType)
            {
                var typeParameterQualifiers = type.GetGenericArguments()
                    .Select(t => t.XmlCommentsQualifier())
                    .ToArray();

                builder
                    .Replace(String.Format("`{0}", typeParameterQualifiers.Count()), String.Empty)
                    .Append(String.Format("{{{0}}}", String.Join(",", typeParameterQualifiers).TrimEnd(',')))
                    .ToString();
            }

            return builder.ToString();
        }

        public static string FullNameSansTypeParameters(this Type type)
        {
            var fullName = type.FullName;
            var chopIndex = fullName.IndexOf("[[");
            return (chopIndex == -1) ? fullName : fullName.Substring(0, chopIndex);
        }
    }
}