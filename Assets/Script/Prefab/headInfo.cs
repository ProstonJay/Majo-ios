using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class headInfo : MonoBehaviour {

    public Image img_icon;
    public Text txt_name;
    public Image img_fangzhu;
    public Image img_zhuangjia;
    public Image img_gold;
    public Text txt_gold;
    public Image Img_offLine;

    private int IconId;

    // Use this for initialization
    void Start () {
        //img_fangzhu.gameObject.SetActive(false);
        //img_zhuangjia.gameObject.SetActive(false);
        //img_gold.gameObject.SetActive(false);
        //txt_gold.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetOffLine(bool boo)
    {
        Img_offLine.transform.gameObject.SetActive(boo);
        if (boo == true)
        {
            Sprite sp = Resources.Load("Sprite/head/toux1", typeof(Sprite)) as Sprite;
            img_icon.sprite = sp;
            GameEvent.DoMsgTipEvent("玩家"+ txt_name.text+ "断线了！");
        }
        else
        {
            Sprite sp = Resources.Load("Sprite/head/head_" + IconId.ToString(), typeof(Sprite)) as Sprite;
            img_icon.sprite = sp;
            GameEvent.DoMsgTipEvent("玩家" + txt_name.text + "上线了！");
        }
    }

    public void SetHeadIcon(int iconId)
    {
        IconId = iconId;
        Sprite sp = Resources.Load("Sprite/head/head_" + iconId.ToString(), typeof(Sprite)) as Sprite;
        img_icon.sprite = sp;
    }

    public void SetPlayerName(string str)
    {
        txt_name.text = str;
    }

    public void setPlayerGold(int value)
    {
        txt_gold.text = value.ToString();
    }

    public void setHost(int value)
    {
        if (value == 1)
        {
            img_fangzhu.gameObject.SetActive(true);
        }
        else
        {
            img_fangzhu.gameObject.SetActive(false);
        }
    }

    public void setZj(int value)
    {
        Debug.Log("设置庄家 传入位置="+ value+"当前庄家="+ GameInfo.Instance.zhuangjia);
        if (value == GameInfo.Instance.zhuangjia)
        {
            img_zhuangjia.transform.gameObject.SetActive(true);
        }
        else
        {
            img_zhuangjia.transform.gameObject.SetActive(false);
        }
    }
}
