using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tribes_Movement : MonoBehaviour
{
    private Rigidbody RB;
    public float forbackspeed;
    public float sidespeed;
    public float lookSpeed = 3;
    private Vector2 rotation = Vector2.zero;
    private bool timeup;
    private Vector3 PCpos;
    private float targetTime;
    public bool floorstick_enable;
    public bool jump_enable;

    // just an observation variable
    public float overallspeed;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        RB = GetComponent<Rigidbody>();

        targetTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // just an observation variable
        overallspeed = RB.velocity.magnitude;

        PCpos = RB.velocity;

        #region movement

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        RB.AddRelativeForce(new Vector3(moveHorizontal, 0.0f, 0.0f) * sidespeed);
        RB.AddRelativeForce(new Vector3(0.0f, 0.0f, moveVertical) * forbackspeed);


        #endregion


        #region camera

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);

        #endregion

        #region jump

        if (jump_enable == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.localScale -= new Vector3(0, 0.2f, 0);
                timeup = true;
            }

            if (timeup == true)
            {
                targetTime += Time.deltaTime;
            }


            if (Input.GetKeyUp(KeyCode.Space))
            {
                transform.localScale += new Vector3(0, 0.2f, 0);
                timeup = false;


                if (targetTime < 0.4f)
                {
                    floorstick_enable = false;
                    PCpos.y = 1;
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if ((targetTime >= 0.4f) && (targetTime < 0.8f))
                {
                    floorstick_enable = false;
                    PCpos.y = 5;
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if ((targetTime >= 0.8f) && (targetTime < 1.25f))
                {
                    floorstick_enable = false;
                    PCpos.y = 10;
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if ((targetTime >= 1.25f) && (targetTime < 1.5f))
                {
                    floorstick_enable = false;
                    PCpos.y = 15;
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if (targetTime >= 1.5f)
                {
                    floorstick_enable = false;
                    PCpos.y = 20;
                    RB.velocity = PCpos;
                    targetTime = 0;
                }
            }
        }
        #endregion

        #region floorstick

        if (floorstick_enable == true)
        {

            Vector3 pos = transform.position;
            float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
            transform.position = new Vector3(pos.x, terrainHeight + 1, pos.z);
        }

        
        #endregion

    }

    void OnCollisionStay(Collision collision)
    {
        floorstick_enable = true;
    }
}
