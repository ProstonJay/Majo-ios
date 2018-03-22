
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour {

    public Image img_power;
	// Use this for initialization
	void Start () {
        StartCoroutine("UpdataBattery");
    }

    IEnumerator UpdataBattery()
    {
        while (true)
        {
            //此处的battery是一个百分比数字，比如电量是93%，则这个数字是93
            img_power.rectTransform.sizeDelta = new Vector2(40* getBatteryLvel(), 15);
            yield return new WaitForSeconds(300f);
        }
    }

    private float getBatteryLvel()
    {
        float ff;
        if (SystemInfo.batteryLevel > 0)
        {
            ff = SystemInfo.batteryLevel;
        }
        else
        {
            ff = 1;
        }
        return ff;
    }


// Update is called once per frame
void Update () {
		
	}
}
