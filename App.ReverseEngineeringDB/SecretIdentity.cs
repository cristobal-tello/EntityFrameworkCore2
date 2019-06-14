using System;
using System.Collections.Generic;

namespace App.ReverseEngineeringDB
{
    public partial class SecretIdentity
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public int SamuraiId { get; set; }

        public virtual Samurais Samurai { get; set; }
    }
}
