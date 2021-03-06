﻿/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            TrackManager.Instance.SetTrackStatus(gameObject, previousStatus, newStatus);
            //MyTrackableStateChanged(previousStatus, newStatus);
            //if (newStatus == TrackableBehaviour.Status.DETECTED ||
            //    newStatus == TrackableBehaviour.Status.TRACKED ||
            //    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            //{
            //    //OnTrackingFound();
                
            //}
            //else
            //{
            //    //OnTrackingLost();
            //}
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS

        public void MyTrackableStateChanged(bool isFound) 
        {
            if (isFound)
            {
                MyTrackingFound();
            }
            else
            {
                MyTrackingLost();
            }
        }

        void MyTrackingFound() 
        {
            if (!TrackManager.Instance.isShowModel)
                return;

            Model[] modelArray = transform.GetComponentsInChildren<Model>(true);
            for (int i = 0; i < modelArray.Length; i++)
            {
				//Timer.Add (0.5f, (id, args)=>
				//{
					modelArray[i].gameObject.SetActive(true);
                //});

                EventCenter.Instance.PostEvent(EventName.ModelShow, modelArray[i].name);
            }
        }

        void MyTrackingLost() 
        {
            Model[] modelArray = transform.GetComponentsInChildren<Model>(true);
            for (int i = 0; i < modelArray.Length; i++)
            {
                //模型需要做数据恢复。
                modelArray[i].gameObject.SetActive(false);
                modelArray[i].End();

                EventCenter.Instance.PostEvent(EventName.ModelHide, modelArray[i].name);
            }
        }
    }
}
