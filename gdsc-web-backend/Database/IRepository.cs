using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gdsc_web_backend.Database
{
    public interface IRepository<T> where T : Model
    {
        Task<T> Add(T entity);
        Task<T> Get(string Id);
        Task<IEnumerable<T>> Get();
    }
}