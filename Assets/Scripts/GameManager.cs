using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Entity> characters = new List<Entity>();
    public List<Entity> enemies = new List<Entity>();
    List<EntityUI> characterUIs = new List<EntityUI>();

    [SerializeField]
    GameObject[] characterPrefabs;
    [SerializeField]
    GameObject[] enemyPrefabs;
    [SerializeField] CharacterList characterList;
    [SerializeField] GameObject BattleUI;
    [SerializeField] GameObject entityUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    void Start()
    {
        characterList.abilitySelected.AddListener(SetAbility);
        Setup();
        StartBattle();
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
            GameObject obj = Instantiate(characterPrefabs[i], new Vector3(-8 + i * 2, (-characterPrefabs.Length / 2 + i) * spacing - 1, 0), Quaternion.identity);
            obj.name = characterPrefabs[i].name;
            Entity entity = obj.GetComponent<Entity>();
            EntityUI ui = InstantiateUI(entityUI, entity.transform.position + Vector3.down).GetComponent<EntityUI>();
            ui.entity = entity;
            entity.ui = ui;

            characters.Add(entity);
            characterList.AddCharacter(entity);
        }
    }

    void SetAbility(Ability ability)
    {
        ability.entity.selectedAbility = ability;
        ability.target = null;
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
            enemy.selectedAbility = null;
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

    public void StartExecuteTurn()
    {
        StartCoroutine(ExecuteTurn());
    }

    IEnumerator ExecuteTurn()
    {
        for (int priority = 1; priority <= 3; priority++)
        {
            foreach (Entity character in characters)
            {
                if (character.CanAct() && character.selectedAbility != null && priority == character.selectedAbility.priority)
                {
                    Ability ability = character.selectedAbility;
                    string animationName = "Base Layer." + ability.abilityName;
                    character.Execute();
                    yield return null;
                    while (character.animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) || character.animator.GetNextAnimatorStateInfo(0).IsName(animationName))
                    {
                        yield return null;
                    }
                }
            }
            foreach (Entity enemy in enemies)
            {
                if (enemy.CanAct() && enemy.selectedAbility != null && priority == enemy.selectedAbility.priority)
                {
                    Ability ability = enemy.selectedAbility;
                    string animationName = "Base Layer." + ability.abilityName;
                    enemy.Execute();
                    yield return null;
                    while (enemy.animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) || enemy.animator.GetNextAnimatorStateInfo(0).IsName(animationName))
                    {
                        yield return null;
                    }
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

        if (playerLost)
        {
            GameOver();
        }
        else if (playerWon)
        {
            EndBattle();
            StartBattle();
        }
        else
        {
            StartTurn();
        }
    }

    void EndBattle()
    {
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.ui);
            Destroy(enemy);
        }
        enemies.Clear();
    }

    void StartBattle()
    {
        int enemyCount = 3;
        float spacing = 3;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject obj = Instantiate(enemyPrefabs[0], new Vector3(2, (-enemyCount / 2 + i) * spacing, 0), Quaternion.identity);
            obj.name = enemyPrefabs[0].name + " " + i.ToString();
            Entity entity = obj.GetComponent<Entity>();
            enemies.Add(entity);
            EntityUI ui = InstantiateUI(entityUI, entity.transform.position + Vector3.down).GetComponent<EntityUI>();
            ui.entity = entity;
            entity.ui = ui;
        }

        StartTurn();
    }

    void GameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
