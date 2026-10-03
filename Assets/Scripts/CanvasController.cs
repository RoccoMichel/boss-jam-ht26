using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;
    [SerializeField] private List<GameObject> abilityElements = new();
    [SerializeField] private GameObject inputGroup;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Feel free to remove the following!
        if (GetComponent<CanvasScaler>().uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
            Debug.LogWarning($"{gameObject.name} is currently set to 'Constant Pixel Size', this is usually undesired!");
        if (FindAnyObjectByType<EventSystem>() == null)
            Debug.LogWarning("No Event System in Scene!");

    }

    /// <summary>
    /// Instantiate GameObject onto Canvas from ResourceFolder
    /// </summary>
    /// <param name="resourceName">Prefab path within "Resources/UI/"</param>
    /// <return>The Instantiated GameObject (child of CanvasController)</return>
    public GameObject InstantiateMenu(string resourceName)
    {
        return Instantiate((GameObject)Resources.Load($"UI/{resourceName}"), transform);
    }

    public GameObject InstantiateMenu(string resourceName, GameObject parent)
    {
        return Instantiate((GameObject)Resources.Load($"UI/{resourceName}"), parent.transform);
    }

    public void UpdateInputMenu(Player.Ability[] abilitiesList)
    {
        for (int i = 0; abilityElements.Count > 0; i = 0)
        {
            Destroy(abilityElements[i]);
            abilityElements.RemoveAt(i);
        }

        foreach(Player.Ability a in abilitiesList)
        {
            if (!a.enable) continue;

            GameObject go = InstantiateMenu("Ability Element", inputGroup);
            go.GetComponent<AbilityUI>().SetAbility(a);
            abilityElements.Add(go);
        }
    }

    public void UpdateInputMenu(Player.Ability[] abilitiesList, int index)
    {
        abilityElements[index].GetComponent<AbilityUI>().SetAbility(abilitiesList[index]);
    }

    private void Reset()
    {
        gameObject.name = "--- Canvas ---";
    }
}