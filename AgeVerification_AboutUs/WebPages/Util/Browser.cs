using System;
using System.Text.RegularExpressions;

namespace AgeVerification_and_AboutUs.WebPages.Util {
    public enum Browser { 
        IE = 1, Firefox = 2, Edge = 3, Chrome = 4
    }

    public static class BrowserExtensions
    {
        public static Browser ToBrowser(this string bString) {
            if (Regex.IsMatch(bString.Trim(), @"(?i:chrome)"))
                return Browser.Chrome;
            if (Regex.IsMatch(bString.Trim(), @"(?i:ie)"))
                return Browser.IE;
            if (Regex.IsMatch(bString.Trim(), @"(?i:edge)"))
                return Browser.Edge;
            if (Regex.IsMatch(bString.Trim(), @"(?i:firefox)"))
                return Browser.Firefox;
            throw (new Exception("Browser name '"+bString+"' is unknown"));
        }
    }
}