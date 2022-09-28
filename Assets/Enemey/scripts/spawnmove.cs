using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmove : MonoBehaviour
{
    private float R = 15; //ƒvƒŒƒCƒ„[‚ð‰~‚Ì’†S‚Æ‚µ‚½Žž‚Ì”¼Œa
    private float Angle; // Šp“x
    public float addAngle; // ‰~‰^“®‚Ì‘¬“x
    public float centerX;
    public float centerY;
    private float posX;
    private float posY;
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Angle = 0;
        //center = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= 1800)
        {
            Destroy(gameObject);
        }
        posX = this.transform.position.x;
        posY = this.transform.position.y;
        float addX = Mathf.Cos(Angle) * R;
        float addY = Mathf.Sin(Angle) * R;
        //Vector3 pos = new Vector3(center.transform.position.x + addX, center.transform.position.y + addY,0 );
        gameObject.transform.position = new Vector3( centerX +addX, centerY+addY, 0 );
        Angle += addAngle;
        count++;
    }
}
