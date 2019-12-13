using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Shake
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

    /// <summary>
    /// sample point range from [-1, 1]
    /// </summary>
    private List<float> _samples;

    private int _t;

    private bool _enabled;
    private bool _isFinished;
    private bool _isLoop;
    private bool _reSampleOnLoop;

    public Action OnFinish;

    public Shake(int duration=1000, int frequency=20,float magnitude =1f)
    {
        this._enabled = true;
        this._samples = new List<float>();
        this.SetData(duration, frequency, magnitude);
    }

    public bool IsFinished
    {
        get
        {
            if (!this.Enabled)
            {
                return true;
            }
            return this._isFinished;
        }
    }

    public bool ReSampleOnLoop
    {
        get { return this._reSampleOnLoop; }
        set { this._reSampleOnLoop = value; }
    }

    public bool Loop
    {
        get { return this._isLoop; }
        set { this._isLoop = value; }
    }

    public bool Enabled
    {
        get { return this._enabled; }
        set { this._enabled = value;}
    }

    public void SetData(int duration, int frequency, float magnitude)
    {
        this.Duration = duration;
        this.Frequency = frequency;
        this.Magnitude = magnitude;
        this.Sample();
    }

    public int EclapsedTime
    {
        get { return this._t; }
    }

    public List<float> Samples
    {
        get { return this._samples; }
    }

    private void Sample()
    {
        this._samples.Clear();
        float sec = (float) this.Duration/1000;
        int sampleCount = Mathf.RoundToInt(sec * this.Frequency);
        for (int i = 0; i < sampleCount; i++)
        {
            float value = UnityEngine.Random.Range(-1f, 1f);
            this._samples.Add(value);
        }
    }

    public void Reset()
    {
        this._isFinished = false;
        this._t = 0;
    }

    /// <summary>
    /// Update the shake, setting the current time variable
    /// </summary>
    public void Update(float dt)
    {
        if (!this._enabled)
        {
            return;
        }
        if (this._isFinished)
        {
            return;
        }
        this._t += Mathf.FloorToInt(dt*1000);
        if (this._t >= this.Duration)
        {
            this._t = this.Duration;
            if (this._isLoop)
            {
                this._t = 0;
                if (this._reSampleOnLoop)
                {
                    this.Sample();
                }
            }
            else
            {
                this.Finish();
            }
        }
    }

    private void Finish()
    {
        if (this._isFinished)
        {
            return;
        }
        this._isFinished = true;
        if (this.OnFinish == null)
        {
            return;
        }
        this.OnFinish();
    }

    public float Amplitude(int t)
    {
        float k = this.Decay(t);

        float s = (float)t / 1000 * this.Frequency;
        int s0 = Mathf.FloorToInt(s);
        int s1 = s0 + 1;
        float rlt = (this.Noise(s0) + (s - (float) s0)*(this.Noise(s1) - this.Noise(s0)))*k*this.Magnitude;
        return rlt;
    }

    public float Amplitude()
    {
        if (!this._enabled)
        {
            return 0;
        }
        int t = this._t;
        return this.Amplitude(t);
    }

    private float Noise(int s)
    {
        if (this._samples.Count == 0)
        {
            return 0;
        }
        if (s >= this._samples.Count)
        {
            return 0;
        }
        return this._samples[s];
    }

    public float Decay(float t)
    {
        if (t >= this.Duration)
        {
            return 0;
        }
        return (this.Duration - t)/this.Duration;
    }
}
