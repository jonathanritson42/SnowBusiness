using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTerrain : MonoBehaviour
{
    public Terrain terrain;
    private float overallspeed;
    public GameObject PC;

    void Start()
    {
        TreeInstance[] treeInstance = Terrain.activeTerrain.terrainData.treeInstances;
        
    }

    private void Update()
    {
        overallspeed = FindObjectOfType<Tribes_Movement>().overallspeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");

        if (overallspeed > 10 && ((collision.gameObject.name == "PC test") || (collision.gameObject.name == "PC slide")))
        {
            PC.GetComponent<Tribes_Movement>().Death();
        }
    }
}
