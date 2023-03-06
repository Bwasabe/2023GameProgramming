using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceAmountUI : MonoBehaviour
{
    [SerializeField]
    private ResourceTypeSO _resourceTypeSO;

    private void Start() {
        int amount = ResourceManager.Instance.GetResource(_resourceTypeSO);

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.SetText(amount.ToString());
    }

    private void Update() {
        Start();
    }
}
