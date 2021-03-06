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
    /// https://developers.facebook.com/docs/reference/fql/connection
    /// </summary>
    [Table(Name = "connection")]
    public class Connection
    {
        /// <summary>
        /// Indicates whether the source is connected to the target Page (for example, if the source is a supporter or fan). Will be false if the user has hidden the friend or target's news feed.
        /// 
        /// original type is: bool
        /// </summary>
        [Column(Name = "is_following" , IsPrimaryKey = true)]
        public bool? IsFollowing { get; set; }

        /// <summary>
        /// The ID of the user, the source of the connection.
        /// 
        /// original type is: id
        /// </summary>
        [Column(Name = "source_id" )]
        public Uid SourceId { get; set; }

        /// <summary>
        /// The target(s) of the connection. The ID of the friend or Facebook Page with whom the specified user is either a friend or fan/supporter.
        /// 
        /// original type is: id
        /// </summary>
        [Column(Name = "target_id" )]
        public ConnectionTargetId TargetId { get; set; }

        /// <summary>
        /// Indicates whether the target is a user or a page.
        /// 
        /// original type is: string
        /// </summary>
        [Column(Name = "target_type" )]
        public string TargetType { get; set; }

    }
}
