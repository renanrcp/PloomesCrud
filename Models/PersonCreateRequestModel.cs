using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PloomesCrud.Models;

public class PersonCreateRequestModel
{
  [Required]
  public required string Name { get; set; }

  [Required]
  public required string LastName { get; set; }

  [Required]
  public required int Age { get; set; }

  [Required]
  public required string MainStack { get; set; }
}
