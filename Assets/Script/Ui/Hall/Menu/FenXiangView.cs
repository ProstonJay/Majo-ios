using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FenXiangView : MonoBehaviour {

    public Button closeBtn;
    public Button fxBtn;

    public InputField numText;
    public InputField idText;

    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(PressedClosed);
        fxBtn.onClick.AddListener(PressedFx);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //分享
    public void PressedFx()
    {
        if (idText.text=="")
        {
            GameEvent.DoMsgTipEvent("请输入玩家ID!");
            return;
        }
        if (int.Parse(idText.text) <= 0)
        {
            GameEvent.DoMsgTipEvent("玩家ID不合法!");
            return;
        }
        if (numText.text == "")
        {
            GameEvent.DoMsgTipEvent("请输入数量!");
            return;
        }
        if (int.Parse(numText.text) <= 0)
        {
            GameEvent.DoMsgTipEvent("数量不合法!");
            return;
        }
        if (int.Parse(numText.text) > GameInfo.Instance.UserFK)
        {
            GameEvent.DoMsgTipEvent("钻石不足!");
            return;
        }
        SocketModel FenXiangRequset = new SocketModel();
        FenXiangRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);
        FenXiangRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_FENXIANG);
        FenXiangRequset.SetCommand(0);
        List<int> message = new List<int>();
        message.Add(int.Parse(idText.text));
        message.Add(int.Parse(numText.text));
        FenXiangRequset.SetData(message);
        NettySocket.Instance.SendMsg(FenXiangRequset);//发送这条消息给服务器
        idText.text = "";
        numText.text = "";
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }

    //关闭
    public void PressedClosed()
    {
        idText.text = "";
        numText.text = "";
        this.transform.gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }
}
