/// <reference path="F:\Mahesh_New\Articles\MVC\Infra\A8_SPA_New_MVC_Using_Angular\MVC_Using_Angular\Scripts/angular.min.js" />

/// <reference path="F:\Mahesh_New\Articles\MVC\Infra\A8_SPA_New_MVC_Using_Angular\MVC_Using_Angular\MyScripts/Module.js" />

app.service("SinglePageCRUDService", function ($http) {

    this.BaseUrl = "http://localhost:33388/";
    //Function to create new Employee
    this.Login = function (Login) {
        var request = $http({
            method: "post",
            url: this.BaseUrl+"api/Login/CheckLogin",
            data: Login
        });
        return request;
    };
    //Function to create new Employee
    this.GetSlots = function (userId) {
        
        return $http.get(this.BaseUrl + "/api/booking/slots?UserId=" + userId)
    };
    //Function to create Message
    this.SaveMessage = function (messageStatus) {
        var request = $http({
            method: "post",
            url: this.BaseUrl + "api/booking/SaveStatus",
            data: messageStatus
        });
        return request;
    };

    //Function to create new Employee
    this.GetSlotMessages = function (userId, BookingId, Status) {
       
        return $http.get(this.BaseUrl + "/api/booking/GetMessage?UserId=" + userId + "&BookingId=" + BookingId+"&Status="+Status)
    };

   
    

});








