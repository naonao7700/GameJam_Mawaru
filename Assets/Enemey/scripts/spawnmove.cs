using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmove : MonoBehaviour
{
    private float R = 15; //ÉvÉåÉCÉÑÅ[Çâ~ÇÃíÜêSÇ∆ÇµÇΩéûÇÃîºåa
    private float Angle; // äpìx
    public float addAngle; // â~â^ìÆÇÃë¨ìx
    //private GameObject center;
    private float posX;
    private float posY;
    // Start is called before the first frame update
    void Start()
    {
        Angle = 0;
        //center = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        float addX = Mathf.Cos(Angle) * R;
        float addY = Mathf.Sin(Angle) * R;
        //Vector3 pos = new Vector3(center.transform.position.x + addX, center.transform.position.y + addY,0 );
        gameObject.transform.position = new Vector3( addX, addY, 0 );
        Angle += addAngle;
    }
}
