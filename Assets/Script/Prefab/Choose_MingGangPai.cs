using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Choose_MingGangPai : MonoBehaviour, IPointerClickHandler
{

    public Image mj1;
    public Image mj3;
    public Image mj4;


    private int mjId;

    //传入数据
    public void setPic(int mjid)
    {
        this.mjId = mjid;
        Sprite sp = Resources.Load("Sprite/mj/xia_w/xiao_" + mjid.ToString(), typeof(Sprite)) as Sprite;
        mj1.sprite = sp;
        mj3.sprite = sp;
        mj4.sprite = sp;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //告诉服务器杠牌
        SocketModel ActionRequset = new SocketModel();
        ActionRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ActionRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_MINGGANG);
        ActionRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        datalist.Add(mjId);//杠的什么牌
        ActionRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ActionRequset);//发送这条消息给服务器
         //
        SendMessageUpwards("closeAct");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
