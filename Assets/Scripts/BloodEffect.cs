using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    [SerializeField]private PlayerController playerController;
    private Image BloodImage;
    // Start is called before the first frame update
    void Start()
    {
        BloodImage = GetComponentInChildren<Image>();
        SetColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        EffectControl();
    }

    public void EffectControl()
    {
        int opaque1 = (int)playerController.player.Health * 255 / 10;
        int opaque2 = 10 - opaque1;
        SetColor(opaque2);
    }

    public void SetColor(int opaque)
    {
        Color temp = BloodImage.color;
        temp.a = opaque;
        BloodImage.color = temp;
    }
}
