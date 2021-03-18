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
            //Less/Equal:
            if (isMature) {
                //valid years
                date[2] = Xu.Next(19, 100);
                //limit year: 2003 = 2021 - (19 - 1)
                if (date[2] == 19) {
                    //month upper-bounded
                    date[1] = Xu.Next(1, DateTime.Now.Month);
                    if (date[1] == DateTime.Now.Month) {
                        //day upper-bounded
                        date[0] = Xu.Next(1, DateTime.Now.Day);
                        return date;
                    }
                }
                else//free
                    date[1] = Xu.Next(1, 12);
                date[0] = Xu.Next(1, DateTime.DaysInMonth(
                    (DateTime.Now.Year - date[2] + 1),
                    date[1]
                ));
                return date;
            }
            //Greater:
            else {
                date[2] = Xu.Next(1, 19);
                if (date[2] == 19) {
                    //month lower-bounded
                    date[1] = Xu.Next((DateTime.Now.Month + (((DateTime.Now.Month < 12)) ? 1 : 0)), 12);
                    //special case: December
                    if (date[1] == DateTime.Now.Month) {
                        if ((DateTime.Now.Day + 1) < DateTime.DaysInMonth(
                            (DateTime.Now.Year - date[2] + 1), date[1]
                        )) {//day lower-bounded
                            date[0] = Xu.Next(
                                (DateTime.Now.Day + 1),
                                DateTime.DaysInMonth((DateTime.Now.Year - date[2] + 1), date[1])
                            );
                            return date;
                        }
                    }
                }
                else//free
                    date[1] = Xu.Next(1, 12);
                date[0] = Xu.Next(1, DateTime.DaysInMonth((DateTime.Now.Year - date[2] + 1), date[1]));
                return date;
            }
        }
        public static void MakeInvalidMonth(ref int[] date)
        {
            //No. days in chosen month
            int days = DateTime.DaysInMonth(
                (DateTime.Now.Year - date[2] + 1),
                date[1]
            );
            if (days == 31)//pick month with less days
                date[1] = MONTHS_LESS_THAN_31[Xu.Next(MONTHS_LESS_THAN_31.Length - 1)];
            days = DateTime.DaysInMonth((DateTime.Now.Year - date[2] + 1), date[1]);
            //ensure the day larger than month's days
            date[0] = days + 1 + (date[0] % (31 - days));
        }
    }
}