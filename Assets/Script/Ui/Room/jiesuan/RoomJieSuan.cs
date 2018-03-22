using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomJieSuan : MonoBehaviour {

    //小结算
    private int jiesuanRound;

    //总结算
    private int jiesuanEnd;

    private GameObject xiaojiesuan;
    public GameObject dajiesuan;

    void Awake()
    {
        GameEvent.JieSuanEvent += JieSuanEvent;
        GameEvent.ZongJieSuanEvent += ZongJieSuanEvent;
    }

    //结算
    private void JieSuanEvent()
    {
        jiesuanRound = 1;

    }

    //总结算
    private void ZongJieSuanEvent()
    {
        jiesuanEnd = 1;

    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (jiesuanRound > 0)
        {
            xiaojiesuan = Instantiate(Resources.Load("Prefab/GameObject_jiesuan_round")) as GameObject;
            xiaojiesuan.transform.SetParent(this.transform, false);
            xiaojiesuan.GetComponent<XiaoJieSuan>().setJieSuanData();
            jiesuanRound = 0;
        }

        if (jiesuanEnd > 0) 
        {
            dajiesuan = Instantiate(Resources.Load("Prefab/GameObject_jiesuan_gameOver")) as GameObject;
            dajiesuan.transform.SetParent(this.transform, false);
            dajiesuan.GetComponent<DaJieSuan>().showGameOver();
            jiesuanEnd = 0;
        }
    }
}
