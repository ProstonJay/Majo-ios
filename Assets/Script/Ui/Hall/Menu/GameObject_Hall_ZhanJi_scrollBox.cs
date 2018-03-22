using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObject_Hall_ZhanJi_scrollBox : MonoBehaviour {

    public Text Text_Info;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //
    public void setInfo(BattleData bd)
    {
        Text_Info.text = "<color=#A72F2FFF>" + bd.GetDataTime() + "  </color>" + bd.GetRoomRule();
        int len = bd.GetPlayerList().Count;
        for (int i = 0; i < len; i++)
        {
            Player p = bd.GetPlayerList()[i];
            if (p.GetGid() >0)
            {
                GameObject playerBox = Instantiate(Resources.Load("Prefab/GameObject_Hall_ZhanJi_scrollBox_player")) as GameObject;
                playerBox.transform.SetParent(this.transform, false);
                playerBox.transform.localPosition = new Vector3(-300 + i * 200, 30, 0);
                playerBox.GetComponent<GameObject_Hall_ZhanJi_scrollBox_player>().setPlayerInfo(p);
            }
        }
    }
}
