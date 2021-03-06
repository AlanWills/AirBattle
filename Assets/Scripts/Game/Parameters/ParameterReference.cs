﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirBattle.Parameters
{
    [Serializable]
    public class ParameterReference<T, TValue, TReference> : ScriptableObject
                where TValue : ParameterValue<T>
                where TReference : ParameterReference<T, TValue, TReference>
    {
        #region Properties and Fields

        [SerializeField]
        private bool isConstant = true;
        public bool IsConstant
        {
            get { return isConstant; }
            set 
            { 
                isConstant = value; 

                if (isConstant)
                {
                    referenceValue = null;
                }
            }
        }

        [SerializeField]
        private T constantValue;

        [SerializeField]
        private TValue referenceValue;

        public T Value
        {
            get { return isConstant ? constantValue : referenceValue.value; }
            set
            {
                if (isConstant)
                {
                    constantValue = value;
                    referenceValue = null;
                }
                else
                {
                    referenceValue.value = value;
                }
            }
        }

        #endregion
    }
}
