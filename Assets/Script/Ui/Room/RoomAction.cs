using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomAction : MonoBehaviour {

    public GameObject BotOutMj;
    public GameObject RightOutMj;
    public GameObject TopOutMj;
    public GameObject LeftOutMj;

    public GameObject actMask;
    public Button PengBtn;
    public Button GangBtn;
    public Button HuBtn;
    public Button GuoBtn;
    //

    private string posStr = "";
    private string mjStr = "";

    //胡牌类型
    private int huType = 0;
    //
    //本轮可操作类型列表(由服务器推送)
    private List<int> actlist = new List<int>();

    //自摸动画
    private string zimoPos;
    private int zimoMj;
    //吃胡动画
    private int fangpaoPos;

    void Awake()
    {
        RoomEvent.ActionListEvent += ActionListEvent;
        RoomEvent.ZiMoEvent += ZiMoEvent;
        RoomEvent.ChiHuEvent += ChiHuEvent;
    }

    //本轮可操作类型列表
    private void ActionListEvent(List<int> list)
    {
        actlist = list;  
    }

    //自摸
    private void ZiMoEvent(string pos, int mj, List<PlayerData> list)
    {
        zimoPos = pos;
        zimoMj = mj;
    }

    //吃胡
    private void ChiHuEvent(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        this.fangpaoPos = fangpao;

    }

    void Update()
    {
   
        if (actlist!=null)
        {
            int count = actlist.Count;
            if (count > 0)
            {
                doAction(actlist);
                actlist.Clear();
            }

        }
        //吃胡
        if (fangpaoPos > 0)
        {
            string tmPos = "";
            if (fangpaoPos == GameInfo.Instance.positon)
            {
                tmPos = "bot";
            }
            else
            {
                tmPos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, fangpaoPos);
            }
            playZimoAnimotor(tmPos);
            fangpaoPos = 0;
        }
        //自摸
        if (zimoMj > 0)
        {
            playZimoAnimotor(zimoPos);
            int inps = 0;
            switch (zimoPos)
            {
                case "bot":
                    inps = GameInfo.Instance.positon;
                    break;
                case "right":
                    inps = GameInfo.Instance.rightPositon;
                    break;
                case "top":
                    inps = GameInfo.Instance.topPostion;
                    break;
                case "left":
                    inps = GameInfo.Instance.leftPostion;
                    break;
            }
            showChuPai(inps, zimoMj,true);
            zimoPos = "";
            zimoMj = 0;
        }
    }

    private void playZimoAnimotor(string pos)
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_huLight")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        //ani.GetComponent<GameAnimator>().Play();
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(0, 50, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(180, 150, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(0, 300, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-180, 150, 0);
                break;
        }
    }

    //显示本轮可操作类型
    public void doAction(List<int> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            switch (list[i])
            {
                case CardView.CHI_PENG_碰牌:
                    PengBtn.gameObject.SetActive(true);
                    break;
                case CardView.CHI_GANG_杠牌:
                case CardView.CHI_MINGGANG_明杠:
                case CardView.CHI_ANGANG_暗杠:
                    GangBtn.gameObject.SetActive(true);
                    break;
                case CardView.CHI_HU_糊牌:
                    huType = CardView.CHI_HU_糊牌;
                    HuBtn.gameObject.SetActive(true);
                    break;
                case CardView.CHI_GANG_抢杠:
                    huType = CardView.CHI_GANG_抢杠;
                    HuBtn.gameObject.SetActive(true);
                    break;
                case CardView.CHI_ZIMO_自摸:
                    huType = CardView.CHI_ZIMO_自摸;
                    HuBtn.gameObject.SetActive(true);
                    break;
            }
        }
        GuoBtn.gameObject.SetActive(true);
        actMask.SetActive(true);
    }

    public void showChuPai(int pos, int mj,bool isKeep)
    {
        if (pos == GameInfo.Instance.positon)
        {
            BotOutMj.GetComponent<My_out_Bin>().setPic(mj.ToString());
            BotOutMj.gameObject.SetActive(true);
            RightOutMj.gameObject.SetActive(false);
            TopOutMj.gameObject.SetActive(false);
            LeftOutMj.gameObject.SetActive(false);
            posStr = "bot";
        }
        else
        {
            switch (GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pos))
            {
                case "right":
                    RightOutMj.GetComponent<Right_out_Bin>().setPic(mj.ToString());
                    BotOutMj.gameObject.SetActive(false);
                    RightOutMj.gameObject.SetActive(true);
                    TopOutMj.gameObject.SetActive(false);
                    LeftOutMj.gameObject.SetActive(false);
                    posStr = "right";
                    break;
                case "top":
                    TopOutMj.GetComponent<Top_out_Bin>().setPic(mj.ToString());
                    BotOutMj.gameObject.SetActive(false);
                    RightOutMj.gameObject.SetActive(false);
                    TopOutMj.gameObject.SetActive(true);
                    LeftOutMj.gameObject.SetActive(false);
                    posStr = "top";
                    break;
                case "left":
                    LeftOutMj.GetComponent<Left_out_Bin>().setPic(mj.ToString());
                    BotOutMj.gameObject.SetActive(false);
                    RightOutMj.gameObject.SetActive(false);
                    TopOutMj.gameObject.SetActive(false);
                    LeftOutMj.gameObject.SetActive(true);
                    posStr = "left";
                    break;
            }
        }
        mjStr = mj.ToString();
        if (isKeep == false)//如果是FALSE ,是没人会碰,吃操作,1秒以后通知打牌区添加这张牌
        {
            hideall();
        }
    }

    //清除MJ而且添加到出牌
    public void hideall()
    {
        StartCoroutine(RmoveOutMj());
    }

    private IEnumerator RmoveOutMj()
    {
        yield return new WaitForSeconds(1f);
        BotOutMj.gameObject.SetActive(false);
        RightOutMj.gameObject.SetActive(false);
        TopOutMj.gameObject.SetActive(false);
        LeftOutMj.gameObject.SetActive(false);
        addOutMj();
    }

    public void addOutMj()
    {
        if (posStr != "")
        {
            GameEvent.DoPlayEvent(posStr, mjStr);
            posStr = "";
            mjStr = "";
        }
    }
    //只清除牌 不添加出牌
    public void clearOutMj()
    {
        BotOutMj.gameObject.SetActive(false);
        RightOutMj.gameObject.SetActive(false);
        TopOutMj.gameObject.SetActive(false);
        LeftOutMj.gameObject.SetActive(false);
        posStr = "";
        mjStr = "";
    }

    public void reset()
    {
        BotOutMj.gameObject.SetActive(false);
        RightOutMj.gameObject.SetActive(false);
        TopOutMj.gameObject.SetActive(false);
        LeftOutMj.gameObject.SetActive(false);
        //
        //
        actMask.SetActive(false);
        PengBtn.gameObject.SetActive(false);
        GangBtn.gameObject.SetActive(false);
        HuBtn.gameObject.SetActive(false);
        GuoBtn.gameObject.SetActive(false);
        //
        posStr = "";
        mjStr = "";
        //
    
        if (ChiPaiList != null )
        {
            foreach (GameObject item in ChiPaiList)
            {
                Destroy(item);
            }
        }
        ChiPaiList.Clear();
    }

    // Use this for initialization
    void Start () {
        //注册点击事件
        PengBtn.onClick.AddListener(PengPressed);
        GangBtn.onClick.AddListener(GangPressed);
        HuBtn.onClick.AddListener(HuPressed);
        GuoBtn.onClick.AddListener(GuoPressed);
    }

    private void PengPressed()
    {
        //告诉服务器摸牌
        SocketModel ActionRequset = new SocketModel();
        ActionRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_PENGPAI);
        ActionRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        ActionRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ActionRequset);//发送这条消息给服务器
        closeAct();
    }

    private List<GameObject> ChiPaiList = new List<GameObject>();
    private void GangPressed()
    {
        Debug.Log("有"+ GameInfo.Instance.gangList.Count + "个杠牌");
        if (GameInfo.Instance.gangList.Count > 1)
        {
            for (int i = 0; i < GameInfo.Instance.gangList.Count; i++)
            {
                Action act = GameInfo.Instance.gangList[i];
                if (act.getActionType() == CardView.CHI_MINGGANG_明杠)
                {
                    GameObject gang = Instantiate(Resources.Load("Prefab/GameObject_Choose_MingGang")) as GameObject;
                    gang.GetComponent<Choose_MingGangPai>().setPic(act.getValue());
                    gang.transform.SetParent(this.transform, false);
                    gang.transform.localPosition = new Vector3(-300 + (i * 200) + (i * 20), 0, 0);
                    ChiPaiList.Add(gang);
                }
                else
                {
                    GameObject gang = Instantiate(Resources.Load("Prefab/GameObject_Choose_AnGang")) as GameObject;
                    gang.GetComponent<Choose_AnGangPai>().setPic(act.getValue());
                    gang.transform.SetParent(this.transform, false);
                    gang.transform.localPosition = new Vector3(-300 + (i * 200) + (i * 20), 0, 0);
                    ChiPaiList.Add(gang);
                }
            }
        }
        else//如果只有一个可以杠,就直接发消息给服务器
        {
            //告诉服务器杠牌
            int gangType = GameInfo.Instance.gangList[0].getActionType();
            int subCmd = 0;
            switch (gangType)
            {
                case CardView.CHI_GANG_杠牌:
                    subCmd = ProtocolSC.Sub_Cmd_GAME_GANGPAI;
                    break;
                case CardView.CHI_MINGGANG_明杠:
                    subCmd = ProtocolSC.Sub_Cmd_GAME_MINGGANG;
                    break;
                case CardView.CHI_ANGANG_暗杠:
                    subCmd = ProtocolSC.Sub_Cmd_GAME_ANGANG;
                    break;
            }
            int gangMj = GameInfo.Instance.gangList[0].getActionData()[0];
            SocketModel ActionRequset = new SocketModel();
            ActionRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
            ActionRequset.SetSubCmd(subCmd);
            ActionRequset.SetCommand(0);
            List<int> datalist = new List<int>();
            datalist.Add(GameInfo.Instance.roomId);//房间号
            datalist.Add(GameInfo.Instance.positon);//位置
            datalist.Add(gangMj);//杠的什么牌
            ActionRequset.SetData(datalist);
            NettySocket.Instance.SendMsg(ActionRequset);//发送这条消息给服务器
            closeAct();
        }
    }
    private void HuPressed()
    {
        //告诉服务器胡牌
        SocketModel ActionRequset = new SocketModel();
        ActionRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        if (huType == CardView.CHI_HU_糊牌)
        {
            ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_HUPAI);
        }
        else if (huType == CardView.CHI_GANG_抢杠)
        {
            ActionRequset.SetCommand(1);
            ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_QIANGGANG);
        }
        else if (huType == CardView.CHI_ZIMO_自摸) {
            ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_ZIMO);
        }
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        ActionRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ActionRequset);//发送这条消息给服务器
        closeAct();
    }
    private void GuoPressed()
    {
        //告诉服务器摸牌
        SocketModel ActionRequset = new SocketModel();
        ActionRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        if(huType == CardView.CHI_GANG_抢杠)
        {
            ActionRequset.SetCommand(2);
            ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_QIANGGANG);
        }
        else
        {
            ActionRequset.SetCommand(0);
            ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_GUOPAI);
        }
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        ActionRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ActionRequset);//发送这条消息给服务器
        closeAct();
    }

    private void closeAct()
    {
        actMask.SetActive(false);
        PengBtn.gameObject.SetActive(false);
        GangBtn.gameObject.SetActive(false);
        HuBtn.gameObject.SetActive(false);
        GuoBtn.gameObject.SetActive(false);
        if(ChiPaiList!=null)
        {
            foreach(GameObject item in ChiPaiList)
            {
                Destroy(item);
            }
        }
        ChiPaiList.Clear();
        //
        GameInfo.Instance.gangList.Clear();
        this.huType = 0;
    }
}
