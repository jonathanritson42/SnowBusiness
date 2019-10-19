using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnimScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAnim()
    {
        GetComponent<Animator>().SetBool("PickedUp", false);
    }
}
