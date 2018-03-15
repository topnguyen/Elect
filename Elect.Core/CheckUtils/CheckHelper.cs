﻿using System;

namespace Elect.Core.CheckUtils
{
    public static class CheckHelper
    {
        /// <exception cref="ArgumentException"></exception>
        public static void CheckNullOrWhiteSpace(string propertyValue, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyValue))
            {
                throw new ArgumentException($"{propertyName} cannot be null or empty or whitespace", propertyName);
            }
        }
    }
}