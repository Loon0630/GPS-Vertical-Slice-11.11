using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turrent;
    public TurrentBlueprint turrentBlueprint;
    public bool isUpgraded = false;


    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    public bool afterBuild = false;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        Debug.Log("test");
        if (turrent != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurrentOn(buildManager.GetTurrentToBuild());
    }

    public void BuildTurrentOn(TurrentBlueprint blueprint)
    {

        if (afterBuild == false)
        {
            if (PlayerStats.Money < blueprint.cost)
            {
                Debug.Log("No money");
                return;
            }

            PlayerStats.Money -= blueprint.cost;

            GameObject _turrent = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
            turrent = _turrent;

            turrentBlueprint = blueprint;
            SoundManager.PlaySound("Build1");
        }
        afterBuild = true;

    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turrentBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turrentBlueprint.upgradeCost;
        Destroy(turrent);

        GameObject _turret = (GameObject)Instantiate(turrentBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turrent = _turret;

        isUpgraded = true;

    }

    public void SellTurret()
    {
        PlayerStats.Money += turrentBlueprint.GetSellAmount();

        Destroy(turrent);
        turrentBlueprint = null;
    }

    void OnMouseEnter()
    {
       if (!buildManager.CanBuild)
           return;

       if(afterBuild == false)
        {
            if (buildManager.HasMoney)
            {
                rend.material.color = hoverColor;
            }
            else
            {
                rend.material.color = notEnoughMoneyColor;
            }
        }
       
       
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
