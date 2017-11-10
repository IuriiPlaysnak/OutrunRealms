using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using System;

public class OutrunRealmHTMLDataSource : IOutrunRealmDataSource {
	
	#region IOutrunRealmDataSource implementation

	public event Action<OutrunRealmDataProvider.SettingData> OnLoadingComplete;

	public void Load (string url)
	{
		Debug.Log ("OutrunRealmHTMLDataSource");

		OutrunRealmDataProvider.instance.StartCoroutine(DownloadHTML(url));
	}

	#endregion

	private IEnumerator DownloadHTML(string link) {

		Debug.Log ("Loading...");

		WWW request = new WWW (link);
		yield return request;

		Debug.Log ("Loading complete");

		if (request.error != null) {

			Debug.LogError (request.error);
		} else {

			ParseHTML(request.text);
		}
	}

	private void ParseHTML(string text) {

		text = text.Replace ("<!DOCTYPE html>", string.Empty);
		text = System.Text.RegularExpressions.Regex.Replace(text, "\\&[a-z]{1,5};", string.Empty);

		XDocument xdoc = XDocument.Parse (text);
		var nodes = xdoc
			.Root
			.Descendants ()
			.Where (node => node.Attribute ("class") != null)
			;

		nodes
			.Where (node => node.Attribute ("class").Value == BLOG_POST_CLASS)
			.ToList ()
			.ForEach (blogpost => ParseBlogPost (blogpost));

		nodes
			.Where (node => node.Attribute ("class").Value == GALLERY_CLASS)
			.ToList ()
			.ForEach (gallery => ParseGallery(gallery));

		OutrunRealmDataProvider.SettingData result = new OutrunRealmDataProvider.SettingData ();
		result.newsData = _posts [0];
		result.galleryData = _galleries [0];

		if (OnLoadingComplete != null)
			OnLoadingComplete (result);
	}

	private const string BLOG_POST_CLASS = "WVR-blogpost";
	private const string BLOG_HEADER_CLASS = "WVR-blogheader";
	private const string BLOG_IMAGE_CLASS = "WVR-blogimg";
	private const string BLOG_LINK_CLASS = "WVR-link";
	private const string GALLERY_CLASS = "WVR-gallery";

	private List<OutrunRealmDataProvider.GalleryData> _galleries;
	private void ParseGallery(XElement gallery) {

		Debug.LogError ("Imaplement new parsing method");

		/*
		_galleries = new List<OutrunRealmDataProvider.GalleryData> ();
		OutrunRealmDataProvider.GalleryData data = new OutrunRealmDataProvider.GalleryData ();
		data.title = gallery.Attribute("data-title").Value ?? "No title";

		data.images = new List<string> ();

		gallery
			.Descendants ()
			.Where (n => n.Name == "img")
			.ToList ()
			.ForEach (n => data.images.Add(n.Attribute("src").Value));
		
		data.thumbnailImageURL = gallery.Attribute("data-thumbnail").Value ?? data.images [0];

		_galleries.Add (data);
		*/
	}


	private List<OutrunRealmDataProvider.NewsData> _posts;
	private void ParseBlogPost(XElement post) {

		if (_posts == null)
			_posts = new List<OutrunRealmDataProvider.NewsData> ();

		var nodes = post
			.Descendants()
			.Where (node => node.Attribute ("class") != null)
			;

		OutrunRealmDataProvider.NewsData postData = new OutrunRealmDataProvider.NewsData();

		postData.title = 
			nodes
				.Where (node => node.Attribute ("class").Value == BLOG_HEADER_CLASS)
				.Select (node => node.Value)
				.FirstOrDefault()
				;

		postData.imageURL = 
			nodes
				.Where (node => node.Attribute ("class").Value == BLOG_IMAGE_CLASS)
				.Select (node => node.Attribute ("src").Value)
				.FirstOrDefault ()
				;

		postData.link =
			nodes
				.Where (node => node.Attribute ("class").Value == BLOG_LINK_CLASS)
				.Select (node => node.Attribute("href").Value)
				.FirstOrDefault()
				;

		_posts.Add(postData);
	}
}
