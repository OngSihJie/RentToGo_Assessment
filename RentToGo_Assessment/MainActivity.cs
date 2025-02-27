﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using RentToGo_Assessment.Models;
using System.Collections.Generic;

namespace RentToGo_Assessment
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        ListView PropertyList;
        List<Property> mylist = new List<Property>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            PropertyList = FindViewById<ListView>(Resource.Id.listView1);
            mylist = DetailsManager.GetPropertyData();
            PropertyList.Adapter = new DetailsAdapter(this, mylist);
            PropertyList.ItemClick += PropertyList_ItemClick;
        }
        //Adds Add to the Menu in the top right of your screen.
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            menu.Add("Customer Profile");
            menu.Add("Logout");
            return base.OnPrepareOptionsMenu(menu);
        }

        //When you choose Add from the Menu run the Add Activity. Good to know to add more options
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var itemTitle = item.TitleFormatted.ToString();

            switch (itemTitle)
            {
                case "Customer Profile":
                    StartActivity(typeof(CustomerProfile));
                    break;
                case "Logout":
                    StartActivity(typeof(Login));
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void PropertyList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var Property_Item = mylist[e.Position];

            var View_Property = new Intent(this, typeof(PropertyDetails));
            View_Property.PutExtra("Property_Name", Property_Item.PropertyName);
            View_Property.PutExtra("Property_Room", Property_Item.PropertyRoom);
            View_Property.PutExtra("Property_Bath", Property_Item.PropertyBath);
            View_Property.PutExtra("Property_Rent", Property_Item.PropertyRent);
            View_Property.PutExtra("Property_Address", Property_Item.PropertyAddress);

            StartActivity(View_Property);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}