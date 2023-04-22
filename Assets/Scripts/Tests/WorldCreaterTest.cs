using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class WorldCreatorSpawner
{
    private WorldCreator m_worldCreator;
    private PathFinder m_pathfinder;
    private SingleTargetTower m_towerPrefab;
    private TileEntity m_tileEntity;
    private ResourceController m_resourceController;

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
        m_resourceController = GameObject.FindObjectOfType<ResourceController>();
        m_tileEntity = GameObject.FindObjectOfType<TileEntity>();
        m_towerPrefab = GameObject.FindObjectOfType<SingleTargetTower>();
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
        //Create 10 by 10 world
        m_worldCreator.rows = 10;
        m_worldCreator.columns = 10;
        WorldCreator worldCreator = GameObject.Instantiate<WorldCreator>(m_worldCreator);

        yield return new WaitForEndOfFrame();
        //Give the path finder the new world set
        PathFinder pathFinder = GameObject.Instantiate<PathFinder>(m_pathfinder);
        pathFinder.SetWorldTiles(worldCreator);
        yield return new WaitForEndOfFrame();
        //Get the tiles so we can set towers at the correct positions to test
        TileEntity[,] tiles = worldCreator.GetWorldArray();
        //Place one in front and one on the side of the start node.
        worldCreator.SelectTileEntity(tiles[0, 1]);
        worldCreator.ButtonTowerOneClicked();
        worldCreator.SelectTileEntity(tiles[1, 0]);
        worldCreator.ButtonTowerTwoClicked();
        //Check if path is not blocked
        Assert.IsTrue(pathFinder.CanGeneratePath());
    }

}
