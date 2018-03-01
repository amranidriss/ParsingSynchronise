using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDay.iCal;
using DDay.Collections;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ReplicationDonnees
{
    class Program
    {
        static void Main(string[] args)
        {
            String sUrl ="https://planning.univ-lorraine.fr/jsp/custom/modules/plannings/anonymous_cal.jsp?resources=94694,94695,137604,137605,143847,161667,131172,28064,180436,180437,187172,187173,188343,189790,190400,191558,195038,195039,195267,196054,196183,196296,206014,206831,207691,223135,223136,224368,229624,231294,234516&projectId=5&calType=ical&nbWeeks=16";
           
           
         // List<IDateTime> ilist = ParseIcsFile.parseics(path);
        
         //  Download.copyURLToFile(sUrl);
          //  ParseIcsFile.detectmodified(path,ilist);
       
       
        

            //ADD GOOGLE API AND ADD TEST EVENT
       /*string[] Scopes = { CalendarService.Scope.Calendar };
         string ApplicationName = "Google Calendar API .NET Quickstart";

       
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
          /*  Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }*/

            //ADD TEST EVENT 
           /* Google.Apis.Calendar.v3.Data.Event newEvent = new Google.Apis.Calendar.v3.Data.Event();
            newEvent.Summary = "Summary";
            newEvent.Description = "New EVent Test";
            newEvent.Start = new EventDateTime();
            newEvent.Start.DateTime = new DateTime(2018, 5, 16, 15, 13, 13, 12);

            newEvent.End = new EventDateTime();
            newEvent.End.DateTime = new DateTime(2018, 7, 16, 15, 13, 13, 12);
            service.Events.Insert(newEvent, "primary").Execute();
             */

        /* var calendars = iCalendar.LoadFromFile(@path);
           IList<Occurrence> occurrences = calendars.GetOccurrences(new DateTime(2017, 1, 1), new DateTime(2018, 3, 26));

           int i = 0;
           foreach (Occurrence occurrence in occurrences)
           {
               DateTime occurrenceTime = occurrence.Period.StartTime.Local.AddHours(1);
               IRecurringComponent rc = occurrence.Source as IRecurringComponent;
               if (rc != null)
               {
                   Google.Apis.Calendar.v3.Data.Event newEvent = new Google.Apis.Calendar.v3.Data.Event();
                   newEvent.Summary = rc.Calendar.Events[i].Summary;
                      
                      DateTime o= new DateTime(rc.Calendar.Events[i].Start.Year,rc.Calendar.Events[i].Start.Month,
                          rc.Calendar.Events[i].Start.Day,rc.Calendar.Events[i].Start.Hour,rc.Calendar.Events[i].Start.Minute,
                          rc.Calendar.Events[i].Start.Second);
                      newEvent.Start = new EventDateTime();
                      newEvent.Start.DateTime=o ;
                      o = new DateTime(rc.Calendar.Events[i].DTEnd.Year, rc.Calendar.Events[i].DTEnd.Month,
                             rc.Calendar.Events[i].DTEnd.Day, rc.Calendar.Events[i].DTEnd.Hour, rc.Calendar.Events[i].DTEnd.Minute,
                             rc.Calendar.Events[i].DTEnd.Second);
                      newEvent.End = new EventDateTime();
                   newEvent.End.DateTime = o;
                   newEvent.Description = rc.Calendar.Events[i].Description;
                   newEvent.Location = rc.Calendar.Events[i].Location;
                   service.Events.Insert(newEvent, "primary").Execute();
                   Console.WriteLine(rc.Calendar.Events[i].DTEnd + rc.Calendar.Events[i].Summary);
                   i++;
               }
           }*/


            //Download.copyURLToFile(sUrl,"ade.ics");
            //Download.copyURLToFile(sUrl, "tmp.ics");
            List<IEvent> ilist = ParseIcsFile.parseics("ade.ics");
            List<IEvent> ilist1 = ParseIcsFile.parseics("tmp.ics");


            ParseIcsFile.compareEvents(ilist, ilist1);

            Console.WriteLine("Element supprimé : ");
            foreach (IEvent e in ParseIcsFile.listeSuppression)
            {
                Console.WriteLine(e.Summary);
            }

            Console.WriteLine("\nElement inséré : ");
            foreach (IEvent e in ParseIcsFile.listeInsertion)
            {
                Console.WriteLine(e.Summary);
            }

            Console.WriteLine("\nElement modif : ");
            foreach (IEvent e in ParseIcsFile.listeModification)
            {
                Console.WriteLine(e.Summary);
            }
          
            Console.Read();


        }
    }
}
