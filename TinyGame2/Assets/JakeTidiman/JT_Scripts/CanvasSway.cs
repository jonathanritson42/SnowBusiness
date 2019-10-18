using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSway : MonoBehaviour
{

    public GameObject uiElements;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        MoveUI();
    }

    public float timer;

    public void MoveUI()
    {
        if (Input.GetKey(KeyCode.A) && uiElements.transform.localPosition.x < 30)
        {
            uiElements.transform.Translate(Time.deltaTime * 50, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            timer = 0;
        }

        if (Input.GetKey(KeyCode.D) && uiElements.transform.localPosition.x > -30)
        {
            uiElements.transform.Translate(Time.deltaTime * -50, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            timer = 0;
        }

        if (!Input.anyKey)
        {
            timer += Time.deltaTime * 5;
            uiElements.transform.position = Vector3.Lerp(uiElements.transform.position, transform.position, timer);
        }
    }
}
