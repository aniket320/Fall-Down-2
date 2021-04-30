using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviourPun/*, IPunObservable*/
{
    public static EnemyAI instance;
    //[SerializeField] private PhotonView photonview;
    public NavMeshAgent agent;
    [SerializeField] private GameObject FinishLine;
    public GameObject[] destination; int i = 0;
    public bool canMove;
    //[SerializeField] private float smoothRottime;
    //[SerializeField] private float speed;
    //[SerializeField] private float JumpForce = 100f;
    //[SerializeField] private float smoothMove = 10f;
    //[SerializeField] private LayerMask GroundLayer;
    //[SerializeField] private Transform Groundpos;
    //[SerializeField] private GameObject thirdpersonCamera;
    [SerializeField] private TMP_Text EnemyNameText;
    //public bool canMove;
    //Rigidbody rb;
    //float currentvelocity;
    //float GroundSphereRadius = 0.1f;
    //private GameObject Camera;
    //Joystick joystick;
    //Button JumpBtn;
    //bool isGrounded;
    //Vector3 smoothdamp;
    //Quaternion smoothRotation;

    //// Start is called before the first frame update
    //private void Awake()
    //{
    //    GameObject Btn = GameObject.Find("JumpButton");
    //    JumpBtn = Btn.GetComponent<Button>();
    //    GameObject joy = GameObject.Find("Fixed Joystick");
    //    joystick = joy.GetComponent<Joystick>();
    //}
    void Start()
    {
        canMove = true;
        EnemyNameText.text = "FallDown#" + Random.Range(0000, 9999);
        destination = GameObject.FindGameObjectsWithTag("Destination");
        //destination = new List<GameObject>();
        foreach(Transform t in transform)
        {
            //destination.add
        }
        ////GameManager.instace.WinPanel.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }

        //firstPlayer = false;
        //canMove = true;

        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //FinishLine = GameObject.FindGameObjectWithTag("Finishline");
        //Camera = GameObject.Find("Main Camera");

        //if (photonview.IsMine)
        //{
        //    thirdpersonCamera.SetActive(true);
        //    PlayerNameText.text = PhotonNetwork.NickName;
        //}
        //else
        //    PlayerNameText.text = photonView.Owner.NickName;

        //JumpBtn.onClick.AddListener(JumpButton);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (photonView.IsMine)
        //{
        //    if (canMove)
        //        playerMove();
        //    PlayerPrefs.GetString("UserName");

        //}
        //else
        //{
        //    SmoothMoveOtherScreenPlayer();
        //}
    }
    private void Update()
    {

        if (canMove)
        {
            if(GameManager.instace.firstPlayer) agent.isStopped = true;
            agent.SetDestination(destination[i].transform.position);         
            if (transform.position.z >= destination[i].transform.position.z)
            {
                //Destroy(destination[i]);
                i++;
                if (i >= destination.Length)
                    canMove = false;
            }

    //    if (this.transform.position.y <= -15f)
    //    {
    //    Destroy(this.gameObject);
    //    PhotonNetwork.Instantiate(GameManager.instace.EnemyPrefab.name, GameManager.instace.instatiatepos.transform.position, Quaternion.identity, 0);
         //}
        }



    }
    //private void LateUpdate()
    //{

    //}
    //public void SmoothMoveOtherScreenPlayer()
    //{
    //    transform.position = Vector3.Lerp(transform.position, smoothdamp, Time.deltaTime * smoothMove);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, smoothRotation, Time.deltaTime * smoothMove);
    //}
    //public void playerMove()
    //{
    //    isGrounded = Physics.CheckSphere(Groundpos.position, GroundSphereRadius, GroundLayer);

    //    if (Input.GetButtonDown("Jump") && isGrounded)
    //    {
    //        rb.AddForce(Vector3.up * JumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    //    }

    //    if (this.transform.position.y <= -15f)
    //    {
    //    Destroy(this.gameObject);
    //    PhotonNetwork.Instantiate(GameManager.instace.PlayerPrefab.name, GameManager.instace.instatiatepos.transform.position, Quaternion.identity, 0);
    ////}

    //    float x = /*Input.GetAxis("Horizontal") ||*/ joystick.Horizontal;
    //    float y = /*Input.GetAxis("Vertical") ||*/ joystick.Vertical;

    //    Vector3 direction = new Vector3(x, 0, y);

    //    if (direction.magnitude >= 0.1f)
    //    {
    //        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.transform.eulerAngles.y;
    //        float rot = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentvelocity, smoothRottime);
    //        transform.rotation = Quaternion.Euler(new Vector3(0, rot, 0));
    //        Vector3 moveAngle = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
    //        rb.MovePosition(rb.position + moveAngle * Time.fixedDeltaTime * speed);
    //    }
    //}
    //public void JumpButton()
    //{
    //    if (isGrounded)
    //    {
    //        rb.AddForce(Vector3.up * JumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    //    }
    //}
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    if (stream.IsReading)
    //    {
    //        smoothdamp = (Vector3)stream.ReceiveNext();
    //        smoothRotation = (Quaternion)stream.ReceiveNext();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //if (GameManager.instace.NoOfPlayerQualified <= GameManager.instace.NoOfPlayerCanQualifie)
        //{
        if (other.CompareTag("Finishline"))
        {
            GameManager.instace.firstPlayer = true;
            if (GameManager.instace.firstPlayer)
            {
                GameManager.instace.WinPanel.SetActive(true);
                GameManager.instace.WinnernameText.text = " Winner: " + EnemyNameText.text;
                //PhotonNetwork.Destroy(this.gameObject);
            }
            //if (photonview.IsMine)
            //GameManager.instace.coroutineCall();

            //GameManager.instace.NoOfPlayerQualified++;
        }
        //}

    }

}
