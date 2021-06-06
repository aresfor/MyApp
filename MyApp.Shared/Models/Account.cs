using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Shared.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        public List<Collection> Collecitons { get; set; }
        public string Name { get; set; }
        //public Account() { }
        //public Account(Account account)
        //{
        //    AccountId = account.AccountId;
        //    Collecitons = account.Collecitons;
        //    Name = account.Name;
        //}

    }
}
