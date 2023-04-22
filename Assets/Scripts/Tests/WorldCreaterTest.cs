using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class WorldCreaterSpawner
{
    private WorldCreator m_worldCreator;
    private PathFinder m_pathfinder;
    private Tower m_towerPrefab;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("UnitTestWorldCreator");
    }

    [UnitySetUp]
    public IEnumerator SetUpTests()
    {
        yield return new WaitForSeconds(0.5f);

        m_worldCreator = GameObject.FindObjectOfType<WorldCreator>();
        m_pathfinder = GameObject.FindObjectOfType<PathFinder>();
    }

    [UnityTest]
    public IEnumerator AmountOfTilesAfterWorldCreatedIsCorrect()
    {
        m_worldCreator.rows = 20;
        m_worldCreator.columns = 10;
        WorldCreator worldCreator = GameObject.Instantiate<WorldCreator>(m_worldCreator);
        yield return new WaitForEndOfFrame();
        Assert.GreaterOrEqual(worldCreator.GetGameObjectsTiles().Count, m_worldCreator.rows * m_worldCreator.columns);
    }

    [UnityTest]
    public IEnumerator CanGeneratePathAfterWorldGeneration()
    {
        WorldCreator worldCreator = GameObject.Instantiate<WorldCreator>(m_worldCreator);
        worldCreator.rows = 10;
        worldCreator.columns = 10;
        yield return new WaitForEndOfFrame();
        PathFinder pathFinder = GameObject.Instantiate<PathFinder>(m_pathfinder);
        pathFinder.SetWorldTiles(worldCreator);
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(pathFinder.CanGeneratePath());
    }

    [UnityTest]
    public IEnumerator CantBlockPathByBuildingTowers()
    {
        WorldCreator worldCreator = GameObject.Instantiate<WorldCreator>(m_worldCreator);
        worldCreator.rows = 10;
        worldCreator.columns = 10;
        yield return new WaitForEndOfFrame();
        PathFinder pathFinder = GameObject.Instantiate<PathFinder>(m_pathfinder);
        pathFinder.SetWorldTiles(worldCreator);
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(pathFinder.CanGeneratePath());
    }

}
