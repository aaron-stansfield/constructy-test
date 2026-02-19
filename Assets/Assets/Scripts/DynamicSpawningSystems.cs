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
    public List<GameObject> goodScenarios = new List<GameObject>();
    public List<GameObject> badScenarios = new List<GameObject>();
    public List<GameObject> mehScenarios = new List<GameObject>();

    private Dictionary<Category, List<GameObject>> scenarios =
        new Dictionary<Category, List<GameObject>>();

    [Header("spawned ones")]
    public List<GameObject> ActiveScenarios = new List<GameObject>();

    public void Start()
    {
        scenarios.Add(Category.good, goodScenarios);
        scenarios.Add(Category.bad, badScenarios);
        scenarios.Add(Category.meh, mehScenarios);
    }

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