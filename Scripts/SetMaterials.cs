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

public class SetMaterials : MonoBehaviour
{
    Renderer[] childrenMR;
    private void Awake()
    {
        childrenMR = GetComponentsInChildren<Renderer>();
    }

    public void SetMaterial(Material _material)
    {
        foreach(Renderer renderer in childrenMR)
        {
            renderer.material = _material;
        }
    }
}
