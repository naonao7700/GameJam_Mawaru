using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    PlayerObject player;

    [SerializeField]
    GameObject mainBulletPrefab;

    [SerializeField]
    GameObject subBulletPrefab;

    [SerializeField]
    AudioClip bulletSound;

    [SerializeField]
    float mainShotInterval; // 通常時の長針の弾のインターバル

    [SerializeField]
    float subShotInterval; // 通常時の短針の弾のインターバル

    [SerializeField]
    float timeStopMainShotInterval; // 時止め時の長針の弾のインターバル

    [SerializeField]
    float timeStopSubShotInterval; // 時止め時の短針の弾のインターバル

    [SerializeField]
    float subShotWayAngle; // 短針の3wayの角度

    private float mainShotTimer;
    private float subShotTimer;

    public bool mainShot;
    public bool subShot;

    
    private void Update()
    {
        mainShotTimer += Time.deltaTime;
        subShotTimer += Time.deltaTime;
       
       
        if (mainShot)
        {
            if (GameManager.IsTimeStop())
            {
                if (mainShotTimer < timeStopMainShotInterval)
                    return;
                MainShot();
                mainShotTimer = 0f;
            }
            else if (!GameManager.IsTimeStop())
            {
                if (mainShotTimer < mainShotInterval)
                    return;
                MainShot();
                mainShotTimer = 0f;
            }
        }
        if (subShot)
        {
            if (GameManager.IsTimeStop())
            {
                if (subShotTimer < timeStopSubShotInterval)
                    return;
                SubShot();
                subShotTimer = 0f;
            }
            if (subShotTimer < subShotInterval)
                return;
            SubShot();
            subShotTimer = 0f;
        }
    }

    public void MainShot()
    {
        Instantiate(mainBulletPrefab, player.mainShotPos.position, Quaternion.Euler(0, 0, player.mainRot));
    }
    public void SubShot()
    {
        Instantiate(subBulletPrefab, player.subShotPos.position, Quaternion.Euler(0, 0, player.subRot));
        Instantiate(subBulletPrefab, player.subShotPos.position, Quaternion.Euler(0, 0, player.subRot - subShotWayAngle));
        Instantiate(subBulletPrefab, player.subShotPos.position, Quaternion.Euler(0, 0, player.subRot + subShotWayAngle));
        GameManager.PlaySE(bulletSound);
    }
}
