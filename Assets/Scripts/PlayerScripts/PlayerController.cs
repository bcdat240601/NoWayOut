using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using System;

[RequireComponent(typeof(MouseLook))]
public class PlayerController : MonoBehaviour
{    
    [SerializeField] private CharacterController character;
    private MouseLook ML;
    public Player player;    
        
    Vector3 velocity;
    [SerializeField] private GameObject TheGround;
    private bool isGrounded = true;
    private float CheckDistance = 0.5f;    
    [SerializeField] private LayerMask Ground;
    public event Action OnRunning;
    public event Action OnWalk;
    public event Action<Vector3> OnSounded;
    private RunningUI RunUI;
    private AudioSource FootStep;

    public bool IsHide = false;
    [HideInInspector] public DoorCheck[] Doors;

    void Start()
    {
        ML = GetComponent<MouseLook>();
        player = new Player();
        player.Died += Player_Died;        
        RunUI = FindObjectOfType<RunningUI>();
        FootStep = GetComponent<AudioSource>();     

    }


    private void Player_Died(object sender, System.EventArgs e)
    {
        StartCoroutine(ChangeSceneGameOver());
    }

    void Update()
    {
        Gravity();
        Movement();        
    }
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 vt = transform.right * x + transform.forward * z;        
        if (RunUI != null && RunUI.CurrentStamina != 0 && Input.GetKey(KeyCode.LeftShift) && vt != Vector3.zero)
        {
            OnRunning?.Invoke();
            if (OnSounded != null) OnSounded(TheGround.transform.position);

            character.Move(vt * player.RunSpeed * Time.deltaTime);            

            if (!FootStep.isPlaying) FootStep.Play();            
        }
        else
        {
            if (FootStep.isPlaying) FootStep.Stop();
            OnWalk?.Invoke();
            character.Move(vt * player.NormalSpeed * Time.deltaTime);            
        }        
        
        
    }
    //void Jump()
    //{
    //    isGrounded = Physics.CheckSphere(TheGround.transform.position, CheckDistance, Ground);
    //    if (isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = 0f;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    //    {
    //        character.Move(new Vector3(0f, Jumpheight* Time.deltaTime, 0f));
    //        isGrounded = false;
    //    }

    //    velocity.y += gravity * Time.deltaTime;

    //    character.Move(velocity * Time.deltaTime /2);
    //}
    void Gravity()
    {
        velocity.y += player.Gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime / 2);
        isGrounded = Physics.CheckSphere(TheGround.transform.position, CheckDistance, Ground);
        if (isGrounded && velocity.y < 0) velocity.y = 0f;        
    }    

    IEnumerator ChangeSceneGameOver()
    {
        ML.enabled = false;
        ML.DeadCamera();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
