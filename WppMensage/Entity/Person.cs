using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WppMensage.Entity
{
    public class Person
    {
        public Person(string status)
        {
            this.status = status;
        }
        public Person(string name, string client, string status)
        {
            this.name = name;
            this.status = status;
            this.client = client;
        }
        public string name { get; set; }
        public string client { get; set; }
        public string status { get; set; }
    }
}