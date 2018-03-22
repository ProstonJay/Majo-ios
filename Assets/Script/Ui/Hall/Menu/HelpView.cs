using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpView : MonoBehaviour {

    public Button closeBtn;

	// Use this for initialization
	void Start () {
        closeBtn.onClick.AddListener(PressedClose);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //帮助
    public void PressedClose()
    {
        this.transform.gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }
}
