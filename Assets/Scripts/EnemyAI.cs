using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{    
    private NavMeshAgent agent;
    private Animator animat;
    private bool PlayerinChaseSight;
    private bool PlayerinAttackSight;    
    private float SneakDistance = 25;
    private float ChaseDistance = 20;
    private float AttackDistance = 6;
    private Transform player;
    [SerializeField] private LayerMask playerMask;    
    [SerializeField] private LayerMask Door;
    //[SerializeField] private LayerMask EnclosedSpace;
    //[SerializeField] private GameObject TheGround;
    private bool WalkPointSet = false;
    [SerializeField] private Vector3 WalkPoint = Vector3.zero;
    public float WalkPointRange = 20f;
    private Vector3 DistanceToWalkPoint1;
    private Vector3 DistanceToWalkPoint2;
    private bool isAttack = false;
    private DoorCheck[] Doors;
    private PlayerController playerController;
    private bool HearTheSound;    
    private bool IsNearDoor;    
    private bool ListisClear = true;
    private NavMeshPath path;
    [SerializeField] private List<DoorCheck> CopyList;
    private Vector3 TempPosition;
    private Animation HurtAnimation;

    private float DistancetoDestination;
    
    // Start is called before the first frame update        
    void Start()
    {        
        //HurtAnimation = GameObject.FindGameObjectWithTag("Hurt").GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        animat = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animat.SetBool("Trigger", true);
        Doors = FindObjectsOfType<DoorCheck>();
        foreach (DoorCheck Door in Doors)
        {
            Door.OnSounded += Door_OnSounded;
        }        
        playerController = player.GetComponent<PlayerController>();
        playerController.OnSounded += GetPlayerPosition_OnSounded;
        path = new NavMeshPath();
        CopyList = new List<DoorCheck>();
        foreach (DoorCheck door in Doors)
        {
            CopyList.Add(door);
        }
    }

    private void GetPlayerPosition_OnSounded(Vector3 PlayerPosition)
    {
        if (Vector3.Distance(transform.position, PlayerPosition) > 150f) return;
        HearTheSound = true;        
        WalkPoint = PlayerPosition;
        ListisClear = true;
        RenewList();
        WalkPointSet = true;        
    }

    private void Door_OnSounded(Vector3 DoorPosition)
    {
        if (Vector3.Distance(transform.position, DoorPosition) > 150f) return;
        HearTheSound = true;        
        WalkPoint = DoorPosition;
        ListisClear = true;
        RenewList();
        WalkPointSet = true;        
    }


    //Update is called once per frame
    void Update()
    {
        CheckPlayerIsHided();
        DistancetoDestination = Vector3.Distance(transform.position, WalkPoint);
        
        PlayerinChaseSight = Physics.CheckSphere(transform.position, ChaseDistance, playerMask);
        PlayerinAttackSight = Physics.CheckSphere(transform.position, AttackDistance, playerMask);

        if (!PlayerinChaseSight && !PlayerinAttackSight) Patroling();

        else if (PlayerinChaseSight && !PlayerinAttackSight) Chase();

        //else if (PlayerinAttackSight) Attack();
    }
    private void Patroling()
    {
        if (!WalkPointSet) SearchWalkPoint();
        else if (CheckCanBeReached(transform.position, WalkPoint))
        {
            agent.SetDestination(WalkPoint);
        }
        else if (!CheckCanBeReached(transform.position, WalkPoint) && ListisClear)
        {
            ListisClear = false;          
            TempPosition = FindShortestPath(TempPosition);            
            agent.SetDestination(TempPosition);            
        }
        IsNearDoor = Physics.CheckSphere(transform.position, 5.5f, Door);
        if (IsNearDoor)
        {
            OpenDoorToPatrol();
        }
        if (!HearTheSound)
        {
            animat.SetBool("SneakyWalk", false);
            animat.SetBool("Chase", false);            
        }
        else
        {
            animat.SetBool("Chase", true);
        }
        if (HearTheSound && SneakDistance > DistancetoDestination)
        {
            animat.SetBool("SneakyWalk", true);
        }


        DistanceToWalkPoint1 = transform.position - WalkPoint;
        DistanceToWalkPoint2 = transform.position - TempPosition;
        if (DistanceToWalkPoint1.magnitude < 5f)
        {            
            if (!ListisClear)
            {
                ListisClear = true;
            }
            RenewList();
            WalkPointSet = false;
            HearTheSound = false;
        }
        if (DistanceToWalkPoint2.magnitude < 3f)
        {
            if (!ListisClear)
            {                
                ListisClear = true;
            }
        }
    }
    private void SearchWalkPoint()
    {
        float RandomX = Random.Range(-WalkPointRange, WalkPointRange);
        float RandomZ = Random.Range(-WalkPointRange, WalkPointRange);
        WalkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(WalkPoint, out hit, 1f, NavMesh.AllAreas))
        {
            HearTheSound = false;            
            WalkPointSet = true;
        }
        
    }
    private void Chase()
    {
        transform.LookAt(player);
        if (!animat.GetBool("Chase"))
        {
            animat.SetBool("Chase",true);
        }
        if (animat.GetBool("SneakyWalk"))
        {
            animat.SetBool("SneakyWalk", false);
        }
        IsNearDoor = Physics.CheckSphere(transform.position, 5.5f, Door);
        if (IsNearDoor)
        {
            OpenDoorToPatrol();
        }
    }
    //private void Attack()
    //{
    //    agent.SetDestination(transform.position);
    //    if (!isAttack)
    //    {
    //        StartCoroutine(AttackAnimation());
    //    }
    //}
    //IEnumerator AttackAnimation()
    //{
    //    isAttack = true;
    //    animat.SetBool("attack", true);
    //    HurtAnimation.Play();
    //    playerController.player.TakeDamage();
    //    yield return new WaitForSeconds(1.5f);
    //    animat.SetBool("attack", false);
    //    isAttack = false;
    //}
    private void OpenDoorToPatrol()
    {
        DoorCheck DoorNearest = FindDoorToOpen();
        if (!DoorNearest.DoorOpen)
        {
            DoorNearest.OpenDoor();
        }
    }
    private Vector3 FindShortestPath(Vector3 TempPosition)
    {
        // khởi tạo các biến so sánh
        DoorCheck Door = null;
        Vector3 Doorposition = Vector3.zero;
        float MinimunDistance = Mathf.Infinity;
        // chạy vòng lặp lấy tất cả các script DoorCheck
        foreach (DoorCheck door in CopyList)
        {
            // kiểm tra Enemy ở phía nào của Door
            bool FirstObject = CheckCanBeReached(transform.position,door.Position1.position);
            bool SecondObject = CheckCanBeReached(transform.position, door.Position2.position);

            if (FirstObject && !SecondObject)
            {
                //Áp dụng thuật toán tìm đường đi
                // Distance1 tính cost, hàm g
                float Distance1 = Vector3.Distance(transform.position, door.Position1.position);
                // Distance2 tính H
                float Distance2 = Vector3.Distance(door.Position2.position, WalkPoint);
                float Distance = Distance1 + Distance2;
                if (Distance < MinimunDistance)
                {
                    Doorposition = door.Position1.position;
                    MinimunDistance = Distance;
                    Door = door;
                }
            }
            else if (!FirstObject && SecondObject)
            {
                float Distance1 = Vector3.Distance(transform.position, door.Position2.position);
                float Distance2 = Vector3.Distance(door.Position1.position, WalkPoint);
                float Distance = Distance1 + Distance2;
                if (Distance < MinimunDistance)
                {
                    Doorposition = door.Position2.position;
                    MinimunDistance = Distance;
                    Door = door;
                }
            }
            
        }
        if (MinimunDistance == Mathf.Infinity)
        {
            return TempPosition;
        }
        CopyList.Remove(Door);
        return Doorposition;
    }
    //open the door if enemy gets close
    private DoorCheck FindDoorToOpen()
    {
        DoorCheck DoorNearest = null;
        float MinimunDistance = Mathf.Infinity;
        foreach (DoorCheck Door in Doors)
        {
            float Distance1 = Vector3.Distance(transform.position, Door.GetComponent<Transform>().position);
            if (Distance1 < MinimunDistance)
            {
                MinimunDistance = Distance1;
                DoorNearest = Door;
            }
        }
        return DoorNearest;
    }
    private bool CheckCanBeReached(Vector3 Start, Vector3 Destination)
    {        
        NavMesh.CalculatePath(Start, Destination, NavMesh.AllAreas, path);        
        return path.status == NavMeshPathStatus.PathComplete;
    }
    private void RenewList()
    {
        CopyList.Clear();
        foreach (DoorCheck door in Doors)
        {
            CopyList.Add(door);
        }
    }

    private void CheckPlayerIsHided()
    {
        if (playerController.IsHide)
        {
            ChaseDistance = 1f;
            AttackDistance = 0f;
        }
        else
        {
            ChaseDistance = 20f;
            AttackDistance = 6f;
        }
    }

}
