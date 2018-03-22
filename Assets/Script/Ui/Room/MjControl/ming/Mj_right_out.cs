using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mj_right_out : MonoBehaviour {

    public Image bg;
    public Image pic;

    // Use this for initialization
    void Start () {
        Sprite sp = Resources.Load("Sprite/mj/you/you_out", typeof(Sprite)) as Sprite;
        bg.sprite = sp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPic(string mjId)
    {
        Sprite sp = Resources.Load("Sprite/mj/you/you_" + mjId, typeof(Sprite)) as Sprite;
        pic.sprite = sp;
    }
}
