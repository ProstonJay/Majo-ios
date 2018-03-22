using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QiutPanel : MonoBehaviour {

    public Button okBtn;
    public Button canceBtn;

    // Use this for initialization
    void Start () {
        okBtn.onClick.AddListener(okPressed);
        canceBtn.onClick.AddListener(cancePressed);
    }

    //确定
    public void okPressed()
    {
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        Application.Quit();
    }
    //取消
    public void cancePressed()
    {
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
        this.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
