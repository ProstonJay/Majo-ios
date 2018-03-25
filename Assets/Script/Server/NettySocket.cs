using ProtoBuf;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class NettySocket : MonoBehaviour
{
    public static Socket clientSocket;
    public static NettySocket Instance = null;
    //是否已连接的标识  
    //public bool IsConnected = false;

        
    //外网地址
    //public static string ipArderrs = "112.74.163.31";
    //内网地址
    public static string ipArderrs = "192.168.0.104";

    private Thread threadReceive;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    void Start()
    {
       
    }

    ////同步连接
    //public void TongBuConnect()
    //{
    //    try
    //    {
    //        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //        IPAddress ipAdress = IPAddress.Parse(ipArderrs);//解析IP地址
    //        IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 6666);
    //        clientSocket.Connect(ipEndPoint);


    //        threadReceive = new Thread(ReceiveMessage);
    //        threadReceive.IsBackground = true;
    //        threadReceive.Start();

    //        GameEvent.DoMsgTipEvent("连接服务器成功");
    //        GameEvent.DoSocketConnetEvent("connet");
    //        Debug.Log("连接服务器成功\r\n");
    //    }
    //    catch
    //    {
    //        GameEvent.DoMsgEvent("连接服务器失败");
    //        Debug.Log("连接服务器失败\r\n");
    //    }
    //}

    //异步连接
    public void Conenet()
    {
        GameEvent.DoNetSocket(1);
        try
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdress = IPAddress.Parse(ipArderrs);//解析IP地址
            IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 8228);
            IAsyncResult result = clientSocket.BeginConnect(ipEndPoint,new AsyncCallback(onConnectSuccess), clientSocket);
            bool success = result.AsyncWaitHandle.WaitOne(5000,true);
            if (!success)//超时
            {
                //GameEvent.DoNetSocket(2);
                GameEvent.DoMsgEvent("连接服务器超时");
            }
        }
        catch(System.Net.Sockets.SocketException e)
        {
            //GameEvent.DoNetSocket(2);
            GameEvent.DoMsgEvent("连接服务器失败 e="+e.ErrorCode);
        }
    }

    //连接结果
    private void onConnectSuccess(IAsyncResult ar)
    {
        if ((ar.AsyncState as Socket).Connected){
            //开启线程接收数据
            threadReceive = new Thread(ReceiveMessage);
            threadReceive.IsBackground = true;
            threadReceive.Start();
            GameEvent.DoMsgTipEvent("连接服务器成功，准备登录");
            //通知UI，继续登录
            GameEvent.DoSocketConnetEvent("connet");
        }else{
          
            GameEvent.DoMsgEvent("连接服务器失败");
        }
        GameEvent.DoNetSocket(2);
    }

    //关闭Socket     
    public void Closed()
    {
        if(threadReceive!= null)
        {
            threadReceive.Abort();
            threadReceive = null;
        }

        if (clientSocket != null && clientSocket.Connected)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            clientSocket = null;
        }
   
    }

    public void SendMsg(SocketModel socketModel)
    {
        if (clientSocket.Connected==false)
        {
            GameEvent.DoMsgEvent("发数据：Sokcet 断开=" + clientSocket.Connected);
            return;
        }
        try
        {
           // GameEvent.DoNetSocket(1);
            socketModel.SetToken(GameInfo.Instance.ToKen);
            byte[] msg = Serial(socketModel);
            //消息体结构：消息体长度+消息体
            byte[] data = new byte[4 + msg.Length];
            IntToBytes(msg.Length).CopyTo(data, 0);
            msg.CopyTo(data, 4);
            clientSocket.Send(data);
        }
        catch (System.Net.Sockets.SocketException e)
        {
            GameEvent.DoMsgEvent("发送数据网络异常 e="+e.ErrorCode);
        }



    }

    ///<summary>  
    ///接收消息  
    ///</summary>  
    private void ReceiveMessage()
    {
        while (true)
        {
            if (clientSocket.Connected==false)
            {
               // LogTxt.text += "收数据：Sokcet 断开 =" + clientSocket.Connected + "/n";
                GameEvent.DoMsgEvent("收数据：Sokcet 断开 =" + clientSocket.Connected);
                break;
            }
            try
            {
                //LogTxt.text += "接收数据/n";
                //接受消息头（消息校验码4字节 + 消息长度4字节 + 身份ID8字节 + 主命令4字节 + 子命令4字节 + 加密方式4字节 = 28字节）  
                int HeadLength = 4;
                //存储消息头的所有字节数  
                byte[] recvBytesHead = new byte[HeadLength];
                //如果当前需要接收的字节数大于0，则循环接收  
                while (HeadLength > 0)
                {
                    byte[] recvBytes1 = new byte[4];
                    //将本次传输已经接收到的字节数置0  
                    int iBytesHead = 0;
                    //如果当前需要接收的字节数大于缓存区大小，则按缓存区大小进行接收，相反则按剩余需要接收的字节数进行接收  
                    if (HeadLength >= recvBytes1.Length)
                    {
                        iBytesHead = clientSocket.Receive(recvBytes1, recvBytes1.Length, 0);
                    }
                    else
                    {
                        iBytesHead = clientSocket.Receive(recvBytes1, HeadLength, 0);
                    }
                    //将接收到的字节数保存  
                    recvBytes1.CopyTo(recvBytesHead, recvBytesHead.Length - HeadLength);
                    //减去已经接收到的字节数  
                    HeadLength -= iBytesHead;
                }
                //接收消息体（消息体的长度存储在消息头的0至4索引位置的字节里） 
                byte[] bytes = new byte[4];
                System.Array.Copy(recvBytesHead, bytes, 4);
                int BodyLength = BytesToInt(bytes, 0);
                //LogTxt.text += "数据包长度 = " + BodyLength+"/n";
                //Debug.Log("数据包长度=" + BodyLength);
                //存储消息体的所有字节数  
                byte[] recvBytesBody = new byte[BodyLength];
                //如果当前需要接收的字节数大于0，则循环接收  
                while (BodyLength > 0)
                {
                    byte[] recvBytes2 = new byte[BodyLength < 1024 ? BodyLength : 1024];
                    //将本次传输已经接收到的字节数置0  
                    int iBytesBody = 0;
                    //如果当前需要接收的字节数大于缓存区大小，则按缓存区大小进行接收，相反则按剩余需要接收的字节数进行接收  
                    if (BodyLength >= recvBytes2.Length)
                    {
                        iBytesBody = clientSocket.Receive(recvBytes2, recvBytes2.Length, 0);
                    }
                    else
                    {
                        iBytesBody = clientSocket.Receive(recvBytes2, BodyLength, 0);
                    }
                    //将接收到的字节数保存  
                    recvBytes2.CopyTo(recvBytesBody, recvBytesBody.Length - BodyLength);
                    //减去已经接收到的字节数  
                    BodyLength -= iBytesBody;
                }
                //一个消息包接收完毕，解析消息包  
                //GameEvent.DoMsgEvent("数据读取完了，可以解包了 数据长度 recvBytesBody = " + recvBytesBody.Length);
                //LogTxt.text += "数据读取完了，可以解包了 数据长度 recvBytesBody = " + recvBytesBody.Length + "/n";
                // Debug.Log("数据读取完了，可以解包了 数据长度 recvBytesBody = " + recvBytesBody.Length);
                SocketModel message = DeSerial(recvBytesBody);
                ServerManager.GetInstance().ReceiveMsg(message);
                //GameEvent.DoNetSocket(2);
            }
            catch (System.Net.Sockets.SocketException e) {
                GameEvent.DoMsgEvent("网络异常 e=" + e.ErrorCode + "Connected=" + clientSocket.Connected);
                Debug.Log(" clientSocket.Connected=" + clientSocket.Connected);
                Debug.Log("接收数据网络异常 Exception caught: " + e.ErrorCode);
                Debug.Log("接收数据网络异常 Exception caught: " + e.SocketErrorCode);
                Debug.Log("接收数据网络异常 Exception caught: " + e.NativeErrorCode);
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket.Disconnect(true);
                break;
            }

        }
    }

    private byte[] Serial(SocketModel socketModel)//将SocketModel转化成字节数组
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Serializer.Serialize<SocketModel>(ms, socketModel);
            byte[] data = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(data, 0, data.Length);
            return data;
        }
    }
    private SocketModel DeSerial(byte[] msg)//将字节数组转化成我们的消息类型SocketModel
    {
        using (MemoryStream ms = new MemoryStream())
        {
            ms.Write(msg, 0, msg.Length);
            ms.Position = 0;
            SocketModel socketModel = Serializer.Deserialize<SocketModel>(ms);
            return socketModel;
        }
    }
    public static int BytesToInt(byte[] data, int offset)
    {
        int num = 0;
        for (int i = offset; i < offset + 4; i++)
        {
            num <<= 8;
            num |= (data[i] & 0xff);
        }
        return num;
    }

    public static byte[] IntToBytes(int num)
    {
        byte[] bytes = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            bytes[i] = (byte)(num >> (24 - i * 8));
        }
        return bytes;
    }

    void OnApplicationQuit()
    {
        Closed();
    }
}