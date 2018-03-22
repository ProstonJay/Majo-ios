using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatTxt : MonoBehaviour {

    public Text txt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setTxt(string str)
    {
        this.transform.gameObject.SetActive(true);
        this.txt.text = str;
        Invoke("closeThis", 2);
    }

    private void closeThis()
    {
        this.txt.text = "";
        this.transform.gameObject.SetActive(false);
    }
}
