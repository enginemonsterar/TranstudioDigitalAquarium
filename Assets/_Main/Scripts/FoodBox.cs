using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBox : MonoBehaviour
{
    [SerializeField] private GameObject fishFood;
    private Animator animator;    
    private const string fishFoodParentTag = "FishFoodParent";

    void Awake(){
        animator = GetComponent<Animator>();        
    }
     
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(OpenBox());
    }

    void LateUpdate () {
        // transform.position = Vector3.Lerp(transform.position, new Vector3(0,-2,0), Time.deltaTime * 2);        
    }


    public void OpenBox(){
        
        animator.SetTrigger("OpenFoodBox");

        for (int i = 0; i < Random.Range(30,50); i++)
        {
            Instantiate(fishFood,transform.position,transform.rotation, GameObject.FindGameObjectWithTag(fishFoodParentTag).transform);            
        }

        // yield return new WaitForSeconds(3);           
        // Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
