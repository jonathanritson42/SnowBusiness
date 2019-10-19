using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSway : MonoBehaviour
{

    public GameObject uiElements;
    public Text timerText;
    float timeCount;

    public GameObject[] windParticles;
    public GameObject[] windParticles2;

    public bool hitEndTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hitEndTrigger)
        {
            timeCount += Time.deltaTime;
            timerText.text = timeCount.ToString("0.0");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            hitEndTrigger = true;
            StartCoroutine(ShowEndScreenDelay());
        }

        ShowJumpCharge();
        MoveUI();
        ShowSpeed();
        IncreaseSpeedEffect();
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

    public void IncreaseSpeedEffect()
    {
        if (FindObjectOfType<Tribes_Movement>().overallspeed > 20)
        {
            foreach (var item in windParticles)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (var item in windParticles)
            {
                item.SetActive(false);
            }
        }

        if(FindObjectOfType<Tribes_Movement>().overallspeed > 40)
        {
            foreach (var item in windParticles2)
            {
                item.SetActive(true);
            }
        }
        else
        {
            foreach (var item in windParticles2)
            {
                item.SetActive(false);
            }
        }
    }

    public int collectables;
    public GameObject endCanvas;
    public Text timetext, scoreText;

    public IEnumerator ShowEndScreenDelay()
    {
        timetext.text = timeCount.ToString("0.0");
        scoreText.text = collectables.ToString();
        yield return new WaitForSeconds(1.5f);
        endCanvas.SetActive(true);
    }


}
