using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Photon.Pun;
public class Playercontroller : MonoBehaviourPun,IPunObservable
{
    [SerializeField] private PhotonView photonview;
    [SerializeField] private float smoothRottime;
    private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight =5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float smoothMove = 10f;
    private float currentvelocity;
    private float GroundSphereRadius=0.1f;
    [SerializeField]private LayerMask GroundLayer;
    private GameObject Camera;
    [SerializeField] private Transform Groundpos;
    [SerializeField] private GameObject thirdpersonCamera;
    [SerializeField] private Joystick joy;
    bool isGrounded;
    Vector3 Velocity;
    Vector3 smoothdamp;
    Quaternion smoothRotation;
    bool jumpbtnPressed =false;
    // Start is called before the first frame update

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Camera = GameObject.Find("Main Camera");
        if (photonview.IsMine)
        {
            thirdpersonCamera.SetActive(true);            
        }
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
        
        float x = joy.Horizontal;
        float y = joy.Vertical;

        Vector3 direction = new Vector3(x, 0, y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;
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
}
