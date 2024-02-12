using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Sensitivity")]
    
    [SerializeField] float power;
    [SerializeField] float maxDrawDistance;
    [SerializeField] static public int cameraSens;
    [SerializeField] static public int shotSens;

    [Header("Setup")]
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;

    [Header("CueStick")]
    [SerializeField] GameObject cueStick;

    [Header("Buttons")]
    [SerializeField] GameObject toShotButton;
    [SerializeField] GameObject shotButton;
    [SerializeField] GameObject toViewButton;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI powerText;

    private float horizontalInput;

    private bool isTakingShot = false;
    private bool isToView = false;
    private bool istoShot = false;
    private bool isShot = false;

    private float savedMousePosition;

    private float xAxis;
    private float yAxis;


    readonly float rotationSpeed = 360;

    Transform cueBall;
    GameManager gameManager;

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

    void Update()
    {
        
        if (cueBall != null && !isTakingShot)
        {
            horizontalInput = xAxis * rotationSpeed * Time.deltaTime * (3+cameraSens)/31;

            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }
       Shoot();
    }

    public void SetToShot()
    {
        istoShot = true;
        isToView = false;
        toShotButton.SetActive(false);
        toViewButton.SetActive(true);
        shotButton.SetActive(true);

    }


    public void SetToView()
    {
        istoShot = false;
        toShotButton.SetActive(true);
        toViewButton.SetActive(false);
        shotButton.SetActive(false);
        isToView = true;

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
                if (isToView)
                {
                    isTakingShot = false;
                    return;

                }
                if(savedMousePosition+ yAxis*0.01f*shotSens <= 0)
                {
                    savedMousePosition +=  yAxis * (0.01f) * shotSens;

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
                    toViewButton.SetActive(false);
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

    public void SetCameraSens(int value)
    {
        cameraSens = value;
    }

    public void SetShotSens(int value)
    {
        shotSens = value;
    }


}

