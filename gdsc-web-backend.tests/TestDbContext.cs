using System;
using System.Collections.Generic;
using gdsc_web_backend.Database;
using gdsc_web_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.tests
{
    public class TestDbContext<T> where T : class, IModel
    {
        public AppDbContext Object { get; }

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
    }
}
