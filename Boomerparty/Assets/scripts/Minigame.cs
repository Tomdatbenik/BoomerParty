using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame
{
    public string title;
    public string smallDescription;
    public string imgUrl;
    public string url;

    public List<string> explanationSteps;

    public Minigame(string title, string smallDescription, string imgUrl, string url, List<string> explanationSteps)
    {
        this.title = title;
        this.smallDescription = smallDescription;
        this.imgUrl = imgUrl;
        this.url = url;
        this.explanationSteps = explanationSteps;
    }
}
