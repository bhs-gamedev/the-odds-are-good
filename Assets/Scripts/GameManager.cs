using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Entity> characters = new List<Entity>();
    public List<Entity> enemies = new List<Entity>();
    List<EntityUI> characterUIs = new List<EntityUI>();

    [SerializeField]
    GameObject[] characterPrefabs;
    [SerializeField]
    GameObject[] enemyPrefabs;
    [SerializeField]
    CharacterList characterList;
    [SerializeField] GameObject BattleUI;
    [SerializeField] GameObject entityUI;

    void Start()
    {
        characterList.abilitySelected.AddListener(SetAbility);
        Setup();
    }

    GameObject InstantiateUI(GameObject obj, Vector3 position)
    {
        return Instantiate(obj, RectTransformUtility.WorldToScreenPoint(Camera.main, position), Quaternion.identity, BattleUI.transform);
    }

    public void Setup()
    {
        characterList.Clear();
        float spacing = 3;
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(characterPrefabs[i], new Vector3(-2, (-characterPrefabs.Length / 2 + i) * spacing, 0), Quaternion.identity);
            Entity entity = obj.GetComponent<Entity>();
            EntityUI ui = InstantiateUI(entityUI, entity.transform.position + Vector3.up).GetComponent<EntityUI>();
            ui.entity = entity;
            entity.ui = ui;

            characters.Add(entity);
            characterList.AddCharacter(entity);
        }

        int enemyCount = 5;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = Instantiate(enemyPrefabs[0], new Vector3(2, (-enemyCount / 2 + i) * spacing, 0), Quaternion.identity);
            Entity entity = obj.GetComponent<Entity>();
            enemies.Add(entity);
            EntityUI ui = InstantiateUI(entityUI, entity.transform.position + Vector3.up).GetComponent<EntityUI>();
            ui.entity = entity;
            entity.ui = ui;
        }
    }

    void SetAbility(Ability ability)
    {
        ability.entity.selectedAbility = ability;
        if (ability.canTargetAlly)
        {
            foreach (Entity entity in characters)
            {
                if (ability.IsValidTarget(entity))
                {
                    entity.ui.AllowTarget(true);
                    entity.ui.targetButton.onClick.AddListener(delegate{ability.target = entity; HideAllTargets();});
                }
            }
        }
        if (ability.canTargetOpponent)
        {
            foreach (Entity entity in enemies)
            {
                if (ability.IsValidTarget(entity))
                {
                    entity.ui.AllowTarget(true);
                    entity.ui.targetButton.onClick.AddListener(delegate{ability.target = entity; HideAllTargets();});
                }
            }
        }
    }

    void HideAllTargets()
    {
        foreach (Entity entity in characters)
        {
            entity.ui.AllowTarget(false);
            entity.ui.targetButton.onClick.RemoveAllListeners();
        }
        foreach (Entity entity in enemies)
        {
            entity.ui.AllowTarget(false);
            entity.ui.targetButton.onClick.RemoveAllListeners();
        }
    }

    public void StartTurn()
    {
        foreach (Entity character in characters)
        {
            character.selectedAbility = null;
            character.Roll();
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.Roll();
            enemy.SelectAbility();
            foreach (Entity character in characters)
            {
                if (character.IsAlive())
                {
                    enemy.selectedAbility.target = character;
                    break;
                }
            }
        }
    }

    public void ExecuteTurn()
    {
        for (int priority = 1; priority <= 3; priority++)
        {
            foreach (Entity character in characters)
            {
                if (character.CanAct() && character.selectedAbility != null && priority == character.selectedAbility.priority)
                {
                    character.selectedAbility.Execute();
                    character.selectedAbility = null;
                }
            }
            foreach (Entity enemy in enemies)
            {
                if (enemy.CanAct() && enemy.selectedAbility != null && priority == enemy.selectedAbility.priority)
                {
                    enemy.selectedAbility.Execute();
                }
            }
        }
        bool playerLost = true;
        foreach (Entity character in characters)
        {
            if (character.IsAlive())
            {
                playerLost = false;
                break;
            }
        }

        bool playerWon = true;
        foreach (Entity enemy in enemies)
        {
            if (enemy.IsAlive())
            {
                playerWon = false;
                break;
            }
        }

        if (playerLost || playerWon)
        {
            EndBattle();
        }
    }

    void EndBattle()
    {

    }

}
