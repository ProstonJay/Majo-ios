using System;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
     Text textssss;     //UGUI 

    Transform root;

    void Start()
    {
        //获得脚本挂的Transform
        root = this.transform;
        //获取场景中按钮的引用
        textssss = root.Find("MsgTxt").GetComponent<Text>();
    }

    //使Window显示出来  如果有需要延迟消失   就delay后消失  
    public void active(String value)
    {
        //获得脚本挂的Transform
        root = this.transform;
        //获取场景中按钮的引用
        textssss = root.Find("MsgTxt").GetComponent<Text>();
        textssss.text = value;
        //如果WarningModel设置了延迟时间  
        if (value != null)
        {
            //delay时间后执行close函数  
            Invoke("close", 2);
        }
        gameObject.SetActive(true);
    }
    //关闭Window   如果有需要运行的方法就运行  
    public void close()
    {
        //close函数是否正待等候调用   很明显他已经调用了  现在要删除它  
        if (IsInvoking("close"))
        {
            //取消调用  
            CancelInvoke("close");
        }
        gameObject.SetActive(false);
    }
}
