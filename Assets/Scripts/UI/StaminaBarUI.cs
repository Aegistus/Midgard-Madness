using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarUI : MonoBehaviour
{
    public Transform staminaBar;
    public GameObject warningBar;

    private AgentStamina stamina;

    private void Start()
    {
        stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentStamina>();
    }

    private void Update()
    {
        staminaBar.localScale = new Vector3(stamina.CurrentStamina / stamina.MaxStamina, 1, 1);
        if (stamina.CurrentStamina / stamina.MaxStamina < .25f)
        {
            StartCoroutine(FlashWarning());
        }
    }

    public IEnumerator FlashWarning()
    {
        warningBar.SetActive(true);
        yield return new WaitForSeconds(.1f);
        warningBar.SetActive(false);
    }
}
