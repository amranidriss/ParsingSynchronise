using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DDay.iCal;
using DDay.Collections;
namespace ReplicationDonnees
{
    class Program
    {
        static void Main(string[] args)
        {
            String sUrl ="https://planning.univ-lorraine.fr/jsp/custom/modules/plannings/anonymous_cal.jsp?resources=94694,94695,137604,137605,143847,161667,131172,28064,180436,180437,187172,187173,188343,189790,190400,191558,195038,195039,195267,196054,196183,196296,206014,206831,207691,223135,223136,224368,229624,231294,234516&projectId=5&calType=ical&nbWeeks=16";
            String path = "ade.ics";
           
          List<IDateTime> ilist = ParseIcsFile.parseics(path);
        
           Download.copyURLToFile(sUrl);
            ParseIcsFile.detectmodified(path,ilist);
          Console.Read();
        }
    }
}
