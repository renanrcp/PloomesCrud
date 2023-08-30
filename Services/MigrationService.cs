using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PloomesCrud.Data;

namespace PloomesCrud.Services;

public class MigrationService : BackgroundService
{
  private readonly IServiceScopeFactory _serviceScopeFactory;

  public MigrationService(IServiceScopeFactory serviceScopeFactory)
  {
    _serviceScopeFactory = serviceScopeFactory;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await using var scope = _serviceScopeFactory.CreateAsyncScope();

    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    await dataContext.Database.MigrateAsync(stoppingToken);
  }
}
