using System;

namespace AgeVerification_and_AboutUs.WebPages.Util 
{  
    [Flags]
    public enum Birthday { 
        Day = 1, Month = 2, InvalidMonth = 4, Year = 8
    }


    public static class BirthDateGenerator
    {
        private static readonly int[] MONTHS_LESS_THAN_31 = { 2, 4, 6, 9, 11 };
        /**
         * to generate random dates
         */
        private static Random Xu;

        static BirthDateGenerator(){
            Xu = new Random(DateTime.Now.Millisecond);
        }

        /**
         * output: <day, month, year> indices
         */
        public static int[] MakeMature(bool isMature)
        {
            int[] date = new int[3];
            if (isMature)
            {
                date[2] = Xu.Next(17, 100);
                if (date[2] == 17)
                {
                    date[1] = Xu.Next(1, DateTime.Now.Month);
                    if (date[1] == DateTime.Now.Month)
                        date[0] = Xu.Next(1, DateTime.Now.Day);
                }
                else
                    date[1] = Xu.Next(1, 12);
                date[0] = Xu.Next(1, DateTime.DaysInMonth(
                    (DateTime.Now.Year - date[2]),
                    date[1]
                ));
                return date;
            }
            else
            {//83 = 100 -(18 - 1)
                date[2] = Xu.Next(1, 83);
                if (date[2] == 83)
                {
                    date[1] = Xu.Next(DateTime.Now.Month, 12);
                    if (date[1] == DateTime.Now.Month)
                    {
                        if ((DateTime.Now.Day + 1) < DateTime.DaysInMonth(
                            (DateTime.Now.Year - date[2]), date[1]
                        ))
                            date[0] = Xu.Next(
                                (DateTime.Now.Day + 1),
                                DateTime.DaysInMonth((DateTime.Now.Year - date[2]), date[1])
                            );
                        else
                            date[0] = DateTime.DaysInMonth(
                                (DateTime.Now.Year - date[2]),
                                date[1]
                            );
                    }
                    else
                        date[0] = Xu.Next(1, DateTime.DaysInMonth(
                            (DateTime.Now.Year - date[2]),
                            date[1]
                        ));
                    return date;
                }
                date[1] = Xu.Next(1, 12);
                date[2] = Xu.Next(1, DateTime.DaysInMonth((DateTime.Now.Year - date[2]), date[1]));
                return date;
            }
        }
        public static void MakeInvalidMonth(ref int[] date)
        {
            int days = DateTime.DaysInMonth(
                (DateTime.Now.Year + date[2]),
                date[1]
            );
            if (days == 31)
                date[1] = MONTHS_LESS_THAN_31[Xu.Next(MONTHS_LESS_THAN_31.Length - 1)];
            days = DateTime.DaysInMonth((DateTime.Now.Year - date[2]), date[1]);
            date[0] = days + (date[0] % (31 - days - 1));
        }
    }
}