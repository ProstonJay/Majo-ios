using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mj_my_out : MonoBehaviour {

    public Image bg;
    public Image pic;

    // Use this for initialization
    void Start () {
        Sprite sp = Resources.Load("Sprite/mj/xia_w/xiao_banzi", typeof(Sprite)) as Sprite;
        bg.sprite = sp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPic(string mjname)
    {
        Sprite sp = Resources.Load("Sprite/mj/xia_w/xiao_" + mjname, typeof(Sprite)) as Sprite;
        pic.sprite = sp;
    }
}
