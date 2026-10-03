using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    public float lifeTime = 3;
    [SerializeField] private TMP_Text infoDisplay;

    public void StartEffect(string information)
    {
        infoDisplay.text = information;
        Destroy(gameObject, lifeTime);
    }
}
