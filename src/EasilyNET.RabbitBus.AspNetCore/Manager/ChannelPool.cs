using System.Collections.Concurrent;
using EasilyNET.RabbitBus.AspNetCore.Abstraction;
using RabbitMQ.Client;

namespace EasilyNET.RabbitBus.AspNetCore.Manager;

internal sealed class ChannelPool(IConnection connection, uint poolCount) : IChannelPool
{
    private readonly ConcurrentBag<IChannel> _channels = [];
    private int _currentCount; // ʹ��ԭ�Ӽ����������ٳ��е�ͨ������

    private bool _disposed; // To detect redundant calls

    /// <inheritdoc />
    public async Task<IChannel> GetChannel()
    {
        if (!_channels.TryTake(out var channel)) return await connection.CreateChannelAsync();
        Interlocked.Decrement(ref _currentCount); // ��ȫ�ؼ��ټ���
        return channel;
    }

    /// <inheritdoc />
    public async Task ReturnChannel(IChannel channel)
    {
        // ��32��64λƽ̨��,��int���͵Ķ�ȡ��������ԭ�ӵ�,���Բ���ҪInterlocked.Read(ref _currentCount)
        if (_currentCount >= poolCount)
        {
            await channel.CloseAsync();
            channel.Dispose();
        }
        else
        {
            _channels.Add(channel);
            Interlocked.Increment(ref _currentCount); // ��ȫ�����Ӽ���
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        foreach (var channel in _channels)
        {
            try
            {
                channel.Dispose();
            }
            catch
            {
                // Log the exception or handle it as needed.
            }
        }
        _disposed = true;
    }
}