using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteraction, IMessage
{
    [SerializeField] private GameObject[] CeilingLights;
    [SerializeField] private GameObject[] CeilingLightsPuzzle;
    private float TimeCoolDown = 0;
    public void interact()
    {
        if (Time.time >= TimeCoolDown)
        {
            TimeCoolDown = Time.time + 3f;
            StartCoroutine(ActivateLight());
        }
        
    }

    public string ShowMessage()
    {
        string message = "Press [E] to Activate";
        return message;
    }
    IEnumerator ActivateLight()
    {
        foreach (GameObject light in CeilingLights)
        {
            light.GetComponentInChildren<Light>().enabled = false;
        }
        yield return new WaitForSeconds(1f);
        foreach (GameObject light in CeilingLightsPuzzle)
        {
            light.GetComponentInChildren<Light>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject light in CeilingLights)
        {
            light.GetComponentInChildren<Light>().enabled = true;
        }
    }
}
