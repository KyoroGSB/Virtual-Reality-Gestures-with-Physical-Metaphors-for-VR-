using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private AudioSource audi_c;
    public AudioClip cancel_c;
    // Start is called before the first frame update
    void Start()
    {
        audi_c = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Task1Obj") || other.gameObject.CompareTag("Task2Obj")) {

            audi_c.PlayOneShot(cancel_c);
            var tmp_hand = other.gameObject.GetComponent<HandGrabbable>().grabbedBy;
            tmp_hand.GrabEnd();

            Destroy(other.gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Task1Obj") || other.gameObject.CompareTag("Task2Obj"))
        {

            //if (!other.gameObject.GetComponent<HandGrabbable>().isGrabbed) { 
            //    audi_c.PlayOneShot(cancel_c);
            //    var tmp_hand = other.gameObject.GetComponent<HandGrabbable>().grabbedBy;
            //    tmp_hand.GrabEnd();
                
            //    Destroy(other.gameObject);
            //}

        }
    }

}
