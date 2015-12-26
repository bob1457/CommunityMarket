var userViewModel;

function User(userId, userFirstName, userLastName, userName, active, joinDate) {

    var self = this;

    self.Id = ko.observable(userId);
    self.FirstName = ko.observable(userFirstName);
    self.LastName = ko.observable(userLastName);
    self.UserName = ko.observable(userName);
    self.EmailConfirmed = ko.observable(active);
    self.CreateDate = ko.observable(joinDate);

};

//List user
function UserList() {

    var self = this;

    self.users = ko.observableArray([]);

    self.getUsers = function () {

        self.users.removeAll();

        var url = '/UsersAdmin/Listusers';
        debugger;
        $.getJSON(url, function (data) {
            $.each(data, function (key, value) {

                

                self.users.push(new User(value.Id, value.UserProfile.FirstName, value.UserProfile.LastName, value.UserName, value.EmailConfirmed, value.UserProfile.CreateDate));
            });
        });

    };
}

userViewModel = {
    UserList: new UserList(),
    User: new User()
};

$(document).ready(function () {
    //
   
    ko.applyBindings(userViewModel);

    userViewModel.UserList.getUsers();
});