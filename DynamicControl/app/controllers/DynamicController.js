app.controller('FormGenerationController', function ($scope, reportService) {

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

    $scope.selectedLanguages = function () {
       // alert("Languages");
        $scope.selectedLanguageValues = $filter('filter')($scope.WebMaster.Controls[1].ControlType.Options
            , {checked: true }

            );
    }

    $scope.populateParameters = function () {
        var params = reportService.populateParameters($scope.ReportId_PK); //The MEthod Call from service

        params.then(function (pl) {
            $scope.WebMaster.Controls = pl.data;
        },
              function (errorPl) {
                  $log.error('failure loading Web Report Parameter', errorPl);
              });
    }

    $scope.GenerateReport = function () {
        //alert("Hello");
        var reportID = $scope.ReportId_PK;
        window.location.href = 'Report.aspx?rpt=' + reportID;

        //var customerID = $scope.WebMaster.Controls.SelectedValue.DisplayUniqueID;
        //var customerID = $scope.WebMaster.Controls.ControlType.DisplayUniqueID;
        //var languages = $scope.WebMaster.Controls.ControlType.checked;

        //var customerID = $scope.WebMaster.Customers.CustomerId;
        //var countryID = $scope.WebMaster.Countries.CountryId;
        //if (reportID == undefined && customerID == undefined && countryID == undefined) {
        //    alert("Select Report <br> Customer </br> and Country");
        //    return;
        //}
        //var stateID = $scope.WebMaster.States.StateId;
        //var cityID = $scope.WebMaster.Cities.CityId;
        //var language = $scope.WebMaster.Languages;


        //window.location.href = 'Report.aspx?rpt=' + reportID + '&cst=' + customerID + '&cnt=' + countryID + '&st=' + stateID + '&ct=' + cityID;


    }


    //$scope.fields = [
    //    {
    //              name: 'name',  
    //              title: 'Name',  
    //              required: true,  
    //              type: {
    //                  view: 'input'
    //              }
            
    //    }
    //];




  
    //// entity to edit  
    //$scope.entity = {  
    //    name: 'Max',  
    //    country: 2,  
    //    licenceAgreement: true,  
    //    description: 'I use AngularJS'  
    //};  
  
    //// fields description of entity  
    //$scope.fields = [  
    //  {  
    //      name: 'name',  
    //      title: 'Name',  
    //      required: true,  
    //      type: {  
    //          view: 'input'  
    //      }  
    //  },  
    //  {  
    //      name: 'country',  
    //      title: 'Country',  
    //      type: {  
    //          view: 'select',  
    //          options: [  
    //            {id: 0, name: 'USA'},  
    //            {id: 1, name: 'German'},  
    //            {id: 2, name: 'Russia'}  
    //          ]  
    //      }  
    //  },  
    //  {  
    //      name: 'licenceAgreement',  
    //      title: 'Licence Agreement',  
    //      type: {  
    //          view: 'checkbox'  
    //      }  
    //  },  
    //  {  
    //      name: 'description',  
    //      title: 'Description',  
    //      type: {  
    //          view: 'textarea'  
    //      }  
    //  }  
    //];  
  
});  