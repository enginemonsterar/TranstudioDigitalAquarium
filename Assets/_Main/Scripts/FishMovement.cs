using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] private float moveForwardSpeed;

    private float changeDirTime;
    private float zFirstPos;
   

    // Start is called before the first frame update
    void Start()
    {
        zFirstPos = transform.position.z;
        moveForwardSpeed = Random.Range(2f,3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        
        if(changeDirTime < 10){
            changeDirTime += 1 * Time.deltaTime;
        } else
        {
            changeDirTime = 0;
            // ChangeDir();
        }

        
    }

    void ChangeDir(){
        transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x * -1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        moveForwardSpeed *= -1;
    }

    private void MoveForward(){
        GetComponent<Rigidbody>().velocity = (transform.forward * moveForwardSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, zFirstPos);
    }

    

}
