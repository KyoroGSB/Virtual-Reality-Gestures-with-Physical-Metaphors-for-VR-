using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabbable : OVRGrabbable
{
    
        protected override void Start()
        {
            base.Start();
           
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            
        }

    private void Update()
    {
        if (base.isGrabbed) {
            if(this.gameObject.CompareTag("Task1Obj"))
            this.gameObject.GetComponent<scaleStatus>().Modify_Done = true;
        }
    }

}
