using cn.sharesdk.unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyView : MonoBehaviour {

    public Image img_my;
    public Image img_right;
    public Image img_top;
    public Image img_left;

    public Text Txt_roomInfo;
    public Button Btn_Invet;
    public Button Btn_Ready;
    //邀请 
    public ShareSDK ssdk;

    public GameObject obj_my;
    public GameObject obj_right;
    public GameObject obj_top;
    public GameObject obj_left;

    private int newplayerPos = 0;
    private string readyPos = "";

    void Awake()
    {
        GameEvent.PlayerEnterRoomEvent += PlayerEnterRoom;
        RoomEvent.PlayerReadyEvent += PlayerReadyEvent;
    }

    //玩家准备
    private  void PlayerReadyEvent(string pos)
    {
        readyPos = pos;
    }
    //玩家进入
    private void PlayerEnterRoom(int pos ,string name)
    {

        newplayerPos = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (newplayerPos > 0)
        {
            addNewPlayer();
            newplayerPos = 0;
        }
        if (readyPos != "")
        {
            PlayerGetReady();
            readyPos = "";
        }
    }

    private void addNewPlayer()
    {
        switch (GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, newplayerPos))
        {
            case "right":
                obj_right.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.rightIcon)); ;
                obj_right.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.rightName);
                break;
            case "top":
                obj_top.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.topIcon));
                obj_top.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.topName);
                break;
            case "left":
                obj_left.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.leftIcon));
                obj_left.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.leftName);
                break;
        }
    }

    private void PlayerGetReady()
    {
        switch (readyPos)
        {
            case "bot":
                img_my.gameObject.SetActive(true);
                break;
            case "right":
                img_right.gameObject.SetActive(true);
                break;
            case "top":
                img_top.gameObject.SetActive(true);
                break;
            case "left":
                img_left.gameObject.SetActive(true);
                break;
        }
    }

        // Use this for initialization
    void Start () {
        Btn_Invet.onClick.AddListener(InvertPresss);
        Btn_Ready.onClick.AddListener(PresssBtn_Ready);
        //截屏分享
        ssdk = GetComponent<ShareSDK>();
        ssdk.shareHandler += ShareResultHandler;
    }
    //初始化准备界面数据
    public void InitReadyInfo()
    {
        Txt_roomInfo.text = "房号: " + GameInfo.Instance.roomId.ToString() + "  密码: " + GameInfo.Instance.roomPassWord.ToString();

        //刷新自己座位信息
        obj_my.GetComponent<headInfo>().SetHeadIcon(GameInfo.Instance.UserIcon);
        obj_my.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.UserName);
        //再刷新其他座位信息
        if (GameInfo.Instance.rightPositon > 0)
        {
            obj_right.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.rightIcon));
            obj_right.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.rightName);
            if (GameInfo.Instance.isRightReady == 1)
            {
                img_right.gameObject.SetActive(true);
            }
        }
        if (GameInfo.Instance.topPostion > 0)
        {
            obj_top.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.topIcon));
            obj_top.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.topName);
            if (GameInfo.Instance.isTopReady == 1)
            {
                img_top.gameObject.SetActive(true);
            }
        }
        if (GameInfo.Instance.leftPostion > 0)
        {
            obj_left.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.leftIcon));
            obj_left.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.leftName);
            if (GameInfo.Instance.isLeftReady == 1)
            {
                img_left.gameObject.SetActive(true);
            }
        }
    }

    //准备
    private void PresssBtn_Ready()
    {
        Btn_Ready.gameObject.SetActive(false);
        //
        SocketModel ReadyRequset = new SocketModel();
        ReadyRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ReadyRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_READY);
        ReadyRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        ReadyRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ReadyRequset);//发送这条消息给服务器
    }
    //重置
    public void reset()
    {
        img_my.gameObject.SetActive(false);
        img_right.gameObject.SetActive(false);
        img_top.gameObject.SetActive(false);
        img_left.gameObject.SetActive(false);
        //
        Btn_Ready.gameObject.SetActive(true);
        if (GameInfo.Instance.isGameStart == 0)
        {
            Btn_Invet.gameObject.SetActive(true);
        }
        else
        {
            Btn_Invet.gameObject.SetActive(false);
        }

    }


    void InvertPresss()
    {
        ShareContent content = new ShareContent();
        //content.SetTitle("【开心麻将馆】");
        string roomiD = GameInfo.Instance.roomId.ToString();
        string pw = "无密码";
        if (GameInfo.Instance.roomPassWord > 0)
        {
            pw = GameInfo.Instance.roomPassWord.ToString();
        }
        string maxPoint = GameInfo.Instance.maxPoint.ToString();
        string maxRound = GameInfo.Instance.maxRound.ToString();
        string zjDouble = "";
        if (GameInfo.Instance.isZjDouble == 1)
        {
            zjDouble = "庄翻倍";
        }
        string isQiangGang = "";
        if (GameInfo.Instance.canQiangGang == 1)
        {
            isQiangGang = "可抢杠";
        }
        string isDaiGen = "";
        if (GameInfo.Instance.isDaiGen == 1)
        {
            isDaiGen = "带根";
        }
        string isZimoHu = "";
        if (GameInfo.Instance.isZiMoHu == 1)
        {
            isZimoHu = "自摸胡";
        }
        content.SetText("【房号:" + roomiD + " 密码:" + pw + "】" + maxRound + "局," + maxPoint + "封顶," + zjDouble+"," + isQiangGang + "," + isDaiGen + "," + isZimoHu+ "■■■四川大众麻将■■■");
        //content.SetComment("快来人，三缺一啦");
        content.SetShareType(ContentType.Text);
        //ssdk.ShowPlatformList(null, content, 100, 100);
        ssdk.ShowShareContentEditor(PlatformType.WeChat, content);
    }

    void ShareResultHandler(int repID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            GameEvent.DoMsgTipEvent("分享成功");
        }
        //失败  
        else if (state == ResponseState.Fail)
        {
            GameEvent.DoMsgTipEvent("分享失败");
        }
        //关闭  
        else if (state == ResponseState.Cancel)
        {
            GameEvent.DoMsgTipEvent("分享取消");
        }
    }

}
