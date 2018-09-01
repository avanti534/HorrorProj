using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTextControl : MonoBehaviour {

    Text actionText;

	// Use this for initialization
	void Start () {
        actionText = GetComponent<Text>();
        actionText.text = "";
	}
	
	public void SetActionText(string newText)
    {
        actionText.text = newText;
    }
}
