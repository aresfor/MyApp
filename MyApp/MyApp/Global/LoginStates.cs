using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Global
{
    public static class LoginStates
    {
        public static bool isLogged { get; set; } = false;
        public static string LoginAccountName { get; set; } = "请登录";
        public static string LoginAvatar { get; set; } = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";

        public static Account account;
        
    }
}
