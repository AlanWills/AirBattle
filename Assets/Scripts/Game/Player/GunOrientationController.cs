using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirBattle.Player
{
    public enum Orientation
    {
        Thirty = 30,
        Sixty = 60,
        Ninety = 90
    }

    [AddComponentMenu(ComponentMenuConstants.PLAYER_FOLDER + "Gun Orientation Controller")]
    [RequireComponent(typeof(SpriteRenderer))]
    public class GunOrientationController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Transform barrelTransform;

        [SerializeField]
        private Sprite ninetyDegreeSprite;

        [SerializeField]
        private Sprite sixtyDegreeSprite;

        [SerializeField]
        private Sprite thirtyDegreeSprite;

        [SerializeField]
        private KeyCode orientToNinetyKey = KeyCode.DownArrow;

        [SerializeField]
        private KeyCode orientToThirtyKey = KeyCode.UpArrow;

        private Orientation orientation = Orientation.Sixty;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (spriteRenderer == null || spriteRenderer.gameObject != this)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            if (barrelTransform == null && transform.childCount > 0)
            {
                barrelTransform = transform.GetChild(0);
            }

            if (spriteRenderer.sprite != sixtyDegreeSprite)
            {
                spriteRenderer.sprite = sixtyDegreeSprite;
            }
        }

        public void Awake()
        {
            string name = gameObject.name;

            Debug.AssertFormat(spriteRenderer != null, "The Gun Orientation Controller on GameObject {0} is missing a sprite renderer.  Please add one to the GameObject", name);
            Debug.AssertFormat(thirtyDegreeSprite != null, "The sprite for thirty degrees is not set in the Gun Orientation Controller on GameObject {0}", name);
            Debug.AssertFormat(sixtyDegreeSprite != null, "The sprite for sixty degrees is not set in the Gun Orientation Controller on GameObject {0}", name);
            Debug.AssertFormat(ninetyDegreeSprite != null, "The sprite for ninety degrees is not set in the Gun Orientation Controller on GameObject {0}", name);

            SetOrientation(sixtyDegreeSprite, Orientation.Sixty);
        }

        public void Update()
        {
            if (spriteRenderer == null)
            {
                return;
            }

            if (Input.GetKey(orientToNinetyKey))
            {
                SetOrientation(ninetyDegreeSprite, Orientation.Ninety);
            }
            else if (Input.GetKey(orientToThirtyKey))
            {
                SetOrientation(thirtyDegreeSprite, Orientation.Thirty);
            }
            else if (orientation != Orientation.Sixty)
            {
                SetOrientation(sixtyDegreeSprite, Orientation.Sixty);
            }
        }

        #endregion

        #region Orientation Methods

        private void SetOrientation(Sprite sprite, Orientation orientation)
        {
            spriteRenderer.sprite = sprite;
            barrelTransform.rotation = Quaternion.AngleAxis((int)orientation, new Vector3(0, 0, 1));
            this.orientation = orientation;
        }

        #endregion
    }
}