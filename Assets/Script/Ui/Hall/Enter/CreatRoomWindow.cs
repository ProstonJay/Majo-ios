using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatRoomWindow : MonoBehaviour {

    public Button CreatBtn;
    public Button CloseBtn;

    public Toggle J2Toggle;
    public Toggle J3Toggle;
    public Toggle J4Toggle;

    public Toggle F12Toggle;
    public Toggle F24Toggle;
    public Toggle F36Toggle;

    public Toggle Toggle_ZhuangDoubel;
    public Toggle Toggle_CanQiangGang;
    public Toggle Toggle_DaiGen;
    public Toggle Toggle_QueMen;

    private int MaxRound;
    private int MaxPoint;
    public int ZhuangjiaDouble;
    public int QiangGang;
    public int DaiGen;
    public int ZiMoHu;

    public InputField inPut_PassWord;

    // Use this for initialization
    void Start () {
        //
        MaxRound = 12;
        MaxPoint = 24;

        ZhuangjiaDouble = 1;
        QiangGang = 1;
        DaiGen = 1;
        ZiMoHu = 0;

        int vid = UnityEngine.Random.Range(1, 9) * 111;
        inPut_PassWord.text = vid.ToString();
        //
        CreatBtn.onClick.AddListener(CreatPressed);
        CloseBtn.onClick.AddListener(ClosePressed);

        J2Toggle.onValueChanged.AddListener((bool value) => OnJuClick(J2Toggle, value));
        J3Toggle.onValueChanged.AddListener((bool value) => OnJuClick(J3Toggle, value));
        J4Toggle.onValueChanged.AddListener((bool value) => OnJuClick(J4Toggle, value));

        F12Toggle.onValueChanged.AddListener((bool value) => OnFanClick(F12Toggle, value));
        F24Toggle.onValueChanged.AddListener((bool value) => OnFanClick(F24Toggle, value));
        F36Toggle.onValueChanged.AddListener((bool value) => OnFanClick(F36Toggle, value));


        Toggle_ZhuangDoubel.onValueChanged.AddListener((bool value) => OnZhuangJiaClick(Toggle_ZhuangDoubel, value));
        Toggle_CanQiangGang.onValueChanged.AddListener((bool value) => OnqiangGangClick(Toggle_CanQiangGang, value));
        Toggle_DaiGen.onValueChanged.AddListener((bool value) => OnbuDaiGenClick(Toggle_DaiGen, value));
        Toggle_QueMen.onValueChanged.AddListener((bool value) => OnbuQueMenClick(Toggle_QueMen, value));
    }


    //庄家加倍
    public void OnZhuangJiaClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            this.ZhuangjiaDouble = 1;
        }
        else
        {
            this.ZhuangjiaDouble = 0;
        }
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
    }

    //可抢杠胡
    public void OnqiangGangClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            this.QiangGang = 1;
        }
        else
        {
            this.QiangGang = 1;
        }
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
    }

    //带根
    public void OnbuDaiGenClick(Toggle toggle, bool ison)
    {    
        if (ison)
        {
            this.DaiGen = 1;
        }
        else
        {
            this.DaiGen = 0;
        }
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
    }

    //自摸胡
    public void OnbuQueMenClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            this.ZiMoHu = 1;
        }
        else
        {
            this.ZiMoHu = 0;
        }
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
    }

    //局数
    public void OnJuClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
            switch (toggle.name)
            {
                case "2ju":
                    MaxRound = 12;
                    break;
                case "3ju":
                    MaxRound = 24;
                    break;
                case "4ju":
                    MaxRound = 36;
                    break;
                default:
                    break;
            }
        }
    }

    //番数
    public void OnFanClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
            switch (toggle.name)
            {
                case "12fan":
                    MaxPoint = 12;
                    break;
                case "24fan":
                    MaxPoint = 24;
                    break;
                case "36fan":
                    MaxPoint = 36;
                    break;
                default:
                    break;
            }
        }
    }

    void CreatPressed()
    {
        Debug.Log("开房需要" + MaxRound / 12 + "钻石"+"密码="+ inPut_PassWord.text);
        if (GameInfo.Instance.UserFK <0|| GameInfo.Instance.UserFK < MaxRound/12)
        {
            GameEvent.DoMsgEvent("房卡不足,请联系管理员!");
            return;
        }
        SocketModel CreatRoomRequset = new SocketModel();
        CreatRoomRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);//消息的类型为Login
        CreatRoomRequset.SetSubCmd(ProtocolSC.Sub_Cmd_CreatRoomRqs);//动作为请求登录
        CreatRoomRequset.SetCommand(0);
        List<int> list = new List<int>();
        list.Add(MaxRound);
        list.Add(MaxPoint);
        list.Add(ZhuangjiaDouble);
        list.Add(QiangGang);
        list.Add(DaiGen);
        list.Add(ZiMoHu);
        list.Add(int.Parse(inPut_PassWord.text));
        CreatRoomRequset.SetData(list);
        NettySocket.Instance.SendMsg(CreatRoomRequset);//发送这条消息给服务器
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    void ClosePressed()
    {
        Destroy(this.transform.gameObject);
        AudioMgr.Instance.SoundPlay("btnClose", 2f, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

