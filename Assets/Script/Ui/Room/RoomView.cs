using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomView : MonoBehaviour {

    public GameObject BotHead;
    public GameObject RightHead;
    public GameObject TopHead;
    public GameObject LeftHead;

    public Image botPos;
    public Image rightPos;
    public Image topPos;
    public Image leftPos;

    public Image lightMc;

    public Text text_mjNum;

    //暗杠
    private string angangPos;
    //明杠
    private string minggangPos;
    //直杠
    private string zhiGangPos;
    //放杠位置
    private string fangGangPos;
    //自摸
    private string zimoPos;
    //吃胡
    private string chiHuPos;
    //更新剩余牌
    private int mjNum = -1;
    //玩家掉线
    private int offLinePos;
    //玩家上线
    private int onLinePos;

    private List<PlayerData> plist = new List<PlayerData>();

    void Awake()
    {
        RoomEvent.AnGangEvent += AnGangEvent;
        RoomEvent.MingGangEvent += MingGangEvent;
        RoomEvent.ZhiGangEvent += ZhiGangEvent;
        RoomEvent.ZiMoEvent += ZiMoEvent;
        RoomEvent.ChiHuEvent += ChiHuEvent;
        RoomEvent.LeftMjEvent += LeftMjEvent;
        RoomEvent.PlayerOffLineEvent += PlayerOffLineEvent;
        RoomEvent.PlayerOnLineEvent += PlayerOnLineEvent;

    }

    //玩家上线了 
    private void PlayerOnLineEvent(int value)
    {
        onLinePos = value;
    }

    //玩家掉线 
    private void PlayerOffLineEvent(int value)
    {
        offLinePos = value;
    }

    //更新剩余牌
    private void LeftMjEvent(int value)
    {
        this.mjNum = value;
    }

    //吃胡
    private void ChiHuEvent(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        chiHuPos = pos;
        this.plist = list;

    }

    //自摸
    private void ZiMoEvent(string pos, int mj, List<PlayerData> list)
    {
        zimoPos = pos;
        this.plist = list;

    }


    //直杠
    private void ZhiGangEvent(string pos, int mj, int fg)
    {
        this.zhiGangPos = pos;
        if (fg == GameInfo.Instance.positon)
        {
            this.fangGangPos = "bot";
        }
        else
        {
            this.fangGangPos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, fg);
        }
       
    }

    //明杠
    private void MingGangEvent(string pos, int mj)
    {
        this.minggangPos = pos;
    }

    //暗杠
    private void AnGangEvent(string pos, int mj)
    {
        this.angangPos = pos;
    }

    //重置
    public void reset()
    {
        botPos.gameObject.SetActive(false);
        rightPos.gameObject.SetActive(false);
        topPos.gameObject.SetActive(false);
        leftPos.gameObject.SetActive(false);

        BotHead.GetComponent<headInfo>().setZj(0);
        RightHead.GetComponent<headInfo>().setZj(0);
        TopHead.GetComponent<headInfo>().setZj(0);
        LeftHead.GetComponent<headInfo>().setZj(0);
    }
    // Use this for initialization
    void Start () {
  
    }
    // Update is called once per frame
    void Update () {
        if (this.mjNum >=0)
        {
            updateMjNum(mjNum);
            mjNum = -1;
        }
        //吃胡
        if (chiHuPos != "")
        {
            addZiMo(plist);
            chiHuPos = "";
        }
        //自摸
        if (zimoPos!="")
        {
            addZiMo(plist);
            zimoPos = "";
        }
        //暗杠
        if (angangPos !="")
        {
            addAnGang(angangPos);
            angangPos = ""; 
        }
        //明杠
        if (minggangPos !="")
        {
            addMingGang(minggangPos);
            minggangPos = "";
        }
        //直杠
        if (zhiGangPos != "")
        {
            addZhiGang(zhiGangPos, fangGangPos);
            zhiGangPos = "";
            fangGangPos = "";
        }
        //掉线
        if (this.offLinePos >0)
        {
            PlayerOffLine();
            offLinePos = 0;
        }
        //上线
        if (this.onLinePos > 0)
        {
            PlayerOnLine();
            onLinePos = 0;
        }
        
    }

    private void PlayerOnLine()
    {
        string pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, onLinePos);
        switch (pos)
        {
            case "right":
                RightHead.GetComponent<headInfo>().SetOffLine(false);
                break;
            case "top":
                TopHead.GetComponent<headInfo>().SetOffLine(false);
                break;
            case "left":
                LeftHead.GetComponent<headInfo>().SetOffLine(false);
                break;
        }
    }

    private void PlayerOffLine()
    {
        string pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, offLinePos);
        switch (pos)
        {
            case "right":
                RightHead.GetComponent<headInfo>().SetOffLine(true);
                break;
            case "top":
                TopHead.GetComponent<headInfo>().SetOffLine(true);
                break;
            case "left":
                LeftHead.GetComponent<headInfo>().SetOffLine(true);
                break;
        }
    }

    //自摸
    public void addZiMo(List<PlayerData> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            PlayerData pd = list[i];
            string pos = "";
            if (pd.getUserId() == GameInfo.Instance.positon)//位置是自己
            {
                pos = "bot";
            }
            else
            {
                pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pd.getUserId());
            }
            switch (pos)
            {
                case "bot":
                    GameInfo.Instance.myGold += pd.getWinGold();
                    BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                    break;
                case "right":
                    GameInfo.Instance.rightGlod += pd.getWinGold();
                    RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                    break;
                case "top":
                    GameInfo.Instance.topGold += pd.getWinGold();
                    TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                    break;
                case "left":
                    GameInfo.Instance.leftGold += pd.getWinGold();
                    break;
            }
        }
    }

    //直杠
    public void addZhiGang(string pos, string fgPos)
    {
        switch (pos)
        {
            case "bot":
                GameInfo.Instance.myGold += 200;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                break;
            case "right":
                GameInfo.Instance.rightGlod += 200;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                break;
            case "top":
                GameInfo.Instance.topGold += 200;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                break;
            case "left":
                GameInfo.Instance.leftGold += 200;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
        }

        switch (fgPos)
        {
            case "bot":
                GameInfo.Instance.myGold -= 200;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                break;
            case "right":
                GameInfo.Instance.rightGlod -= 200;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                break;
            case "top":
                GameInfo.Instance.topGold -= 200;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                break;
            case "left":
                GameInfo.Instance.leftGold -= 200;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
        }

    }

    //明杠
    public void addMingGang(string pos)
    {

        switch (pos)
        {
            case "bot":
                GameInfo.Instance.myGold += 300;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod -= 100;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold -= 100;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold -= 100;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
            case "right":
                GameInfo.Instance.myGold -= 100;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod += 300;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold -= 100;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold -= 100;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
            case "top":
                GameInfo.Instance.myGold -= 100;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod -= 100;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold += 300;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold -= 100;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
            case "left":
                GameInfo.Instance.myGold -= 100;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod -= 100;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold -= 100;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold += 300;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
        }
    }

    //暗杠
    public void addAnGang(string pos)
    {
        switch (pos)
        {
            case "bot":
                GameInfo.Instance.myGold += 600;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod -= 200;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold -= 200;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold -= 200;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
            case "right":
                GameInfo.Instance.myGold -= 200;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod += 600;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold -= 200;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold -= 200;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
            case "top":
                GameInfo.Instance.myGold -= 200;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod -= 200;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold += 600;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold -= 200;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
            case "left":
                GameInfo.Instance.myGold -= 200;
                BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
                GameInfo.Instance.rightGlod -= 200;
                RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
                GameInfo.Instance.topGold -= 200;
                TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
                GameInfo.Instance.leftGold += 600;
                LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
                break;
        }
    }


    //开局初始化玩家信息
    public void initPlayerInfo()
    {
        //先设置自己的
        BotHead.GetComponent<headInfo>().SetHeadIcon(GameInfo.Instance.UserIcon);
        BotHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.UserName);
        BotHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.myGold);
        BotHead.GetComponent<headInfo>().setHost(GameInfo.Instance.positon);
        BotHead.GetComponent<headInfo>().setZj(GameInfo.Instance.positon);

        if (GameInfo.Instance.rightPositon>0&& GameInfo.Instance.rightName != "")
        {
            RightHead.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.rightIcon));
            RightHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.rightName);
            RightHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.rightGlod);
            RightHead.GetComponent<headInfo>().setHost(GameInfo.Instance.rightPositon);
            RightHead.GetComponent<headInfo>().setZj(GameInfo.Instance.rightPositon);
        }
        if (GameInfo.Instance.topPostion > 0 && GameInfo.Instance.topName != "")
        {
            TopHead.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.topIcon));
            TopHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.topName);
            TopHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.topGold);
            TopHead.GetComponent<headInfo>().setHost(GameInfo.Instance.topPostion);
            TopHead.GetComponent<headInfo>().setZj(GameInfo.Instance.topPostion);
        }
        if (GameInfo.Instance.leftPostion > 0 && GameInfo.Instance.leftName != "")
        {
            LeftHead.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.leftIcon));
            LeftHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.leftName);
            LeftHead.GetComponent<headInfo>().setPlayerGold(GameInfo.Instance.leftGold);
            LeftHead.GetComponent<headInfo>().setHost(GameInfo.Instance.leftPostion);
            LeftHead.GetComponent<headInfo>().setZj(GameInfo.Instance.leftPostion);
        }
    }

    //当前操作方向指示
    public void setCurrentPos(int pos)
    {
        if (GameInfo.Instance.positon==pos)
        {
            botPos.transform.gameObject.SetActive(true);
            //botPos.gameObject.SetActive(true);
            rightPos.gameObject.SetActive(false);
            topPos.gameObject.SetActive(false);
            leftPos.gameObject.SetActive(false);
            StartCoroutine(MoveToPosition(new Vector3(-588, -138, 0)));
            lightMc.GetComponent<GameAnimator>().Play();
        }
        else
        {
            switch (GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pos))
            {
                case "right":
                    botPos.gameObject.SetActive(false);
                    rightPos.gameObject.SetActive(true);
                    topPos.gameObject.SetActive(false);
                    leftPos.gameObject.SetActive(false);
                    StartCoroutine(MoveToPosition(new Vector3(588, 163, 0)));
                    lightMc.GetComponent<GameAnimator>().Play();
                    break;
                case "top":
                    botPos.gameObject.SetActive(false);
                    rightPos.gameObject.SetActive(false);
                    topPos.gameObject.SetActive(true);
                    leftPos.gameObject.SetActive(false);
                    StartCoroutine(MoveToPosition(new Vector3(330, 312, 0)));
                    lightMc.GetComponent<GameAnimator>().Play();
                    break;
                case "left":
                    botPos.gameObject.SetActive(false);
                    rightPos.gameObject.SetActive(false);
                    topPos.gameObject.SetActive(false);
                    leftPos.gameObject.SetActive(true);
                    StartCoroutine(MoveToPosition(new Vector3(-588, 163, 0)));
                    lightMc.GetComponent<GameAnimator>().Play();
                    break;
            }
        }
   
    }

    private IEnumerator MoveToPosition(Vector3 vt3)
    {
        yield return new WaitForSeconds(0f);
        lightMc.transform.localPosition = vt3;
    }



    public void updateMjNum(int value)
    {

        text_mjNum.text = value.ToString();
    }
}
