using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Events
{
    [AddComponentMenu(ComponentMenuConstants.EVENTS_FOLDER + "GameObject Event Listener")]
    public class GameObjectEventListener : ParameterisedEventListener<GameObject, GameObjectEvent, GameObjectUnityEvent>
    {
    }
}
