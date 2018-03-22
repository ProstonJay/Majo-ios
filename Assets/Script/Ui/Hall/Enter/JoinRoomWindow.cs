using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomWindow : MonoBehaviour
{

    public Button JoinBtn;
    public Button CloseBtn;

    public InputField InputText_RoomId;
    public InputField InputText_PassWord;

    // Use this for initialization
    void Start()
    {
        JoinBtn.onClick.AddListener(JoinPressed);
        CloseBtn.onClick.AddListener(ClosePressed);
    }



    void JoinPressed()
    {
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
        if (InputText_RoomId.text == "")
        {
            GameEvent.DoMsgEvent("房间号不能为空!");
            return;
        }
        if (int.Parse(InputText_RoomId.text)<=0)
        {
            GameEvent.DoMsgEvent("房间号不合法!");
            return;
        }
        Debug.Log("加入的房间号=" + int.Parse(InputText_RoomId.text));
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        SocketModel JoinRoomRequset = new SocketModel();
        JoinRoomRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);
        JoinRoomRequset.SetSubCmd(ProtocolSC.Sub_Cmd_JoinRoomRqs);
        JoinRoomRequset.SetCommand(0);
        List<string> message = new List<string>();
        message.Add(InputText_RoomId.text);
        if (InputText_PassWord.text == "")
        {
            message.Add("000");
        }
        else
        {
            message.Add(InputText_PassWord.text);
        }
     
        JoinRoomRequset.SetMessage(message);
        NettySocket.Instance.SendMsg(JoinRoomRequset);//发送这条消息给服务器
    }

    void ClosePressed()
    {
        Destroy(this.transform.gameObject);
        AudioMgr.Instance.SoundPlay("btnClose", 2f, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
