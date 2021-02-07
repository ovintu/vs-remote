using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsRemote.Models
{
    public class VsSolution
    {
        public string Name { get; set; }
        public IEnumerable<VsProject> VsProjects {get; set;}
    }
}
