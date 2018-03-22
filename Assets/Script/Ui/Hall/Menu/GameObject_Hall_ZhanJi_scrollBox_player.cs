using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObject_Hall_ZhanJi_scrollBox_player : MonoBehaviour {

    public Image Img_headIcon;
    public Text Text_Name;
    public Text Text_Id;
    public Text Text_Gold;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPlayerInfo(Player pl)
    {
        Sprite sp = Resources.Load("Sprite/head/head_" + pl.GetPlyerIcon().ToString(), typeof(Sprite)) as Sprite;
        Img_headIcon.sprite = sp;

        Text_Name.text = pl.GetPlyerName();
        Text_Id.text = "ID: "+pl.GetGid().ToString();
        int gold = pl.GetPlyerDenFen();
        if (gold >= 0)
        {
            Text_Gold.text = "<color=#8D273EFF>" + "+" + gold.ToString() + "  </color>";
        }
        else
        {
            Text_Gold.text = "<color=#106F11FF>" + gold.ToString() + "  </color>";
        }
    }
}
