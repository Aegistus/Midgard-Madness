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
        if (vigor.CurrentVigor / vigor.MaxVigor < .25f)
        {
            StartCoroutine(FlashWarning());
        }
    }

    public IEnumerator FlashWarning()
    {
        warningBar.SetActive(true);
        yield return new WaitForSeconds(.5f);
        warningBar.SetActive(false);
    }
}
