using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PloomesCrud.Data.Models;

namespace PloomesCrud.Data.EntityConfigurations;

public class PersonEntityConfiguration : IEntityTypeConfiguration<PersonModel>
{
  public void Configure(EntityTypeBuilder<PersonModel> builder)
  {
    _ = builder.HasKey(x => x.Id);
    _ = builder.Property(x => x.Id);
  }
}
