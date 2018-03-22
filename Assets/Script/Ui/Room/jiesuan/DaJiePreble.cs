using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaJiePreble : MonoBehaviour {

    public Text playerName;
    public Text Txt_ID;
    public Text Txt_GangPai;
    public Text hupaiGangTxt;
    public Text dianpaoGangTxt;
    public Text goldTxt;
    public Text Txt_DeFen;



    public Image paoshou;
    public Image dayingjia;

    public Image headIcon;
 
    public Image fangzhu;

    public void setData(PlayerData pd)
    {
        playerName.text = pd.getPlayerName();
        Txt_ID.text = "ID:【 "+ pd .getUserId().ToString()+ " 】";
        Txt_GangPai.text = "x " + pd.getTotalGang().ToString();
        hupaiGangTxt.text = "x " + pd.getHuPaicCunt().ToString();
        dianpaoGangTxt.text = "x " + pd.getFangPaoCunt().ToString();
        goldTxt.text =  pd.getWinGold().ToString();
        int fen = pd.getWinGold() - 100000;
        if (fen > 0)
        {
            Txt_DeFen.text = "+" + fen.ToString();
        }
        else
        {
            Txt_DeFen.text = fen.ToString();
        }
      
        Sprite sp = Resources.Load("Sprite/head/head_" + pd.getPlayerIcon().ToString(), typeof(Sprite)) as Sprite;
        headIcon.sprite = sp;

        if (pd.getZhuangjia() == 1)
        {
            fangzhu.transform.gameObject.SetActive(true);
        }

    }

    public void setPaoShou()
    {
        paoshou.transform.gameObject.SetActive(true);
    }

    public void setDaYingJia()
    {
        dayingjia.transform.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
