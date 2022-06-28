using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    public GameObject player;
    private bool isShowed = false;
    public Text textbox;
    float timedisappear;
    public ParticleSystem Attentioneffect;
    // Start is called before the first frame update
    void Start()
    {
        Attentioneffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (timedisappear <= Time.time && isShowed == true)
        {
            textbox.text = "";
            player.GetComponent<PlayerController>().enabled = true;           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isShowed == false)
        {
            player.GetComponent<PlayerController>().enabled = false;
            timedisappear = Time.time + 2f;            
            textbox.text = "Is there a weapon on the table ?";
            isShowed = true;
            Attentioneffect.Play();

        }
             
    }
}
