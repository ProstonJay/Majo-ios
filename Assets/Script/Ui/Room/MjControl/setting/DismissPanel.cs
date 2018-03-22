using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DismissPanel : MonoBehaviour {

    public Button Btn_Agree;
    public Button Btn_DisAgree;

    public Text Txt_dis;

    public void ShowPanel(string Pname)
    {
        Txt_dis.text = "<color=#420383FF>【" + Pname + "】</color>" + " 申请解散房间！";
    }

    // Use this for initialization
    void Start () {
        Btn_Agree.onClick.AddListener(Btn_AgreePressed);
        Btn_DisAgree.onClick.AddListener(Btn_DisAgreePressed);
    }
    //同意
    private void Btn_AgreePressed()
    {
        SocketModel ReadyRequset = new SocketModel();
        ReadyRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ReadyRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_VOTE_DISMISS_投票解散);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        datalist.Add(1);
        ReadyRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ReadyRequset);//发送这条消息给服务器

        Txt_dis.text = "";
        this.transform.gameObject.SetActive(false);
    }
    //不同意
    private void Btn_DisAgreePressed()
    {
        SocketModel ReadyRequset = new SocketModel();
        ReadyRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ReadyRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_VOTE_DISMISS_投票解散);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        datalist.Add(2);
        ReadyRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ReadyRequset);//发送这条消息给服务器

        Txt_dis.text = "";
        this.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
