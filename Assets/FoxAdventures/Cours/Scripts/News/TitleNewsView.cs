using System;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using PlayFab;

using PlayFab.ClientModels;




[System.Serializable]

public class TitleNewsViewEntry {

    public string title;

    public string body;

    public DateTime displayedDate;

    public string DisplayedDateStr {
        get {
            return this.displayedDate.ToLongDateString();
        }
    }

}




public class TitleNewsView : MonoBehaviour {

    public Transform contentRoot = null;

    public Text contentText = null;

    public bool automaticShowLatestNews = false;

    void OnEnable() {
        this.HideView();
        if (this.automaticShowLatestNews == true)
            this.ShowLatestNews();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) == true) {
            this.HideView();
        }

    }




    public void ShowLatestNews() {
        if (PlayfabAuth.IsLoggedIn == true) {
            PlayFabClientAPI.GetTitleNews(new GetTitleNewsRequest(), OnGetTitleNewsSuccess, OnGetTitleNewsError);
        }
    }




    private void OnGetTitleNewsSuccess(GetTitleNewsResult result) {
        List<TitleNewsViewEntry> news = new List<TitleNewsViewEntry>();
        foreach (var item in result.News) {
            DateTime dateTime = item.Timestamp.ToLocalTime();
            news.Add(new TitleNewsViewEntry(){
                title = item.Title,
                body = item.Body,
                displayedDate = dateTime
            });
        }
        
        if (news != null && news.Count > 0) {
            if (this.contentText != null) {
                string newsContent = string.Empty;
                for (int i = 0; i < news.Count; i++) {
                    if (string.IsNullOrWhiteSpace(newsContent) == false)
                        newsContent += "\n\n";
                        newsContent += "- " + news[i].DisplayedDateStr + " -";
                        newsContent += "\n<color=orange>" + news[i].title + "</color>";
                        newsContent += "\n" + news[i].body;

                }

                this.contentText.text = newsContent;
            }
            this.ShowView();
        } else {
            this.HideView();
        }

    }




    private void OnGetTitleNewsError(PlayFabError error) {
        Debug.LogError("TitleNewsView.OnGetTitleNewsError() - Error: " + error.GenerateErrorReport());
        this.HideView();
    }

    public void ShowView() {
        if (this.contentRoot != null)
            this.contentRoot.gameObject.SetActive(true);
    }




    public void HideView() {
        if (this.contentRoot != null)
            this.contentRoot.gameObject.SetActive(false);
    }

}