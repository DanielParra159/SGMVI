using UnityEngine;

namespace SGJVI.Core {

	public static class GameLayers {

        public static int LAYER_DEFAULT = 0;
        public static int LAYER_TRANSPARENTFX = 1;
        public static int LAYER_IGNORERAYCAST = 2;
        public static int LAYER_WATER = 4;
        public static int LAYER_UI = 5;

        public static int LAYER_BREAKABLE = 8;
        public static int LAYER_FLOOR = 9;
        public static int LAYER_ENEMY = 10;
        public static int LAYER_ENEMY_PROJECTILE = 11;
        public static int LAYER_CHARACTER = 12;
        public static int LAYER_CHARACTER_PROJECTILE = 13;
        public static int LAYER_WALL = 14;
        public static int LAYER_TRIGGER = 15;
        public static int LAYER_INVISIBLE_ENEMY_WALL = 16;

        public static int ObstaclesMask = (1 << LAYER_FLOOR) | (1 << LAYER_WALL) | (1 << LAYER_BREAKABLE);
        public static int FloorMask = (1 << LAYER_FLOOR);
		public static int BreakableMask = (1 << LAYER_BREAKABLE);
        public static int PlayerMask = (1 << LAYER_CHARACTER);
        public static int EnemiesMask = (1 << LAYER_ENEMY);
        public static int InvisibleEnemyWall = (1 << LAYER_INVISIBLE_ENEMY_WALL);
	}
}