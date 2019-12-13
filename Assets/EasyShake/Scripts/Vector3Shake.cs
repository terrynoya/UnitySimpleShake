using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Vector3Shake
{
    public Shake x;
    public Shake y;
    public Shake z;

    public Vector3Shake(ShakeData dataX,ShakeData dataY,ShakeData dataZ)
    {
        this.x = new Shake(dataX.Duration,dataX.Frequency,dataX.Magnitude);
        this.y = new Shake(dataY.Duration, dataY.Frequency, dataY.Magnitude);
        this.z = new Shake(dataZ.Duration, dataZ.Frequency, dataZ.Magnitude);
    }

    public void Start()
    {
        this.x.Reset();
        this.y.Reset();
        this.z.Reset();
    }

    public void Update(float dt)
    {
        this.x.Update(dt);
        this.y.Update(dt);
        this.z.Update(dt);
    }

    public Vector3 Amplitude()
    {
        float ampX = this.x.Amplitude();
        float ampY = this.y.Amplitude();
        float ampZ = this.z.Amplitude();

        return new Vector3(ampX,ampY,ampZ);
    }

    public bool IsFinished
    {
        get { return this.x.IsFinished && this.y.IsFinished && z.IsFinished; }
    }
}
