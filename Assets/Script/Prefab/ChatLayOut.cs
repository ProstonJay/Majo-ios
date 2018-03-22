using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatLayOut : MonoBehaviour, IPointerClickHandler
{
    public string str="";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {

        SocketModel LoginRequset = new SocketModel();
        LoginRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        LoginRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_CHAT);
        LoginRequset.SetCommand(0);
        List<int> message = new List<int>();
        message.Add(GameInfo.Instance.roomId);
        message.Add(GameInfo.Instance.positon);
        message.Add(int.Parse(str));
        LoginRequset.SetData(message);
        NettySocket.Instance.SendMsg(LoginRequset);

        SendMessageUpwards("hideChat");
    }
}
