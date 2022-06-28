using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
    int index;
    private Animation LightAnim;
    // Start is called before the first frame update
    void Start()
    {
        LightAnim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (index == 0)
        {
            StartCoroutine(LightAni());
        }
        
    }
    IEnumerator LightAni()
    {
        index = Random.Range(1, 4);
        if (index == 1)
        {
            LightAnim.Play("TorchLightanim");
        }
        else if (index == 2)
        {
            LightAnim.Play("TorchLightanim2");
        }
        else
        {
            LightAnim.Play("TorchLightanim3");
        }
        yield return new WaitForSeconds(1f);
        index = 0;
        
    }
}
