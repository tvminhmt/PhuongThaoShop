using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Core.Entities
{
    public class SeoInfo
    {
        public string Title;
        public string Description;
        public string Keywords;
        public string H1Tag;
        public string Canonical;
        public string PageId;
        public bool IsIndex = true;
        public bool IsFollow = true;
        public bool IsHaveAmp = false;
        public string SeoFooter;
        public string SeoHeader;

        public string SocialTitle;
        public string SocialDesc;
        public string SocialImage;

        public DateTime DatePublished;
        public DateTime DateModified;
        public UserProfile Author;
    }
}
