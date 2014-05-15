using System;

namespace SimpleXmlSerializer.Utils
{
    public static class Preconditions
    {
         public static void NotNull<T>(T value, string parameterName) where T: class
         {
             if (value == null)
             {
                 throw new ArgumentNullException(parameterName);
             }
         }

         public static void NotEmpty(string value, string parameterName)
         {
             if (string.IsNullOrWhiteSpace(value))
             {
                 throw new ArgumentException("Value can not be empty.", parameterName);
             }
         }
    }
}