using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] charges;
    void Start()
    {   
        //Start the coroutine we define below named ExampleCoroutine.
        InvokeRepeating( "SpawnCharge", 1f, 3f );
    }

    private void SpawnCharge()
    {
        //Instantiate a chare
        var position = new Vector3(Random.Range(-28.0f, 28.0f), 5, Random.Range(-27.0f, 27.0f));
        Instantiate(charges[Random.Range(0, charges.Length)], position,Quaternion.identity);
    }
}
