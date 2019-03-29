using CalculatorService.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVCalculator.Models
{

    public class Journal
    {
        [Key]
        public int id { get; set; }  

        public List<string> query(int id)
        {
            List<string> values = new List<string>();
            
            if (Program.Journal.journal.ContainsKey(id))
            {
                foreach(string calc in Program.Journal.journal[id])
                {
                    values.Add(calc);
                } 
            }

            return values;
        }

        public void store(int id, string query)
        {
            List<string> aux = new List<string>();
            aux.Add(query);

            if (Program.Journal.journal.ContainsKey(id))
            {
                Program.Journal.journal[id].Add(query);
            }
            else
            {
                Program.Journal.journal.Add(id, aux);
            }
         
    
        }
    }
}
