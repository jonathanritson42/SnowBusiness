using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public Text Clock;
    public bool Timeup, Timedown;
    public float Target, Timedown_val;
    private float upval, downval;

    // Start is called before the first frame update
    void Start()
    {
        upval = 0;
        downval = Timedown_val;
        
        if (Timeup == true)
        {
            StartCoroutine(StartCountUp());
        }

        if (Timedown == true)
        {
            StartCoroutine(StartCountDown());
        }

        if ((Timeup && Timedown) == true)
        {
            Debug.LogAssertion("Clock Error");
        }
    }

    IEnumerator StartCountUp()
    {
        while (upval <= Target)
        {
            Clock.text = upval.ToString();
            yield return new WaitForSeconds(1);
            upval += 1;
        }
    }

    IEnumerator StartCountDown()
    {
        while (downval >= 0)
        {
            Clock.text = downval.ToString();
            yield return new WaitForSeconds(1);
            downval -= 1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
