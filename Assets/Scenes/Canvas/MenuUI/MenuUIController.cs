using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

    SceneControl sc;
    Animator anim;

    PlayerC pc;

	// Use this for initialization
	void Start () {
        sc = FindObjectOfType<SceneControl>();
        pc = FindObjectOfType<PlayerC>();
        anim = GetComponent<Animator>();
	}
	

    public void OpenMenuUI()
    {
        Cursor.lockState = CursorLockMode.None;
        anim.SetBool("Open", true);
    }
    public void CloseMenuUI()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pc.canMove = true;
        pc.inMenu = false;
        anim.SetBool("Open", false);
    }
    public void ReturnToMenu()
    {
        sc.LoadScene("Menu");
    }
    public void QuitGame()
    {
        sc.QuitProj();
    }

}
