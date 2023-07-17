using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace EasilyNET.MongoGridFS.AspNetCore;

/// <summary>
/// GridFSBucketFactory
/// </summary>
/// <remarks>
/// ���캯��
/// </remarks>
/// <param name="optionsMonitor"></param>
internal sealed class GridFSBucketFactory(IOptionsMonitor<GridFSBucketOptions> optionsMonitor) : IGridFSBucketFactory
{
    /// <summary>
    /// �����ͻ���
    /// </summary>
    /// <returns></returns>
    public IGridFSBucket CreateBucket(IMongoDatabase db)
    {
        var options = optionsMonitor.Get(Constant.ConfigName);
        return new GridFSBucket(db, options);
    }
}