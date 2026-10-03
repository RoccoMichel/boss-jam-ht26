using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameDisplay;

    private Player.Ability ability;

    public void SetAbility(Player.Ability newAbility)
    {
        ability = newAbility;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        Sprite inputIcon = Resources.Load<Sprite>($"Inputs/keyboard_question");

        if (ability.discovered)
        {
            string inputName = ability.input.ToString().ToLower();
            inputIcon = Resources.Load<Sprite>($"Inputs/keyboard_{inputName}");
        }

        icon.sprite = inputIcon;
        nameDisplay.text = ability.name;
    }
}
