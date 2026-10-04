using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public bool canInteract = true;

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

    private Ability[] startAbilities;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;

        for (int i = 0; i < abilities.Count; i++)
        {
            if (!abilities[i].inputRandomized) continue;

            Ability randomInputAbility = new()
            {
                name = abilities[i].name,
                enable = abilities[i].enable,
                discovered = abilities[i].discovered,
                inputRandomized = abilities[i].inputRandomized,
                input = GetRandomInput(),
                ability = abilities[i].ability
            };
            abilities[i] = randomInputAbility;
        }

        startAbilities = abilities.ToArray();
        CanvasController.instance.UpdateInputMenu(abilities.ToArray());
    }

    private void Update()
    {
        if (!canInteract) return;

        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].enable && Input.GetKeyDown(abilities[i].input))
            {
                if (!abilities[i].discovered) DiscoverAbility(i);
                abilities[i].ability.Invoke();
            }
        }
    }

    private IEnumerator ExplosionEffect()
    {
        canInteract = false;

        yield return new WaitForSeconds(0.1f);

        Instantiate(Resources.Load<GameObject>("Effects/Explosion Effect"), transform.position, Quaternion.identity);
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(1.8f);

        GameController.instance.ResetLevel();
        CanvasController.instance.UpdateInputMenu(startAbilities);
        abilities = startAbilities.ToList();

        canInteract = true;
        transform.position = startPosition;
        GetComponent<MeshRenderer>().enabled = true;

        yield break;
    }

    public void DiscoverAbility(int index)
    {
        Ability discoveredAbility = new()
        {
            name = abilities[index].name,
            enable = abilities[index].enable,
            discovered = true,
            inputRandomized = abilities[index].inputRandomized,
            input = abilities[index].input,
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
                    discovered = abilities[i].discovered,
                    inputRandomized = abilities[i].inputRandomized,
                    input = abilities[i].input,
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
                    discovered = discoveredState,
                    inputRandomized = abilities[i].inputRandomized,
                    input = abilities[i].input,
                    ability = abilities[i].ability
                };
                abilities[i] = toggledAbility;
                break;
            }
        }

        CanvasController.instance.UpdateInputMenu(abilities.ToArray());
    }

    // ABILITIES

    internal KeyCode GetRandomInput()
    {
        KeyCode[] allowedKeys = {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I,
            KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
            KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z, KeyCode.Space,
            KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
        };

        KeyCode result = allowedKeys[UnityEngine.Random.Range(0, allowedKeys.Length)];
        while (KeyCodeInUse(result)) result = allowedKeys[UnityEngine.Random.Range(0, allowedKeys.Length)];
        
        print(result);
        return result;
    }

    private bool KeyCodeInUse(KeyCode checkValue)
    {
        foreach (Ability ability in abilities)
        {
            if (ability.input == checkValue) return true;
        }
        return false;
    }

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
    public void Explode()
    {
        StartCoroutine(ExplosionEffect());
    }

}
