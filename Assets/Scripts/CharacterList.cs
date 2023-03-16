using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterList : MonoBehaviour
{
    [SerializeField]
    GameObject characterButton;
    [SerializeField]
    GameObject abilityButton;
    [SerializeField]
    GameObject targetButton;


    [SerializeField]
    GameObject abilityList;
    GameObject targetList;
    public void Clear()
    {
        abilityList.SetActive(false);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClearAbilityList()
    {
        foreach (Transform child in abilityList.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddCharacter(Entity character)
    {
        GameObject button = Instantiate(characterButton, transform);
        button.transform.GetChild(0).GetComponent<TMP_Text>().text = character.name;
        button.GetComponent<Button>().onClick.AddListener(delegate{ShowAbilities(character);});
    }

    void ShowAbilities(Entity character)
    {
        foreach (Transform child in abilityList.transform)
        {
            Destroy(child.gameObject);
        }
        ClearAbilityList();
        foreach (Ability ability in character.abilities)
        {
            GameObject button = Instantiate(abilityButton, transform);
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = character.name;
            button.GetComponent<Button>().onClick.AddListener(delegate{ShowAbilities(character);});
        }
        abilityList.SetActive(true);
    }

    void SetAbility(Entity character, Ability ability)
    {
        character.selectedAbility = ability;
        if (ability.targetType != TargetType.None)
        {
            ShowTargetSelector(ability);
        }
    }

    void ShowTargetSelector(Ability ability)
    {

    }
}
