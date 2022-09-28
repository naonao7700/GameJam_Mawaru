using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class NewGaugeScript : MonoBehaviour
{
    [SerializeField] SkeletonGraphic skeletonGraphic;

    TrackEntry track1;
    TrackEntry track2;
    TrackEntry track3;


    // Start is called before the first frame update
    void Start()
    {
        StartAnim();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartAnim()
    {
        track1 = skeletonGraphic.AnimationState.AddAnimation(1, "gear_1", true, 0);
        track2 = skeletonGraphic.AnimationState.AddAnimation(2, "gear_2", true, 0);
        track3 = skeletonGraphic.AnimationState.AddAnimation(3, "gear_3", true, 0);

        ChangeSpeed(0.5f);
    }

    //スピードを調整する関数
    public void ChangeSpeed(float _speed)
    {
        float gearSpeed = 0f;
        gearSpeed = _speed;
        if (gearSpeed > 1)
        {
            gearSpeed = 1f;
        }
        if (gearSpeed < 0)
        {
            gearSpeed = 0f;
        }

        //  var track:TrackEntry =
        //      skeletonAnimation.state.setAnimationByName(trackIndex, "animName");
        //  track.timeScale = 2;
        track1.TimeScale = gearSpeed;
        track2.TimeScale = gearSpeed;
        track3.TimeScale = gearSpeed;

    }


    public void FillAnim()
    {
        skeletonGraphic.AnimationState.AddAnimation(4, "fill", false, 0);
        skeletonGraphic.AnimationState.AddAnimation(5, "shake", true, 0);

    }

    public void CloseAnim()
    {
        skeletonGraphic.AnimationState.AddAnimation(4, "fill_end", false, 0);
        skeletonGraphic.AnimationState.AddAnimation(5, "shake", false, 0);
    }

}
