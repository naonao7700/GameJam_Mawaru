using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmove : MonoBehaviour
{
    public float R = 15; //?v???C???[???~?̒??S?Ƃ??????̔??a
    private float Angle; // ?p?x
    public float addAngle; // ?~?^???̑??x
    public float centerX;
    public float centerY;
    private float posX;
    private float posY;
    [SerializeField] private int count;
    [SerializeField] private float _count2;
    // Start is called before the first frame update
    void Start()
    {
        _count2 = 0.0f;
        count = 0;
        Angle = 0;
        //center = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _count2 += Time.deltaTime;

        if( _count2 > 1.0f )
        {
            _count2 = 0.0f;
            count++;
        }

        if(count >= 3)
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
        //count++;
    }
}
