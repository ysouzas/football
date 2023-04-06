using System;
using F.API.Data.Storage.Interfaces;
using F.API.Models.DTO.Model;
using F.API.Models.TableStorage;
using Microsoft.Azure.Cosmos.Table;

namespace F.API.Data.Storage;

public class PlayerTableStorage : IPlayerTableStorage
{
    private CloudTable _table;

    public PlayerTableStorage(string connectionString)
    {
        var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
        var tableClient = cloudStorageAccount.CreateCloudTableClient(new TableClientConfiguration());
        _table = tableClient.GetTableReference("football");
    }

    public async Task InsertOrReplace(PlayerTableStorageEntity entity)
    {
        var insertOrMergeOperation = TableOperation.InsertOrReplace(entity);

        var tableResult = await _table.ExecuteAsync(insertOrMergeOperation);
    }
}

