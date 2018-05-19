using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonListener : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
    , IEventSystemHandler {
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log("Enter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
    }
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    

    }
    
}
