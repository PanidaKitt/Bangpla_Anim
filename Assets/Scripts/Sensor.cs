using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sensor : MonoBehaviour
{
    [SerializeField] private int id;
    private bool isOn = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public int GetSensorId() {
        return id;
    }

    public bool GetSensorStatus() {
        return isOn;
    }

    public Animator GetSensorAnimator() {
        return anim;
    }

    public void SetSensor(){
        Debug.Log("click");
        isOn = !isOn;

        if(anim){
            anim.SetBool("isSensorOn", isOn);
        }
        
    }
}
