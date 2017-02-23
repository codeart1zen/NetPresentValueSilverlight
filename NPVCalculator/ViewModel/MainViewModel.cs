using GalaSoft.MvvmLight;
using System;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using NPVCalculator.Model;
using System.Collections.Generic;

namespace NPVCalculator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand CreateRandomCashflowCommand { get; private set; }
        public RelayCommand CalculateNPVCommand { get; private set; }
        public RelayCommand RestartCommand { get; private set; }
        public RelayCommand CloseErrorDialogCommand { get; private set; }

        public MainViewModel()
        {
            ResetDefaultParameters();
            CreateCashflows();            

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.     

                CalculatedNPVs.Add(new NPVPoint() { DiscountRate = 5, NPV = 200000 });
                CalculatedNPVs.Add(new NPVPoint() { DiscountRate = 10, NPV = 100000 });
                CalculatedNPVs.Add(new NPVPoint() { DiscountRate = 15, NPV = 50000 });
            }
            else
            {
                CreateRandomCashflowCommand = new RelayCommand(CreateRandomCashflow);
                CalculateNPVCommand = new RelayCommand(CalculateNPV, CanCalculateNPV);
                RestartCommand = new RelayCommand(Restart);
                CloseErrorDialogCommand = new RelayCommand(CloseErrorDialog);
            }
        }

        private void ResetDefaultParameters()
        {
            DiscountRateLowerBound = 5;
            DiscountRateUpperBound = 25;
            DiscountRateSampleInterval = 1;
        }

        private bool _hasChartData;
        public bool HasChartData
        {
            get { return _hasChartData; }
            set
            {
                _hasChartData = value;
                RaisePropertyChanged("HasChartData");
            }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value;               
                RaisePropertyChanged("HasError");
                if (HasError)
                {
                    IsCalculating = false;                    
                }
            }
        }

        private bool _isCalculating;
        public bool IsCalculating
        {
            get { return _isCalculating; }
            set
            {
                _isCalculating = value;
                RaisePropertyChanged("IsCalculating");
            }
        }

        private ObservableCollection<NPVPoint> _calculatedNPVs = new ObservableCollection<NPVPoint>();
        public ObservableCollection<NPVPoint> CalculatedNPVs
        {
            get
            {
                return _calculatedNPVs;
            }
            set
            {
                _calculatedNPVs = value;
                RaisePropertyChanged("CalculatedNPVs");
            }
        }

        public decimal MaximumDiscountRate
        {
            get { return 50M; }
        }

        public decimal MinimumDiscountRate
        {
            get { return 0M; }
        }

        public decimal MaximumDiscountRateInterval
        {
            get { return 5M; }
        }

        public decimal MinimumDiscountRateInterval
        {
            get { return 0.01M; }
        }

        private ObservableCollection<CalcService.Cashflow> _cashflows = new ObservableCollection<CalcService.Cashflow>();
        public ObservableCollection<CalcService.Cashflow> Cashflows
        {
            get
            {
                return _cashflows;
            }
            set
            {
                _cashflows = value;
                RaisePropertyChanged("Cashflows");
            }
        }

        private decimal _discountRateLowerBound;
        public decimal DiscountRateLowerBound
        {
            get { return _discountRateLowerBound; }
            set
            {
                _discountRateLowerBound = Math.Round(value, 2);
                if (DiscountRateLowerBound > DiscountRateUpperBound)
                {
                    DiscountRateLowerBound = DiscountRateUpperBound;
                }
                RaisePropertyChanged("DiscountRateLowerBound");
            }
        }

        private decimal _discountRateUpperBound;
        public decimal DiscountRateUpperBound
        {
            get { return _discountRateUpperBound; }
            set
            {
                _discountRateUpperBound = Math.Round(value, 2);
                if (DiscountRateUpperBound < DiscountRateLowerBound)
                {
                    DiscountRateUpperBound = DiscountRateLowerBound;
                }
                RaisePropertyChanged("DiscountRateUpperBound");
            }
        }

        private decimal _discountRateSampleInterval;
        public decimal DiscountRateSampleInterval
        {
            get { return _discountRateSampleInterval; }
            set
            {
                _discountRateSampleInterval = Math.Round(value, 2);
                RaisePropertyChanged("DiscountRateSampleInterval");
            }
        }

        private bool UserCancelled = false;

        private void Restart()
        {
            UserCancelled = true;
            IsCalculating = false;
            HasChartData = false;
            ResetDefaultParameters();
            CalculatedNPVs.Clear();
            Cashflows.Clear();
            CreateCashflows();
        }

        private void CloseErrorDialog()
        {
            HasError = false;            
        }

        private void CreateRandomCashflow()
        {
            Cashflows.Add(CashflowFactory.GetRandomCashflow());            
        }

        private void CreateCashflows()
        {
            for (int i = 0; i < 6; i++)
            {
                CreateRandomCashflow();
            }
        }

        private void CalculateNPV()
        {
            UserCancelled = false;
            IsCalculating = true;
            
            svc = new CalcService.CalculationServiceClient();
            svc.SetupNPVRangeCalculationCompleted += svc_SetupNPVRangeCalculationCompleted;
            svc.SetupNPVRangeCalculationAsync(DiscountRateLowerBound, DiscountRateUpperBound, DiscountRateSampleInterval, Cashflows, ServerClientId);
        }

        CalcService.CalculationServiceClient svc;
        string ServerClientId = Guid.NewGuid().ToString();
        int calculationsRemaining = 0;
        int calculationsTotal = 0;

        private int _calculationProgressPercentage;
        public int CalculationProgressPercentage
        {
            get { return _calculationProgressPercentage; }
            set
            {
                _calculationProgressPercentage = value;
                RaisePropertyChanged("CalculationProgressPercentage");
            }
        }

        void svc_SetupNPVRangeCalculationCompleted(object sender, CalcService.SetupNPVRangeCalculationCompletedEventArgs e)
        {
            svc.SetupNPVRangeCalculationCompleted -= svc_SetupNPVRangeCalculationCompleted;

            if (e.Error != null)
            {                
                HasError = true;
                return;
            }

            HasChartData = true;

            calculationsRemaining = e.Result;
            calculationsTotal = calculationsRemaining;
            CalculationProgressPercentage = 0;

            
            svc.GetNextNPVCompleted += svc_GetNextNPVCompleted;
            svc.GetNextNPVAsync(ServerClientId);            
        }

        void svc_GetNextNPVCompleted(object sender, CalcService.GetNextNPVCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                HasError = true;
                FinishDownloadingCalculations();
                return;
            }

            if (UserCancelled)
            {
                FinishDownloadingCalculations();
            }
            else
            {
                CalculatedNPVs.Add(new NPVPoint() { DiscountRate = e.Result.DiscountRate, NPV = e.Result.Amount });
                calculationsRemaining -= 1;                
                if (calculationsRemaining == 0)
                {
                    FinishDownloadingCalculations();                    
                }
                else
                {
                    CalculationProgressPercentage = GetCalculationCompletePercentage();
                    svc.GetNextNPVAsync(ServerClientId);
                }
            }
        }

        private int GetCalculationCompletePercentage()
        {
            var decimalPercentage = 1M - Convert.ToDecimal(calculationsRemaining) / calculationsTotal;
            return Convert.ToInt32(100 * decimalPercentage);
        }

        private void FinishDownloadingCalculations()
        {
            IsCalculating = false;            
            svc.GetNextNPVCompleted -= svc_GetNextNPVCompleted;            
            svc.CloseAsync(); // not important as basicHttpBinding has no server sessions to release, hence simple close 
        }

        /// <summary>
        /// Opted to hide the button on the UI instead, better UI effect, leaving this here to demonstrate the CanExecute of command
        /// </summary>
        /// <returns></returns>
        private bool CanCalculateNPV()
        {
            return !IsCalculating;            
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}