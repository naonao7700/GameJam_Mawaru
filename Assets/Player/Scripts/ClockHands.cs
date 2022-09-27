using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ˜ZŽÔ
public class ClockHands : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    GameObject shotPos;

    float shotTimer;
    public float shotInterval;

    private void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer < shotInterval)
        {
            return;
        }
        shotTimer = 0f;
        if (Input.GetKey(KeyCode.Space))
        {
            Shot();
        }
    }

    public void Shot()
    {
        Instantiate(bullet, shotPos.transform.position, transform.localRotation);
    }
}
