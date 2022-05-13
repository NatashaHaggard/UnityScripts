using UnityEngine;

public class ObjectBuilderScript : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int numberOfObjects = 100;

    public float xAreaSpread = 10;
    public float yAreaSpread = 0;
    public float zAreaSpread = 10;

    public void BuildObject()
    {
        Vector3 randPosition = new Vector3(Random.Range(-xAreaSpread, xAreaSpread),
                                           Random.Range(-yAreaSpread, yAreaSpread),
                                           Random.Range(-zAreaSpread, zAreaSpread)) + transform.position;

        // _ is a discard, which is a placeholder variable that is intentionally unused in application code
        _ = Instantiate(objectToSpawn, randPosition, Quaternion.identity);
    }

    public void BuildObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            BuildObject();
        }
    }
}
