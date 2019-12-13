using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class ShakeData
{
    /// <summary>
    /// 震动频率，hz，1秒内震动几下
    /// </summary>
    public int Frequency;

    /// <summary>
    /// 震动时间长度，ms毫秒
    /// </summary>
    public int Duration;

    /// <summary>
    /// 振幅大小
    /// </summary>
    public float Magnitude;

    public ShakeData(int frequency = 20, int duration = 1000, float magnitude = 1f)
    {
        this.Frequency = frequency;
        this.Duration = duration;
        this.Magnitude = magnitude;
    }
}
