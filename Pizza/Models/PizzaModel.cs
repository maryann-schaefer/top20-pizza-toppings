using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
  public class PizzaModel
  {
    public string Toppings { get; set;}
    public int Rank { get; set; }
    public int Count { get; set; }

    public PizzaModel() { }

    public PizzaModel(string[] toppings) {
      Toppings = string.Join(",", toppings);
    }

  }
}
