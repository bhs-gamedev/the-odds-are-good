using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


[System.Serializable]
public class AbilitySelectedEvent: UnityEvent<Ability> {}


public class CharacterList : MonoBehaviour
{
    [SerializeField]
    GameObject characterButton;
    [SerializeField]
    GameObject abilityButton;

    [SerializeField] GameObject abilityList;
    [SerializeField] GameObject characterList;

    GameManager gameManager;
    public AbilitySelectedEvent abilitySelected = new AbilitySelectedEvent();

    public void Clear()
    {
        abilityList.SetActive(false);
        foreach (Transform child in characterList.transform)
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
        GameObject button = Instantiate(characterButton, characterList.transform);
        button.transform.GetChild(0).GetComponent<TMP_Text>().text = character.name;
        button.GetComponent<Button>().onClick.AddListener(delegate{if (character.CanAct()) ShowAbilities(character); else ClearAbilityList();});
    }

    void ShowAbilities(Entity character)
    {
        ClearAbilityList();
        foreach (Ability ability in character.abilities)
        {
            GameObject button = Instantiate(abilityButton, abilityList.transform);
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = ability.abilityName;
            button.GetComponent<Button>().onClick.AddListener(delegate{abilitySelected.Invoke(ability);});
        }
        abilityList.SetActive(true);
    }
}
