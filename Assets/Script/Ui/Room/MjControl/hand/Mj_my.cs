using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mj_my : MonoBehaviour , IPointerClickHandler
{
    public string mjId;
    public string state = "down";
    public Image bg;
    public Image pic;

    Transform root;
    // Use this for initialization
    void Start () {

        Sprite sp = Resources.Load("Sprite/mj/xia/z_banzi", typeof(Sprite)) as Sprite;
        bg.sprite = sp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPic(string str)
    {
        Sprite sp = Resources.Load("Sprite/mj/xia/z_" + str, typeof(Sprite)) as Sprite;
        pic.sprite = sp;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("连击次数" + eventData.clickCount);
        //Debug.Log("鼠标位置" + eventData.position);
        SendMessageUpwards("SelectCard", this.name);
        AudioMgr.Instance.SoundPlay("xuanPai", 1f, 2);
    }

    public void setUp() {
        Vector3 locpV3 = this.transform.localPosition;
        locpV3.y = 50;
        this.transform.localPosition = locpV3;
        this.state = "up";
    }

    public void setDown()
    {
        Vector3 locpV3 = this.transform.localPosition;
        locpV3.y = 0;
        this.transform.localPosition = locpV3;
        this.state = "down";
    }

    public void ReSetX(int value)
    {
        Vector3 locpV3 = this.transform.localPosition;
        locpV3.x= value*-83;
        this.transform.localPosition = locpV3;
    }
}
