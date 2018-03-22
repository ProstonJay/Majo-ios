using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatPanel : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        this.transform.gameObject.SetActive(false);
    }


    public void hideChat()
    {
        this.transform.gameObject.SetActive(false);
    }
}
