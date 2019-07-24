using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFood : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer childSR;

    private float factor;
    private Vector3 velocity;

    void Awake(){
        
        childSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        childSR.sprite = sprites[Random.Range(0, sprites.Length)];

        factor = Random.Range(1f,3f);
        

    }

	// Use this for initialization
	void Start () {
        velocity.x = Random.Range(-2f, 2f);
        velocity.y = Random.Range(-0.5f, 2f);
        velocity.z = 0;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += velocity*factor*Time.deltaTime;
        factor = Mathf.Lerp(factor, 0.01f, Time.deltaTime);
    }

    

    
}
