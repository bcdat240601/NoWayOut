using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfMoved : MonoBehaviour
{
    [SerializeField] private List<int> CurrentCombination;
    [SerializeField] private SkullInteract[] Skulls;
    private Animation animt;
    // Start is called before the first frame update
    void Start()
    {
        animt = GetComponent<Animation>();
        CurrentCombination = new List<int>();        
        foreach (SkullInteract skull in Skulls)
        {
            skull.OnInteractSkull += Skull_OnInteractSkull;
        }
    }

    private void Skull_OnInteractSkull(string name)
    {
        switch (name)
        {
            case "Skull1":
                if (CurrentCombination.Count == 4)
                {
                    CurrentCombination.RemoveAt(0);
                }
                CurrentCombination.Add(1);
                break;
            case "Skull2":
                if (CurrentCombination.Count == 4)
                {
                    CurrentCombination.RemoveAt(0);
                }
                CurrentCombination.Add(2);
                break;
            case "Skull3":
                if (CurrentCombination.Count == 4)
                {
                    CurrentCombination.RemoveAt(0);
                }
                CurrentCombination.Add(3);
                break;
            case "Skull4":
                if (CurrentCombination.Count == 4)
                {
                    CurrentCombination.RemoveAt(0);
                }
                CurrentCombination.Add(4);
                break;
        }
        CheckCorrect();
    }
    private void CheckCorrect()
    {
        if (CurrentCombination.Count != 4)
        {
            return;
        }
        if (CurrentCombination[0] == 1 && CurrentCombination[1] == 2 && CurrentCombination[2] == 3 && CurrentCombination[3] == 4)
        {
            animt.Play();
        }
    }
}
