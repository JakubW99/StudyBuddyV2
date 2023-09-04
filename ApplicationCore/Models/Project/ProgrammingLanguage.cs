using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Project
{
     public class ProgrammingLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProgrammingLanguage( string name)
        {
            Name = name;
            
        }
    }
}
