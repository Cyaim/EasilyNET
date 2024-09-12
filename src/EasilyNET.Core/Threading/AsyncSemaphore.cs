using System.Collections.Concurrent;

namespace EasilyNET.Core.Threading;

/// <summary>
/// �첽�źš�
/// </summary>
internal sealed class AsyncSemaphore
{
    private static readonly Task _completed = Task.FromResult(true);
    private readonly ConcurrentQueue<TaskCompletionSource<bool>> _waiters = new();
    private int _isTaken;

    /// <summary>
    /// ��ȡ�Ƿ�ռ��
    /// </summary>
    /// <returns></returns>
    public int GetTaken() => _isTaken;

    /// <summary>
    /// ��ȡ�ȴ�����������
    /// </summary>
    /// <returns></returns>
    public int GetQueueCount() => _waiters.Count;

    /// <summary>
    /// �첽�ȴ�
    /// </summary>
    /// <returns></returns>
    public Task WaitAsync()
    {
        // ��� _isTaken ��ֵ�� 0����������Ϊ 1��������һ������ɵ�����
        if (Interlocked.CompareExchange(ref _isTaken, 1, 0) == 0)
        {
            return _completed;
        }
        // ��� _isTaken ��ֵ���� 0������һ���µ� TaskCompletionSource<bool>������������Ϊ�첽���С�
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        // �� TaskCompletionSource<bool> ʵ����ӵ��ȴ������С�
        _waiters.Enqueue(tcs);
        // ���� TaskCompletionSource<bool> ������
        return tcs.Task;
    }

    /// <summary>
    /// �ͷ�
    /// </summary>
    public void Release()
    {
        if (_waiters.TryDequeue(out var toRelease))
        {
            toRelease.SetResult(true);
        }
        else
        {
            Interlocked.Exchange(ref _isTaken, 0);
        }
    }
}