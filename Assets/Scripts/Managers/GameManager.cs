using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public Layer[] Layers;

	public ObjectSpawner ObjectSpawner;

	private ScoreMenuHandlers ScoreMenu;

	public Texture Noise;
	public Texture Borders;

	private float InitialSpeed;

    DiscoSetting _discosetting;

	void Awake()
	{
        _discosetting = FindObjectOfType<DiscoSetting>();
		Global.Instance = new Global();



       /* if (_discosetting.IsDisco == true)
        {
            Global.Instance.IsDisco = true;
            print("global: " + Global.Instance.IsDisco);
            if (Global.Instance.IsDisco)
            {
                _discosetting.IsDisco = true;
                print("the if statement where discosetting.disco =  true returns: " + _discosetting.IsDisco);
                Camera.main.gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<DiscoSetting>().DiscoClip;
            }
           
        }*/

		ScoreMenu = FindObjectOfType<ScoreMenuHandlers>();

		InitialSpeed = Global.Instance.Speed;
	}

    void Start()
    {

        if (_discosetting.IsDisco )
        {
            Global.Instance.IsDisco = true;
            print("wtfffff ");
            Camera.main.gameObject.GetComponent<AudioSource>().clip = FindObjectOfType<DiscoSetting>().DiscoClip;
        }
    }

	void Update ()
	{
		if(Global.Instance.IsPlaying)
		{
			foreach(Layer layer in Layers)
			{
				layer.UpdateLayer();
			}

			ObjectSpawner.UpdateSpawner();
			
			Global.Instance.DistanceScore += (Global.Instance.Speed * Global.Instance.ComboMultiplier) * Time.deltaTime;

			Global.Instance.Speed = Global.Instance.InitialSpeed + (Global.Instance.DistanceScore * 0.0075F);
		}
		else
		{
			Global.Instance.Speed = 0;

			if(Global.Instance.IsPlayerDead && !ScoreMenu.IsVisible)
			{
				ScoreMenu.ShowScoreMenu();
			}
			else if(!Global.Instance.IsPlayerDead && !ScoreMenu.IsVisible)
			{
				ScoreMenu.ShowPauseMenu();
			}
		}
	}

	public void Pause()
	{
		Global.Instance.IsPlaying = false;
	}

	// Doing the GUI in here for now.
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Noise);
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Borders);
	}
}
