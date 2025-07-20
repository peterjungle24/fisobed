using UnityEngine;
using System;
using System.IO;

namespace help
{
    public class files
    {
        /// <summary>
        /// Get the image file for use and load it
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetProperElementName(string elementName)
        {
            if (elementName == null)
            {
                throw new ArgumentNullException("Element name cannot be null");
            }

            if (Futile.atlasManager.DoesContainElementWithName(elementName))
            {
                return elementName; 
            }

            //Split path by segments
            string[] pathSegments = elementName.Split(new char[]
            {
                System.IO.Path.DirectorySeparatorChar,
                System.IO.Path.AltDirectorySeparatorChar
            });

            if (pathSegments.Length <= 1)
            {
                Debug.LogWarning("No path segments found. Original element cannot be found");
                return elementName;
            }

            char separator = elementName[pathSegments[0].Length];

            char alternateSeparator;
            string elementNameToCheck;
            if (pathSegments.Length == 2) //Easiest, and most common situation
            {
                //Trim data to check for empty strings
                pathSegments[0] = pathSegments[0].Trim();
                pathSegments[1] = pathSegments[1].Trim();

                if (pathSegments[0] == string.Empty)
                    return pathSegments[1]; //Strip, and hope it is correct
                else if (pathSegments[1] == string.Empty)
                    return pathSegments[0];

                //We have an actual path to work with
                alternateSeparator = getAlternateSeparator(separator);
                elementNameToCheck = pathSegments[0] + alternateSeparator + pathSegments[1];
            }
            else
            {
                //We have an actual path to work with
                alternateSeparator = getAlternateSeparator(separator);

                //Replaces all separators with the alternate separator
                elementNameToCheck = string.Empty;
                foreach (string pathSegment in pathSegments)
                {
                    elementNameToCheck += pathSegment + alternateSeparator;
                    elementNameToCheck = elementNameToCheck.Trim(alternateSeparator);
                }
            }

            if (Futile.atlasManager.DoesContainElementWithName(elementNameToCheck))
            {
                return elementNameToCheck;
            }

            Debug.LogWarning("Original element could not be found");
            return elementName;

            char getAlternateSeparator(char separator)
            {

                return separator == System.IO.Path.DirectorySeparatorChar ? System.IO.Path.AltDirectorySeparatorChar : System.IO.Path.DirectorySeparatorChar;
            }
        }
    }
}