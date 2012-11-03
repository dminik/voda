using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Zveen.MultiMenu
{
	public class Permissions : IPermissionProvider
	{
		public static readonly Permission ManageMultiMenu = new Permission { Description = "Manage multi menu", Name = "ManageMultiMenu" };

		public virtual Feature Feature { get; set; }

		public IEnumerable<Permission> GetPermissions()
		{
			return new[] { ManageMultiMenu };
		}

		public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
		{
			return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageMultiMenu}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {ManageMultiMenu}
                },
                new PermissionStereotype {
                    Name = "Moderator"
                },
                new PermissionStereotype {
                    Name = "Author"
                },
                new PermissionStereotype {
                    Name = "Contributor"
                },
            };
		}

	}
}


