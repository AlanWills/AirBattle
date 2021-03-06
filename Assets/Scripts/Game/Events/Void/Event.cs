﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "Event", menuName = ComponentMenuConstants.EVENTS_FOLDER + "Event")]
    public class Event : ScriptableObject, IEvent
    {
        #region Properties and Fields

        private List<IEventListener> gameEventListeners = new List<IEventListener>();

        #endregion

        #region Event Management

        public void AddEventListener(IEventListener listener)
        {
            gameEventListeners.Add(listener);
        }

        public void RemoveEventListener(IEventListener listener)
        {
            gameEventListeners.Remove(listener);
        }

        public void Raise()
        {
            Debug.Log(string.Format("Event {0} was raised", name));
            
            for (int i = gameEventListeners.Count - 1; i >= 0; --i)
            {
                gameEventListeners[i].OnEventRaised();
            }
        }

        #endregion
    }
}
