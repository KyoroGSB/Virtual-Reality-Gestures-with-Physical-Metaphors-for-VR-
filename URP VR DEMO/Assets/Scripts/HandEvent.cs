using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandEvent : MonoBehaviour
{
    
    public enum HandType {L,R };
    public HandType hand = HandType.L;
    public GameObject cylinder;
    public GameObject Task2_sphere;
    public Transform spawnspot;
    public GameObject trashcan;

    public  bool InScale = false;
    private Vector3 LastPos;
    public Vector3 velocity;


    

    private ExperimentManager ex;


    private void Start()
    {
        ex = FindObjectOfType<ExperimentManager>(); ;
       
        
    }
    private void Update()
    {
        if (hand == HandType.R)
        {
            ex.direction = transform.rotation.eulerAngles;
            
        }
        else if (hand == HandType.L) {
            ex.direction_L = transform.rotation.eulerAngles;
            //Debug.Log(ex.direction_L);
        }
    }
    private void FixedUpdate()
    {
        Vector3 distance = this.transform.position - LastPos;
        
        velocity = distance / 0.001f; ;
        if (hand == HandType.R)
        {

            ex.velocity_R = velocity;
        }
        else if (hand == HandType.L)
        {

            ex.velocity_L = velocity;
        }



        LastPos = this.transform.position;
    }

    private void OnTriggerEnter(Collider a)
    {
        if (a.gameObject.CompareTag("UIButton")) {
            NextStage();
            ex.pop_sound();
        }
        if (a.gameObject.CompareTag("trash"))
        {
            bool b = trashcan.activeInHierarchy;
            b = !b;
            trashcan.SetActive(b);
            ex.pop_sound();
        }
        if (a.gameObject.CompareTag("Task1")) {

            CreateObj1();
            ex.pop_sound();
        }

        if (a.gameObject.CompareTag("Task2")) {

            CreateObj2();
            ex.pop_sound();
        }
        

    }



    public void CreateObj1() {

        GameObject a = Instantiate(cylinder, spawnspot.position,spawnspot.rotation,null);
    }
    public void CreateObj2()
    {

        GameObject a = Instantiate(Task2_sphere, spawnspot.position, spawnspot.rotation, null);
    }
    public void NextStage() {
        ex.Stage++;
        if (ex.Stage > 3) {
            ex.Stage = 0;
        }
        ex.ChangeStage(ex.Stage);
        if (ex.Stage == 2) {
            trashcan.SetActive(false);
        }

    }

}
