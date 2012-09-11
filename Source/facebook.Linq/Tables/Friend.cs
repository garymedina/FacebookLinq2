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
    /// http://developers.facebook.com/docs/reference/fql/friend/
    /// </summary>
    [Table(Name = "friend")]
    public class Friend
    {
        /// <summary>
        /// The user ID of the first user in a particular friendship link.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "uid1" , IsPrimaryKey = true)]
        public Uid Uid1 { get; set; }

        /// <summary>
        /// The user ID of the second user in a particular friendship link.
        /// 
        /// original type is: int
        /// </summary>
        [Column(Name = "uid2" )]
        public Uid Uid2 { get; set; }

    }
}
