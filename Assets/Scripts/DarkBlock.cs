using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkBlock : MonoBehaviour
{
    public BoxCollider BCollider;    
    public Text TextAlert;
    private Latern GetLatern;
    private void Start()
    {
        GetLatern = Object.FindObjectOfType<Latern>();
        GetLatern.OnPickUp += DarkBlock_OnPickUp;       
    }

    private void DarkBlock_OnPickUp()
    {
        BCollider.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetText());
        }
    }

    IEnumerator SetText()
    {
        TextAlert.text = "It's too dark inside, I need something before I get in";
        yield return new WaitForSeconds(2f);
        TextAlert.text = "";
    }
}
