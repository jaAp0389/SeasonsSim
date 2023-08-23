/*****************************************************************************
* Project: MapGen
* File   : ShaderTest.cs
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

public class GrassManager : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] GameObject objectGrass;

    [SerializeField] Material[] grassMats;
    [SerializeField] Material[] flowerMats;

    [SerializeField] float distanceApart,
                           randomPosRange;

    [SerializeField] float colorRotationValue;

    Material material;

    float colorRot = 0f;

    void Awake()
    {
        material = objectGrass.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        RefLibrary.sGrassManager = GetComponent<GrassManager>();
    }

    private void Start()
    {
        PlaceObjects(1f, grassMats);
        PlaceObjects(0.2f, flowerMats);
    }

    void FixedUpdate()
    {
        //material.SetFloat("_colorRot", colorRot);
        //colorRot = colorRotationValue;
    }

    void PlaceObjects(float placeChance, params Material[] mats)
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (Random.Range(0f, 1f) > placeChance)
                    continue;
                GameObject newGo = Instantiate(objectGrass, transform.position 
                    + new Vector3(x + Random.Range(-randomPosRange, 
                    randomPosRange) * distanceApart, 0, y + 
                    Random.Range(-randomPosRange, randomPosRange)) 
                    * distanceApart, transform.rotation);

                newGo.transform.localScale.Set(0, Random.Range(0.1f, 3f), 0);

                newGo.GetComponent<SetMaterials>().SetMaterial
                    (mats[Random.Range(0, mats.Length - 1)]);
                newGo.transform.SetParent(transform);
            }
        }
    }
}
