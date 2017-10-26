using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using System.Collections.Generic;

namespace ListcontactsApp
{
    [Activity(Label = "ListcontactsApp", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var listconta = FindViewById<ListView>(Resource.Id.listView1);


            string[] projection = { ContactsContract.CommonDataKinds.Phone.Number,ContactsContract.Contacts.InterfaceConsts.DisplayName };
            string selection = "_id = " + ContactsContract.Contacts.InterfaceConsts.Id;

            var cursor = ContentResolver.Query(ContactsContract.CommonDataKinds.Phone.ContentUri, projection, selection, null, null);

            var contactList = new List<string>();


            if (cursor.MoveToFirst())
            {
                do
                {
                    string phoneNumber = cursor.GetString(cursor.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number));


                    contactList.Add(cursor.GetString(
                            cursor.GetColumnIndex(projection[1])) + " " + phoneNumber);
                } while (cursor.MoveToNext());
            }

            ArrayAdapter adapter = new ArrayAdapter(this,
                      Android.Resource.Layout.SimpleSpinnerItem, contactList);
            listconta.Adapter = adapter;
        }
    }
}

