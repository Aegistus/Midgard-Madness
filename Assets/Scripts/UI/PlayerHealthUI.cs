using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    public Transform healthBar;
    public GameObject damageWarning;

    private AgentHealth player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentHealth>();
        player.OnAgentTakeDamage += DamageWarning;
    }

    private void DamageWarning()
    {
        damageWarning.SetActive(true);
        StartCoroutine(DeactivateWarning());
    }

    private IEnumerator DeactivateWarning()
    {
        yield return new WaitForSeconds(.3f);
        damageWarning.SetActive(false);
    }

    private void Update()
    {
        healthBar.localScale = new Vector3(player.CurrentHealth / 100f, 1, 1);
    }
}
