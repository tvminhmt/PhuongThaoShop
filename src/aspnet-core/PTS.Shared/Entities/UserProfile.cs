using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Shared.Core.Entities
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public int UserId { get; set; }
        public string UserProfileName { get; set; }
        public string UserProfileDesc { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string LinkFacebook { get; set; }
        public string LinkTwitter { get; set; }
        public string LinkWeb { get; set; }
        public string LinkZalo { get; set; }
        public Nullable<int> OneStarReviews { get; set; }
        public Nullable<int> TwoStarReviews { get; set; }
        public Nullable<int> ThreeStarReviews { get; set; }
        public Nullable<int> FourStarReviews { get; set; }
        public Nullable<int> FiveStarReviews { get; set; }
        public string UserCategories { get; set; }
        public short SiteId { get; set; }
        public Nullable<int> ArticleCounter { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        // update trang xosotailoc
        public string LinkPinterest { get; set; }
        public string LinkYoutube { get; set; }
        public string LinkLinkedIn { get; set; }

        // update trang bongda24h them truong chuc vu
        public string Position { get; set; }
        [NotMapped]
        public double Stars { get; set; }
        [NotMapped]
        public int Total { get; set; }
    }
}
