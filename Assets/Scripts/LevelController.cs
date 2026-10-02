using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<Ability> availableAbilities;
    [Serializable]
    public struct Ability
    {
        public string name;
        public bool discovered;
    }

    private void Start()
    {
        CanvasController.instance.InstantiateInputMenu(availableAbilities.ToArray());
    }
}
