﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Author:       Running
** Time:         18.9.4
** Describtion:  模型的管理
*/

/// <summary>
/// 管理所有ImageTarget下的模型。
/// </summary>
public class ModelManager
{
    //所有的模型
    private HashSet<Model> _allModelList = new HashSet<Model>();

    /// <summary>
    /// 两个模型靠近多少距离
    /// </summary>
    private float _nearDistance = 1.5f;

    private static ModelManager _instance = null;

    public static ModelManager Instance
    {
        get 
        {
            if (null == _instance) 
            {
                _instance = new ModelManager();
            }
            return _instance;
        }
    }

    /// <summary>
    /// TrackManager识别到ImageTarget后，把挂在下面的模型添加到这里管理。
    /// </summary>
    /// <param name="model"></param>
    public void AddModel(Model model) 
    {
        _allModelList.Add(model);
    }

    public void Update() 
    {
        foreach(var model1 in _allModelList)
        {
            foreach (var model2 in _allModelList)
            {
                //模型有在显示
                //在一定范围内，模型间有所行动。
                if (model1 != model2 && 
                    //model1.gameObject.activeSelf &&
                    //model2.gameObject.activeSelf &&
                    Vector3.Distance(model1.transform.position, model2.transform.position) < _nearDistance)
                {
                    model1.Action(model2);
                    model2.Action(model1);
                }
            }    
        }
    }
}