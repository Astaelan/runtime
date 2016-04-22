﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Globalization;

namespace System
{
    /// <devdoc>
    ///    <para>Provides a type converter to convert Uri objects to and
    ///       from various other representations.</para>
    /// </devdoc>
    public class UriTypeConverter : TypeConverter
    {
        /// <devdoc>
        ///    <para>Gets a value indicating whether this converter can convert an object in the
        ///       given source type to a Uri.</para>
        /// </devdoc>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == null)
                throw new ArgumentNullException(nameof(sourceType));

            return (sourceType == typeof(string) || sourceType == typeof(Uri));
        }

        /// <devdoc>
        ///    <para>Gets a value indicating whether this converter can
        ///       convert an object to the given destination type using the context.</para>
        /// </devdoc>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return (destinationType == typeof(string) || destinationType == typeof(Uri));
        }

        /// <devdoc>
        ///    <para>Converts the given object to the a Uri.</para>
        /// </devdoc>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string uriString = value as string;
            if (uriString != null)
            {
                if (string.IsNullOrEmpty(uriString))
                    return null;

                // Let the Uri constructor throw any informative exceptions
                return new Uri(uriString, UriKind.RelativeOrAbsolute);
            }

            Uri uri = value as Uri;
            if (uri != null)
            {
                return new Uri(uri.OriginalString); // return new instance
            }

            throw GetConvertFromException(value);
        }

        /// <devdoc>
        ///    <para>Converts the given value object to
        ///       the specified destination type using the specified context and arguments.</para>
        /// </devdoc>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            Uri uri = value as Uri;
            if (uri != null)
            {
                if (destinationType == typeof(string))
                    return uri.OriginalString;

                if (destinationType == typeof(Uri))
                    return new Uri(uri.OriginalString, UriKind.RelativeOrAbsolute);
            }

            throw GetConvertToException(value, destinationType);
        }
    }
}
