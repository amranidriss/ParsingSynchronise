﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
namespace ReplicationDonnees
{
    class Download
    {
        public static void copyURLToFile(string url,String pathname)
        {

                using (var client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(url, pathname);

                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                    
                }

        }

    }
}
