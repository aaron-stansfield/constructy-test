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
        //safety
    }

    [Header("Scenario Parent")]
    public GameObject _scenarios;

    [Header("Spawnpoints")]
    public GameObject locationParent;

    [Header("prefabs by category")]
    public List<GameObject> goodScenarios = new List<GameObject>();
    public List<GameObject> badScenarios = new List<GameObject>();
    public List<GameObject> safetyScenarios = new List<GameObject>();

    private Dictionary<Category, List<GameObject>> scenarios = new Dictionary<Category, List<GameObject>>();

    [Header("spawned ones")]
    public List<GameObject> ActiveScenarios = new List<GameObject>();

    public void Start()
    {
        scenarios.Add(Category.good, goodScenarios);
        scenarios.Add(Category.bad, badScenarios);
        //scenarios.Add(Category.safety, safetyScenarios);
        GenerateScenarios();
    }

    public void GenerateScenarios()
    {
        ActiveScenarios.Clear();

        int goodCount = 0;
        foreach (Transform t in locationParent.GetComponentsInChildren<Transform>())
        {
            if(t == locationParent.transform)
                continue;

            Category thingType = System.Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .PickRandom();
            
            GameObject randomScenario = scenarios[thingType].PickRandom();
            
            if (randomScenario.name == "GuysStanding") { goodCount++; }
            
            if (goodCount > 2)
            {
                while (randomScenario.name == "GuysStanding")
                {
                    thingType = System.Enum.GetValues(typeof(Category))
                    .Cast<Category>()
                            .PickRandom();

                    randomScenario = scenarios[thingType].PickRandom();
                }
            }

            if (randomScenario == null)
                continue;

            GameObject i = Instantiate(randomScenario, t.localPosition, Quaternion.Euler(t.localRotation.x, Random.Range(-720, 721), t.localRotation.z), _scenarios.transform);
            try
            {
                i.transform.GetComponentInChildren<SituationScript>().takeTextSpot();
            }
            catch { Debug.LogWarning("noscripattached :("); }
            ActiveScenarios.Add(i);
        }
    }
}