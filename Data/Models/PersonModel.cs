using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PloomesCrud.Models;

namespace PloomesCrud.Data.Models;

public class PersonModel
{
  public int Id { get; set; }

  public required string Name { get; set; }

  public required string LastName { get; set; }

  public string FullName => $"{Name} {LastName}";

  public required int Age { get; set; }

  public required string MainStack { get; set; }

  public PersonResponseModel AsResponse()
  {
    return new PersonResponseModel
    {
      Id = Id,
      Name = Name,
      LastName = LastName,
      Age = Age,
      MainStack = MainStack,
    };
  }
}
