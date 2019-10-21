using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treecollide : MonoBehaviour
{
    public Terrain terrain;
    private TreeInstance[] _originalTrees;
    [SerializeField] private LayerMask terrainmask;

    // Start is called before the first frame update
    void Start()
    {
        _originalTrees = terrain.terrainData.treeInstances;

        Debug.Log(_originalTrees.Length);
    }

    private void Update()
    {
        RaycastHit ray;

        if (Physics.Raycast(transform.position, Vector3.forward, out ray, terrainmask))
        {
            foreach (TreeInstance tree in _originalTrees)
            {
                if (ray.transform.position == tree.position)
                {
                    Debug.Log("Hit a tree!");
                }
            }
        }

        Debug.DrawRay(transform.position, Vector3.forward, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (TreeInstance tree in _originalTrees)
        {
            if (collision.transform.position == tree.position)
            {
                Debug.Log("Hit a tree!");
            }
        }
    }
}
