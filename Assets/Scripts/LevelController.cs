using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        CanvasController.instance.UpdateInputMenu(player.abilities.ToArray());
    }
}
