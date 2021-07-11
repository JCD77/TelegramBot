using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Collections;

namespace TelegramBot
{
    public class PowerShellHelper
    {
        public string Execute(string command)
        {
            string aux = "";
            using (var ps = PowerShell.Create())
            {
              
                
                var results = ps.AddScript(command).Invoke();
                foreach (var result in results)
                {
                    aux+="\n" + result.ToString();
                    Console.Write(result.ToString());
                }


            }
            return aux;
        }
    }
    }