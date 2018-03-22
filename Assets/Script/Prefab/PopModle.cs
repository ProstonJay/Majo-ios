
using UnityEngine;  
using System.Collections;  
//声明一个警告委托  用于在弹出警告的同时运行其他程序的方法    
//public delegate void WarningResult();
public class PopModle
{
    //需要显示的文字  
    public string value;
    //延迟多久自动关闭  
    public float delay;
    //警告模型  
    public PopModle(string value, float delay = -1)
    {
        this.value = value;
        this.delay = delay;
    }
}