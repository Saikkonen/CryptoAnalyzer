using CryptoAnalyzer.Data;

namespace CryptoAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private async void Button_Fetch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateFrom = TimeProcessor.GetDateAtMidnight(dateTimePickerFrom.Value);
                DateTime dateTo = TimeProcessor.GetDateAtMidnight(dateTimePickerTo.Value).AddDays(1);
                TimeSpan difference = dateTo - dateFrom;

                DataEntry? coinData = new();
                coinData = await CryptoDataProcessor.GetCoinData(dateFrom, dateTo);
                List<ProcessedData> convertedList = new();

                /*  1 day from current time = 5 minute interval data
                    1 - 90 days from current time = hourly data
                    above 90 days from current time = daily data (00:00 UTC)
                */

                string trend = "";
                string volume = "";
                string days = "";

                if (difference.Days <= 1)
                {
                    convertedList = CryptoDataProcessor.ConvertMinuteData(coinData);
                    var highestVolume = CryptoDataProcessor.GetHighestVolume(convertedList);

                    trend = $"Only 1 day selected\n";
                    volume = $"Highest trading volume: {highestVolume.Item1} € was on {highestVolume.Item2:T}\n";
                }
                else if (difference.Days < 90)
                {
                    convertedList = CryptoDataProcessor.ConvertHourlyData(coinData);
                    int lowestTrend = CryptoDataProcessor.GetDownwardTrend(convertedList);
                    var highestVolume = CryptoDataProcessor.GetHighestVolume(convertedList);
                    var buySellDates = CryptoDataProcessor.GetBuySellDays(convertedList);

                    trend = $"Longest downfall trend: {lowestTrend} days\n";
                    volume = $"Highest trading volume: {highestVolume.Item1} € was on {highestVolume.Item2.Date:d}\n";

                    if (buySellDates.Item1)
                    {
                        days = $"Best day to buy {buySellDates.Item2:d} and best day to sell {buySellDates.Item3:d}\n";
                    }
                    else
                    {
                        days = "You should not buy or sell";
                    }
                }
                else if (difference.Days >= 90)
                {
                    convertedList = CryptoDataProcessor.ConvertDailyData(coinData);
                    int lowestTrend = CryptoDataProcessor.GetDownwardTrend(convertedList);
                    var highestVolume = CryptoDataProcessor.GetHighestVolume(convertedList);
                    var buySellDates = CryptoDataProcessor.GetBuySellDays(convertedList);

                    trend = $"Longest downfall trend: {lowestTrend} days\n";
                    volume = $"Highest trading volume: {highestVolume.Item1} € was on {highestVolume.Item2.Date:d}\n";

                    if (buySellDates.Item1)
                    {
                        days = $"Best day to buy {buySellDates.Item2:d} and best day to sell {buySellDates.Item3:d}\n";
                    }
                    else
                    {
                        days = "You should not buy or sell";
                    }
                }

                label_Result.Text = trend + volume + days;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured while fetching the data!");
            }
        }
    }
}