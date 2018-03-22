using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ZhanJi : MonoBehaviour {

    public Button closeBtn;
    public ScrollRect scroView;

    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(PressedClosed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showBattle()
    {
        if (GameInfo.Instance.battleList == null || GameInfo.Instance.battleList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < GameInfo.Instance.battleList.Count; i++)
        {
            BattleData bd = GameInfo.Instance.battleList[i];
            GameObject battleBox = Instantiate(Resources.Load("Prefab/GameObject_Hall_ZhanJi_scrollBox")) as GameObject;
            battleBox.transform.SetParent(scroView.GetComponent<ScrollRect>().content, false);
            battleBox.GetComponent<GameObject_Hall_ZhanJi_scrollBox>().setInfo(bd);
        }

    }

    //关闭
    public void PressedClosed()
    {
        //foreach (Transform child in scroView.GetComponent<ScrollRect>().content.transform)
        //{

        //    Destroy(child.gameObject);

        //}
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
        Destroy(this.transform.gameObject);
    }
}
