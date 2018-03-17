using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using DDay.iCal;
namespace ReplicationDonnees
{
    static class CalendarHelper
    {
        public static bool IsUniqueId(CalendarService service,Google.Apis.Calendar.v3.Data.Event newevent)
        {
            EventsResource.ListRequest request = service.Events.List("primary");

            
            // List events.
              Events events = request.Execute();
         
              if (events.Items != null)
              {
                  foreach (Google.Apis.Calendar.v3.Data.Event eventItem in events.Items)
                  {
                      if (newevent.Description.Equals(eventItem.Description) 
                          && newevent.Summary.Equals(eventItem.Summary) 
                          && newevent.Start.DateTime.Equals(eventItem.Start.DateTime)
                          && newevent.End.DateTime.Equals(eventItem.End.DateTime)) return true;
                  }
              }
              return false;

        }
    }
}
