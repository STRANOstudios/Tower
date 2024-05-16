using UnityEngine;
using UnityEngine.UI;

public class TimeScale : MonoBehaviour
{
    Slider _timescale;

    private void Awake()
    {
        _timescale = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _timescale.onValueChanged.AddListener(ChangeTimeScale);
    }

    private void OnDisable()
    {
        _timescale.onValueChanged.RemoveListener(ChangeTimeScale);
    }

    private void ChangeTimeScale(float arg0)
    {
        Time.timeScale = arg0;
    }
}
