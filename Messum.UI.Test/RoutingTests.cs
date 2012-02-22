using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Messum.UI.Test.Helpers;
using NUnit.Framework;

namespace Messum.UI.Test
{
    [TestFixture]
    class RoutingTests
    {
        //Slash
        [Test]
        public void When_Url_Slash_Home_Index()
        {
            const string url = "~/";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Home",
                Action = "Index",
            });
        }
        //User
        [Test]
        public void slash_user_user_index()
        {
            const string url = "~/User";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "Index",
            });
        }

        [Test]
        public void slash_user_register_user_register()
        {
            const string url = "~/User/Register";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "Register",
            });
        }

        [Test]
        public void slash_user_login_user_login()
        {
            const string url = "~/User/Login";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "Login",
            });
        }

        [Test]
        public void slash_user_list_user_list()
        {
            const string url = "~/User/List";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "List",
            });
        }

        [Test]
        public void slash_user_list_UserName_user_list_UserName()
        {
            const string url = "~/User/List/UserName";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "User",
                Action = "List",
                UserName = "UserName",
            });
        }

        [Test]
        public void slash_user_settings_user_settings()
        {
            const string url = "~/User/Settings";
            RouteHelpers.TestRoute(url, new
                                            {
                                                Controller = "User",
                                                Action = "Settings",
                                            });
        }


        

        [Test]
        public void slash_new_Home_New()
        {
            const string url = "~/New";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Home",
                Action = "New",
            });
        }
        [Test]
        public void slash_Best_Home_Best()
        {
            const string url = "~/Best";
            RouteHelpers.TestRoute(url, new
            {
                Controller = "Home",
                Action = "Best",
            });
        }
    }
}
