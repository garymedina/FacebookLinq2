using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Linq;
using System.Data.Linq.Mapping;
using Facebook;

namespace facebook.Tables
{
    /// <summary>
    /// http://developers.facebook.com/docs/reference/fql/comment/
    /// </summary>
    [Table(Name = "comment")]
    public class Comment
    {
        /// <summary>
        /// The external ID of the fb:comments being queried. You can specify more than one XID.
        /// 
        /// original type is: string
        /// Indexable
        /// </summary>
        [Column(Name = "xid" , IsPrimaryKey = true)]
        public Xid Xid { get; set; }

        /// <summary>
        /// The object_id of an object on Facebook. This can be a video, note, link, photo, or photo album. Note that for photos and albums, the object_id must be queried from the photo and album FQL tables. Note that in the photo and album tables, object_id is a different field from pid and aid. You must specify either an xid or an object_id.
        /// 
        /// original type is: string
        /// Indexable
        /// </summary>
        [Column(Name = "object_id" )]
        public ObjectId ObjectId { get; set; }

        /// <summary>
        /// The ID of the post in the stream.
        /// 
        /// original type is: string
        /// Indexable
        /// </summary>
        [Column(Name = "post_id" )]
        public PostId PostId { get; set; }

        /// <summary>
        /// The user submitting a comment.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "fromid" )]
        public Uid Fromid { get; set; }

        /// <summary>
        /// A Unix timestamp associated with the creation time of a comment.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "time" )]
        public DateTime? Time { get; set; }

        /// <summary>
        /// The text of a comment.
        /// 
        /// original type is: string
        /// </summary>
        [Column(Name = "text" )]
        public string Text { get; set; }

        /// <summary>
        /// A unique ID for a given XID for each comment.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "id" )]
        public CommentId Id { get; set; }

        /// <summary>
        /// The user name that a user entered when they posted a comment. In these cases, the fromid returned will be 0. Note that user names for valid users are not returned here.
        /// 
        /// original type is: string
        /// </summary>
        [Column(Name = "username" )]
        public string Username { get; set; }

        /// <summary>
        /// The target XID for Feed stories generated by the user; applications can retrieve comments made to that story by calling this XID.
        /// 
        /// original type is: string
        /// </summary>
        [Column(Name = "reply_xid" )]
        public Xid ReplyXid { get; set; }

        /// <summary>
        /// The object_id of this comment. This can be used for querying likes for this comment or replies to this comment if the comment came from the comments plugin.
        /// 
        /// original type is: string
        /// </summary>
        [Column(Name = "post_fbid" )]
        public ObjectId PostFbid { get; set; }

        /// <summary>
        /// The application id associated with this comment.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "app_id" )]
        public AppId AppId { get; set; }

        /// <summary>
        /// The number of likes for this comment.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "likes" )]
        public long? Likes { get; set; }

        /// <summary>
        /// The replies to this comment.
        /// 
        /// original type is: comments
        /// </summary>
        [Column(Name = "comments" )]
        public object Comments { get; set; }

        /// <summary>
        /// If the current user is able to like this comment.
        /// 
        /// original type is: boolean
        /// </summary>
        [Column(Name = "can_like" )]
        public bool? CanLike { get; set; }

        /// <summary>
        /// If the current user liked this comment.
        /// 
        /// original type is: boolean
        /// </summary>
        [Column(Name = "user_likes" )]
        public bool? UserLikes { get; set; }

        /// <summary>
        /// Array of mention objects for each object tagged in the comment.
        /// 
        /// original type is: array
        /// </summary>
        [Column(Name = "text_tags" )]
        public Tags TextTags { get; set; }

        /// <summary>
        /// If this comment is private.
        /// 
        /// original type is: boolean
        /// </summary>
        [Column(Name = "is_private" )]
        public bool? IsPrivate { get; set; }

    }
}
