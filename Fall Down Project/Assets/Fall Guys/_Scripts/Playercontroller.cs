using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Playercontroller : MonoBehaviourPun,IPunObservable
{
    public static Playercontroller instance;
    [SerializeField] private PhotonView photonview;
    [SerializeField] private float smoothRottime;
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce = 100f;
    [SerializeField] private float smoothMove = 10f;   
    [SerializeField] private float smoothDampY = .5f;
    [SerializeField] private float smoothDampX = .05f;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private Transform Groundpos;
    [SerializeField] private GameObject thirdpersonCamera;
    [SerializeField] private TMP_Text PlayerNameText;
    [SerializeField] private GameObject[] destination;
    [HideInInspector] public string PlayerUsername;
    [SerializeField] private AudioClip JumpAudioClip;
    Rigidbody rb;
    float smoothDampReference;
    float currentvelocity;
    float GroundSphereRadius = 0.1f;
    private GameObject Camera;
    Joystick joystick;
    Button JumpBtn;
    bool isGrounded;
    Vector3 smoothdamp;
    Quaternion smoothRotation;
    int i = 0;
    float x; float y;
    Animator animator;
    AudioSource audios;
   
    public bool canMove;
    public bool enablemobileInput = false;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject Btn = GameObject.Find("JumpButton");
        JumpBtn = Btn.GetComponent<Button>();
        GameObject joy = GameObject.Find("Fixed Joystick");
        joystick = joy.GetComponent<Joystick>();
        GameObject TouchField = GameObject.Find("TouchField");
    }
    void Start()
    {
        canMove = true;
        //GameManager.instace.WinPanel.SetActive(false);
        if(instance == null)
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody>(); 
        audios = GetComponent<AudioSource>();
        Camera = GameObject.Find("Main Camera");

        if (photonview.IsMine)
        {
            thirdpersonCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.NickName;
            animator = GetComponent<Animator>();
        }
        else
            PlayerNameText.text = photonView.Owner.NickName;

        JumpBtn.onClick.AddListener(JumpButton); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(photonView.IsMine) 
        {
            if(canMove && GameManager.instace.CountDownOver)
            playerMove();
            PlayerPrefs.GetString("UserName");
        }
        else
        {
            SmoothMoveOtherScreenPlayer();
        }
    }
    private void Update()
    {
        if (enablemobileInput)
        {
             x = joystick.Horizontal;
             y = joystick.Vertical;

            float p = Mathf.SmoothDamp(0, FixedTouchField.instance.TouchDist.y, ref smoothDampReference, Time.deltaTime*smoothDampY);
            float q = Mathf.SmoothDamp(0, FixedTouchField.instance.TouchDist.x, ref smoothDampReference, Time.deltaTime*smoothDampX);
            thirdpersonCamera.GetComponent<CinemachineFreeLook>().m_YAxis.Value +=p;
            thirdpersonCamera.GetComponent<CinemachineFreeLook>().m_XAxis.Value -= q;

            thirdpersonCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = null;
            thirdpersonCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = null;

        }
        else
        {
             x = Input.GetAxis("Horizontal");
             y = Input.GetAxis("Vertical");
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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
            audios.PlayOneShot(JumpAudioClip);
            animator.SetBool("jump", true);


        }
        else
        {
            animator.SetBool("jump", false);
            animator.SetBool("run", false);

        }

        if (this.transform.position.y <= -15f)
        {
            Destroy(this.gameObject);
            if (transform.position.z >= GameManager.instace.NextRespawnpos.transform.position.z)
            {
                PhotonNetwork.Instantiate(GameManager.instace.PlayerPrefab.name, GameManager.instace.NextRespawnpos.transform.position, Quaternion.identity, 0);
                i++;
            }
            else
                PhotonNetwork.Instantiate(GameManager.instace.PlayerPrefab.name, GameManager.instace.InitialRespawnpos.transform.position, Quaternion.identity, 0);

        }



        Vector3 direction = new Vector3(x, 0, y);
        if (direction.magnitude >= 0.1f)
        {


            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;
            float rot = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentvelocity, smoothRottime);
            transform.rotation = Quaternion.Euler(new Vector3(0, rot, 0));
            Vector3 moveAngle = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            rb.MovePosition(rb.position + moveAngle * Time.fixedDeltaTime * speed);
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        } }
    public void JumpButton()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
            animator.SetBool("jump", true);
            audios.PlayOneShot(JumpAudioClip);

        }
         else
            {
                animator.SetBool("jump", false);
                animator.SetBool("run", false);
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
            smoothdamp = (Vector3)stream.ReceiveNext();
            smoothRotation = (Quaternion)stream.ReceiveNext();
        }
    }

}
