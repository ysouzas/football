using System;
using Microsoft.Azure.Cosmos.Table;

namespace F.Core.Data;

public interface ITableStorage<T>
{
    Task InsertOrReplace(T entity);
}

