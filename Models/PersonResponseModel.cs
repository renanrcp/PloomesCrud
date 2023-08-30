using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PloomesCrud.Models;

public class PersonResponseModel
{
  [Required]
  public required int Id { get; init; }

  [Required]
  public required string Name { get; init; }

  [Required]
  public required string LastName { get; init; }

  [Required]
  public string FullName => $"{Name} {LastName}";

  [Required]
  public required int Age { get; init; }

  [Required]
  public required string MainStack { get; init; }
}
