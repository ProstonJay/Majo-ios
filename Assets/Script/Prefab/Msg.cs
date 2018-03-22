using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;


public class Msg : MonoBehaviour {

    Text txts;
    Button okbtn;
    Transform root;
    // Use this for initialization
    string msg;
    void Start() {
        root = this.transform;
        //获取场景中按钮的引用
        txts = root.Find("Text_txt").GetComponent<Text>();
        okbtn = root.Find("Button_ok").GetComponent<Button>();
        //注册点击事件
        okbtn.onClick.AddListener(OkPressed);
        txts.text = msg;
    }

    public void setTxt(string value)
    {
        gameObject.SetActive(true);
        msg = value;
    }

    void OkPressed()
    {
        //NettySocket.clientSocket.Shutdown(SocketShutdown.Both);
        //NettySocket.clientSocket.Close();
        gameObject.SetActive(false);
        //Application.Quit();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
