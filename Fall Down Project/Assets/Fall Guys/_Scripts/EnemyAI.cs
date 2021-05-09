using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviourPun
{ 
    public static EnemyAI instance;
    //[SerializeField] private PhotonView photonview;
    private NavMeshAgent agent;
    public GameObject[] destination;
    private Rigidbody rb; int i = 0;
    public Material mat;   
    [SerializeField] private TMP_Text EnemyNameText;
    public bool canMove;

    void Start()
    {
        canMove = true;
        if (instance == null)
        {
            instance = this;
        }
        EnemyNameText.text = "FallDown#" + Random.Range(0000, 9999);
        destination = GameObject.FindGameObjectsWithTag("Destination");
        rb = GetComponent<Rigidbody>();


        //foreach (Transform t in transform)
        //{
        //    //destination.add
        //    new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

        //}
        new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);

       
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;

    }
   
    private void Update()
    {

        if (canMove && GameManager.instace.CountDownOver)
        {
            GetComponent<Animator>().Play("run");

            if (GameManager.instace.firstPlayer) agent.isStopped = true;
            agent.SetDestination(destination[i].transform.position);

            if (transform.position.z >= destination[i].transform.position.z)
            {

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
                PhotonNetwork.Instantiate(GameManager.instace.EnemyPrefab.name, GameManager.instace.InitialRespawnpos.transform.position, Quaternion.identity, 0);
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
                Playercontroller.instance.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                GameManager.instace.WinPanel.SetActive(true);
                GameManager.instace.WinnernameText.text = " Winner: " + EnemyNameText.text;
            }
        }

    }

}
