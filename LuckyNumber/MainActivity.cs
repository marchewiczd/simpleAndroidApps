using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace LuckyNumber
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const string _title = "Get Lucky Number";
        
        private SeekBar _seekBar;
        private TextView _resultTextView;
        private Button _rollButton;
        private Random _rand = new Random();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            BindElements();
            SupportActionBar.Title = _title;
        }

        private void BindElements()
        {
            _seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            _resultTextView = FindViewById<TextView>(Resource.Id.resultTextView);
            _rollButton = FindViewById<Button>(Resource.Id.rollButton);
            _rollButton.Click += (sender, args) =>
            {
                _resultTextView.Text = (_rand.Next(_seekBar.Progress) + 1).ToString();
            };  
        }
    }
}