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
    [SerializeField] GameObject toShotButton;
    [SerializeField] GameObject shotButton;
    private float horizontalInput;
    private bool isTakingShot = false;
    [SerializeField] float maxDrawDistance;
    [SerializeField] TextMeshProUGUI powerText;
    private float savedMousePosition;
    private float xAxis;
    private float yAxis;
    private bool istoShot = false;
    private bool isShot = false;
    

    Transform cueBall;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        toShotButton.SetActive(true);
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
        
       Shoot();
    }

    public void SetToShot()
    {
        istoShot = true;
        toShotButton.SetActive(false);
        shotButton.SetActive(true);

    }


    public void SetIsShot()
    {
        isShot = true;
    }

    public void ResetCamera()
    {
        istoShot = false;
        cueStick.SetActive(true);
        toShotButton.SetActive(true);
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    void Shoot()
    {
        
        if(gameObject.GetComponent<Camera>().enabled)
        {
            if (istoShot && !isTakingShot)
            {
                shotButton.SetActive(true);
                isTakingShot = true;
                savedMousePosition = 0f;
            }
            else if (isTakingShot)
            {

                if(savedMousePosition+ yAxis <= 0)
                {
                    savedMousePosition = savedMousePosition+ yAxis * 0.01f;

                    if(savedMousePosition<=maxDrawDistance)
                    {
                        savedMousePosition = maxDrawDistance;
                    }
                    float powerValueNumber = ((savedMousePosition - 0) / (maxDrawDistance - 0)) * (100 - 0) + 0;
                    int powerValueInt = Mathf.RoundToInt(powerValueNumber);
                    powerText.text = "Power: " + powerValueInt + "%";
                }
                if(isShot)
                {
                    isShot = false;
                    Vector3 hitDirection = transform.forward;
                    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

                    cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power * Mathf.Abs(savedMousePosition), ForceMode.Impulse);
                    cueStick.SetActive(false);
                    shotButton.SetActive(false);
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

    public void SetYAxis(float value)
    {
        yAxis = value;
    }

}
