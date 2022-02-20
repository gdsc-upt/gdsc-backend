using System;
using System.Collections.Generic;
using GdscBackend.Database;
using GdscBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GdscBackend.Tests.Mocks;

public class TestDbContext<T> where T : class, IModel
{
    public TestDbContext(IEnumerable<T> testData = null)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(nameof(T) + "-Database-" + Guid.NewGuid())
           .EnableDetailedErrors()
           .EnableSensitiveDataLogging()
           .Options;

        Object = new AppDbContext(options);

        if (testData is null)
        {
            return;
        }

        Object.Set<T>().AddRange(testData);
        Object.SaveChanges();
    }

    public AppDbContext Object { get; }
}