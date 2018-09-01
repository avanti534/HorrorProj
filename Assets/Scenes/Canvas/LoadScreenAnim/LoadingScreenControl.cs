using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenControl : MonoBehaviour {

    public Text statusText;
    public Text objName;

    public void CallTextUpdate(string st, string on)
    {
        statusText.text = st;
        objName.text = on;
        Debug.Log("Updated Loading Text");
    }
}
