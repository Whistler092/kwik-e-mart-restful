namespace Kwikemark.Entities
{
    using System;
    using Microsoft.AspNetCore.Identity;
    public class Role: IdentityRole
    {
        public string Discriminator { get; set; }
    }
}
