using System;

namespace AgeVerification_and_AboutUs.WebPages.Util {  
    [Flags]
    public enum Birthday { 
        Day = 1, Month = 2, InvalidMonth = 4, Year = 8
    }
}