using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pump : MonoBehaviour
{
    [SerializeField] private string id;
    private bool isOn = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public string GetPumpId() {
        return id;
    }

    public bool GetPumpStatus() {
        return isOn;
    }

    public Animator GetPumpAnimator() {
        return anim;
    }

    public void SetPump(){
        Debug.Log("click");
        isOn = !isOn;

        if(anim){
            anim.SetBool("isPumpOn", isOn);
        }
        
    }
}
