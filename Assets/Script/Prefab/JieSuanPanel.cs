using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JieSuanPanel : MonoBehaviour {

    public Image Img_Head;
    public Image Img_Zhuang;
    public Text Txt_Name;
    public Text Txt_Gold;
    public ScrollRect scroView;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initData(PlayerData pd,int pos=0)
    {
        //头像
        Sprite sp = Resources.Load("Sprite/head/head_" + pd.getPlayerIcon().ToString(), typeof(Sprite)) as Sprite;
        Img_Head.sprite = sp;
        //昵称
        Txt_Name.text = pd.getPlayerName();
        //金币
        int gold = pd.getWinGold();
        if (gold > 0)
        {
            Txt_Gold.text = "<color=#66F0E5FF> +" + gold.ToString() + "</color>";
        }
        else
        {
            Txt_Gold.text = "<color=#6AF066FF> " + gold.ToString() + "</color>";
        }
        //庄家
        if (pd.getZhuangjia() == 1)
        {
            Img_Zhuang.transform.gameObject.SetActive(true);
        }
        //如果是自己，输赢明细
        if (pos == 1)
        {
            List<JsData> jsList = pd.getJsList();
            if (jsList == null)
            {
                return;
            }
            for (int i = 0; i < jsList.Count; i++)
            {
                JsData jsData = jsList[i];
                GameObject jsBox = Instantiate(Resources.Load("Prefab/GameObject_jiesuan_scrollBox")) as GameObject;
                jsBox.GetComponent<GameObject_jiesuan_scrollBox>().setData(jsData);
                jsBox.transform.SetParent(scroView.GetComponent<ScrollRect>().content, false);
            }
        }
    }

    private void setDetail()
    {
        //if (GameInfo.Instance.mailList == null)
        //{
        //    return;
        //}
        //for (int i = 0; i < GameInfo.Instance.mailList.Count; i++)
        //{
        //    MailData md = GameInfo.Instance.mailList[i];
        //    GameObject mailBox = Instantiate(Resources.Load("Prefab/GameObject_mailBox")) as GameObject;
        //    mailBox.GetComponent<MailBox>().setTxt(md);
        //    mailBox.transform.SetParent(scroView.GetComponent<ScrollRect>().content, false);
        //}
    }
}
