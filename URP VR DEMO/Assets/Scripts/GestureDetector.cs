using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}
public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool debugMode = true;
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    private SampleVRTeleporterController SVRT;
    public enum HandType{L,R};
    public HandType hand = HandType.L;

    // Start is called before the first frame update
    void Start()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
        SVRT = FindObjectOfType<SampleVRTeleporterController>();
    }

    // Update is called once per frame
    void Update()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            fingerBones = new List<OVRBone>(skeleton.Bones);
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());

        //check if new gesture
        //Debug.Log("A" + hasRecognized);
        //Debug.Log("B" + !currentGesture.Equals(previousGesture));
        //Debug.Log(currentGesture.name);
        if (hand == HandType.R) {
            if (currentGesture.name == "Gun")
            {
                SVRT.IsGun = true;
            }
            else
            {
                SVRT.IsGun = false;
            
            }
            if (currentGesture.name == "GJ")
            {
                SVRT.IsClick = true;
            }
            else
            {
                SVRT.IsClick = false;

            }
            
        }
        if (hand == HandType.L) { 
        
            if (currentGesture.name == "Gun" )
            {
                SVRT.IsGun_L = true;

            }
            else {
           
                SVRT.IsGun_L = false;
            }
        
            if (currentGesture.name == "GJ")
            {
                SVRT.IsClick_L = true;

            }
            else
            {
           
                SVRT.IsClick_L = false;
            }
        
        }


        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            //New Gesture!!
            Debug.Log("New Gesture Found : " + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }
    }

    void Save()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach (var bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerDatas = data;
        gestures.Add(g);
    }
    Gesture Recognize()
    {
        Gesture currentgesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
                if (distance > threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentgesture = gesture;
                
            }


        }
        
        return currentgesture;

    }
}
