﻿using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviourPun/*, IPunObservable*/
{
    public static EnemyAI instance;
    //[SerializeField] private PhotonView photonview;
    public NavMeshAgent agent;
    [SerializeField] private GameObject FinishLine;
    public GameObject[] destination;
    private Rigidbody rb;
    int i = 0;
    public bool canMove;
    [SerializeField] private TMP_Text EnemyNameText;

    void Start()
    {
        canMove = true;
     
        EnemyNameText.text = "FallDown#" + Random.Range(0000, 9999);
        destination = GameObject.FindGameObjectsWithTag("Destination");
        rb = GetComponent<Rigidbody>();

        foreach (Transform t in transform)
        {
            //destination.add
        }
        if (instance == null)
        {
            instance = this;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;

    }
    public void JumpButton()
    {
        if (true)
        {
            rb.AddForce(Vector3.up * 100 * Time.fixedDeltaTime, ForceMode.Impulse);
            GetComponent<Animator>().Play("jump");
        }
    }
    private void Update()
    {

        if (canMove && GameManager.instace.comeDownOver)
        {
            if(GameManager.instace.firstPlayer) agent.isStopped = true;
            agent.SetDestination(destination[i].transform.position);         
            if (transform.position.z >= destination[i].transform.position.z)
            {
                //Destroy(destination[i]);
                i++;
                if (i >= destination.Length)
                {
                    agent.isStopped = true;
                    canMove = false;
                }
                    

            }

            if (this.transform.position.y <= -15f)
            {
                Destroy(this.gameObject);
                PhotonNetwork.Instantiate(GameManager.instace.EnemyPrefab.name, GameManager.instace.instatiatepos.transform.position, Quaternion.identity, 0);
            }
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finishline"))
        {
            GameManager.instace.firstPlayer = true;
            if (GameManager.instace.firstPlayer)
            {
                agent.isStopped = true;
                Playercontroller.instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                GameManager.instace.WinPanel.SetActive(true);
                GameManager.instace.WinnernameText.text = " Winner: " + EnemyNameText.text;
            }
        }

    }

}
