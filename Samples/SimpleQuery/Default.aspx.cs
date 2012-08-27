﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using facebook;
using Facebook;
using System.Net;
using System.IO;

namespace SimpleQuery
{
	public partial class _Default : System.Web.UI.Page
	{
		string m_clientId = "231464590266507";
		string m_clientSecret = "9dd6ce54b4405dd1325d271d2419bc34";
		string m_scope = "email,read_stream,offline_access,user_checkins,manage_notifications,read_mailbox,friends_birthday,user_hometown,friends_hometown,friends_relationship_details ";

		// the getting the token code is from here: http://multitiered.wordpress.com/tag/c/

		private static string GetRedirectUri()
		{
			var url = HttpContext.Current.Request.Url;
			string redirectUri = HttpUtility.UrlEncode(url.GetLeftPart(UriPartial.Authority) + "/");
			return redirectUri;
		}

		private string GetAccessToken()
        {
            if (HttpRuntime.Cache["access_token"] == null)
            {
                Dictionary<string, string> args = GetOauthTokens(Request.Params["code"]);
                HttpRuntime.Cache.Insert("access_token", args["access_token"], null, DateTime.Now.AddMinutes(Convert.ToDouble(args["expires"])), TimeSpan.Zero);
            } 

            return HttpRuntime.Cache["access_token"].ToString();
        }


        private Dictionary<string, string> GetOauthTokens(string code)
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();

			string redirectUri = GetRedirectUri();

            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}&scope={4}",
							m_clientId, redirectUri, m_clientSecret, code, m_scope);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

			using(HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string retVal = reader.ReadToEnd();

                foreach (string token in retVal.Split('&'))
                {
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                    token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                }
            }

            return tokens;

        }

		protected void Page_Load(object sender, EventArgs e)
		{
			
			if (Request.Params["code"] == null)
			{
				string redirectUri = GetRedirectUri();

				string dialog_url = string.Format("http://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}",
								m_clientId, redirectUri, m_scope);
				Response.Redirect(dialog_url);
			}
			else
			{
				FqlToLinqSample();
			}
		}

		private void FqlToLinqSample()
		{
			var fb = new FacebookClient(GetAccessToken());
			var db = new FacebookDataContext(fb);

			var useQuerySyntax = true;
			var useMethodSyntax = false;

			if (useQuerySyntax)
			{
				//var albumQuery = from album in db.Album where album.Aid == "20531316728_324257" select album.CanUpload;
				//var albumResult = albumQuery.ToArray();

				//var CheckinQuery = from Checkin in db.Checkin where Checkin.AuthorUid == db.Me  select Checkin;
				//var CheckinResult = CheckinQuery.ToArray();


				//var CheckinQuery = from Checkin in db.Comment where Checkin. == db.Me  select Checkin;
				//var CheckinResult = CheckinQuery.ToArray();

				///var ThreadQuery = from Thread in db.Thread where Thread.FolderId == "0" select Thread.ThreadId;
				//var MessageQuery = from Message in db.Message where ThreadQuery.Contains(Message.ThreadId) select Message;

				//var MessageResult = MessageQuery.ToArray();
			

				//var NotificationQuery = from Notification in db.Notification where Notification.RecipientId == db.Me select Notification.CreatedTime;
				//var NotificationResult = NotificationQuery.ToArray();

				//var CommentsQuery = from Comments in db.Comment where Comments.ObjectId == "483854529708" select Comments.Comments;
				//var CommentsResult = CommentsQuery.ToArray();

				//var GroupQuery = from Group in db.Group where Group.Gid == 146797922030397 select Group.Venue;
				//var GroupResult = GroupQuery.ToArray();
				

				var friendIds = from friend in db.Friend where friend.Uid1 == db.Me select friend.Uid2;
				var friendDetails = (from user in db.User where friendIds.Contains(user.Uid) select new { Name = user.MeetingSex, Picture = user.PicSmall });
				listFriends.DataSource = friendDetails.ToArray();
					DataBind();
			}
			else if (useMethodSyntax)
			{
				var friendIds2 = db.Friend.Where(t => t.Uid1 == db.Me).Select(t => t.Uid2);
				var friendDetails2 = db.User.Where(t => friendIds2.Contains(t.Uid)).Select(t => new { Name = t.Name, Picture = t.PicSmall }).Take(5);
				listFriends.DataSource = friendDetails2.ToArray();
				DataBind();
			}
		}

	}
}
