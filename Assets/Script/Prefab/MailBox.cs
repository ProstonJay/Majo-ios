using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailBox : MonoBehaviour {

    public Text timeText;

    public Text gidText;
    public Text fkText;
    public Image zs;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTxt(MailData md)
    {
        string str = "";
        if (md.getStype() ==1)
        {
            str = "<color=#13812BFF>"+ md .getDataTime()+ " </color>  玩家            赠送了您   ";
        }
        else
        {
            str = "<color=#13812BFF>" + md.getDataTime()+ " </color>  您赠送了                给玩家  ";
            gidText.transform.localPosition = new Vector3(300, 0, 0);
            fkText.transform.localPosition = new Vector3(70, 0, 0);
            zs.transform.localPosition = new Vector3(135, 0, 0);
        }
        timeText.text = str;
        gidText.text = md.getGid().ToString();
        fkText.text = md.getFk().ToString(); ;
    }
}
