using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class EnemyTester
{
    private NormalEnemy m_enemy;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("UnitTestEnemy");
    }

    [UnitySetUp]
    public IEnumerator SetupTests()
    {
        yield return new WaitForSeconds(0.5f);

        m_enemy = GameObject.FindObjectOfType<NormalEnemy>();
    }

    [UnityTest]
    public IEnumerator EnemyTakesMoreDamageThanHealthShouldBeDestroyed()
    {
        NormalEnemy testEnemy = GameObject.Instantiate<NormalEnemy>(m_enemy);
        yield return new WaitForEndOfFrame();
        testEnemy.TakeDamage(testEnemy.health + 1);
        yield return new WaitForEndOfFrame();
        Assert.IsTrue(testEnemy == null);
    }
}
