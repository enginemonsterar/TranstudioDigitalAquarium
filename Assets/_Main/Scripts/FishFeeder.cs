using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFeeder : MonoBehaviour
{
    [SerializeField] private GameObject foodBox;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFoodBox", 20, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFoodBox(){
        Instantiate(foodBox);
    }
}
