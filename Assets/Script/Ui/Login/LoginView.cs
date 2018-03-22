using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class LoginView : MonoBehaviour
{

    Button loginBtn;
    public Button xieyiBtn;
    public GameObject  xieyiPanel;

    Transform root;

    private int connectFlag;

    public Toggle blueToggle;


    // Use this for initialization
    void Awake()
    {
        Application.targetFrameRate = 30;
        GameEvent.SocketConnetEvent += SocketConnet;
    }
    private void SocketConnet(string str)
    {
        connectFlag = 1;
    }

    void Start()
    {
        //获得脚本挂的Transform
        root = this.transform;
        //获取场景中按钮的引用
        loginBtn = root.Find("Button_ok").GetComponent<Button>();
        //注册点击事件
        loginBtn.onClick.AddListener(LoginPressed);
        xieyiBtn.onClick.AddListener(PressedXieyi);
        //

        setLocData();
    }
    public static string str = "";          
    void Update()
    {
        if (str != "")
        {
            GameInfo.Instance.DeviceID =  SystemInfo.deviceUniqueIdentifier;
            SceneManager.LoadScene(str);
            LoginView.str = "";
        }
        if (connectFlag == 1)
        {
            connectFlag = 0;
            loginRequst();
         
        }
    }

    void OnGUI()
    {

    }

    //登录
    public void LoginPressed()
    {
        if (!blueToggle.isOn)
        {
            GameEvent.DoMsgTipEvent("未同意用户使用协议!");
            return;
        }
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        NettySocket.Instance.Conenet();
    }

    //服务器连接上,发送登录请求
    private void loginRequst()
    {
        GameEvent.DoNetSocket(1);
        GameEvent.DoMsgTipEvent("请求登录数据");
        //
        string deviceUID =  SystemInfo.deviceUniqueIdentifier;
        //
        SocketModel LoginRequset = new SocketModel();
        LoginRequset.SetMainCmd(ProtocolMC.Main_Cmd_LOGIN);//消息的类型为Login
        LoginRequset.SetSubCmd(ProtocolSC.Sub_Cmd_LoginRqs);//动作为请求登录
        LoginRequset.SetCommand(0);
        List<string> message = new List<string>();//这里存的是用户的账号密码
        message.Add(deviceUID);
        LoginRequset.SetMessage(message);
        NettySocket.Instance.SendMsg(LoginRequset);//发送这条消息给服务器
    }

    private void setLocData() {
        //如果有缓存,初始化
        if (PlayerPrefs.HasKey("ok")==false)
        {
            //缓存
            PlayerPrefs.SetString("ok", "true");
            PlayerPrefs.SetString("TableColor", "blue");
            PlayerPrefs.SetString("Language", "sc");
            PlayerPrefs.SetString("music", "on");
            PlayerPrefs.SetString("sound", "on");
            PlayerPrefs.SetString("vioce", "on");
        }
    }

    private void PressedXieyi()
    {
        xieyiPanel.SetActive(true);
    }

}