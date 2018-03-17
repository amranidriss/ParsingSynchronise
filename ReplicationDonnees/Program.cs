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
using System.Threading;
using System.IO;
namespace ReplicationDonnees
{
    class Program
    {
        static void Main(string[] args)
        {
            String sUrl = "https://planification.univ-lorraine.fr/jsp/custom/modules/plannings/anonymous_cal.jsp?resources=5424&projectId=5&calType=ical&firstDate=2018-01-29&lastDate=2018-02-04";
           
        

            Download.copyURLToFile(sUrl,"ade.ics");
            List<IEvent> ilist = ParseIcsFile.parseics("ade.ics");
            if (!System.IO.File.Exists("tmp.ics"))
            {
                try
                {
                    using (FileStream s = File.Create("tmp.ics"))
                    {
                        string content = File.ReadAllText("ade.ics");
                        s.Close();
                        File.WriteAllText("tmp.ics", content);

                    }

                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {

                List<IEvent> ilist1 = ParseIcsFile.parseics("tmp.ics");



                //Detecter les evenements qui ont été modifiés la derniere fois(Supprimer- Inserer - Modifier)
                ParseIcsFile.compareEvents(ilist, ilist1);

                Console.WriteLine("Element supprimé par rapport à la derniere synchronisation: ");
                foreach (IEvent e in ParseIcsFile.listeSuppression)
                {
                    Console.WriteLine(e.Summary);
                }

                Console.WriteLine("\nElement inséré par rapport à la derniere synchronisation:  ");
                foreach (IEvent e in ParseIcsFile.listeInsertion)
                {
                    Console.WriteLine(e.Summary);
                }

                Console.WriteLine("\nElement modif par rapport à la derniere synchronisation:  ");
                foreach (IEvent e in ParseIcsFile.listeModification)
                {
                    Console.WriteLine(e.Summary);
                }

                FileStream s = File.Create("tmp.ics");
                    
                        string content = File.ReadAllText("ade.ics");
                        s.Close();
                        File.WriteAllText("tmp.ics", content);

            }

                //ADD GOOGLE API AND ADD TEST EVENT
                string[] Scopes = { CalendarService.Scope.Calendar };
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

                // TEST  ONE EVENT 
                /* Google.Apis.Calendar.v3.Data.Event newEvent1 = new Google.Apis.Calendar.v3.Data.Event();
                 newEvent1.Summary = "Summary";
                 newEvent1.Description = "New EVent Test";
                 newEvent1.Start = new EventDateTime();
                 newEvent1.Start.DateTime = new DateTime(2018, 5, 16, 15, 13, 13, 12);

                 newEvent1.End = new EventDateTime();
                 newEvent1.End.DateTime = new DateTime(2018, 7, 16, 15, 13, 13, 12);
                 service.Events.Insert(newEvent1, "primary").Execute();*/
                  

               var calendars = iCalendar.LoadFromFile("tmp.ics");
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

                            DateTime o = new DateTime(rc.Calendar.Events[i].Start.Year, rc.Calendar.Events[i].Start.Month,
                                rc.Calendar.Events[i].Start.Day, rc.Calendar.Events[i].Start.Hour+1, rc.Calendar.Events[i].Start.Minute,
                                rc.Calendar.Events[i].Start.Second);
                            newEvent.Start = new EventDateTime();
                            newEvent.Start.DateTime = o;
                            o = new DateTime(rc.Calendar.Events[i].DTEnd.Year, rc.Calendar.Events[i].DTEnd.Month,
                                   rc.Calendar.Events[i].DTEnd.Day, rc.Calendar.Events[i].DTEnd.Hour+1, rc.Calendar.Events[i].DTEnd.Minute,
                                   rc.Calendar.Events[i].DTEnd.Second);
                            newEvent.End = new EventDateTime();
                            newEvent.End.DateTime = o;
                            newEvent.Description = rc.Calendar.Events[i].Description;
                            newEvent.Location = rc.Calendar.Events[i].Location;

                            if (!CalendarHelper.IsUniqueId(service, newEvent))
                            {
                                service.Events.Insert(newEvent, "primary").Execute();
                                Console.WriteLine(rc.Calendar.Events[i].Summary);
                            }
                            i++;

                        
                    }
                }
            
                Console.Read();

            }
              }
    }

