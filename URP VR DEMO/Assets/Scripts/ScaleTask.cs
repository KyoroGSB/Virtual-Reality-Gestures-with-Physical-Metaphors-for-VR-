using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTask : MonoBehaviour
{
    private GameObject L_Hand;
    private GameObject R_Hand;
    public bool startScale;
    private Vector3 tmp_scale;
    private int WhichHand; // 0 --> R ,  1--> L
    public float LastTwoHand_dis;
    private ExperimentManager ex;
    private float maxVelocity_expand = 0;
    private float maxVelocity_minify = 0;
    [SerializeField]
    private float decrease = 0.8f;
    private float de_x = 0.005f;
    [SerializeField]
    private bool Ex_direction = false;
    [SerializeField]
    private bool Min_direction = false;
    [SerializeField]
    private float maxVelocity = 0;
    //[SerializeField]
    //private float grabCandidates;
    //[SerializeField]
    //private float grabCandidate_2;
    // Start is called before the first frame update
    void Start()
    {
        L_Hand = GameObject.FindGameObjectWithTag("LeftHand");
        R_Hand = GameObject.FindGameObjectWithTag("RightHand");
        ex = FindObjectOfType<ExperimentManager>();
    }
    private void Update()
    {
       
        if (startScale) {
          
            StartScaling();

        }
    }
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("RightHand") ) {
           
    //        bool isgrabbed = this.gameObject.GetComponent<HandGrabbable>().isGrabbed;
    //        float pinchStrength = other.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
    //        grabCandidates = other.GetComponent<HandGrabbing>().SaveCount;

    //        if (isgrabbed && pinchStrength > 0.5f && grabCandidates > 0) {
    //            startScale = true;
    //        }
    //        //if (other.gameObject.CompareTag("RightHand") && startScale)
    //        //{
    //        //    WhichHand = 0;
    //        //}
    //        //else if(other.gameObject.CompareTag("LeftHand") && startScale) { WhichHand = 1; }
    //    }
    //    if (other.gameObject.CompareTag("LeftHand"))
    //    {

    //        bool isgrabbed = this.gameObject.GetComponent<HandGrabbable>().isGrabbed;
    //        if (isgrabbed)
    //        {
    //            startScale = true;
    //        }
    //        //if (other.gameObject.CompareTag("RightHand") && startScale)
    //        //{
    //        //    WhichHand = 0;
    //        //}
    //        //else if (other.gameObject.CompareTag("LeftHand") && startScale) { WhichHand = 1; }
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand"))
        {

            bool isgrabbed = this.gameObject.GetComponent<HandGrabbable>().isGrabbed;
            float pinchStrength = other.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
            
            float pinchStr_2 = L_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
          

            if (isgrabbed && pinchStrength > 0.5f && pinchStr_2 > 0.5f)
            {
                startScale = true;
            }
            else
            {
                startScale = false;
                maxVelocity = 0;
            }
            //if (other.gameObject.CompareTag("RightHand") && startScale)
            //{
            //    WhichHand = 0;
            //}
            //else if (other.gameObject.CompareTag("LeftHand") && startScale) { WhichHand = 1; }
        }
        if (other.gameObject.CompareTag("LeftHand"))
        {

            bool isgrabbed = this.gameObject.GetComponent<HandGrabbable>().isGrabbed;
            float pinchStrength = other.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);
            
            float pinchStr_2 = R_Hand.GetComponent<OVRHand>().GetFingerPinchStrength(OVRHand.HandFinger.Index);

          
            if (isgrabbed && pinchStrength > 0.5f && pinchStr_2 > 0.5f)
            {
                startScale = true;
            }
            else {
                startScale = false;
            }
            //if (other.gameObject.CompareTag("RightHand") && startScale)
            //{
            //    WhichHand = 0;
            //}
            //else if (other.gameObject.CompareTag("LeftHand") && startScale) { WhichHand = 1; }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand") || other.gameObject.CompareTag("LeftHand"))
        {
           // startScale = false; maxVelocity = 0;
        }
    }
    void StartScaling()
    {
        //Debug.Log("AAAAAA");
        tmp_scale = this.transform.localScale;
        Vector3 scalechange = L_Hand.transform.position - R_Hand.transform.position;
       
      
        Vector3 offset = new Vector3(1f, 1f, 1f);
        Vector3 scale = transform.localScale;

        float veloc = (ex.velocity_L.magnitude + ex.velocity_R.magnitude)*0.2f ;


        //normal
        if (LastTwoHand_dis <= scalechange.magnitude * 0.95f ) {
            //scale  += offset * scalechange.magnitude * 0.05f;
            // Debug.Log("O");
            if (!Ex_direction && !Min_direction) {
                Ex_direction = true;
                decrease = 0.8f;
            }

            if (!Ex_direction && Min_direction) { 
            
                Ex_direction = true;
                Min_direction = false;
                maxVelocity = 0;
                
            }
            if (Mathf.Abs(veloc) > maxVelocity)
            {
                maxVelocity = Mathf.Abs(veloc);
                decrease = 0.8f;
            }

        }
        if (LastTwoHand_dis >= scalechange.magnitude * 1.05f) {
            //scale  -= offset * scalechange.magnitude * 0.05f;
            //Debug.Log("X");
            if (!Ex_direction && !Min_direction)
            {
                Min_direction = true;
                decrease = 0.8f;
            }

            if (Ex_direction && !Min_direction)
            {

                Ex_direction = false;
                Min_direction = true;
                maxVelocity = 0;
                
            }
            if (Mathf.Abs(veloc) > maxVelocity)
            {
                maxVelocity = Mathf.Abs(veloc)*-1;
                decrease = 0.8f;
            }
        }
        var tmp = offset * scalechange.magnitude * decrease * maxVelocity * 0.008f;

      
        scale += tmp;
        decrease -= de_x;
        //de_x *= 1.05f;
        if (decrease <= 0) { decrease = 0; }


        if (scale.x <= 0.2f) scale.x = 0.2f;
        if (scale.y <= 0.2f) scale.y = 0.2f;
        if (scale.z <= 0.2f) scale.z = 0.2f;

        
        transform.localScale = scale;

        LastTwoHand_dis = scalechange.magnitude;

    }


    
}
