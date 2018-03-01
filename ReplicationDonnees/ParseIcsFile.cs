﻿using System;
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
        public static List<IEvent> listeInsertion = new List<IEvent>();
		public static List<IEvent> listeSuppression = new List<IEvent>();
		public static List<IEvent> listeModification = new List<IEvent>();

        public static List<IEvent> parseics(String path)
        {
            
            // Load the calendar file
            try
            {
                IICalendarCollection calendars = iCalendar.LoadFromFile(@path);
                List<IDateTime> listdatetime = new List<IDateTime>();
                List<DDay.iCal.IEvent> listevent = new List<DDay.iCal.IEvent>();
                //
                // Get all events that occur in Year.
                //
                IList<Occurrence> occurrences = calendars.GetOccurrences(new DateTime(2017, 1, 1), new DateTime(2018, 3, 26));

                //Console.WriteLine(Environment.NewLine + " Events:");
                int i = 0;
                foreach (Occurrence occurrence in occurrences)
                {

                    DateTime occurrenceTime = occurrence.Period.StartTime.Local.AddHours(1);
                    IRecurringComponent rc = occurrence.Source as IRecurringComponent;
                    if (rc != null)
                    {
                        listevent.Add(rc.Calendar.Events[i]);
                        i++;
                    }
                }
                return listevent;

            }
            catch (Exception e) { 
                    Console.WriteLine("ERREUR : " + e.Message);
                    return null;
            }



        }
        public static void compareEvents(List<IEvent> listeDeBase, List<IEvent> listeMAJ)
        {



            // Element à insérer
            foreach (IEvent e2 in listeMAJ)
            {
                if (!listeDeBase.Contains(e2))
                {
                    listeInsertion.Add(e2);
                }

            }

            // Element à supprimer
            foreach (IEvent e1 in listeDeBase)
            {
                if (!listeMAJ.Contains(e1))
                {
                    listeSuppression.Add(e1);
                }

            }
            // Element à modifier
            foreach (IEvent e1 in listeDeBase)
            {
                foreach (IEvent e2 in listeMAJ)
                {
                    if (e1.UID == e2.UID && e2.LastModified.GreaterThan(e1.LastModified) && !listeModification.Contains(e2))
                    {
                        listeModification.Add(e2);
                    }
                }
            }
        }





    }
}
