using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameAnimator_Move : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //transform.DOLocalJump(new Vector3(200, -200, 0), 10f, 10,1f);
        //transform.DOShakePosition(3, new Vector3(200, 0, 0));

    }

    void RemoveThis()
    {
        Destroy(this.transform.gameObject);
    }

    public void goMove(bool isRmove=false)
    {
        transform.DOPunchPosition(new Vector3(-100, 0, 0), 0.05f);
        if (isRmove == true)
        {
            Invoke("RemoveThis", 2);
        }
    }

    // Use this for initialization
    void Start () {
  
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
      
    }
}
