using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformSpawner : MonoBehaviour
    {
        public GameObject[] Platforms;
        public Transform Character;
        public float BlockWidth;
        public float BlockLeapHeight;
        public float CurrentBlockY;

        private float _currentMaxX;
        private int _currentPlatformIndex;
        private IList<int> _platformIndexes;

        private enum PlatformDirection
        {
            Up,
            Down,
            Forward,
            NotAllowed
        };

        private IDictionary<string, PlatformDirection> _directions;

        // Use this for initialization
        void Start ()
        {
            _currentPlatformIndex = 0;
            _currentMaxX = -10;

            _directions = new Dictionary<string, PlatformDirection>
            {
                {"00", PlatformDirection.Up},
                {"01", PlatformDirection.Forward},
                {"02", PlatformDirection.Forward},
                {"10", PlatformDirection.Up},
                {"11", PlatformDirection.Forward},
                {"12", PlatformDirection.Forward},
                {"20", PlatformDirection.NotAllowed},
                {"21", PlatformDirection.Down},
                {"22", PlatformDirection.Down}
            };

            _platformIndexes = new List<int>() {1};
        }
	
        // Update is called once per frame
        void Update () {
            while (Character.position.x + 50 > _currentMaxX)
            {
                Spawn();
            }
        }

        void Spawn()
        {
            var direction = PlatformDirection.NotAllowed;
            var platformIndex = 0;
            while (direction == PlatformDirection.NotAllowed)
            {
                platformIndex = Random.Range(0, Platforms.Length);
                direction = _directions[string.Format("{0}{1}", _currentPlatformIndex, platformIndex)];              
            }

            _currentPlatformIndex = platformIndex;
            _platformIndexes.Add(platformIndex);
            var platform = Platforms[platformIndex];

            if (direction == PlatformDirection.Up)
            {
                CurrentBlockY += BlockLeapHeight;
            }
            else if (direction == PlatformDirection.Down)
            {
                CurrentBlockY -= BlockLeapHeight;
            }

            Instantiate(platform, new Vector3(_currentMaxX, CurrentBlockY, 0), Quaternion.identity);
            _currentMaxX += BlockWidth;
        }
    }
}
