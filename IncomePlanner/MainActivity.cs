using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText _incomePerHourEditText;
        private EditText _workHourPerDayEditText;
        private EditText _taxRateEditText;
        private EditText _savingRateEditText;

        private TextView _workSummaryTextView;
        private TextView _grossIncomeTextView;
        private TextView _taxPayableTextView;
        private TextView _annualSavingsTextView;
        private TextView _spendableIncomeTextView;

        private Button _calculateButton;
        private RelativeLayout _resultLayout;

        bool inputCalculated = false;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        private void ConnectViews()
        {
            _incomePerHourEditText = FindViewById<EditText>(Resource.Id.dollarPerHourEditText);
            _workHourPerDayEditText = FindViewById<EditText>(Resource.Id.workHoursPerDayEditText);
            _taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            _savingRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);
            
            _workSummaryTextView = FindViewById<TextView>(Resource.Id.workSummaryTextView);
            _grossIncomeTextView = FindViewById<TextView>(Resource.Id.annualGrossIncomeTextView);
            _taxPayableTextView = FindViewById<TextView>(Resource.Id.annualTaxPayableTextView);
            _annualSavingsTextView = FindViewById<TextView>(Resource.Id.annualSavingsTextView);
            _spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spendableIncomeTextView);

            _calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            _resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultsRelativeLayout);
            
            _calculateButton.Click += CalculateButtonOnClick;
        }

        private void CalculateButtonOnClick(object sender, EventArgs e)
        {
            if (inputCalculated)
            {
                inputCalculated = false;
                _calculateButton.Text = "Calculate";
                ClearInput();
                return;
            }

            double incomePerHour = double.Parse(_incomePerHourEditText.Text);
            double workHourPerDay = double.Parse(_workHourPerDayEditText.Text);
            double taxRate = double.Parse(_taxRateEditText.Text);
            double savingsRate = double.Parse(_savingRateEditText.Text);

            double annualWorkHourSummary = workHourPerDay * 5 * 50;
            double annualIncome = incomePerHour * workHourPerDay * 5 * 50;
            double taxPayable = (taxRate / 100) * annualIncome;
            double annualSavings = (savingsRate / 100) * annualIncome;
            double spendableIncome = annualIncome - annualSavings - taxPayable;

            _grossIncomeTextView.Text = annualIncome.ToString("#,##") + " USD";
            _workSummaryTextView.Text = annualWorkHourSummary.ToString("#,##") + " HRS";
            _taxPayableTextView.Text = taxPayable.ToString("#,##") + " USD";
            _annualSavingsTextView.Text = annualSavings.ToString("#,##") + " USD";
            _spendableIncomeTextView.Text = spendableIncome.ToString("#,##") + " USD";

            _resultLayout.Visibility = ViewStates.Visible;
            inputCalculated = true;
            _calculateButton.Text = "Clear";
        }

        private void ClearInput()
        {
            _incomePerHourEditText.Text = String.Empty;
            _workHourPerDayEditText.Text = String.Empty;
            _taxRateEditText.Text = String.Empty;
            _savingRateEditText.Text = String.Empty;
            
            _resultLayout.Visibility = ViewStates.Invisible;
        }
    }
}