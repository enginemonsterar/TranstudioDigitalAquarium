using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLeader : MonoBehaviour
{
    private GameObject nodeParent;
    private int nodeIndex;
    // Start is called before the first frame update
    void Start()
    {
        nodeParent = GameObject.Find("NodeParent");
        MoveToNode();
    }

    void MoveToNode(){
        
        
        while (transform.position != nodeParent.transform.GetChild(nodeIndex).position)
        {
            transform.position = Vector3.Lerp(transform.position, nodeParent.transform.GetChild(nodeIndex).position, 20f);            
        }
        
        Debug.Log("Hai");

        if(nodeIndex < nodeParent.transform.childCount - 1)
            nodeIndex++;
        else
            nodeIndex = 0;

        MoveToNode();
    }

}
