using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureMovePoint : MonoBehaviour {

    [System.NonSerialized]
    CreatureController targetCreature = null;


    public Transform nextNode = null;
    public float delayTime = 1.0f;

    public bool destroySelfAfter = true;
    public bool destroyCreatureAfter = false;

	public void CreatureAtNode(CreatureController cc)
    {
        targetCreature = cc;
        // Creature has arrived at this node
        if (nextNode != null)
        {
            Debug.Log("Invoke move creature to next node");
            Invoke("TargetToNextNode", delayTime);
        }

        if (destroyCreatureAfter)
        {
            Debug.Log("Creature reaced destroy end node");
            Destroy(cc.gameObject);
        }
    }

    void TargetToNextNode()
    {
        Debug.Log("Creature to next node");
        if (targetCreature != null) 
            targetCreature.SetTargetToPoint(nextNode.GetComponent<CreatureMovePoint>());
        targetCreature = null;
        if (destroySelfAfter)
        {
            Debug.Log("Creature finished with node: Destroy");
            Destroy(gameObject);
        }
    }
}