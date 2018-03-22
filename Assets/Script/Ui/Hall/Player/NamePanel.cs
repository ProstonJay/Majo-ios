using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePanel : MonoBehaviour {

    public Button closeBtn;

    public Button saveBtn;

    public InputField inputText;

    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(closePressed);
        saveBtn.onClick.AddListener(savePressed);
    }

    public void resetNameTxt()
    {
        inputText.text = "";
    }

    //保存
    public void savePressed()
    {
        //
        if (inputText.text=="")
        {
            GameEvent.DoMsgTipEvent("昵称不能为空!");
            return;
        }
        if (inputText.text == GameInfo.Instance.UserName)
        {
            GameEvent.DoMsgTipEvent("昵称相同!");
            return;
        }
        SocketModel reNameRequset = new SocketModel();
        reNameRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);
        reNameRequset.SetSubCmd(ProtocolSC.Sub_Cmd_HALL_CHANGENAME);
        reNameRequset.SetCommand(0);
        List<string> message = new List<string>();
        message.Add(inputText.text);
        reNameRequset.SetMessage(message);
        NettySocket.Instance.SendMsg(reNameRequset);//发送这条消息给服务器
        //
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }


    //关闭
    public void closePressed()
    {
        inputText.text = "";
        this.transform.gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
