using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Photon.Pun;
using UnityEngine.UI;

public class Playercontroller : MonoBehaviourPun,IPunObservable
{
    [SerializeField] private PhotonView photonview;
    [SerializeField] private float smoothRottime;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight =5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float smoothMove = 10f;   
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform Groundpos;
    [SerializeField] private GameObject thirdpersonCamera;
    private CharacterController controller;
    private float currentvelocity;
    private float GroundSphereRadius = 0.1f;
    private GameObject Camera;
    public Joystick joystick;
    public Button JumpBtn;
    bool isGrounded;
    Vector3 Velocity;
    Vector3 smoothdamp;
    Quaternion smoothRotation;
    bool jumpbtnPressed =false;
    bool side;
    GameObject bouncer;
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject Btn = GameObject.Find("JumpButton");
        JumpBtn = Btn.GetComponent<Button>();
        GameObject joy = GameObject.Find("Fixed Joystick");
        joystick = joy.GetComponent<Joystick>();
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //controller.isTrigger = true;
        Camera = GameObject.Find("Main Camera");
        if (photonview.IsMine)
        {
            thirdpersonCamera.SetActive(true);            
        }
        JumpBtn.onClick.AddListener(this.JumpButton);
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            playerMove();
        }
        else
        {
            SmoothMoveOtherScreenPlayer();
        }
    }
    public void SmoothMoveOtherScreenPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, smoothdamp, Time.deltaTime * smoothMove);
        transform.rotation = Quaternion.Lerp(transform.rotation, smoothRotation, Time.deltaTime * smoothMove);
    }
    public void playerMove()
    {
        isGrounded = Physics.CheckSphere(Groundpos.position, GroundSphereRadius, GroundLayer);

        if (isGrounded && Velocity.y <= 0)
        {
            Velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded ||  jumpbtnPressed)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpbtnPressed = false;
        }

        float x =/* Input.GetAxis("Horizontal") ||*/ joystick.Horizontal;
        float y = /*Input.GetAxis("Vertical") ||*/ joystick.Vertical;

        Vector3 direction = new Vector3(x, 0, y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg/* + Camera.transform.eulerAngles.y*/;
            float rot = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentvelocity, smoothRottime);
            transform.rotation = Quaternion.Euler(new Vector3(0, rot, 0));
            Vector3 moveAngle = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveAngle.normalized * Time.deltaTime * speed);
        }


        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);
    }
    public void JumpButton()
    {
        jumpbtnPressed = true;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        if (stream.IsReading)
        {
            smoothdamp = (Vector3) stream.ReceiveNext();
            smoothRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other)     
    {
        if (GameManager.instace.NoOfPlayerQualified <= GameManager.instace.NoOfPlayerCanQualifie)
        {
            if (other.CompareTag("Finishline"))
            {
                if (photonview.IsMine)
                    GameManager.instace.coroutineCall();

                GameManager.instace.NoOfPlayerQualified++;


            }
        }
       
    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.collider.CompareTag("spawnner"))
    //    {
    //        Debug.Log("isspawing");
    //        //PhotonNetwork.Instantiate(GameManager.instace.PlayerPrefab.name,GameManager.instace.instatiatepos.transform.position, Quaternion.identity);
    //    }
    //}
}
