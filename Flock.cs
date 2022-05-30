using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    float speed;
    bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // determine the bounding box of the manager cube
        Bounds b = new Bounds(myManager.transform.position, myManager.swimLimits * 2);

        //if fish is outside the bounds of the cube then start turning around
        RaycastHit hit = new RaycastHit();
        Vector3 direction = Vector3.zero;

        if (!b.Contains(transform.position)) // if the position is outside of the bounding box
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
            //Debug.DrawRay(this.transform.position, this.transform.forward * 50, Color.red);
        }
        else
            turning = false;

        if(turning)
        {
            //turn towards the center of the manager cube    
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  myManager.rotationSpeed * Time.deltaTime);
        }

        if (Random.Range(0, 100) < 10)
            speed = Random.Range(myManager.minSpeed,
                                 myManager.maxSpeed);
        if (Random.Range(0, 100) < 20)
            ApplyRules();

        // Translate statement, to drive the fish forward along their forward axis, or their z-axis
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        GameObject[] gos; // creating a holder to hold all of the fish in the current flock
        gos = myManager.allFish;

        Vector3 vcenter = Vector3.zero; // average center of the group
        Vector3 vavoid = Vector3.zero; // average avoidance vector
        float gSpeed = 0.01f; // global speed of the group
        float nDistance; // neighbor distance
        int groupSize = 0; // count how many fish in the flock are in our group

        // loop through all of the fish in the flock
        foreach (GameObject go in gos)
        {
            if(go != this.gameObject) // make sure you don't calculate the fish's data for itself
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {
                    vcenter += go.transform.position;
                    groupSize++;

                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            vcenter = vcenter / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcenter + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
