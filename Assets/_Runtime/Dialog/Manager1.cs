using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Manager1 : MonoBehaviour
{
	public static Manager1 instance;

	[SerializeField]
	private PlayableDirector director;

	[SerializeField]
	private int eventIndex;

	[SerializeField]
	private Dialogue dialogue;

	public void TriggerEvent()
	{
		switch (eventIndex)
		{
		case 0:
			director.Pause();
			dialogue.StartDialogue(0, 2, delegate
			{
				director.Resume();
			});
			break;
		case 1:
			director.Pause();
			dialogue.StartDialogue(3, 5, delegate
			{
				director.Resume();
			});
			break;
		}
		eventIndex++;
	}

	public void PlayCutscene(string scene)
	{
		director.playableAsset = Resources.Load<PlayableAsset>("Timeline/" + scene);
		director.Play();
	}
}
