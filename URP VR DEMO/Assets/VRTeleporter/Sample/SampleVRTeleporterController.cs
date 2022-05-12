using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleVRTeleporterController : MonoBehaviour {

    public VRTeleporter teleporter;
    public VRTeleporter_L teleporter_L;
    private ExperimentManager ex;

    public bool IsGun_L;
    public bool IsClick_L;
    public bool IsGun;
    public bool IsClick;
    private float time_buffer;

    private float Timer = 0;
    private float Timer_L = 0;
    [SerializeField]
    private bool Teleport_Start;
    [SerializeField]
    private bool Teleport_End;
    [Space]
    [SerializeField]
    private bool Teleport_Start_L;
    [SerializeField]
    private bool Teleport_End_L;
    private float maxVelocity;
    private float maxVelocity_L;

    private void Start()
    {
        teleporter.ToggleDisplay(false);
        ex = FindObjectOfType<ExperimentManager>();
    }

    void Update () {

        RightHand_Teleport();
        LeftHand_Teleport();

    }

    //public void IsGestureReadyToGun() {
    //    IsGun = true;
    //}
    //public void Goteleport() {
    //    IsClick = true; ;
    //}


    void RightHand_Teleport() {
        Vector3 veloc = ex.velocity_R;
        float offset = 1.0f;
       
        ///取消射線_設定為閒置1秒
        
        if (!IsGun && !IsClick )
        {
            time_buffer += Time.deltaTime;
            if (time_buffer > 1.0f)
            {

                teleporter.ToggleDisplay(false);
                Teleport_Start = false;
                maxVelocity = 0;
            }
        }
        else { time_buffer = 0; }
        ///判斷手有沒有比出GUN
        if (IsGun && ex.direction.z >= 230f )
        {
            Timer += Time.deltaTime;
            if (Timer > 1.0f)
            {
                
                Teleport_Start = true; //可以開始傳送
            }
        }
        else { Timer = 0; }
        if (Teleport_Start) {
            if (Mathf.Abs(ex.velocity_R.magnitude) > Mathf.Abs(maxVelocity))
            {
                maxVelocity = Mathf.Abs(ex.velocity_R.magnitude);
                teleporter.strength = offset * maxVelocity * 0.8f;
                //Debug.Log("|||||" + maxVelocity);
            }
        }
        ///判斷在已經GUN的情況下CLICK
        if (Teleport_Start && IsClick && ex.direction.y <= 225f)
        {

            Teleport_End = true;
        }
        if (Teleport_Start)
        {
            //顯示射線
            teleporter.ToggleDisplay(true);
        }
        ///傳送
        if (Teleport_End)
        {

            teleporter.Teleport();              //傳送指令
            teleporter.ToggleDisplay(false);    //關掉射線
            Teleport_Start = false;             //重置狀態
            Teleport_End = false;               //重置狀態
            maxVelocity = 0;
        }
    }
    void LeftHand_Teleport() {
        float offset = 1.0f;
        ///LeftHand//////////////////////////////////////////////////
        ///取消射線_設定為閒置1秒
        if (!IsGun_L && !IsClick_L)
        {
            time_buffer += Time.deltaTime;
            if (time_buffer > 1.0f)
            {

                teleporter_L.ToggleDisplay(false);
                Teleport_Start_L = false;
                maxVelocity_L = 0;
            }
        }
        else { time_buffer = 0; }
        ///判斷手有沒有比出GUN
        if (IsGun_L && ex.direction_L.z >= 70f)
        {
            Timer_L += Time.deltaTime;
            if (Timer_L > 1.0f)
            {
                if (Mathf.Abs(ex.velocity_L.magnitude) > Mathf.Abs(maxVelocity_L))
                {
                    maxVelocity_L = Mathf.Abs(ex.velocity_L.magnitude);
                    teleporter_L.strength = offset * maxVelocity_L * 0.8f;
                    //Debug.Log("|||||" + maxVelocity);
                }
                Teleport_Start_L = true; //可以開始傳送
            }
        }
        else { Timer_L = 0; }
        ///判斷在已經GUN的情況下CLICK
        if (Teleport_Start_L && IsClick_L && ex.direction_L.z <= 65f)
        {

            Teleport_End_L = true;
        }
        if (Teleport_Start_L)
        {
            //顯示射線
            teleporter_L.ToggleDisplay(true);
        }
        ///傳送
        if (Teleport_End_L)
        {

            teleporter_L.Teleport();              //傳送指令
            teleporter_L.ToggleDisplay(false);    //關掉射線
            Teleport_Start_L = false;             //重置狀態
            Teleport_End_L = false;               //重置狀態
            maxVelocity_L = 0;
        }
    }
}
