using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorld : MonoBehaviour
{
    private float scale;
    private bool world;
    private int count;
    void Start()
    {
        scale = 0.5f;
        world = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            world = true;
            
        }
        if(world == true)
        {
            count++;
            if (count < 400)
            {
                scale += 0.1f;
                this.transform.localScale = new Vector3(scale, scale, 0);
            }
            if(count >= 400)
            {
                scale -= 0.05f;
                this.transform.localScale = new Vector3(scale, scale, 0);
                if(scale <= 0.5)
                {
                    scale = 0.5f;
                    this.transform.localScale = new Vector3(scale, scale, 0);
                    world = false;
                }
            }
        }
        if(world == false)
        {
            count = 0;
        }
    }
}
