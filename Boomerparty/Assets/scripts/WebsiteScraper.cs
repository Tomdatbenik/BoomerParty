using HtmlAgilityPack;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using UnityEngine;

public class WebsiteScraper : MonoBehaviour
{
    public List<Minigame> minigamesList = new List<Minigame>();

    private void Start()
    {
        StartCoroutine(ReadSamenSpelenWebsite());
    }

    private IEnumerator ReadSamenSpelenWebsite()
    {
        using (WebClient client = new WebClient())
        {
            string htmlCode = client.DownloadString("https://samenspelen.online/spellen-overzicht/");
            HtmlDocument mainDocument = new HtmlDocument();

            mainDocument.LoadHtml(htmlCode);

            List<HtmlNode> minigames = mainDocument.DocumentNode.Descendants("div").Where(node => node.HasClass("item")).ToList();

            foreach (HtmlNode minigameNode in minigames)
            {
                HtmlNode titleNode = minigameNode.Descendants("div").Where(n => n.HasClass("title_link")).First();
                HtmlNode smalldescriptionNode = minigameNode.Descendants("div").Where(n => n.HasClass("excerpt")).First();
                HtmlNode imgurlNode = minigameNode.Descendants("img").First();

                string title = HttpUtility.HtmlDecode(titleNode.InnerText);
                string smallDescription = HttpUtility.HtmlDecode(titleNode.InnerText);
                HtmlNode urlNode = titleNode.Descendants("a").First();
                string url = urlNode.Attributes["href"].Value;
                string imgUrl = imgurlNode.Attributes["data-src"].Value;


                string htmlCodeExplenation = client.DownloadString(url);
                HtmlDocument explanationDocument = new HtmlDocument();
                explanationDocument.LoadHtml(htmlCodeExplenation);

                HtmlNode explanation = explanationDocument.DocumentNode.Descendants("ol").First();

                List<string> steps = new List<string>();

                foreach (HtmlNode step in explanation.Descendants("span").ToList())
                {
                    steps.Add(step.InnerText);
                }

                minigamesList.Add(new Minigame(title, smallDescription, imgUrl, url, steps));
            }
        }

        foreach(Minigame minigame in minigamesList)
        {
            Debug.Log("title: " + minigame.title + " description: " + minigame.smallDescription);

            foreach (string step in minigame.explanationSteps)
            {
                Debug.Log(step);
            }
        }

     

        yield return null;
    }
}
