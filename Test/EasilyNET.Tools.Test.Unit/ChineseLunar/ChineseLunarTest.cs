using EasilyNET.Extensions;
using FluentAssertions;

namespace EasilyNET.Tools.Test.Unit.ChineseLunar;

public class ChineseLunarTest
{
    [Fact]
    public void ChineseLunar()
    {
        var date = "1994-11-15".ToDateTime();
        Lunar.Init(date);
        Lunar.Constellation.Should().Be("��Ы��");
        Lunar.Animal.Should().Be("��");
        Lunar.ChineseLunar.Should().Be("һ�ž�����ʮ��ʮ��");
    }
}