using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;
using System.ComponentModel.DataAnnotations;

namespace Zveen.MultiMenu.Models
{
	public class MultiMenuItemPartRecord : ContentPartRecord
	{
        [Required]
        public virtual String Position { get; set; }
        [Required]
        public virtual String Url { get; set; }
        [Required]
        public virtual String Title { get; set; }
	}
}