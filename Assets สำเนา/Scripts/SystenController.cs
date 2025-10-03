using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystenController : MonoBehaviour
{
    [SerializeField] private List<Tank> tanks = new List<Tank>();

    [System.Serializable]
    public class Tank{
        public string tankId;
        public List<Machine> pumps = new List<Machine>();
        public List<Machine> adders = new List<Machine>();
        public List<Machine> sensors = new List<Machine>();
        public List<GameObject> tubes = new List<GameObject>();
    }

    [System.Serializable]
    public class Machine{
        public string machineId;
        public GameObject machineObj;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickPump(int id){

    }
}
