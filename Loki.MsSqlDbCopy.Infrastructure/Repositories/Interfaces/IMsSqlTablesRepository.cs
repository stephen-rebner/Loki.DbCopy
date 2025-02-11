﻿using Loki.MsSqlCopy.Data;

namespace Loki.MsSqlDbCopy.Infrastructure.Repositories.Interfaces;

public interface IMsSqlTablesRepository
{
    Task<TableInfo[]> GetTablesAsync(string connectionString);
    
    Task SaveTableAsync(string connectionString, TableInfo tableInfo);
}