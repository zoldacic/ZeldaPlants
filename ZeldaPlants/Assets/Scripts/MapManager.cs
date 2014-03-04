using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class MapManager : MonoBehaviour
    {
		public int SizeX ;
			public int SizeY;
			
        private MapGenerator _mapGenerator;

        void Start ()
        {
			Debug.Log("Starting generating");
            var pathProbablities = new List<double>() {1.0, 0.5};
	        _mapGenerator = new MapGenerator(SizeX, SizeY, 9, 5, pathProbablities);     
            _mapGenerator.Generate();

			var textures = Resources.LoadAll("Desert");
			var sprites = Resources.LoadAll<Sprite>("Desert");
			Debug.Log(string.Format("Retrieved {0} textures.", textures.Length));
			Debug.Log(string.Format("Retrieved {0} sprites.", sprites.Length));
			//Object[] textures = Resources.LoadAll("Desert");
			var map = _mapGenerator.Map;
			for (var x = 0; x < SizeX - 1; x++)
			{
				for (var y = 0; y < SizeY - 1; y++)
				{
					//var textureRect = new Rect(x*81, y*98, 81, 98);
					//var texture = textures[map[x, y]];
					//sprite = Sprite.Create((texture, textureRect, new Vector2(0, 0), 100.0f);
					
					if (map[x,y] > -1) {
						var go = new GameObject();
						var spriteRenderer = go.AddComponent<SpriteRenderer>();
						
						var sprite = sprites[map[x,y]];
						
						//go.transform.Translate(x * 80, y * 80, 0);
						go.transform.position = new Vector2(x,0); //= new Vector3(x * 80f, y * 80f);
						
						//sprite.textureRect.x = 0;
						//sprite.rect.y = 0;
						
						spriteRenderer.sprite = sprite;
						Debug.Log(sprite.rect);
					}
				}
			}

        }
	
        void Update ()
        {

        }

        void OnGUI()
        {


   }
	}


}
