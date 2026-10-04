using UnityEngine;

public class PerformanceLimiter : MonoBehaviour
{
    public Settings settings;

    private void Start()
    {
        if (settings.vSync) QualitySettings.vSyncCount = 1;
    }
    private void Update()
    {
        Application.targetFrameRate = settings.vSync ? settings.targetFrameRate : -1;
    }
}
