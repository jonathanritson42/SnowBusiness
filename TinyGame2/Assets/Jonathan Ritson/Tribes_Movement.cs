using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool floorstick;
    public PhysicMaterial Ice, EndTrig;
    private bool iceonoff;
    private bool moveonoff;
    public float jumpmass;
    public float[] jumpforce;
    private bool jumpreset;
    private int checknum;
    private Vector3 checkpoint;

    public float overallspeed;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        RB = GetComponent<Rigidbody>();

        targetTime = 0;
        iceonoff = false;
        moveonoff = true;

        RB.mass = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region movement

        if (moveonoff == true)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            RB.AddRelativeForce(new Vector3(moveHorizontal, 0.0f, 0.0f) * sidespeed);
            RB.AddRelativeForce(new Vector3(0.0f, 0.0f, moveVertical) * forbackspeed);
        }

        #endregion
    }

    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.name == "Terrain")
        {
            floorstick = true;
            jumpreset = true;
            RB.mass = 100;
        }
    }

    void Update()
    {
        // observation 
        overallspeed = RB.velocity.magnitude;

        PCpos = RB.velocity;


        #region camera

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);

        #endregion


        #region floorstick

        if (floorstick_enable == true && floorstick == true)
        {
            Vector3 pos = transform.position;
            float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
            transform.position = new Vector3(pos.x, terrainHeight + 1, pos.z);
        }


        #endregion


        #region jump

        if (jump_enable == true && jumpreset == true)
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
                RB.mass = jumpmass;
                jumpreset = false;

                if (targetTime < 0.4f)
                {
                    floorstick = false;
                    PCpos.y = jumpforce[0];
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if ((targetTime >= 0.4f) && (targetTime < 0.8f))
                {
                    floorstick = false;
                    PCpos.y = jumpforce[1];
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if ((targetTime >= 0.8f) && (targetTime < 1.25f))
                {
                    floorstick = false;
                    PCpos.y = jumpforce[2];
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if ((targetTime >= 1.25f) && (targetTime < 1.5f))
                {
                    floorstick = false;
                    PCpos.y = jumpforce[3];
                    RB.velocity = PCpos;
                    targetTime = 0;
                }

                if (targetTime >= 1.5f)
                {
                    floorstick = false;
                    PCpos.y = jumpforce[4];
                    RB.velocity = PCpos;
                    targetTime = 0;
                }
            }
        }
        #endregion

    }

    void OnTriggerEnter(Collider collider)
    {
        #region icemoveloss

        if (iceonoff == false && collider.gameObject.name == "icetrig")
        {
            GetComponent<Collider>().material = Ice;
            iceonoff = true;
            moveonoff = false;
            return;
        }

        if (iceonoff == true && collider.gameObject.name == "icetrig")
        {
            GetComponent<Collider>().material = null;
            iceonoff = false;
            moveonoff = true;
            return;
        }

        #endregion


        #region collectables

        if (collider.gameObject.name == "Collectable")
        {
            Debug.Log("collected");
        }

        #endregion


        #region checkpointupdate

        if (collider.gameObject.name == "Checkpoint")
        {
            checkpoint = this.transform.position;
            checknum++;
        }

        #endregion


        #region endtrigger

        if (collider.gameObject.name == "End_Trigger")
        {
            GetComponent<Collider>().material = EndTrig;
            iceonoff = true;
            moveonoff = false;
            RB.mass = 300;

            StartCoroutine(endstop());
        }

        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!(collision.gameObject.name == "Terrain") && (overallspeed > 10))
        {

            #region Death

            if (checknum == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            else
            {
                transform.position = checkpoint;

                jump_enable = true;
                jumpreset = true;

                //bug - Hopefully fixed - not able to jump after reset to checkpoint if jumping previously.
            }

            #endregion
        }
    }

    IEnumerator endstop()
    {
        yield return new WaitForSeconds(2);

        RB.constraints = RigidbodyConstraints.FreezeAll;
    }
}
