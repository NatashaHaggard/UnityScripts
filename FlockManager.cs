using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab; // fish prefab to use
    public int numFish = 20; // number of fish in the flock
    public GameObject[] allFish; // array of fish
    public Vector3 swimLimits = new Vector3(5, 5, 5); //the range of locations the fish can be generated within, radius value; so it's a box of 10x10x10
    // these swim limits will be used to generate the fish location within that box
    public Vector3 goalPos;

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed = 0.35f;
    [Range(0.0f, 5.0f)]
    public float maxSpeed = 1;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance = 5;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish]; // create all of the fish and array big enough to hold them

        // for loop, creates a position to put the fish, based on the position of the flock manager plus a random vector 3 value that is within the swim limits
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                Random.Range(-swimLimits.y, swimLimits.y),
                                                                Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity); //put the fish in the position, using neutral rotation
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position; //goal position for fish to head towards
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,100) < 10)
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                        Random.Range(-swimLimits.y, swimLimits.y),
                                                        Random.Range(-swimLimits.z, swimLimits.z));
    }
}
