using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Left_out_Bin : MonoBehaviour {


    public void setPic(string mjId)
    {
        Image icon = transform.Find("Image_pic").GetComponent<Image>();
        Sprite sp = Resources.Load("Sprite/mj/zuo/zuo_" + mjId, typeof(Sprite)) as Sprite;
        icon.sprite = sp;

        AudioMgr.Instance.SoundPlay("chuPai", 1f, 2);

        Debug.Log("出牌配音路径" + MajooUtil.getChuPaiViocePath(mjId));
        AudioMgr.Instance.SoundPlay(MajooUtil.getChuPaiViocePath(mjId), 1, 0);
    }

    // Use this for initialization
    void Start()
    {

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
