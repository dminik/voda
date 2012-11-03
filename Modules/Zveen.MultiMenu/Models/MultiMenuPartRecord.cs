using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement.Records;
using System.ComponentModel.DataAnnotations;

namespace Zveen.MultiMenu.Models
{
    public class MultiMenuPartRecord : ContentPartRecord
    {
        [Required]
        public virtual String Includes { get; set; }
        [Required]
        public virtual String Script { get; set; }
        [Required]
        public virtual String Style { get; set; }
        [Required]
        public virtual String BeforeHtml { get; set; }
        [Required]
        public virtual String ElementHtml { get; set; }
		[Required]
        public virtual String ElementHtmlLevel2 { get; set; }
		[Required]
        public virtual String ElementHtmlLevel3 { get; set; }
        [Required]
        public virtual String AfterHtml { get; set; }

    }
}
