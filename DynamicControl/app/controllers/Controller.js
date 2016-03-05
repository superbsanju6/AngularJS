/// <reference path="../angular.js" />


/// <reference path="Module.js" />

//The controller is having 'reportService' dependency.
//This controller makes call to methods from the service
app.controller('reportController', function ($scope, reportService) {

    init();

    //Function to load all Web Report Parameter records
    function init() {
        var promiseGet = reportService.getWebReportParameter(); //The MEthod Call from service

        promiseGet.then(function (pl) {
            $scope.WebMaster = pl.data
        },
              function (errorPl) {
                  $log.error('failure loading Web Report Parameter', errorPl);
              });
    }

    $scope.customerChanged = function () {
        var countryGet = reportService.getCountry($scope.WebMaster.Customers.CustomerId);
        countryGet.then(function (rs)
        {
            var result = rs.data;
            $scope.WebMaster.Countries = result;
        },
        function (error)
        {
            console.log('failure loading countries', error);
        }
        );
    }


    $scope.countryChanged = function () {
        //alert($scope.WebMaster.Countries);
        var promiseGet = reportService.getState($scope.WebMaster.Countries.CountryId);

        promiseGet.then(function (pl) {
            var res = pl.data;
            $scope.WebMaster.States = res;
        },
            function (error) {
                console.log('failure loading States', error);
            });
    }

    $scope.stateChanged = function()
    {
        var promiseGet = reportService.getCity($scope.WebMaster.States.StateId);

        promiseGet.then(function (pl) {
            var res = pl.data;
            $scope.WebMaster.Cities = res;
        },
            function (error) {
                console.log('failure loading States', error);
            });
    }


    $scope.languageChange = function () {
        if ($scope.array.toString() !== $scope.array_.toString()) {
            return "Changed";
        } else {
            return "Not Changed";
        }
    };

    $scope.GenerateReport = function () {
        //alert("Hello");
        var reportID = $scope.ReportId_PK;
        var customerID = $scope.WebMaster.Customers.CustomerId;
        var countryID = $scope.WebMaster.Countries.CountryId;
        if (reportID == undefined && customerID == undefined && countryID == undefined) {
            alert("Select Report <br> Customer </br> and Country");
            return;
        }
        var stateID = $scope.WebMaster.States.StateId;
        var cityID = $scope.WebMaster.Cities.CityId;
        var language = $scope.WebMaster.Languages;

        
            window.location.href = 'Report.aspx?rpt=' + reportID + '&cst=' + customerID + '&cnt=' + countryID + '&st=' + stateID + '&ct=' + cityID;
       
       
    }
});