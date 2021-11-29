using System;
using System.Collections.Generic;

namespace Resource.Builder
{

    class ResourceProduct // this will be the createDto
    {
        public string TitleFromUser { get; set; }
        public int UserId { get; set; }
        public string TitleFromSource { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfReference { get; set; }
        public DateTime TimeOfPublication { get; set; }
        public string Url { get; set; }
        public string HostBaseUrl { get; set; }
        public bool IsOfficialDocumentation { get; set; }
        public int InitialRating { get; set; }
        public bool deprecated { get; set; }
        public DateTime LastCheckedForDeprecation { get; set; }
        public bool IsVideo { get; set; }
        public ICollection<string> TagsFoundInSource { get; set; }
    }
}