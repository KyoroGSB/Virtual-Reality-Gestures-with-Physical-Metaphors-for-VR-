using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public List<GameObject> prefabs;

    private OVRHand _ohand;
    public enum Hand
    {
        None = OVRPlugin.Hand.None,
        HandLeft = OVRPlugin.Hand.HandLeft,
        HandRight = OVRPlugin.Hand.HandRight,
    }
    [SerializeField]
    private Hand HandType = Hand.None;

    [SerializeField]
    private bool[] IsPinch;
    [SerializeField]
    private float[] PinchSTR;
    public void Spawn(int index) {
        GameObject a = Instantiate(prefabs[index], transform.position, transform.rotation);
        Destroy(a, 3.0f);
    }

    void Start()
    {
        _ohand = GetComponent<OVRHand>();
        IsPinch = new bool[6]{false,false,false,false,false,false }; 
        PinchSTR  = new float[6] { 0, 0, 0, 0, 0, 0 };
        

    }

    private void Update()
    {

        //IsPinch[0] = _ohand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);
        //IsPinch[1] = _ohand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        //IsPinch[2] = _ohand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        //IsPinch[3] = _ohand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        //IsPinch[4] = _ohand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);
        //IsPinch[5] = _ohand.GetFingerIsPinching(OVRHand.HandFinger.Max);
        PinchSTR[0] = _ohand.GetFingerPinchStrength(OVRHand.HandFinger.Thumb);
        PinchSTR[1] = _ohand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        PinchSTR[2] = _ohand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        PinchSTR[3] = _ohand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        PinchSTR[4] = _ohand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);
        PinchSTR[5] = _ohand.GetFingerPinchStrength(OVRHand.HandFinger.Max);
       


        
    }
}
