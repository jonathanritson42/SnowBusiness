using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulder : MonoBehaviour
{
    private Rigidbody RB;
    public float thrust;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.AddRelativeForce(transform.forward * thrust);
    }
}
