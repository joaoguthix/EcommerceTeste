﻿using DomainProduct.Interfaces.IRepositorys;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generic
{
    public class RepositoryGeneric<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<SqlLiteDbContext> _OptionsBuilder;

        public RepositoryGeneric()
        {
            _OptionsBuilder = new DbContextOptions<SqlLiteDbContext>();
        }

        public async Task Add(T Objeto)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();

            }
        }

        public async Task<T> GetById(int Id)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> GetAll()
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                return await data.Set<T>().ToListAsync();
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new SqlLiteDbContext(_OptionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();

            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}