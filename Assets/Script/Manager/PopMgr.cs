using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopMgr : MonoBehaviour {
    [SerializeField]
    public Msg Msg;//警告窗口  
    public string MsgTxt;
    [SerializeField]
    private Warning window;   //信息提示窗口                          
    public  List<String> errors = new List<String>(); //存放所有信息Model的列表  

    //通信遮罩组件
    public GameObject netSocketMask;
    //通信遮罩组件开关
    private int netKye;

    // Use this for initialization
    void Awake() {
        GameEvent.MsgEvent += Do;
        GameEvent.MsgTipEvent += TipEvent;
        GameEvent.NetSocketEvent += NetSocketEvent;
    }

    private void NetSocketEvent(int value)
    {
        netKye = value;
    }

    private void TipEvent(string message)
    {
        errors.Add(message);
    }

    private void Do(string message)
    {
        //Msg.setTxt(message);
        MsgTxt = message;
    }



    // Update is called once per frame
    void Update () {
        if (errors.Count > 0)
        {
            //取出列表的第一个  
            String err = errors[0];
            //然后删除  
            errors.RemoveAt(0);
            //最后显示  
            window.active(err);
        }
        if (MsgTxt != "")
        {
            Msg.setTxt(MsgTxt);
            MsgTxt = "";
        }

        if(netKye>0)
        {
            Boolean boo;
            if (netKye == 1) { boo = true; } else { boo = false; };
            showNetMask(boo);
            netKye = 0;
        }
    }

    private void showNetMask(Boolean boo)
    {
        netSocketMask.GetComponent<NetSocketMask>().showUp(boo);
    }

    void Start()
    {


    }


}
