using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimCorpWien
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //}

        //    public static List<int> balancedOrNot(List<string> expressions, List<int> maxReplacements)
        //    {
        //        List<int> res = new List<int>();

        //        for (int i = 0; i < expressions.Count; i++)
        //        {
        //            string expression = expressions[i];
        //            int maxReplacement = maxReplacements[i];

        //            int replaced = 0;
        //            int count = 0;
        //            foreach (var c in expression)
        //            {
        //                if (c == '<')
        //                {
        //                    count++;
        //                }
        //                else
        //                {
        //                    if (count > 0) count--;
        //                    else replaced++;
        //                }
        //            }
        //            res.Add(count == 0 && replaced <= maxReplacement ? 1 : 0);
        //        }

        //        return res;
        //    }

        //public static int minSum(List<int> num, int k)
        //{
        //    var data = new SortedDictionary<int, int>();
        //    foreach (var key in num)
        //    {
        //        if (data.ContainsKey(key)) data[key]++;
        //        else data[key] = 1;
        //    }

        //    while (k > 0)
        //    {
        //        KeyValuePair<int, int> pair = data.Last();
        //        if (pair.Key == 1) break;

        //        int moveCount = Math.Min(k, pair.Value);

        //        if (pair.Value == moveCount)
        //            data.Remove(pair.Key);
        //        else data[pair.Key] -= moveCount;

        //        var nextKey = (int)Math.Ceiling(pair.Key / 2.0);
        //        if (data.ContainsKey(nextKey)) data[nextKey] += moveCount;
        //        else data[nextKey] = moveCount;

        //        k -= moveCount;
        //    }

        //    int res = 0;
        //    foreach (var pair in data)
        //    {
        //        res += pair.Key * pair.Value;
        //    }
        //    return res;
        //}


        //class Movie
        //{
        //    public string Title { get; set; }
        //    public int Year { get; set; }
        //    public string imdbID { get; set; }
        //}

        //class MovieInfo
        //{
        //    public int page { get; set; }
        //    public int per_page { get; set; }
        //    public int total { get; set; }
        //    public int total_pages { get; set; }
        //    public List<Movie> data { get; set; } = new List<Movie>();
        //}

        ///*
        //* Complete the function below.
        //* Base url: https://jsonmock.hackerrank.com/api/movies/search/?Title=
        //*/
        //static string[] getMovieTitles(string substr)
        //{
        //    int page = 1;
        //    string[] res = null;
        //    int index = 0;
        //    do
        //    {
        //        var url = $"https://jsonmock.hackerrank.com/api/movies/search/?Title={substr}&page={page}";
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        Stream receiveStream = response.GetResponseStream();
        //        StreamReader readStream = new StreamReader(receiveStream);
        //        var json = readStream.ReadToEnd();

        //        MovieInfo info = JsonConvert.DeserializeObject<MovieInfo>(json);
        //        if (res == null)
        //        {
        //            res = new string[info.total];
        //        }

        //        foreach (var movie in info.data)
        //        {
        //            res[index++] = movie.Title;
        //        }

        //        if (page == info.total_pages) break;
        //        else page++;
        //    } while (true);

        //    Array.Sort(res);
        //    return res;
        //}

        public static Car MostExpensiveCar(List<Car> cars)
        {

            var mostExpensiveCar = cars[0];
            //return mostExpensiveCar = cars.MaxBy(x => x.Price);
             mostExpensiveCar = cars.OrderByDescending(x => x.Price).First();
            return mostExpensiveCar;
        }

        public static Car CheapestCar(List<Car> cars)
        {
            var cheapestCar = cars[0];
            //return cheapestCar = cars.MinBy(x => x.Price);
            cheapestCar = cars.OrderByDescending(x => x.Price).Last();
            return cheapestCar;

        }

        public static int AveragePriceOfCars(List<Car> cars)
        {
            int averagePrice = (int)cars.Average(x => x.Price);
            return averagePrice;
        }

        public static Dictionary<string, Car> MostExpensiveModelForEachBrand(List<Car> cars)
        {
            SortedDictionary<string, Car> sortedDictionary = new SortedDictionary<string, Car>();
            Dictionary<string, Car> unsortedDictionary = new Dictionary<string, Car>();

            //   var mostExpensiveByBrand = cars.GroupBy(b => b.Brand).Select(x => x.Max(x => x.Price));

            IEnumerable<Car> mostExpensiveByBrand = (IEnumerable<Car>)cars
                .GroupBy(x => x.Brand).Select(g => g.OrderByDescending(o => o.Price).First());
                //.Select(g => g.Max(x => x.Price));

            foreach (var c in mostExpensiveByBrand)
                unsortedDictionary.Add(c.Brand, c);

            return unsortedDictionary;
            
                    return cars
            .GroupBy(p => p.Brand)
            .Select(p => new {key = p.Key, value = p.OrderByDescending(t => t.Price).First() })
            .OrderBy(p => p.key)
            .ToDictionary(p => p.key, p => p.value);
        }

        public static void Main()
        {
            int countOfCars = int.Parse(Console.ReadLine());
            var cars = new List<Car>();
            for (int i = 0; i < countOfCars; i++)
            {
                string str = Console.ReadLine();
                string[] strArr = str.Split(' ');
                cars.Add(new Car
                {
                    Brand = strArr[0],
                    Model = strArr[1],
                    Price = int.Parse(strArr[2])
                });
            }

            var mostExpensiveCar = MostExpensiveCar(cars);
            Console.WriteLine($"The most expensive car is {mostExpensiveCar.Brand} {mostExpensiveCar.Model}");

            var cheapestCar = CheapestCar(cars);
            Console.WriteLine($"The cheapest car is {cheapestCar.Brand} {cheapestCar.Model}");

            Console.WriteLine($"The average price of all cars is {AveragePriceOfCars(cars)}");

            foreach (var res in MostExpensiveModelForEachBrand(cars))
            {
                Console.WriteLine($"The most expensive model for brand {res.Key} is {res.Value.Model} having price {res.Value.Price}");
            }
        }
    }

    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
    }



}
