using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeParent : MonoBehaviour
{
    float zValue;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 v3 = transform.GetChild(i).position;
            transform.GetChild(i).position = new Vector3(v3.x, v3.y, v3.z + zValue);
            zValue += 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
