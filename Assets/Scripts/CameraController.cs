using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    [SerializeField] float power;
    [SerializeField] GameObject cueStick;
    private float horizontalInput;
    private bool isTakingShot = false;
    [SerializeField] float maxDrawDistance;
    [SerializeField] TextMeshProUGUI powerText;
    private float savedMousePosition;
    private float xAxis;
    

    Transform cueBall;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if (ball.GetComponent<Ball>().IsCueBall())
            {
                cueBall = ball.transform;
                break;
            }
        }
        ResetCamera();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (cueBall != null && !isTakingShot)
        {
            // xAxis = Input.GetAxis("Horizontal");
            horizontalInput = xAxis * rotationSpeed * Time.deltaTime ;

            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }
        
        /*
        //Temporary
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }
        
        //End Temporary
        
        if(Input.GetButtonDown("Fire1")&& gameObject.GetComponent<Camera>().enabled)
        {
            Vector3 hitDirection = transform.forward;
            hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection*power,ForceMode.Impulse);
            cueStick.SetActive(false);
            gameManager.SwitchCameras();
        }
        */
       //Shoot();
    }

    public void ResetCamera()
    {
        cueStick.SetActive(true);
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    void Shoot()
    {
        if(gameObject.GetComponent<Camera>().enabled)
        {
            if (Input.GetButtonDown("Fire1") && !isTakingShot)
            {
                isTakingShot = true;
                savedMousePosition = 0f;
            }
            else if (isTakingShot)
            {
                if(savedMousePosition+Input.GetAxis("Mouse Y") <= 0)
                {
                    savedMousePosition += Input.GetAxis("Mouse Y");

                    if(savedMousePosition<=maxDrawDistance)
                    {
                        savedMousePosition = maxDrawDistance;
                    }
                    float powerValueNumber = ((savedMousePosition - 0) / (maxDrawDistance - 0)) * (100 - 0) + 0;
                    int powerValueInt = Mathf.RoundToInt(powerValueNumber);
                    powerText.text = "Power: " + powerValueInt + "%";
                }
                if(Input.GetButtonDown("Fire1"))
                {
                    Vector3 hitDirection = transform.forward;
                    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

                    cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power * Mathf.Abs(savedMousePosition), ForceMode.Impulse);
                    cueStick.SetActive(false);
                    gameManager.SwitchCameras();
                    isTakingShot = false;
                    
                }
            }
        }

    
    }

    public void SetXAxis(float value)
    {
        xAxis = value;
    }

}
