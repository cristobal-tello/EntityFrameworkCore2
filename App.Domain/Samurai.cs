﻿using System.Collections.Generic;

namespace App.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            this.Quotes = new List<Quote>();
            this.SecretIdentity = new SecretIdentity();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; set; }
        //public int BattleId { get; set; }
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        public SecretIdentity SecretIdentity { get; set; }
    }
}
