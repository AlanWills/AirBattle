using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Level
{
    [AddComponentMenu(ComponentMenuConstants.LEVEL_FOLDER + "Level Initializer")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class LevelInitializer : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private Camera worldCamera;

        [SerializeField]
        private GameObject gun;

        [SerializeField]
        private GameObject enemySpawnPoint;

        [SerializeField, Tooltip("The percentage across the screen width the centre of the player will be placed.  0 = left edge, 1 = right edge"), Range(0, 1)]
        private float gunXCoordRelativeToScreen = 0.25f;

        [SerializeField, Tooltip("The percentage up the screen height the centre of the enemy spawn will be placed.  0 = bottom edge, 1 = top edge"), Range(0, 1)]
        private float enemySpawnYCoordRelativeToScreen = 0.85f;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            worldCamera = worldCamera != null ? worldCamera : Camera.main;

            if (worldCamera != null && ! worldCamera.orthographic)
            {
                worldCamera.orthographic = true;
            }

            if (GetComponent<BoxCollider2D>() == null)
            {
                gameObject.AddComponent<BoxCollider2D>();
            }

            PositionGun();
            PositionSpawnPoint();
        }

        private void Awake()
        {
            PositionGun();
            PositionSpawnPoint();

            float height = worldCamera.orthographicSize * 2.0f;
            float width = height * worldCamera.aspect;
            GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        }

        #endregion

        #region Positioning Methods

        private void PositionGun()
        {
            Debug.AssertFormat(gun != null, "LevelInitializer has no 'gun' set on GameObject {0}", gameObject.name);
            if (gun != null)
            {
                PositionGameObjectRelative(gun, gunXCoordRelativeToScreen, 0);
            }
        }

        private void PositionSpawnPoint()
        {
            Debug.AssertFormat(enemySpawnPoint != null, "LevelInitializer has no 'enemySpawnPoint' set on GameObject {0}", gameObject.name);
            if (enemySpawnPoint != null)
            {
                PositionGameObjectRelative(enemySpawnPoint, 0, enemySpawnYCoordRelativeToScreen);
            }
        }

        private void PositionGameObjectRelative(GameObject gameObject, float relativeX, float relativeY)
        {
            Vector3 position = worldCamera.ScreenToWorldPoint(new Vector3(relativeX * Screen.width, relativeY * Screen.height, 0));
            position.z = 0;
            gameObject.transform.position = position;
        }

        #endregion
    }
}
