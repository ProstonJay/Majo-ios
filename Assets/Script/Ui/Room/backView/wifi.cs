using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wifi : MonoBehaviour {

    public Image img_wifi;
    public Text text_time;
    public Text text_wifi;

    private Ping ping;

    // Use this for initialization
    void Start () {
        StartCoroutine("UpdataTime");
        StartCoroutine("UpdataWifi");
        StartCoroutine("UpdataPing");
    }

    IEnumerator UpdataTime()
    {
        while (true)
        {
            //text_time.text = System.DateTime.Now.ToShortTimeString().Substring(0, 5);
            text_time.text = System.DateTime.Now.ToShortTimeString();
            yield return new WaitForSeconds(60f);
        }
    }

    IEnumerator UpdataWifi()
    {
        while (true)
        {
            string network = string.Empty;
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                network = "无网络";
            }
            else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                //network = "3G/4G";
            }
            else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                //network = "WiFi";
            }
            //Debug.Log("网络状态=" + network);
            text_wifi.text = network;
            yield return new WaitForSeconds(30f);
        }
    }

    IEnumerator UpdataPing()
    {
        while (true)
        {
            ping = new Ping(NettySocket.ipArderrs);
            //ping = new Ping("119.75.217.109");
            yield return new WaitForSeconds(10f);
        }
    }

    // Update is called once per frame
    void Update () {
        if (ping !=null && ping.isDone)
        {
            float delayTime = ping.time;
            ping.DestroyPing();
            ping = null;
            upDateWifiStaut(delayTime);
        }
    }

    private void upDateWifiStaut(float delayTime)
    {
        string imgPath = "Sprite/wifi/5";
        if (delayTime>=0&& delayTime<30)
        {
            imgPath = "Sprite/wifi/1";
        }
        else if (delayTime > 50 && delayTime < 100)
        {
            imgPath = "Sprite/wifi/2";
        }
        else if (delayTime > 100 && delayTime < 150)
        {
            imgPath = "Sprite/wifi/3";
        }
        else if (delayTime > 150 && delayTime < 200)
        {
            imgPath = "Sprite/wifi/4";
        }
        else if (delayTime > 200)
        {
            imgPath = "Sprite/wifi/5";
        }
        Sprite sp = Resources.Load(imgPath, typeof(Sprite)) as Sprite;
        img_wifi.sprite = sp;
        //
        text_wifi.text = delayTime.ToString();
    }
}
