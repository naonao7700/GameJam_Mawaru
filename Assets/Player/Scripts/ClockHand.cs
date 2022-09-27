using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockHand : MonoBehaviour
{
    [SerializeField]
    Transform LongHandTransform;
    [SerializeField]
    Transform ShortHandTransform;

    float rotateX;
    private void Update()
    {
        RotationHands();
    }

    void RotationHands()
    {
        float stickRH = Input.GetAxis("R_Horizontal");
        float stickRV = Input.GetAxis("R_Vertical");
        float stickLH = Input.GetAxis("L_Horizontal");
        float stickLV = Input.GetAxis("L_Vertical");
        Debug.Log(new Vector2(stickRH,stickRV));

        Vector2 v = new Vector2(stickRH, stickRV).normalized;
        float rotateR = Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg;

        //float RotateR = Mathf.Atan2(stickRH, stickRV) * Mathf.Rad2Deg;
        if (rotateR < 0)
            rotateR += 360;

        LongHandTransform.localEulerAngles = new Vector3(0, 0, rotateR);
    }
}
