/*######################*********************#############################
  
 Created by: Sanjeev Kumar
  http://www.vynsoft.com
  

 ######################*********************##############################*/

var app;

(function () {
    app = angular.module("vynsoftReportApp", [])
    .directive("datepicker", function () {
        return {
            restrict: "A",
            link: function (scope, el, attr) {
                el.datepicker();
            }
        };
    })
    ;
})

();

//app.config(function ($routeProvider) {
      

//    $routeProvider.otherwise({ redirectTo: "/" });

//});