using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DDay.iCal;
using DDay.Collections;
namespace ReplicationDonnees
{
    class ParseIcsFile
    {
        public static List<IDateTime> parseics(String path)
        {
            // Load the calendar file
            IICalendarCollection calendars = iCalendar.LoadFromFile(@path);
            List<IDateTime> listdatetime = new List<IDateTime>();
            //
            // Get all events that occur in Year.
            //
            IList<Occurrence> occurrences = calendars.GetOccurrences(new DateTime(2017, 1, 1), new DateTime(2018, 3, 26));

            Console.WriteLine(Environment.NewLine + " Events:" );

            foreach (Occurrence occurrence in occurrences)
            {
                DateTime occurrenceTime = occurrence.Period.StartTime.Local.AddHours(1);
                IRecurringComponent rc = occurrence.Source as IRecurringComponent;
                if (rc != null)
                {
                    Console.WriteLine(rc.Summary + ": " + occurrenceTime.ToString() + "  Modified on : " + rc.LastModified.AddHours(1) + "   Last Synch :" + rc.DTStamp.AddHours(1) + "END DATE : " );
                    listdatetime.Add(rc.DTStamp.AddHours(1));
                }
            }

            return listdatetime;
        }
        public static void detectmodified(String path,List<IDateTime> ilist)
        {
            IICalendarCollection calendars = iCalendar.LoadFromFile(@path);
           
            //
            // Get all events that occur in Year.
            //
            IList<Occurrence> occurrences = calendars.GetOccurrences(new DateTime(2017, 1, 1), new DateTime(2018, 3, 26));

            Console.WriteLine(Environment.NewLine + " Events:");

            foreach (Occurrence occurrence in occurrences)
            {
                DateTime occurrenceTime = occurrence.Period.StartTime.Local.AddHours(1);
                IRecurringComponent rc = occurrence.Source as IRecurringComponent;
                if (rc != null)
                {
                    if(!ilist[0].Equals(rc.DTStamp.AddHours(1)))
                    Console.WriteLine(rc.Summary + ": " + occurrenceTime.ToString() + "  Modified on : " + rc.LastModified.AddHours(1) + "   Last Synch :" + rc.DTStamp.AddHours(1));
                  
                }
            }

        }
    }
}
