using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;//注意要用到这个dll
[ProtoContract]
public class PlayerData  {

    [ProtoMember(1)]
    private int userId;
    [ProtoMember(2)]
    private string playerName;
    [ProtoMember(3)]
    private int playerIcon;//头像
    [ProtoMember(4)]
    private int zhuangjia;//1 是  0 否
    [ProtoMember(5)]
    private int hupai;//0 否  1 自摸 2 吃胡
    [ProtoMember(6)]
    private List<int> handlist;//手牌
    [ProtoMember(7)]
    private List<Action> actionlist;//碰,吃
    [ProtoMember(8)]
    private List<int> outlist;//打出的牌
    [ProtoMember(9)]
    private int winGold;//输赢金币
    [ProtoMember(10)]
    private int totalGang;//杠牌次数
    [ProtoMember(11)]
    private int huPaicCunt;//胡牌次数
    [ProtoMember(12)]
    private int fangPaoCunt;//放炮次数
    [ProtoMember(13)]
    private List<JsData> jsList;//小结算明细
    [ProtoMember(14)]
    private string dataStamp;//时间戳
    [ProtoMember(15)]
    private List<int> PghList;//可操作类型列表
    [ProtoMember(16)]
    private List<Action> GangList;//多个操作类型列表

    public PlayerData()
    {

    }

    public PlayerData(int uid = 0, string pn = "", int pi = 0, int zj = 0, int hupai = 0, List<int> hl = null,
        List<Action> al = null, List<int> ol = null, int wg = 0, int tg = 0, int hp = 0, int fp = 0, List<JsData> js = null,string ds="",
        List<int> pghlist=null, List<Action> gangList=null)
    {
        this.userId = uid;
        this.playerName = pn;
        this.playerIcon = pi;
        this.zhuangjia = zj;
        this.hupai = hupai;
        this.handlist = hl;
        this.actionlist = al;
        this.outlist = ol;
        this.winGold = wg;
        this.totalGang = tg;
        this.huPaicCunt = hp;
        this.fangPaoCunt = fp;
        this.jsList = js;
        this.dataStamp = ds;
        this.PghList=pghlist;
        this.GangList = gangList;
    }


    public List<JsData> getJsList()
    {
        return jsList;
    }

    public void setJsList(List<JsData> list)
    {
        this.jsList = list;
    }

    public int getPlayerIcon()
    {
        return playerIcon;
    }
    public void setPlayerIcon(int value)
    {
        this.playerIcon = value;
    }

    public int getTotalGang()
    {
        return totalGang;
    }
    public void setTotalGang(int value)
    {
        this.totalGang = value;
    }
    public string getDataStamp()
    {
        return dataStamp;
    }
    public void setDataStamp(string value)
    {
        this.dataStamp = value;
    }
    public int getHuPaicCunt()
    {
        return huPaicCunt;
    }
    public void setHuPaicCunt(int value)
    {
        this.huPaicCunt = value;
    }
    public int getFangPaoCunt()
    {
        return fangPaoCunt;
    }
    public void setFangPaoCunt(int value)
    {
        this.fangPaoCunt = value;
    }

    public int getWinGold()
    {
        return winGold;
    }
    public void setWinGold(int wg)
    {
        this.winGold = wg;
    }

    public int getUserId()
    {
        return userId;
    }
    public void setUserId(int uid)
    {
        this.userId = uid;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void setPlayerName(string pname)
    {
        this.playerName = pname;
    }

    public int getZhuangjia()
    {
        return zhuangjia;
    }
    public void setZhuangjia(int zj)
    {
        this.zhuangjia = zj;
    }

    public int getHupai()
    {
        return hupai;
    }
    public void setHupai(int hp)
    {
        this.hupai = hp;
    }


    public List<int> gethandlist()
    {
        return handlist;
    }


    public void sethandlist(List<int> list)
    {
        this.handlist = list;
    }

    public List<Action> getactionlist()
    {
        return actionlist;
    }


    public void setactionlist(List<Action> list)
    {
        this.actionlist = list;
    }

    public List<int> getoutlist()
    {
        return outlist;
    }


    public void setoutlist(List<int> list)
    {
        this.outlist = list;
    }

    public List<Action> GetGangList()
    {
        return GangList;
    }
    public void SetGangList(List<Action> list)
    {
        this.GangList = list;
    }

    public List<int> GetPghList()
    {
        return PghList;
    }
    public void SetPghList(List<int> list)
    {
        this.PghList = list;
    }
}
