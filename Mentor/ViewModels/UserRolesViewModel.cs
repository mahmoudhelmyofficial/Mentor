﻿namespace Mentor.ViewModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<RoleManagerViewModel> roleManager { get; set; }
    }
}
