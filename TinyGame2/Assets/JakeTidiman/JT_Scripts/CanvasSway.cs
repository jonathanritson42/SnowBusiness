using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        ShowJumpCharge();
        MoveUI();
        ShowSpeed();
    }

    public float timer;

    public void MoveUI()
    {
        if (Input.GetKey(KeyCode.A) && uiElements.transform.localPosition.x < 15)
        {
            uiElements.transform.Translate(Time.deltaTime * 50, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            timer = 0;
        }

        if (Input.GetKey(KeyCode.D) && uiElements.transform.localPosition.x > -15)
        {
            uiElements.transform.Translate(Time.deltaTime * -50, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            timer = 0;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            timer += Time.deltaTime * 5;
            uiElements.transform.position = Vector3.Lerp(uiElements.transform.position, transform.position, timer);
        }
    }

    public float chargeAmount;
    public Slider jumpSlider;
    public GameObject tutorialText;

    public void ShowJumpCharge()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            chargeAmount += Time.deltaTime;
            tutorialText.SetActive(false);
        }
        else
        {
            chargeAmount = 0;
        }
        jumpSlider.value = chargeAmount;
    }

    public Text speedText;

    public void ShowSpeed()
    {
        speedText.text = FindObjectOfType<Tribes_Movement>().overallspeed.ToString("0.0");
    }
}
