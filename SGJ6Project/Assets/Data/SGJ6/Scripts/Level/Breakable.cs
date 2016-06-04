﻿using UnityEngine;

namespace PONME_NAMESPACE {

	public class Breakable : MonoBehaviour {

        [SerializeField]
        private int maxLife = 1;
        public int CurrentLife
        {
            get { return maxLife; }
        }

        public bool Hit()
        {
            --maxLife;
            if (maxLife == 0)
            {
                //Break
            }
            return (maxLife == 0);
        }

	}
}