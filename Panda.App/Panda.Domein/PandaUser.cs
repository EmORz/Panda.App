using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Panda.Domein
{
    public class PandaUser : IdentityUser 
    {

        public PandaUser()
        {
            this.Packages = new List<Package>();
            this.Reciepts = new List<Reciept>();
        }
        public List<Package> Packages { get; set; }

        public List<Reciept> Reciepts { get; set; } 
    }
}
