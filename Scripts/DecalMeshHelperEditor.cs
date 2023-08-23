﻿/*****************************************************************************
* Project: MapGen
* File   : DecalMeshHelperEditor.cs
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
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneManager))]
class DecalMeshHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Next Season"))
        {
            RefLibrary.sSceneManager.NextSeason();
        }
    }
}