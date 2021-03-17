using System;
using System.Text.RegularExpressions;

namespace AgeVerification_and_AboutUs.WebPages.Util {
    public enum Browser { 
        Firefox = 1, Edge = 2, Chrome = 3
    }

    public static class BrowserExtensions
    {
        public static Browser ToBrowser(this string bString) {
            if (Regex.IsMatch(bString.Trim(), @"(?i:chrome)"))
                return Browser.Chrome;
            if (Regex.IsMatch(bString.Trim(), @"(?i:edge)"))
                return Browser.Edge;
            if (Regex.IsMatch(bString.Trim(), @"(?i:firefox)"))
                return Browser.Firefox;
            throw (new Exception("Browser name '"+bString+"' is unknown"));
        }
    }
}