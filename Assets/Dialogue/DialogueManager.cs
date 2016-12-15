using UnityEngine;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class DialogueManager : MonoBehaviour
{
    Conversation Data;

    // Use this for initialization
    void Start()
    {
        Data = ReadDialogueData("Squarepants");
        Say("Spongebob", 1);
        Say("Mr. Krabs", 1);
        Say("Sean Murphy", 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Conversation ReadDialogueData(string conversationName)
    {
        TextAsset asset = Resources.Load(Path.Combine("Dialogue", conversationName)) as TextAsset;
        return JsonConvert.DeserializeObject<Conversation>(asset.text);
    }

    public void Say(string speakerName, int phraseId)
    {
        var speaker = Data.Speakers.FirstOrDefault(s => s.Name == speakerName);
        if (speaker == null)
        {
            Debug.LogError("Say() Unknow speaker: " + speakerName);
        } else
        {
            var phrase = speaker.Phrases.FirstOrDefault(p => p.Id == phraseId);
            if (phrase == null)
            {
                Debug.LogError("Say() Unknow phrase " + phraseId + " for " + speakerName);
            } else
            {
                // Do something better here
                Debug.Log(speaker.Name + " says '" + phrase.Text);
                if (phrase.Next != null)
                {
                    Say(speakerName, phrase.Next.Value);
                }
            }
        }
    }
}