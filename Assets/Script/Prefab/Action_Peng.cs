using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action_Peng : MonoBehaviour {

    public Image mj1;
    public Image mj2;
    public Image mj3;

    //明杠
    public Image mingBg;
    public Image mingPic;

    private int mjId;

    //type 1碰,2杠
    public void setPic(string path,int type)
    {
        Sprite sp = Resources.Load("Sprite/mj/" + path, typeof(Sprite)) as Sprite;
        mj1.sprite = sp;
        mj2.sprite = sp;
        mj3.sprite = sp;
        mingPic.sprite = sp;
        if (type == 1)
        {
            mingBg.gameObject.SetActive(false);
            mingPic.gameObject.SetActive(false);
        }
        else
        {
            mingBg.gameObject.SetActive(true);
            mingPic.gameObject.SetActive(true);
        }

    }

    public void setMjid(int mjid)
    {
        mjId = mjid;
    }

    public int getMjid()
    {
        return mjId;
    }

    //转换成明杠
    public void changToMingGang()
    {
        mingBg.gameObject.SetActive(true);
        mingPic.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
