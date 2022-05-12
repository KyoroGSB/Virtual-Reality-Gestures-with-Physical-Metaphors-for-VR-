using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleStatus : MonoBehaviour
{
    private Vector3 scale;
    //public GameObject follow_obj;

    private GameObject L_Hand;
    private GameObject R_Hand;
    public GameObject Top_spot;
    [SerializeField]
    public bool Modify_Done;
    [SerializeField]
    private bool startModify;
    public float modifyrate;
    private HandEvent he;
    private ExperimentManager ex;
    private Vector3 tmp_Height;
    
    private bool startScale;
    private int WhichHand; // 0 --> R ,  1--> L
    public Vector3 distance_2Hand;
    private float ph_Speed = 0.8f;
    public  float maxVelocity = 0;
    public float Decrease = 0.95f;
    private bool changetoward;
    private bool tmp_b;
    void Start()
    {
        he = GetComponent<HandEvent>();
        ex = FindObjectOfType<ExperimentManager>();
        L_Hand = GameObject.FindGameObjectWithTag("LeftHand");
        R_Hand = GameObject.FindGameObjectWithTag("RightHand");
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!Modify_Done) { 

            
            if (startModify) {
                ModifywithfollowObj();
               
            }

            if (CheckHandpose() && CheckTwoHandClose())
            {
                startModify = true;
                tmp_Height = (L_Hand.transform.position + R_Hand.transform.position)/2;
            }
            else {
                startModify = false;
            }


           
        }
        
        
       
    }
    
    public void ModifywithfollowObj() {
        scale = transform.localScale;
        Vector3 follow_obj = (L_Hand.transform.position + R_Hand.transform.position)/2;

       
        float veloc_y = (ex.velocity_L.y + ex.velocity_R.y) / 2;
        Debug.Log(veloc_y);
        //if (veloc_y > 0)
        //{
        //    changetoward = true;
        //}
        //else if (veloc_y < 0)
        //{
        //    changetoward = false;
        //}
        //if (tmp_b != changetoward)
        //{
        //    maxVelocity = veloc_y;
        //}
        if (Mathf.Abs(veloc_y) >Mathf.Abs(maxVelocity)) {
            maxVelocity = veloc_y;
            Decrease = 0.8f;
        }
        //if (veloc_y < 0) {
        //    veloc_y *= -1;
        //}
        //Debug.Log("H : "+follow_obj.y);


        //Vector3 velocity = (L_Hand.gameObject.GetComponent<Rigidbody>().velocity + L_Hand.gameObject.GetComponent<Rigidbody>().velocity) / 2;
        // 一般的方式生成圓柱
        //if (Top_spot.transform.position.y < follow_obj.y -0.01f)
        //{
        //    scale.y += 0.01f;
        //}
        //else if (Top_spot.transform.position.y > follow_obj.y+0.01f) 
        //{
        //    scale.y -= 0.01f;
        //}
        //Debug.Log(veloc_y);

        //物理隱喻

        //Debug.Log(maxVelocity);
        //scale.y += Mathf.Lerp(tmp_Height.y, follow_obj.y, 0.05f) / modifyrate * veloc_y; 
        float plus = modifyrate * maxVelocity * Decrease;
        //Debug.Log(plus);
        if (Mathf.Abs(plus) > 0.001) {
            scale.y += plus;
        }
        
        //if (follow_obj.y - tmp_Height.y >= 0.0005)
        //{

        //}
        //if (follow_obj.y - tmp_Height.y <= -0.0005)
        //{
        //    scale.y -= Mathf.Lerp(tmp_Height.y, follow_obj.y, 0.05f) / modifyrate * veloc_y;
        //}
        Decrease -= 0.005f;
        if (Decrease <= 0) Decrease = 0;

        tmp_b = changetoward;
        if (scale.y<=0) scale.y = 0;
        transform.localScale = scale;
    }

    public void ScalewithfollowObj() { 

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand") || other.gameObject.CompareTag("LeftHand")) {
            maxVelocity = 0;
            Decrease = 0.8f;
            //bool isgrabbed = this.gameObject.GetComponent<HandGrabbable>().isGrabbed;
            //if (isgrabbed) {
            //    startScale = true;
            //}
            //if (other.gameObject.CompareTag("RightHand"))
            //{
            //    WhichHand = 0;
            //}
            //else { WhichHand = 1; }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        

    }
    private void OnTriggerExit(Collider other)
    {
        Decrease = 0.8f;

    }

    bool CheckTwoHandClose() {
        Collider[] hit = Physics.OverlapCapsule(this.transform.position,new Vector3(transform.position.x,transform.position.y + 1.5f,transform.position.z),0.3f);
        bool L = false;
        bool R = false;
        foreach (Collider h in hit)
        {
            if (h.gameObject.CompareTag("LeftHand")) {
                L = true;
            }
            if (h.gameObject.CompareTag("RightHand")) {
                R = true;
            }
        }
        if (L & R) {
            return true;
        }
        return false;
    }

    bool CheckHandpose() {
        float[] PinchSTR = new float[5] { 0, 0, 0, 0, 0};
        float[] PinchSTR_R = new float[5] { 0, 0, 0, 0, 0 };
        PinchSTR[0] = L_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
        PinchSTR[1] = L_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
        PinchSTR[2] = L_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        PinchSTR[3] = L_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        PinchSTR[4] = L_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        PinchSTR_R[0] = R_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
        PinchSTR_R[1] = R_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
        PinchSTR_R[2] = R_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        PinchSTR_R[3] = R_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        PinchSTR_R[4] = R_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        foreach (float a in PinchSTR) {
            if (a != 0)
            {
                return false;
                
            }
        }
        foreach (float a in PinchSTR_R)
        {
            if (a != 0)
            {
                return false;

            }
        }
        return true;
    }
}
