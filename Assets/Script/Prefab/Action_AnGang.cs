using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action_AnGang : MonoBehaviour {

    public Image mj;

    private int mjId;

    public void setPic(string path)
    {
        Sprite sp = Resources.Load("Sprite/mj/" + path, typeof(Sprite)) as Sprite;
        mj.sprite = sp;
    }

    public void setMjid(int mjid)
    {
        mjId = mjid;
    }

    public int getMjid()
    {
        return mjId;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
