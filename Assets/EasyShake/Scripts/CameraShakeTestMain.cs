using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CameraShakeTestMain:MonoBehaviour
{
    public Button btn;

    public EasyCameraShakeCtrl camShakeCtrl;

    public void Awake()
    {
        //this._dots = new List<GameObject>();
        this.btn.onClick.AddListener(this.OnBtnClick);
    }

    private void OnBtnClick()
    {
        this.camShakeCtrl.Play();
    }
}
