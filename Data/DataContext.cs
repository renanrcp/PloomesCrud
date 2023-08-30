using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PloomesCrud.Data.Models;

namespace PloomesCrud.Data;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  public required DbSet<PersonModel> Persons { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
  }
}