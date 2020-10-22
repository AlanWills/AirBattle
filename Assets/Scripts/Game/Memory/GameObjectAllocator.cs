using AirBattle.Attributes.GUI;
using AirBattle.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Memory
{
    [AddComponentMenu(ComponentMenuConstants.MEMORY_FOLDER + "Game Object Allocator")]
    public class GameObjectAllocator : MonoBehaviour
    {
        #region Properties and Fields

        public uint Capacity { get { return capacity; } }

        public GameObject prefab;

        [SerializeField, ReadOnlyAtRuntime, Min(1), Tooltip("The maximum number of GameObjects that can be active at any one time.")]
        private uint capacity = 5;

        private List<GameObject> cache = new List<GameObject>();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (cache.Capacity == 0)
            {
                cache.Capacity = (int)capacity;

                Debug.AssertFormat(prefab != null, "There is no prefab set on the GameObjectAllocator on GameObject {0}.  Please set one.", gameObject.name);
                for (uint i = 0; i < capacity; ++i)
                {
                    GameObject instance = GameObject.Instantiate(prefab, transform);
                    instance.SetActive(false);
                    cache.Add(instance);
                }
            }
            else
            {
                Debug.LogError("Reinitializing an initialized GameObjectAllocator is not currently supported and so this attempt will be ignored.");
            }
        }

        #endregion

        #region Allocation Methods

        public GameObject Allocate()
        {
            GameObject gameObject = FindInactiveGameObject();
            Debug.Assert(gameObject != null, "Invalid call to Allocate.  Dangerous side effects will occur here - ensure you call CanAllocate() first.");
            return gameObject;
        }

        public bool CanAllocate(uint requestedAmount)
        {
            uint currentCount = 0;

            foreach (GameObject gameObject in cache)
            {
                currentCount = gameObject.activeSelf ? currentCount : currentCount + 1;
            }

            return currentCount >= requestedAmount;
        }

        public void DeallocateAll()
        {
            foreach (GameObject gameObject in cache)
            {
                gameObject.SetActive(false);
            }
        }

        private GameObject FindInactiveGameObject()
        {
            foreach (GameObject gameObject in cache)
            {
                if (!gameObject.activeSelf)
                {
                    return gameObject;
                }
            }

            return null;
        }

        #endregion
    }
}
