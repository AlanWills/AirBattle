using AirBattle.Attributes.GUI;
using AirBattle.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Player
{
    [AddComponentMenu(ComponentMenuConstants.PLAYER_FOLDER + "Gun Firing Controller")]
    public class GunFiringController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private KeyCode fireBulletKey = KeyCode.Space;

        [SerializeField]
        private float bulletSpeed = 1;

        [SerializeField]
        private GameObjectAllocator bulletCache;

        #endregion

        #region Unity Methods

        private void Update()
        {
            if (Input.GetKeyDown(fireBulletKey) && bulletCache.CanAllocate(1))
            {
                GameObject bullet = bulletCache.Allocate();
                Debug.AssertFormat(bullet != null, "A null bullet was allocated in the Gun Firing Controller on GameObject {0}", gameObject.name);
                
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;

                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
                Debug.AssertFormat(bulletRigidBody != null,
                    "The bullet prefab {0} spawned in the Gun Firing Controller on GameObject {1} has no RigidBody2D.  Please add one.", bullet.name, gameObject.name);

                if (bulletRigidBody != null)
                {
                    bulletRigidBody.velocity = bullet.transform.right * bulletSpeed;
                }
            }
        }

        #endregion
    }
}
