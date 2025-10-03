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
            // if(isOn == false){
            //     string animName = "Water_Run2";
            //     yield return StartCoroutine(WaitForAnimationToEnd(anim, animName));
            // }

            anim.SetBool("isPumpOn", isOn);
        }
        
    }

    private IEnumerator WaitForAnimationToEnd(Animator animator, string stateName){
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
    }
}
