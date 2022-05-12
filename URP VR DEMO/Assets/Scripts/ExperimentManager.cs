using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExperimentManager : MonoBehaviour
{
    public int Stage;

    public Vector3 velocity_L = Vector3.zero;
    public Vector3 velocity_R = Vector3.zero;
    public GameObject[] TaskObj1;
    public GameObject[] TaskObj2;
    public GameObject[] TaskObj3;
    private AudioSource audi;
    public AudioClip click;
    public Vector3 direction;
    public Vector3 direction_L;

    private void Start()
    {
        audi = GetComponent<AudioSource>();
        foreach (GameObject a in TaskObj1) {
            a.SetActive(false);
        }
        foreach (GameObject a in TaskObj2)
        {
            a.SetActive(false);
        }
        foreach (GameObject a in TaskObj3)
        {
            a.SetActive(false);
        }
    }
    private void Update()
    {
       
    }
    public void ChangeStage(int stage)
    {
        Refresh();
        if (stage == 0)
        {

        }
        else if (stage == 1)
        {
            
            foreach (GameObject a in TaskObj1)
            {
                a.SetActive(true);
            }
        }
        else if (stage == 2)
        {
            foreach (GameObject a in TaskObj2)
            {
                a.SetActive(true);
            }
        }
        else if (stage == 3)
        {
            foreach (GameObject a in TaskObj3)
            {
                a.SetActive(true);
            }
        }
    }

    public void Refresh() {
        foreach (GameObject a in TaskObj1)
        {
            a.SetActive(false);
            GameObject[] k = GameObject.FindGameObjectsWithTag("Task1Obj");
            foreach (GameObject @object in k) {
                Destroy(@object);
            }
        }
        foreach (GameObject a in TaskObj2)
        {
            a.SetActive(false);
            GameObject[] k = GameObject.FindGameObjectsWithTag("Task2Obj");
            foreach (GameObject @object in k)
            {
                Destroy(@object);
            }
        }
        foreach (GameObject a in TaskObj3)
        {
            a.SetActive(false);
        }
    }
    public void pop_sound() {
        audi.PlayOneShot(click);
    }
}
