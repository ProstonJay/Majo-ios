using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;//注意要用到这个dll
[ProtoContract]
public class SocketModel
{
    [ProtoMember(1)]
    private string Token;
    [ProtoMember(2)]
    private int maincmd;
    [ProtoMember(3)]
    private int subcmd;
    [ProtoMember(4)]
    private int command;//指令
    [ProtoMember(5)]
    private List<string> message;//消息
    [ProtoMember(6)]
    private List<int> dataList;//消息
    [ProtoMember(7)]
    private List<PlayerData> pdata;//消息
    [ProtoMember(8)]
    private List<Action> adata;//数据
    [ProtoMember(9)]
    private List<MailData> mailList;//邮件
    [ProtoMember(10)]
    private List<BattleData> BattleList;

    public SocketModel()
    {

    }

   public SocketModel(string tk,int maincmd, int subcmd, int command, List<string> message, List<int> datalist, List<PlayerData> pdata,
       List<Action> adata, List<MailData> mdata)
    {
        this.Token = tk;
        this.maincmd = maincmd;
        this.subcmd = subcmd;
        this.command = command;
        this.message = message;
        this.dataList = datalist;
        this.pdata = pdata;
        this.adata = adata;
        this.mailList = mdata;
    }

    public string GetToken()
    {
        return Token;
    }
    public void SetToken(string value)
    {
        this.Token = value;
    }
    public int GetMainCmd()
    {
        return maincmd;
    }
    public void SetMainCmd(int maincmd)
    {
        this.maincmd = maincmd;
    }
    public int GetSubCmd()
    {
        return this.subcmd;
    }
    public void SetSubCmd(int subcmd)
    {
        this.subcmd = subcmd;
    }
    public int GetCommand()
    {
        return this.command;
    }
    public void SetCommand(int command)
    {
        this.command = command;
    }
    public List<string> GetMessage()
    {
        return message;
    }
    public void SetMessage(List<string> message)
    {
        this.message = message;
    }

    public List<int> GetData()
    {
        return dataList;
    }
    public void SetData(List<int> datalist)
    {
        this.dataList = datalist;
    }

    public List<PlayerData> GetPdata()
    {
        return pdata;
    }
    public void SetPdata(List<PlayerData> datalist)
    {
        this.pdata = datalist;
    }

    public List<Action> GetAdata()
    {
        return adata;
    }
    public void SetAdata(List<Action> datalist)
    {
        this.adata = datalist;
    }

    public List<MailData> GetMailList()
    {
        return mailList;
    }
    public void SetMailList(List<MailData> list)
    {
        this.mailList = list;
    }

    public List<BattleData> GetBattleList()
    {
        return BattleList;
    }
    public void SetBattleList(List<BattleData> date)
    {
        this.BattleList = date;
    }
}