using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Data
{
    internal class CryptoDataProcessor
    {
        public static async Task<DataEntry?> GetCoinData(DateTime from, DateTime to)
        {
            string unixFrom = TimeProcessor.DateTimeToUnixTime(from);
            string unixTo = TimeProcessor.DateTimeToUnixTime(to);

            string url = $"https://api.coingecko.com/api/v3/coins/bitcoin/market_chart/range?vs_currency=eur&from={unixFrom}&to={unixTo}";

            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            DataEntry? result = JsonSerializer.Deserialize<DataEntry>(json);

            return result;
        }

        public static List<ProcessedData> ConvertHourlyData(DataEntry? coinData)
        {
            List<ProcessedData> processedDataList = new();

            if (coinData != null)
            {
                if (coinData.Prices != null && coinData.Total_volumes != null)
                {
                    for (int i = 0; i < coinData.Prices.Count; i += 24)
                    {
                        DateTime date = TimeProcessor.UnixToDatetime(coinData.Prices[i][0]);
                        double firstPrice = coinData.Prices[i][1];
                        double volumeSum = coinData.Total_volumes[i][1];

                        ProcessedData processedData = new()
                        {
                            Date = date,
                            FirstPrice = Math.Round(firstPrice, 2),
                            VolumeSum = Math.Round(volumeSum, 2),
                        };

                        processedDataList.Add(processedData);
                    }
                }
            }
            return processedDataList;
        }

        public static List<ProcessedData> ConvertDailyData(DataEntry? coinData)
        {
            List<ProcessedData> processedDataList = new();

            if (coinData != null)
            {
                if (coinData.Prices != null && coinData.Total_volumes != null)
                {
                    for (int i = 0; i < coinData.Prices.Count; i++)
                    {
                        DateTime date = TimeProcessor.UnixToDatetime(coinData.Prices[i][0]);
                        double firstPrice = coinData.Prices[i][1];
                        double volumeSum = coinData.Total_volumes[i][1];

                        ProcessedData processedData = new()
                        {
                            Date = date,
                            FirstPrice = Math.Round(firstPrice, 2),
                            VolumeSum = Math.Round(volumeSum, 2),
                        };

                        processedDataList.Add(processedData);
                    }
                }
            }
            return processedDataList;
        }

        public static List<ProcessedData> ConvertMinuteData(DataEntry? coinData)
        {
            List<ProcessedData> processedDataList = new();

            if (coinData != null)
            {
                if (coinData.Prices != null && coinData.Total_volumes != null)
                {
                    for (int i = 0; i < coinData.Prices.Count; i++)
                    {
                        DateTime date = TimeProcessor.UnixToDatetime(coinData.Prices[i][0]);
                        double firstPrice = coinData.Prices[i][1];
                        double volumeSum = coinData.Total_volumes[i][1];

                        ProcessedData processedData = new()
                        {
                            Date = date,
                            FirstPrice = Math.Round(firstPrice, 2),
                            VolumeSum = Math.Round(volumeSum, 2),
                        };

                        processedDataList.Add(processedData);
                    }
                }
            }
            return processedDataList;
        }

        public static int GetDownwardTrend(List<ProcessedData> convertedList)
        {
            int maxDownwardTrend = 0;
            int currentTrend = 0;

            for (int i = 1; i < convertedList.Count; i++)
            {
                double firstPrice = convertedList[i - 1].FirstPrice;
                double secondPrice = convertedList[i].FirstPrice;
                if (firstPrice > secondPrice)
                {
                    currentTrend++;
                    if (currentTrend > maxDownwardTrend)
                    {
                        maxDownwardTrend = currentTrend;
                    }
                }
                else
                {
                    currentTrend = 0;
                }
            }
            return maxDownwardTrend;
        }

        public static (double, DateTime) GetHighestVolume(List<ProcessedData> convertedList)
        {
            double highestVolume = convertedList[0].VolumeSum;
            DateTime date = convertedList[0].Date;

            for (int i = 1; i < convertedList.Count; i++)
            {
                if (convertedList[i].VolumeSum > highestVolume)
                {
                    highestVolume = convertedList[i].VolumeSum;
                    date = convertedList[i].Date;
                }
            }
            return (highestVolume, date);
        }

        public static (bool, DateTime, DateTime) GetBuySellDays(List<ProcessedData> convertedList)
        {
            int maxUpwardsTrend = 0;
            int currentTrend = 0;
            int startIndex = 0;
            int endIndex = 0;

            for (int i = 1; i < convertedList.Count; i++)
            {
                double firstPrice = convertedList[startIndex].FirstPrice;
                double secondPrice = convertedList[i].FirstPrice;

                if (firstPrice < secondPrice)
                {
                    currentTrend++;
                    if (currentTrend > maxUpwardsTrend)
                    {
                        maxUpwardsTrend = currentTrend;
                        endIndex = i;
                    }
                }
                else
                {
                    startIndex = 0;
                    currentTrend = 0;
                }
            }
            if (maxUpwardsTrend > 0)
            {
                DateTime startDate = convertedList[startIndex].Date;
                DateTime endDate = convertedList[endIndex].Date;

                return (true, startDate, endDate);
            }
            else
            {
                return (false, convertedList[0].Date, convertedList[0].Date);
            }
        }

    }
}
