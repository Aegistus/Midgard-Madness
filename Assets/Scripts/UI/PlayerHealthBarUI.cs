using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarUI : MonoBehaviour
{
    public Transform healthBar;

    private AgentHealth player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentHealth>();
    }

    private void Update()
    {
        healthBar.localScale = new Vector3(player.CurrentHealth / 100f, 1, 1);
    }
}
