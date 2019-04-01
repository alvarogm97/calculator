using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCalculator.Models
{

    public class Journal
    {
        [Key]
        public int id { get; set; }  

        // Look for a certain id
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

        // Accumulates the operation 
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
