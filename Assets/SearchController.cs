using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour {

    ActionTextControl atc;

    private void Start()
    {
        atc = FindObjectOfType<ActionTextControl>();
    }

    public void PlayOpenAnim()
    {
        GetComponent<Animator>().SetTrigger("Open");
        Destroy(GetComponent<BoxCollider>());
        atc.SetActionText("");
    }

    // Manage action text
    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.GetComponent<PlayerC>())
        {
            atc.SetActionText("F to open, Right click to pick up items");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.GetComponent<PlayerC>())
        {
            atc.SetActionText("");
        }
    }
}
