using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatView : MonoBehaviour
{

    public Button chatBtn;
    public GameObject chatPanel;

    public GameObject chat1;
    public GameObject chat2;
    public GameObject chat3;
    public GameObject chat4;

    //public scro

    private int chatPos;
    private int chatTxt;

    void Awake()
    {
        GameEvent.ChatSocketEvent += ChatSocketEvent;
        //

    }

    //聊天
    private void ChatSocketEvent(int pos,int vlaue)
    {
        chatPos = pos;
        chatTxt = vlaue;
    }
    // Use this for initialization
    void Start () {
        chatBtn.onClick.AddListener(chatPressed);
    }
	
	// Update is called once per frame
	void Update () {
        if (chatPos > 0)
        {
            setChatTxt();
        }
	}

    public void chatPressed()
    {
        chatPanel.SetActive(true);
    }

    public void setChatTxt()
    {
        string str = MajooUtil.getChatText(chatTxt);
        string pos = "";
        if(chatPos == GameInfo.Instance.positon)
        {
            pos = "bot";
        }
        else
        {
             pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, chatPos);
        }
        switch (pos)
        {
            case "bot":
                chat1.GetComponent<ChatTxt>().setTxt(str);
                break;
            case "right":
                chat2.GetComponent<ChatTxt>().setTxt(str);
                break;
            case "top":
                chat3.GetComponent<ChatTxt>().setTxt(str);
                break;
            case "left":
                chat4.GetComponent<ChatTxt>().setTxt(str);
                break;
        }
        string sex = MajooUtil.getPlayerSex(pos);
        AudioMgr.Instance.SoundPlay("chat/"+sex+"/chat_" + chatTxt.ToString(),1f, 2);
        chatPos = 0;
        chatTxt = 0;
    }

}
