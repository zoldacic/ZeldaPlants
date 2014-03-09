using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MapManager : MonoBehaviour
    {
        public int SizeX;
        public int SizeY;

        private MapGenerator _mapGenerator;
		private bool _isGenerated;

        private void Start()
        {
            Debug.Log("Starting generating");
            var pathProbablities = new List<double>() {1.0, 0.5};
            _mapGenerator = new MapGenerator(SizeX, SizeY, 9, 5, pathProbablities);
            _mapGenerator.Generate();

			_isGenerated = false;

        }

        private void Update()
        {

        }

        private void OnGUI()
        {
            var textures = Resources.LoadAll("Desert");
            var sprites = Resources.LoadAll<Sprite>("Desert");

            //Object[] textures = Resources.LoadAll("Desert");

			if (_mapGenerator != null && !_isGenerated) {
				Debug.Log(string.Format("Retrieved {0} textures.", textures.Length));
				Debug.Log(string.Format("Retrieved {0} sprites.", sprites.Length));
				Debug.Log(string.Format("Screen.width: {0}, screen.height: {1}", Screen.width, Screen.height));

            var map = _mapGenerator.Map;
            for (var x = 0; x < SizeX - 1; x++)
            {
                for (var y = 0; y < SizeY - 1; y++)
                {
                    //var textureRect = new Rect(x*81, y*98, 81, 98);
                    //var texture = textures[map[x, y]];
                    //sprite = Sprite.Create((texture, textureRect, new Vector2(0, 0), 100.0f);

                    if (map[x, y] > -1)
                    {
                        var go = new GameObject();
                        var spriteRenderer = go.AddComponent<SpriteRenderer>();

                        var spriteNo = map[x, y] + 1; // The first sprite is the full image so the number is one of

                        if (spriteNo < sprites.Length)
                        {

                            var sprite = sprites[spriteNo];

                            //go.transform.Translate(x * 80, y * 80, 0);
                            // go.transform.position = new Vector2(x * 180 / (float)Screen.width, y * 80 / (float)Screen.height); //= new Vector3(x * 80f, y * 80f);
                            //go.transform.position = new Vector2(x, y);

                            var screenPoint = new Vector3(x*40, 0, 0);
                            var worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
                            worldPos.z = 0f;
                            worldPos.y = 0f;
                            go.transform.position = worldPos;

                            //sprite.textureRect.x = 0;
                            //sprite.rect.y = 0;

                            spriteRenderer.sprite = sprite;
                            Debug.Log(sprite.rect);
                        }
                        else
                        {
                            Debug.LogError("Calculated sprite index is larger than array!");
                        }
                    }
                }
            }

				_isGenerated = true;
			}

        }
    }


}
