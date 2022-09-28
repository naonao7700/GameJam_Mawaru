using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class BarManagaer : MonoBehaviour
{
    SkeletonGraphic skeletonGraphic;

    TrackEntry trackEntry; 


    // Start is called before the first frame update
    void Start()
    {
        skeletonGraphic = GetComponent<SkeletonGraphic>();
        StartAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartAnim()
    {
        skeletonGraphic.AnimationState.AddAnimation(1, "gear001", true, 0);
        skeletonGraphic.AnimationState.AddAnimation(2, "gear002", true, 0);
        skeletonGraphic.AnimationState.AddAnimation(3, "gear003", true, 0);
    }
}
