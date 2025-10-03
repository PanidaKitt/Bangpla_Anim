using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TankController : MonoBehaviour
{
    public List<Pump> pumps = new List<Pump>();
    public List<Sensor> sensors = new List<Sensor>();
    public GameObject tube;
    public GameObject water;
    private Animator tubeAnim;
    private Animator waterAnim;

    private int waterState = 0;

    // Start is called before the first frame update
    void Start()
    {
        tubeAnim = tube.GetComponent<Animator>();
        if(water){
            waterAnim = water.GetComponent<Animator>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickPump(string id){
        StartCoroutine(ClickPumpSequence(id));
        // pumps.Find(f=>f.GetPumpId() == id).SetPump();

        // if(pumps.Any(a=>a.GetPumpStatus())){
        //     if(!tubeAnim.GetBool("isTubeOn")){
        //         StartCoroutine(PlayAnimationSequence(true));
        //     }
        // }
        // else{
        //     if(tubeAnim.GetBool("isTubeOn")){
        //         StartCoroutine(PlayAnimationSequence(false));
        //     }
        // }

        // StartCoroutine(PlayAnimationSequence());

        // tubeAnim.SetBool("isTubeOn", true);
    }

    private IEnumerator ClickPumpSequence(string id){
        Pump pump = pumps.Find(f => f.GetPumpId() == id);
        pump.SetPump();
        Debug.Log("id"+ id);
        char firstChar = id[0];
        Debug.Log("firstChar"+ firstChar);

        if(firstChar != '0'){
            Animator pumpAnim = pump.GetPumpAnimator();

            Debug.Log("pumpAnim", pumpAnim);
            string animName = tubeAnim.GetBool("isTubeOn")? "Pump_Off":"Pump_On";

            yield return new WaitUntil(() =>
            {
                Debug.Log("wait");
                AnimatorStateInfo state = pumpAnim.GetCurrentAnimatorStateInfo(0);
                return state.IsName(animName) && state.normalizedTime >= 1f;
            });

            Debug.Log("tubeAnim", tubeAnim);
        }
        
        if (pumps.Any(a => a.GetPumpStatus()))
        {
            if (!tubeAnim.GetBool("isTubeOn"))
            {
                tubeAnim.SetBool("isTubeOn", true);
            }
        }
        else
        {
            if (tubeAnim.GetBool("isTubeOn"))
            {
                tubeAnim.SetBool("isTubeOn", false);
            }
        }
    }

    public void ClickSensor(int id){
        StartCoroutine(HandleClickSensor(id));
    }

    private IEnumerator HandleClickSensor(int id){
        Debug.Log("id" + id);

        if ((sensors?.Count ?? 0) >= id){
            if (sensors[id - 1].GetSensorStatus()){
                List<int> offNumber = new List<int>();
                for (int i = sensors.Count; i >= id; i--){
                    Debug.Log($"i = {i}");
                    if (sensors[i - 1].GetSensorStatus()){
                        sensors[i - 1].SetSensor();

                        offNumber.Add(i);
                    }
                }
                Debug.Log("offNumber"+ offNumber);
                yield return new WaitForSeconds(0.5f);

                if (waterAnim){
                    waterAnim.speed = 0.5f;

                    // string animName = "Water_Full";
                    // waterAnim.Play(animName, 0, 0);
                    // Debug.Log("anim " + animName);

                    // waterAnim.SetInteger("State", 0);

                    string animName = "";

                    // for (int i = sensors.Count; i >= id; i--){
                    foreach(int i in offNumber){
                        int stateNumber = ((sensors.Count * 10) + i)*-1;
                        Debug.Log("stateNumber " + stateNumber);

                        animName = $"Water{sensors.Count}_{i}R";
                        waterAnim.Play(animName, 0, 0);
                        Debug.Log("anim " + animName);

                        // waterAnim.SetInteger("State", stateNumber);

                        waterAnim.SetTrigger(stateNumber);

                        yield return StartCoroutine(WaitForAnimationToEnd(waterAnim, animName));
                    }
                }
                
                // if (waterAnim){
                //     waterAnim.speed = -0.5f;
                // }

                // for (int i = sensors.Count; i >= id; i--){
                //     int stateNumber = (sensors.Count * 10) + (i-1);
                //     Debug.Log("stateNumber " + stateNumber);

                //     if (waterAnim){
                //         string animName = $"Water{sensors.Count}_{i}";
                //         waterAnim.Play(animName, 0, 0);
                //         Debug.Log("anim " + animName);

                //         yield return StartCoroutine(WaitForAnimationToEnd(waterAnim, animName));

                //         waterAnim.SetInteger("State", stateNumber);
                //     }
                // }
            }
            else{
                List<int> offNumber = new List<int>();
                for (int i = 1; i <= id; i++){
                    Debug.Log($"i = {i}");
                    if (!sensors[i-1].GetSensorStatus()){
                        sensors[i-1].SetSensor();
                        offNumber.Add(i);
                    }
                }

                yield return new WaitForSeconds(0.5f);

                if (waterAnim){
                    waterAnim.speed = 0.5f;

                    string animName = "";
                    waterAnim.Play(animName, 0, 0);
                    Debug.Log("anim " + animName);

                    // waterAnim.SetInteger("State", 0);

                    // for (int i = 1; i <= id; i++){
                    foreach(int i in offNumber){
                        int stateNumber = (sensors.Count * 10) + i;
                        Debug.Log("stateNumber " + stateNumber);

                        // yield return StartCoroutine(WaitForAnimationToEnd(waterAnim, animName));

                        animName = $"Water{sensors.Count}_{i}";
                        waterAnim.Play(animName, 0, 0);
                        Debug.Log("anim " + animName);

                        // waterAnim.SetInteger("State", stateNumber);
                        waterAnim.SetTrigger(stateNumber);

                        yield return StartCoroutine(WaitForAnimationToEnd(waterAnim, animName));
                    }
                }

                // for (int i = 1; i <= id; i++){
                //     int stateNumber = (sensors.Count * 10) + i;
                //     Debug.Log("stateNumber " + stateNumber);

                //     if (waterAnim){
                //         string animName = $"Water{sensors.Count}_{i}";
                //         waterAnim.Play(animName, 0, 0);
                //         Debug.Log("anim " + animName);

                //         yield return StartCoroutine(WaitForAnimationToEnd(waterAnim, animName));

                //         waterAnim.SetInteger("State", stateNumber);
                //         Debug.Log("stateNumber " + stateNumber);
                //     }
                // }
            }
        }
    }

    private IEnumerator WaitForAnimationToEnd(Animator animator, string stateName){
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;
    }
}
