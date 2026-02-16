using System.Collections.Generic;
using System.Linq;
using Assets.Assets.Scripts;
using UnityEngine;

public class DynamicSpawningSystems : MonoBehaviour
{
    public enum Category
    {
        good,
        bad,
        meh
    }

    [Header("Spawnpoints")]
    public GameObject locationParent;

    [Header("prefabs by category")]
    public Dictionary<Category, List<GameObject>> scenarios =
        new Dictionary<Category, List<GameObject>>();

    [Header("spawned ones")]
    public List<GameObject> ActiveScenarios = new List<GameObject>();


    public void GenerateScenarios()
    {
        ActiveScenarios.Clear();

        foreach (Transform t in locationParent.GetComponentsInChildren<Transform>())
        {
            if (t == locationParent.transform)
                continue;

            Category thingType = System.Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .PickRandom();

            GameObject randomScenario = scenarios[thingType].PickRandom();
            if (randomScenario == null)
                continue;

            GameObject i = Instantiate(randomScenario, t.position, t.rotation, t);

            ActiveScenarios.Add(i);
        }
    }
}