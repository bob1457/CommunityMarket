var roleViewModel;

//Add role
function Role(roleId, roleName, roleDesc) {
    var self = this;


    self.Id = ko.observable(roleId);
    self.Name = ko.observable(roleName);
    self.Description = ko.observable(roleDesc);
};

//List role
function RoleList() {
    var self = this;

    self.roles = ko.observableArray([]);
    //debugger;
    self.getRoles = function () {

        self.roles.removeAll();

        var url = '/RolesAdmin/GetAllRoles';
        //debugger;
        // retrieve role list from server side and push each object to model's role list
        $.getJSON(url, function(data) {
            $.each(data, function(key, value) {
                self.roles.push(new Role(value.Id, value.Name, value.Description));
            });
        });

    };
};

roleViewModel = {
    RoleList: new RoleList(),
    Role: new Role()
};

$(document).ready(function () {
    //
    ko.applyBindings(roleViewModel);

    roleViewModel.RoleList.getRoles();
});