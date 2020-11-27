﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemoMap.Domain.SeedWork
{
    public interface IRepository<T> where T:Entity
    {
        T Update(T e);
        T Delete(T e);
        // update and insert functionality in a one method, so we don't have to implement Create and Update separately
        T Upsert(T e);

        T FindByID(int id);
        // Asychronous methods return Task object.
        Task<T> CreateAsync(T e);   
        Task<List<T>> FindAll();
    }
}