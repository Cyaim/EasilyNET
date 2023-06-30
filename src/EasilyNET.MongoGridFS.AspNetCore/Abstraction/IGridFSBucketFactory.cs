using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace EasilyNET.MongoGridFS.AspNetCore;

/// <summary>
/// �����ӿ�
/// </summary>
internal interface IGridFSBucketFactory
{
    /// <summary>
    /// �����ͻ���
    /// </summary>
    /// <param name="db">Mongo���ݿ�</param>
    /// <returns></returns>
    IGridFSBucket CreateBucket(IMongoDatabase db);
}