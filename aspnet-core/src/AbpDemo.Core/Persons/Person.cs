using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Persons
{
    public class Person : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
    }
}
