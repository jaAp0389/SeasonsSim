/*****************************************************************************
* Project: MapGen
* File   : PrefabCreator.cs
* Date   : 25.11.2021
* Author : Jan Apsel (JA)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*   15.11.2021	JA	Created
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eSeason { Spring, Summer, Fall, Winter}
public class SceneManager : MonoBehaviour
{
    [SerializeField]eSeason currSeason = eSeason.Spring, lastSeason = eSeason.Winter;
    //GrassManager grassManager;

    [SerializeField] ParticleSystem particleSnow;
    [SerializeField] Material snowMaterial;

    [SerializeField] Material[] grass;
    [SerializeField] Material[] flowers;

    [SerializeField] GameObject sun;
    [SerializeField] bool isRotateSun;
    [SerializeField] float sunRotSpeed = 0.1f;

    float snowHeight, targetSnowHeight, lastSnowHeight;
    float swayAmount, targetSwayAmount, lastSwayAmount;
    float plantHeight, targetPlantHeight, lastPlantHeight;
    float flowerHeight, targetFlowerHeight, lastFlowerHeight;
    float colorRot, targetColorRot, lastColorRot;

    float lerpTime;

    bool isAutoSeason;

    private void Awake()
    {
        RefLibrary.sSceneManager = GetComponent<SceneManager>();
        //grassManager = RefLibrary.sGrassManager;
    }
    private void FixedUpdate()
    {
        if(isRotateSun)
        {
            RotateSun(sunRotSpeed);
        }
        if(isAutoSeason)SwitchSeason(Time.time);
        DoSeasons();
        LerpValues();
        SetSnowHeight();
        SetVegetationSway( grass);
        SetVegetationSway( flowers);
        SetVegetationColorRot( grass);
        SetVegetationColorRot( flowers);
        SetVegetationHeight(grass, plantHeight);
        SetVegetationHeight(flowers, flowerHeight);

    }
    public void NextSeason()
    {
        currSeason += 1;
        if ((int)currSeason > 3) currSeason = 0;
    }

    void RotateSun(float _angle)
    {
        sun.transform.Rotate(Vector3.right, _angle);
    }
    void SwitchSeason(float _time)
    {
        currSeason = (eSeason)((int)((_time) / 5) % 4);
    }


    void DoSeasons()
    {
        if (currSeason == lastSeason)
            return;
        lastSeason = currSeason;
        AssignLastValues();
        switch (currSeason)
        {
            case eSeason.Spring:
                targetSnowHeight = 0f;
                targetSwayAmount = 0.75f;
                targetPlantHeight = 1f;
                targetFlowerHeight = 1.1f;
                targetColorRot = 0.13f;
                StopSnowParticle();
                SwitchHideFlowers(false);
                break;
            case eSeason.Summer:
                targetSnowHeight = 0f;
                targetSwayAmount = 1f;
                targetPlantHeight = 1.14f;
                targetFlowerHeight = 1.21f;
                targetColorRot = 0f;
                StopSnowParticle();
                SwitchHideFlowers(false);
                break;
            case eSeason.Fall:
                targetSnowHeight = 0f;
                targetSwayAmount = 1.21f;
                targetPlantHeight = 1f;
                targetFlowerHeight = -0.1f;
                targetColorRot = -0.75f;
                StopSnowParticle();
                //SwitchHideFlowers(true);
                break;
            case eSeason.Winter:
                targetSnowHeight = 0.5f;
                targetSwayAmount = 0f;
                targetPlantHeight = 0.2f;
                targetFlowerHeight = -0.1f;
                targetColorRot = -0.75f;
                StartSnowParticle();
                SwitchHideFlowers(true);
                break;
        }
        lerpTime = 0f;
    }

    void LerpValues()
    {

        // 

        if (lerpTime >= 1f) return;

        if (targetSnowHeight > lastSnowHeight)
            snowHeight = Mathf.Max(Mathf.Lerp(lastSnowHeight, targetSnowHeight, 
                lerpTime), snowHeight);
        else snowHeight = Mathf.Min(Mathf.Lerp(lastSnowHeight, targetSnowHeight, 
            lerpTime), snowHeight);

        if (targetSwayAmount > lastSwayAmount)
            swayAmount = Mathf.Max(Mathf.Lerp(lastSwayAmount, targetSwayAmount, 
                lerpTime), swayAmount);
        else swayAmount = Mathf.Min(Mathf.Lerp(lastSwayAmount, targetSwayAmount, 
            lerpTime), swayAmount);

        if (targetPlantHeight > lastPlantHeight)
            plantHeight = Mathf.Max(Mathf.Lerp(lastPlantHeight, targetPlantHeight, 
                lerpTime), plantHeight);
        else plantHeight = Mathf.Min(Mathf.Lerp(lastPlantHeight, targetPlantHeight, 
            lerpTime), plantHeight);

        if (targetFlowerHeight > lastFlowerHeight)
            flowerHeight = Mathf.Max(Mathf.Lerp(lastFlowerHeight, targetFlowerHeight,
                lerpTime), flowerHeight);
        else flowerHeight = Mathf.Min(Mathf.Lerp(lastFlowerHeight, targetFlowerHeight,
            lerpTime), flowerHeight);

        if (targetColorRot > lastColorRot)
            colorRot = Mathf.Max(Mathf.Lerp(lastColorRot, targetColorRot, 
                lerpTime), colorRot);
        else colorRot = Mathf.Min(Mathf.Lerp(lastColorRot, targetColorRot,
            lerpTime), colorRot);

        lerpTime += 0.01f;
    }
    void AssignLastValues()
    {
        lastSnowHeight = targetSnowHeight;
        lastPlantHeight = targetPlantHeight;
        lastFlowerHeight = targetFlowerHeight;
        lastColorRot = targetColorRot;
        lastSwayAmount = targetSwayAmount;
    }

    void SetSnowHeight()
    {
        snowMaterial.SetFloat("_UpNode", snowHeight);
    }

    void StartSnowParticle()
    {
        if(particleSnow.isStopped)
            particleSnow.Play();
    }
    void StopSnowParticle()
    {
        if (particleSnow.isPlaying)
            particleSnow.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    void SetVegetationSway(Material[] vegetation)
    {
        foreach(Material veg in vegetation)
        {
            veg.SetFloat("_windStrength", swayAmount);
        }
    }
    void SetVegetationColorRot(Material[] vegetation)
    {
        foreach (Material veg in vegetation)
        {
            veg.SetFloat("_colorRot", colorRot);
        }
    }
    void SetVegetationHeight(Material[] vegetation, float _height)
    {
        foreach (Material veg in vegetation)
        {
            veg.SetFloat("_vertPos", _height);
        }
    }
    void SwitchHideFlowers(bool _isHide)
    {
        //if (isFlowersHide == _isHide)
        //    return;

        //isFlowersHide = _isHide;

        foreach(Material flower in flowers)
        {
            flower.SetFloat("_IsVertChange", _isHide ? 1f : 0f);
            //flower.SetFloat("_vertPos", _isHide? -1f : 1f);
        }
    }
}
