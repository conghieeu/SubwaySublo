using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScripts : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TMP_Text _silderText;

    void Start()
    {
        _slider.onValueChanged.AddListener((v) => { _silderText.text = v.ToString("0.00"); });
    }

}
