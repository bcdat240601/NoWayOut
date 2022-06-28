using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class CursorControl : MonoBehaviour
{
    private RawImage Cursor;
    private PlayableDirector[] Timeline;
    // Start is called before the first frame update
    void Start()
    {

        Cursor = GetComponent<RawImage>();
        Timeline = FindObjectsOfType<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timeline != null)
        {
            int flag = 0;
            foreach (var Cut in Timeline)
            {
                if (Cut.state == PlayState.Playing)
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                Cursor.enabled = true;
            }
            else
            {
                Cursor.enabled = false;
            }
        }
    }
}
