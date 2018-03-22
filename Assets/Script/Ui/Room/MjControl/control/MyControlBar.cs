using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyControlBar : MonoBehaviour {

    private List<GameObject> CardList = new List<GameObject>();

    private int mjLocId = 0;

    //暗杠
    private int angangMj;
    //明杠
    private int minggangMj;
    //碰牌
    private int pengMj;
    //直杠
    private int zhiGangMj;
    //吃胡
    private int chihuMj;
    private string chihuPos;
    //自摸
    private int zimoMj;
    private string zimoPos;

    //摸牌
    private int moFlag;

    void Awake()
    {
        RoomEvent.AnGangEvent += AnGangEvent;
        RoomEvent.MingGangEvent += MingGangEvent;
        RoomEvent.PengPaiEvent += PengPaiEvent;
        RoomEvent.ZhiGangEvent += ZhiGangEvent;
        RoomEvent.ChiHuEvent += ChiHuEvent;
        RoomEvent.ZiMoEvent += ZiMoEvent;
        RoomEvent.ActionMoPaiEvent += ActionMoPaiEvent;
    }


    //摸牌
    private void ActionMoPaiEvent()
    {
        moFlag = 1;

    }

    //自摸
    private void ZiMoEvent(string pos, int mj, List<PlayerData> list)
    {
        if (mj > 0)
        {
            this.zimoMj = mj;
            this.zimoPos = pos;
        }

    }

    //吃胡
    private void ChiHuEvent(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        if (mj>0)
        {
            this.chihuMj = mj;
            this.chihuPos = pos;
        }

    }

    //碰牌
    private void PengPaiEvent(string pos, int mj)
    {
        if (pos == "bot")
        {
            this.pengMj = mj;
        }

    }

    //直杠
    private void ZhiGangEvent(string pos, int mj, int fangGangPos)
    {
        if (pos == "bot")
        {
            this.zhiGangMj = mj;
        }

    }

    //明杠
    private void MingGangEvent(string pos, int mj)
    {
        if (pos == "bot")
        {
            this.minggangMj = mj;
        }

    }

    //暗杠
    private void AnGangEvent(string pos, int mj)
    {
        if (pos == "bot")
        {
            this.angangMj = mj;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //自摸
        if (moFlag > 0)
        {
            StartCoroutine(mopai());
            moFlag = 0;
        }
        //自摸
        if (zimoMj > 0)
        {
            ziMo(zimoMj, zimoPos);
            zimoMj = 0;
            zimoPos = "";
        }
        //吃胡
        if (chihuMj > 0)
        {
            chiHu(chihuMj,chihuPos);
            chihuMj = 0;
            chihuPos = "";
        }
        //暗杠
        if (angangMj > 0)
        {
            anGang(angangMj);
            angangMj = 0;
        }
        //明杠
        if (minggangMj > 0)
        {
            mingGang(minggangMj);
            minggangMj = 0;
        }
        //碰牌
        if (pengMj > 0)
        {
            pengPai(pengMj);
            pengMj = 0;
        }
        //直杠
        if (zhiGangMj > 0)
        {
            zhiGang(zhiGangMj);
            zhiGangMj = 0;
        }

    }

    private IEnumerator mopai()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("本轮操作权是自己,告诉服务器摸牌");
        //告诉服务器摸牌
        SocketModel MoPaiRequset = new SocketModel();
        MoPaiRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        MoPaiRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_MOPAI);
        MoPaiRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        MoPaiRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(MoPaiRequset);//发送这条消息给服务器
    }

    //自摸
    public void ziMo(int mj, string pos)
    {
        //要把牌推倒,如果是当前位置胡了,要把胡的牌放到摸牌的地方

        if (pos == "bot")
        {
        
        }
  
    }

    //吃胡
    public void chiHu(int mj ,string pos)
    {
        //要把牌推倒,如果是当前位置胡了,要把胡的牌放到摸牌的地方

        if (pos == "bot")
        {
        }
    }

    //碰牌
    public void pengPai(int mj)
    {
        int count = 0;
        for (int i = 0; i < CardList.Count; i++)
        {
            string mjId = CardList[i].GetComponent<Mj_my>().mjId;
            if (int.Parse(mjId) == mj)
            {
                Destroy(CardList[i]);
                CardList.RemoveAt(i);
                count++;
                i--;
            }
            if (count == 2)
            {
                break;
            }
        }
        //
        GameInfo.Instance.deleteArrInList(mj, 2);
        //刷新排序
        ResetPostion();
        //设置可以操作了
        GameInfo.Instance.PlayFlag = true;
    }

    //直杠
    public void zhiGang(int mj)
    {
        int count = 0;
        for (int i = 0; i < CardList.Count; i++)
        {
            string mjId = CardList[i].GetComponent<Mj_my>().mjId;
            if (int.Parse(mjId) == mj)
            {
                Destroy(CardList[i]);
                CardList.RemoveAt(i);
                count++;
                i--;
            }
            if (count == 3)
            {
                break;
            }
        }
        //
        GameInfo.Instance.deleteArrInList(mj, 3);
        //刷新排序
        ResetPostion();
        //
        //摸杠牌
        StartCoroutine(ShowA());
    }

    //明杠
    private void mingGang(int mj)
    {
        int count = 0;
        for (int i = 0; i < CardList.Count; i++)
        {
            string mjId = CardList[i].GetComponent<Mj_my>().mjId;
            if (int.Parse(mjId) == mj)
            {
                Destroy(CardList[i]);
                CardList.RemoveAt(i);
                count++;
                i--;
            }
            if (count == 1)
            {
                break;
            }
        }
        //
        GameInfo.Instance.deleteArrInList(mj, 1);
        //刷新排序
        ResetPostion();
        //摸杠牌
        StartCoroutine(ShowA());
    }

    //暗杠
    private void anGang(int mj)
    {
        Debug.Log("暗杠MYControl——mj=" + mj);
        int count = 0;
        for (int i = 0; i < CardList.Count; i++)
        {
            string mjId = CardList[i].GetComponent<Mj_my>().mjId;
            if (int.Parse(mjId) == mj)
            {
                Destroy(CardList[i]);
                CardList.RemoveAt(i);
                count++;
                i--;
            }
            if (count == 4)
            {
                break;
            }
        }
        //
        GameInfo.Instance.deleteArrInList(mj, 2);
        //刷新排序
        ResetPostion();
        //摸杠牌
        StartCoroutine(ShowA());
    }

    private IEnumerator ShowA()
    {
        yield return new WaitForSeconds(2f);
        moGangPai();
    }

    //摸杠牌
    public void moGangPai()
    {
        //告诉服务器摸牌
        SocketModel MoPaiRequset = new SocketModel();
        MoPaiRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        MoPaiRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_MOPAI);
        MoPaiRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        MoPaiRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(MoPaiRequset);//发送这条消息给服务器
    }

    public void resetAll()
    {
        if (CardList.Count > 0)
        {
            foreach (GameObject item in CardList)
            {
                Destroy(item);
            }
        }
        CardList.Clear();
        mjLocId = 0;
    }
    // Use this for initializationint
    void Start()
    {
    }
    //重连
    public void reJoinHnadList(int mjId,int stat=0)
    {
        for (int i = 0; i < GameInfo.Instance.myHandMj.Count; i++)
        {
            string mj = GameInfo.Instance.myHandMj[i].ToString();
            //最后显示  
            GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_mj_my")) as GameObject;
            mjCard.transform.localPosition = new Vector3(i * -83, 0, 0);
            mjCard.transform.SetParent(this.transform, false);
            mjCard.GetComponent<Mj_my>().setPic(mj);
            mjCard.GetComponent<Mj_my>().mjId = mj;
            mjCard.name = mjLocId.ToString();
            mjLocId++;
            CardList.Add(mjCard);
            Debug.Log("mjCard =", mjCard);
            Debug.Log("CardList =" + CardList.Count);
        }
        if (GameInfo.Instance.positon == GameInfo.Instance.actionFlag&& stat==0)
        {
            if (mjId > 0)
            {
                MoPai(mjId);
            }
            else
            {
                //告诉服务器摸牌
                SocketModel MoPaiRequset = new SocketModel();
                MoPaiRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
                MoPaiRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_MOPAI);
                MoPaiRequset.SetCommand(0);
                List<int> datalist = new List<int>();
                datalist.Add(GameInfo.Instance.roomId);//房间号
                datalist.Add(GameInfo.Instance.positon);//位置
                MoPaiRequset.SetData(datalist);
                NettySocket.Instance.SendMsg(MoPaiRequset);//发送这条消息给服务器
            }
        }
    }
    /// <summary>
    ///初始化发牌
    /// </summary>
    public void initHandMj()
    {
        for (int i = 0; i <GameInfo.Instance.myHandMj.Count; i++)
        {
            string mj = GameInfo.Instance.myHandMj[i].ToString();
            //最后显示  
            GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_mj_my")) as GameObject;
            mjCard.transform.localPosition = new Vector3(i * -83, 0, 0);
            mjCard.transform.SetParent(this.transform, false);
            mjCard.GetComponent<Mj_my>().setPic(mj);
            mjCard.GetComponent<Mj_my>().mjId = mj;
            mjCard.name = mjLocId.ToString();
            mjLocId++;
            CardList.Add(mjCard);
            Debug.Log("mjCard =", mjCard);
            Debug.Log("CardList =" + CardList.Count);
        }
        if (GameInfo.Instance.positon== GameInfo.Instance.zhuangjia)//只用来开局判断自己是不是庄家,如果是就摸牌
        {
            //告诉服务器摸牌
            SocketModel MoPaiRequset = new SocketModel();
            MoPaiRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
            MoPaiRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_MOPAI);
            MoPaiRequset.SetCommand(0);
            List<int> datalist = new List<int>();
            datalist.Add(GameInfo.Instance.roomId);//房间号
            datalist.Add(GameInfo.Instance.positon);//位置
            MoPaiRequset.SetData(datalist);
            NettySocket.Instance.SendMsg(MoPaiRequset);//发送这条消息给服务器
        }
    }
    //摸牌
    public void MoPai(int data)
    {
        //处理一个摸牌
        GameObject mj14 = Instantiate(Resources.Load("Prefab/GameObject_mj_my")) as GameObject;
        mj14.transform.localPosition = new Vector3(130, 0, 0);
        mj14.transform.SetParent(this.transform, false);
        mj14.GetComponent<Mj_my>().setPic(data.ToString());
        mj14.GetComponent<Mj_my>().mjId = data.ToString();
        mjLocId++;
        mj14.name = mjLocId.ToString();
        CardList.Add(mj14);
        GameInfo.Instance.PlayFlag = true;
    }



    public void SelectCard(String CardName)
    {
        for (int i=0;i<CardList.Count;i++)
        {
            if(CardList[i].name== CardName)
            {
                if (CardList[i].GetComponent<Mj_my>().state == "down")
                {
                    CardList[i].GetComponent<Mj_my>().setUp();
                }
                else
                {
                         if (GameInfo.Instance.PlayFlag==false)
                        {
                            CardList[i].GetComponent<Mj_my>().setDown();
                         }
                        else
                        {
                            //通知出牌区,添加打的牌
                            string mjname = CardList[i].name;
                            string mjId = CardList[i].GetComponent<Mj_my>().mjId;
                            //GameEvent.DoPlayEvent("my", mjname, mjId);
                            //这里删除这张打出的牌
                            Destroy(CardList[i]);
                            CardList.Remove(CardList[i]);
                            //刷新排序
                            ResetPostion();
                            //重新排序手牌
                            GameInfo.Instance.sortHandList(int.Parse(mjId));   
                            //告诉服务器出牌
                            SocketModel ChuPaiRequset = new SocketModel();
                            ChuPaiRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
                            ChuPaiRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_CHUPAI);
                            ChuPaiRequset.SetCommand(0);
                            List<int> datalist = new List<int>();
                            datalist.Add(GameInfo.Instance.roomId);//房间号
                            datalist.Add(GameInfo.Instance.positon);//位置
                            datalist.Add(int.Parse(mjId));//出的牌
                            ChuPaiRequset.SetData(datalist);
                            NettySocket.Instance.SendMsg(ChuPaiRequset);//发送这条消息给服务器
                            //设置不可以操作了
                            GameInfo.Instance.PlayFlag = false;                   
                    }

                }

            }
            else
            {
                CardList[i].GetComponent<Mj_my>().setDown();
            }
        }
    }
    //重置牌位置
    private void ResetPostion()
    {
        //先根据麻将的ID排序,从大到小
        CardList.Sort((x, y) => -(x.GetComponent<Mj_my>().mjId.CompareTo(y.GetComponent<Mj_my>().mjId)));
        for (int i = 0; i < CardList.Count; i++)
        {
            CardList[i].GetComponent<Mj_my>().ReSetX(i);
        }
    }

}
