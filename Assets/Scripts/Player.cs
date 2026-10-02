using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public float speed = 5;

    private CharacterController controller;

    public List<Ability> abilities = new();
    [Serializable]
    public struct Ability
    {
        public string name;
        public bool enable;
        public KeyCode input;
        public UnityEvent ability;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        foreach(Ability ability in abilities)
        {
            if (ability.enable && Input.GetKeyDown(ability.input))
            {
                ability.ability.Invoke();
            }
        }
    }

    public void ToggleAbility(string name, bool state)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].name == name)
            {
                //abilities[i].enable = state;
                break;
            }
        }
    }

    // ABILITIES

    public void Test()
    {
        Debug.Log($"{gameObject.name} used Test ability");
    }

    public void Move()
    {
        controller.Move(Vector3.forward * Time.deltaTime);
    }
}
