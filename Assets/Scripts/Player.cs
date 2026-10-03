using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Player;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public float speed = 5;

    private CharacterController controller;
    private Vector3 startPosition;

    public List<Ability> abilities = new();
    [Serializable]
    public struct Ability
    {
        public string name;
        public bool enable;
        public bool discovered;
        public bool inputRandomized;
        public KeyCode input;
        public UnityEvent ability;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].enable && Input.GetKeyDown(abilities[i].input))
            {
                if (!abilities[i].discovered) DiscoverAbility(i);
                abilities[i].ability.Invoke();
            }
        }
    }

    public void Die()
    {
        transform.position = startPosition;
    }

    public void DiscoverAbility(int index)
    {
        Ability discoveredAbility = new()
        {
            name = abilities[index].name,
            enable = abilities[index].enable,
            input = abilities[index].input,
            discovered = true,
            ability = abilities[index].ability
        };

        abilities[index] = discoveredAbility;

        CanvasController.instance.UpdateInputMenu(abilities.ToArray(), index);
    }

    public void ToggleAbility(string name, bool state)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].name == name)
            {
                Ability toggledAbility = new()
                {
                    name = name,
                    enable = state,
                    input = abilities[i].input,
                    discovered = abilities[i].discovered,
                    ability = abilities[i].ability
                };
                abilities[i] = toggledAbility;
                break;
            }

            if (i == abilities.Count - 1) Debug.LogWarning($"Could not find ability with name '{name}'");
        }

        CanvasController.instance.UpdateInputMenu(abilities.ToArray());
    }

    public void ToggleAbility(string name, bool enableState, bool discoveredState)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].name == name)
            {
                Ability toggledAbility = new()
                {
                    name = name,
                    enable = enableState,
                    input = abilities[i].input,
                    discovered = discoveredState,
                    ability = abilities[i].ability
                };
                abilities[i] = toggledAbility;
                break;
            }
        }

        CanvasController.instance.UpdateInputMenu(abilities.ToArray());
    }

    // ABILITIES

    public void Test()
    {
        Debug.Log($"{gameObject.name} used Test ability");
    }

    public void MoveUp()
    {
        controller.Move(Vector3.forward);
    }
    public void MoveDown()
    {
        controller.Move(Vector3.back);
    }
    public void MoveLeft()
    {
        controller.Move(Vector3.left);
    }
    public void MoveRight()
    {
        controller.Move(Vector3.right);
    }
}
