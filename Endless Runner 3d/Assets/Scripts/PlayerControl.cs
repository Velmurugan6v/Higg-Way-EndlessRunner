using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    [SerializeField] protected float _slideSpeed;
    [SerializeField] protected float _cameraSlidSpeed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected Transform _camTrans;
    [SerializeField] protected float _turboMoveSpeed;

    [SerializeField] Transform boostDestiny;
    [SerializeField] Vector3 carOftenPos;
    [SerializeField] Transform carObj;
    public float CurrentPos { get; set; }

    public int _speed;


    [SerializeField] List<GameObject> cars;

    CarAnimationBase carAnima;

    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }

        //print(PlayerPrefs.GetInt("SelectedCar"));
        cars[PlayerPrefs.GetInt("SelectedCar")].SetActive(true);   //Show Only Which Car Player Select In Shop

        /*carAnima = cars[PlayerPrefs.GetInt("SelectedCar")].GetComponent<CarAnimationBase>();*/
    }

    void MakeInstance()  // Player makes Instance
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
  
    void Update()
    {
        PlayerBaseMOve();

        /*if (Input.GetKeyDown(KeyCode.W))
        {
            Boost(5);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            UnBoost(0);
        }*/
        /*if (carAnima!=null)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                carAnima.CarTurnRight();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                carAnima.CarTurnLeft();
            }
            else
            {
                carAnima.CarNoTurn();
            }
        }*/

    }

    void PlayerBaseMOve()
    {
        //Only Zero to right
        if (Input.GetKeyDown(KeyCode.D) && CurrentPos == 0 || Input.GetKeyDown(KeyCode.RightArrow) && CurrentPos == 0)
        {

            CurrentPos = 2f;
            PlayerMove(CurrentPos);
            //Applicaple for camera Move
            //CameraMove(1);
            
        }
        //Only Right to Centre
        else if (Input.GetKeyDown(KeyCode.A) && CurrentPos == 2 || Input.GetKeyDown(KeyCode.LeftArrow) && CurrentPos == 2)
        {
            CurrentPos = 0f;
            PlayerMove(CurrentPos);
            //Applicaple for camera Return
            //CameraMove(0);
        }
        //Only Centre to Left
        else if (Input.GetKeyDown(KeyCode.A) && CurrentPos == 0 || Input.GetKeyDown(KeyCode.LeftArrow) && CurrentPos == 0)
        {
            CurrentPos = -2f;
            PlayerMove(CurrentPos);
            //Applicaple for camera Move
            //CameraMove(-1);
        }
        //Only Left to Centre
        else if (Input.GetKeyDown(KeyCode.D) && CurrentPos == -2 || Input.GetKeyDown(KeyCode.RightArrow) && CurrentPos == -2)
        {
            CurrentPos = 0f;
            PlayerMove(CurrentPos);
            //Applicaple for camera Return
            //CameraMove(0);
        }

        //transform.Translate(Vector3.forward * _speed * Time.deltaTime);

    }

    public void PlayerMove(float moveDirection)
    {
        transform.DOMoveX(moveDirection, _slideSpeed);
    }

    public void CameraMove(float moveDirection)
    {
        _camTrans.DOMoveX(moveDirection, _cameraSlidSpeed);
    }

    public void PlayerJump(float jumpVelocity)
    {
        transform.DOMoveY(jumpVelocity, _jumpForce);
    }

    public void Boost(float targetPos)
    {
        carObj.DOLocalMoveZ(targetPos, 0.5f);
        PlayerControl2.instance.CurrentSpeed *= 2;
    }

    public void UnBoost(float targetPos)
    {
        carObj.DOLocalMoveZ(targetPos, 0.5f);
        PlayerControl2.instance.CurrentSpeed /= 2;
    }
}

public static class ExtraFeature
{
    
    public static void TurboSpeed(this PlayerControl mainPlayer,Transform car)
    {
        
    }
}

