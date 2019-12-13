using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EasyCameraShakeCtrl:MonoBehaviour
{
    public ShakeData positionX;
    public ShakeData positionY;
    public ShakeData positionZ;

    public ShakeData rotationX;
    public ShakeData rotationY;
    public ShakeData rotationZ;

    private Vector3Shake _shakeV3;
    private Vector3Shake _shakeRotV3;

    //private Shake _shakeRotX;
    //private Shake _shakeRotY;
    //private Shake _shakeRotZ;

    public GameObject shakeTarget;

    private bool _isPlaying;

    public bool RotationEnabled = true;
    public bool PositionEnabled = true;

    public void Play()
    {
        this._isPlaying = true;

        this._shakeV3 = new Vector3Shake(positionX, positionY, positionZ);
        this._shakeRotV3 = new Vector3Shake(rotationX, rotationY, rotationZ);

        //this._shakeV3.x.Loop = true;
        //this._shakeV3.x.ReSampleOnLoop = true;
        //this._shakeV3.y.Loop = true;
        //this._shakeV3.z.Loop = true;

        //this._shakeV3.z.Enabled = false;
        //this._shakeV3.x.Enabled = false;
        //this._shakeV3.y.Enabled = false;
    }

    public void Update()
    {
        if (!this._isPlaying)
        {
            return;
        }

        float dt = Time.deltaTime;
        this._shakeV3.Update(dt);
        this._shakeRotV3.Update(dt);

        if (this.PositionEnabled)
        {
            //float x = this._shakeV3.x.Amplitude();
            //Debug.Log("x:" + x);
            this.shakeTarget.transform.localPosition = this._shakeV3.Amplitude();
        }
        
        if (this.RotationEnabled)
        {
            this.shakeTarget.transform.localRotation = Quaternion.Euler(this._shakeRotV3.Amplitude());
        }
    }
}
