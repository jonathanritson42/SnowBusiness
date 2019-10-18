using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable_script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
