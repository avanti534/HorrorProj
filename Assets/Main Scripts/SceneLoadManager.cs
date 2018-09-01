using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour {

    public string[] LoadObjNames;
    public GameObject[] DelObjs;
    public bool LoadOnStart = true;
    public float LoadingDelays = 0.8f;

    // For UI Updates
    [System.NonSerialized]
    public int currentIndex = 0;
    [System.NonSerialized]
    public string currentObjName = "";
    [System.NonSerialized]
    public string currentStatus = "";
    GameObject playerObj;

    LoadingScreenControl lsc;

	// Use this for initialization
	void Start () {
        playerObj = FindObjectOfType<PlayerC>().gameObject;
        lsc = FindObjectOfType<LoadingScreenControl>();
        if (LoadOnStart)
        {
            Debug.Log("Begin Load");
            StartCoroutine(LoadFunc());
        }
    }
	
	IEnumerator LoadFunc()
    {
        currentIndex = 0;
        currentStatus = "Spawning Objects";
        for (int i = 0; i < LoadObjNames.Length; i++)
        {
            currentIndex = i;
            if (LoadObjNames[i] != null)
            {
                currentObjName = LoadObjNames[i];
                StatusUpdate();
                // Spawm obj from name in Resource library
                GameObject objInstnace = (GameObject)Resources.Load(LoadObjNames[i]);
                if (objInstnace != null)
                {
                    if (GameObject.Find(LoadObjNames[i]) == null)
                    {
                        Instantiate(objInstnace, objInstnace.transform.position, objInstnace.transform.rotation);
                    } else
                    {
                        Debug.Log("Obj with same name already spawned: skip");
                    }
                }
            }
            Debug.Log("Finished spawning object: " + LoadObjNames[i]);
            yield return new WaitForSeconds(LoadingDelays);
        }
        Debug.Log("Finished Loading Objects, Begin clean");
        currentIndex = 0;
        currentStatus = "Removing Objects";
        currentObjName = "";
        StatusUpdate();
        yield return new WaitForSeconds(LoadingDelays);
        for (int i = 0; i < DelObjs.Length; i++)
        {
            currentIndex = i;
            GameObject currentObj = DelObjs[i];
            if (currentObj != null)
            {
                currentObjName = currentObj.name;
                StatusUpdate();
                Destroy(currentObj);
            }
            yield return new WaitForSeconds(LoadingDelays);
        }
        currentStatus = "Finishing Load";
        currentObjName = "Remove Load UI";
        StatusUpdate();
        yield return new WaitForSeconds(LoadingDelays*2.0f);
        Destroy(lsc.gameObject);
        playerObj.GetComponent<Rigidbody>().isKinematic = false;
        playerObj.GetComponent<Rigidbody>().useGravity = true;
        playerObj.GetComponent<PlayerC>().canMove = true;
    }

    void StatusUpdate()
    {
        lsc.CallTextUpdate(currentStatus, currentObjName);
    }
}