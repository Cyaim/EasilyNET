using EasilyNET.Core.BaseType;
using FluentAssertions;

namespace EasilyNET.Test.Unit;

/// <summary>
/// ����ѩ��ID,��ʵ��MongoDB��ObjectId,������û��ʹ��Mongodb�������,��ȡѩ��ID��һ�ַ���.
/// </summary>
[TestClass]
public class SnowIdTest
{
    /// <summary>
    /// Truncate Test
    /// </summary>
    [TestMethod]
    public void TestSnowId()
    {
        // ��suffix���ȴ���ϣ������󳤶�
        var snow1 = SnowId.GenerateNewId();
        var snow2 = SnowId.GenerateNewId();
        var equal = snow1 == snow2 || snow1.Equals(snow2);
        equal.Should().BeFalse();
        var _2sub1 = snow2.CompareTo(snow1);
        _2sub1.Should().Be(1);
        var temp = snow1.ToString();
        snow1.Should().Be(SnowId.Parse(temp));
        Console.WriteLine(snow1.ToString());
    }
}