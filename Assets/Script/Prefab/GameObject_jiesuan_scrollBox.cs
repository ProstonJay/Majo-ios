using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObject_jiesuan_scrollBox : MonoBehaviour {

    public Text Text_Name;
    public Text Text_Fan;
    public Text Text_Gold;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setData(JsData jd)
    {
        Text_Fan.text = jd.getFunCount().ToString() + "番";
        int gold = jd.getGold();
        if (gold > 0)
        {
            Text_Gold.text = "+"+jd.getGold().ToString();
        }
        else
        {
            Text_Gold.text = jd.getGold().ToString();
        }
 
        string typeStr = Helper_JieSuan.getTypeToStr(jd.getJtype(),jd.getPaiXing(),jd.getZj());
        Text_Name.text = typeStr;
    }
}
