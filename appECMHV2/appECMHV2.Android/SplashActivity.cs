﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace appECMHV2.Droid
{
    [Activity(Label = "ECMH",
       Icon = "@drawable/logomonica",
       Theme = "@style/Theme.Splash",
       MainLauncher = true,
       NoHistory = true)]

    public class SplashActivity : Activity
    {
        #region Singleton
        private static SplashActivity instance;
        public static SplashActivity GetInstance()
        {
            if (instance == null)
            {
                instance = new SplashActivity();
            }
            return instance;
        }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            instance = this;


            switch (Device.Idiom)
            {
                case TargetIdiom.Phone:
                    RequestedOrientation = ScreenOrientation.Portrait;
                    break;
                case TargetIdiom.Tablet:
                    RequestedOrientation = ScreenOrientation.Landscape;
                    break;
            }

            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(1800);


            // Create your application here

            StartActivity(typeof(MainActivity));
        }
    }
}