using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ShakeGraphTestMain:MonoBehaviour
{
    public Button genBtn;
    public Button playBtn;

    public GameObject currentCube;

    public int DurationMilliSec;
    public int FrequencyInHz;
    public float Magnitude;
    public float YScale;
    public float XScale;

    private Shake _shake;

    private List<GameObject> _dots;

    public void Start()
    {
        this._dots = new List<GameObject>();
        this.genBtn.onClick.AddListener(this.OnBtnClick);
    }

    private void OnBtnClick()
    {
        this._shake = new Shake(this.DurationMilliSec, this.FrequencyInHz, this.Magnitude);

        this.ClearGraph();
        this.DrawGraph(this._shake,this.XScale,this.YScale);

        this._shake.Loop = true;
        this._shake.Reset();
    }

    private void ClearGraph()
    {
        int len = this._dots.Count;
        for (int i = 0; i < len; i++)
        {
            GameObject go = this._dots[i];
            GameObject.Destroy(go);
        }
        this._dots.Clear();
    }

    private void DrawGraph(Shake shake,float timeScale,float ampScale)
    {
        int milliSec = shake.Duration;
        List<float> samples = shake.Samples;

        int len = samples.Count;

        for (int i = 0; i < len; i++)
        {
            int slice = milliSec / len;

            int timeSlice = slice * (i + 1);

            GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dot.transform.localScale = Vector3.one * 1f;

            float x = (float)timeSlice / shake.Duration * timeScale;
            float y = shake.Amplitude(timeSlice) * ampScale;

            dot.transform.position = new Vector3(x, y, 0);
            this._dots.Add(dot);
        }
    }

    public void Update()
    {
        if (this._shake == null)
        {
            return;
        }
        float dt = Time.deltaTime;
        this._shake.Update(dt);
        float value = this._shake.Amplitude();

        float graphX = (float)this._shake.EclapsedTime / this._shake.Duration * this.XScale;
        float graphY = value* this.YScale;

        this.currentCube.transform.localPosition = new Vector3(graphX, graphY, 0);
    }

}
