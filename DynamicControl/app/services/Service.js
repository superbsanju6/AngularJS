/// <reference path="../angular.js" />
/// <reference path="Module.js" />


app.service('reportService', function ($http) {


    //Create new record
    //this.post = function (WebReportParameter) {
    //    var request = $http({
    //        method: "post",
    //        url: "/api/WebReportParameterAPI",
    //        data: WebReportParameter
    //    });
    //    return request;
    //}

    //Get All WebReportParameter
    this.getWebReportParameter = function () {
        return $http.get("/api/WebParameter");
    }

    this.populateParameters = function (reportId) {
        return $http.get("/api/populateParameters/" + reportId);
    }

    this.getState = function (countryId) {
        return $http.get("/api/GetStates/" + countryId);
    }

    this.getCountry = function (customerId) {d
        return $http.get("/api/GetCountries/" + customerId);
    }

    this.ReportPageViewer = function (countryId)
    {
        return $http.get("/api/GenerateReport/" + countryId);
    }

    this.getCity = function(stateId)
    {
        return $http.get("/api/GetCities/" + stateId); 
    }

});