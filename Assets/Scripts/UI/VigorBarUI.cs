using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VigorBarUI : MonoBehaviour
{
    public Transform vigorBar;
    public GameObject warningBar;

    private AgentVigor vigor;

    private void Start()
    {
        vigor = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentVigor>();
    }

    private void Update()
    {
        vigorBar.localScale = new Vector3(vigor.CurrentVigor / vigor.MaxVigor, 1, 1);
    }
}
