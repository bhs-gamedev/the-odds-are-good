using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Entity> characters = new List<Entity>();
    List<Entity> enemies = new List<Entity>();

    [SerializeField]
    GameObject[] characterPrefabs;
    [SerializeField]
    GameObject[] enemyPrefabs;
    [SerializeField]
    CharacterList characterList;

    void Start()
    {
        Setup();
    }

    void Update()
    {

    }

    public void Setup()
    {
        characterList.Clear();
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(characterPrefabs[i], new Vector3(-1, i * 2, 0), Quaternion.identity);
            characters.Add(obj.GetComponent<Entity>());
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject obj = Instantiate(enemyPrefabs[0], new Vector3(1, i * 2, 0), Quaternion.identity);
            enemies.Add(obj.GetComponent<Entity>());
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
                }
            }
            foreach (Enemy enemy in enemies)
            {
                if (enemy.CanAct() && enemy.selectedAbility != null && priority == enemy.selectedAbility.priority)
                {
                    enemy.selectedAbility.Execute();
                }
            }
        }
    }

}
