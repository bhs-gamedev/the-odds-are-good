using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityUI : MonoBehaviour
{
    public Entity entity;

    [SerializeField] TMP_Text valueText;
    [SerializeField] TMP_Text abilityText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] public Button targetButton;

    // Update is called once per frame
    void Update()
    {
        valueText.text = entity.value.ToString();
        healthText.text = entity.health.ToString();
        if (entity.selectedAbility != null)
        {
            abilityText.text = entity.selectedAbility.abilityName;
            if (entity.selectedAbility.target)
            {
                abilityText.text += ": " + entity.selectedAbility.target.name;
            }
        }
    }

    public void AllowTarget(bool value)
    {
        targetButton.gameObject.SetActive(value);
    }
}
