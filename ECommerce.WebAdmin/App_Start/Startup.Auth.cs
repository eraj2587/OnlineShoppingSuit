using System;
using ECommerce.WebAdmin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.LinkedIn;

namespace ECommerce.WebAdmin
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864

        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            // clientId: "",
            // clientSecret: "");

            //app.UseTwitterAuthentication(
            // consumerKey: "",
            // consumerSecret: "");

            //http://www.asp.net/mvc/overview/security/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on
            //http://www.codeproject.com/Articles/874207/LinkedIn-Authentication-in-ASP-NET-MVC
            app.UseLinkedInAuthentication(
                clientId: "81ik3avi1y1th5",
                clientSecret: "vunzY8qVnJnYhaFo"
                );

            app.UseFacebookAuthentication(
      appId: "1085071928252935",
      appSecret: "355a9b3e66535f604681258f74c5fcbb");


            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
                   {
                       ClientId = "30400210697-nn7a1btroecn4p1b41b15n65m0j9id5q.apps.googleusercontent.com",
                       ClientSecret = "6atFzAmoFotCJq_Yb-sck1M5",
                   };
            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }


    }
}