using CryptoAnalyzer.Data;

namespace CryptoAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void Button_Fetch_Click(object sender, EventArgs e)
        {
            try
            {
                await FetchAndDisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occurred while fetching the data!");
            }
        }

        private async Task FetchAndDisplayData()
        {
            DateTime dateFrom = TimeProcessor.GetDateAtMidnight(dateTimePickerFrom.Value);
            DateTime dateTo = TimeProcessor.GetDateAtMidnight(dateTimePickerTo.Value).AddDays(1);
            TimeSpan difference = dateTo - dateFrom;

            DataEntry? coinData = await CryptoDataProcessor.GetCoinData(dateFrom, dateTo);

            string trend = "";
            string volume = "";
            string days = "";

            if (difference.Days <= 1)
            {
                ConvertAndProcessMinuteData(coinData, out trend, out volume);
            }
            else if (difference.Days < 90)
            {
                ConvertAndProcessHourlyData(coinData, out trend, out volume, out days);
            }
            else if (difference.Days >= 90)
            {
                ConvertAndProcessDailyData(coinData, out trend, out volume, out days);
            }

            label_Result.Text = trend + volume + days;
        }

        private static List<ProcessedData> ConvertAndProcessMinuteData(DataEntry? coinData, out string trend, out string volume)
        {
            List<ProcessedData> convertedList = CryptoDataProcessor.ConvertMinuteData(coinData);
            var highestVolume = CryptoDataProcessor.GetHighestVolume(convertedList);

            trend = $"Only 1 day selected\n";
            volume = $"Highest trading volume: {highestVolume.Item1} € was on {highestVolume.Item2:T}\n";

            return convertedList;
        }

        private static List<ProcessedData> ConvertAndProcessHourlyData(DataEntry? coinData, out string trend, out string volume, out string days)
        {
            List<ProcessedData> convertedList = CryptoDataProcessor.ConvertHourlyData(coinData);
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

            return convertedList;
        }

        private static List<ProcessedData> ConvertAndProcessDailyData(DataEntry? coinData, out string trend, out string volume, out string days)
        {
            List<ProcessedData> convertedList = CryptoDataProcessor.ConvertDailyData(coinData);
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

            return convertedList;
        }
    }
}