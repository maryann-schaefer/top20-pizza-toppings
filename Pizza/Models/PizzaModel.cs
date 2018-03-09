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
      //Data assumptions:
      //  - order of toppings does not matter (cheese,pepperoni is the same as pepperoni,cheese)
      //  - data is all lower case
      //  - don't ignore duplicate toppings as it could mean extra (cheese,pepperoni is not the same as cheese,pepperoni,pepperoni)

      //Sort toppings first to ensure order in raw data is consistent.
      Array.Sort(toppings);

      //Join array elements into a string.
      Toppings = string.Join(",", toppings);
    }

  }
}
