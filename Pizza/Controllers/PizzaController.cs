using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pizza.Controllers
{
  public class PizzaController : Controller
  {
    public IActionResult Index()
    {
      List<PizzaModel> pizzaList = Top20Pizzas();

      return View(pizzaList);
    }

    private List<PizzaModel> Top20Pizzas()
    {
      //Get pizza data from json and parse
      string json = GetJsonData("http://files.olo.com/pizzas.json");
      JArray pizzaJson = JArray.Parse(json) as JArray;

      //Create pizza list from json
      List<PizzaModel> pizzaList = new List<PizzaModel>();
      foreach (dynamic item in pizzaJson)
      {
        pizzaList.Add(new PizzaModel(item.toppings.ToObject<string[]>()));
      }

      //Summarize pizza list by toppings, with count
      var sumToppings = from topping in pizzaList
                        group topping by topping.Toppings into toppingGroup
                        select new
                        {
                          Toppings = toppingGroup.Key,
                          Count = toppingGroup.Count()
                        };

      //Get and return top 20 most ordered pizza toppings
      int counter = 1;
      var orderedToppings = (from topping in sumToppings
                             orderby topping.Count descending
                             select new PizzaModel { Rank = counter++, Toppings = topping.Toppings, Count = topping.Count }).Take(20);

      return orderedToppings.ToList();

    }


    public static string GetJsonData(string url)
    {
      Uri uri = new Uri(url);
      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
      request.Method = WebRequestMethods.Http.Get;
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream());
      string json = reader.ReadToEnd();
      response.Close();

      return json;
    }
  }
}