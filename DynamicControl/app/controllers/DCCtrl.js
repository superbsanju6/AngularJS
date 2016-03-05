app.controller('reportDCController', function ($scope, reportService) {

    $scope.stdFormTemplate = {
        "fieldset": {
            "type": "fieldset",
            "label": "fieldset",
            "fields": {
                "text": {
                    "type": "text",
                    "label": "text",
                    "placeholder": "text"
                },
                "date": {
                    "type": "date",
                    "label": "date",
                    "placeholder": "date"
                },
                "datetime": {
                    "type": "datetime",
                    "label": "datetime",
                    "placeholder": "datetime"
                }
            },
            "select": {
                "type": "select",
                "label": "select",
                "empty": "nothing selected",
                "options": {
                    "first": {
                        "label": "first option"
                    },
                    "second": {
                        "label": "second option",
                        "group": "first group"
                    },
                    "third": {
                        "label": "third option",
                        "group": "second group"
                    },
                    "fourth": {
                        "label": "fourth option",
                        "group": "first group"
                    },
                    "fifth": {
                        "label": "fifth option"
                    },
                    "sixth": {
                        "label": "sixth option",
                        "group": "second group"
                    },
                    "seventh": {
                        "label": "seventh option"
                    },
                    "eighth": {
                        "label": "eighth option",
                        "group": "first group"
                    },
                    "ninth": {
                        "label": "ninth option",
                        "group": "second group"
                    },
                    "tenth": {
                        "label": "tenth option"
                    }
                }
            },
            "checklist": {
                "type": "checklist",
                "label": "checklist",
                "options": {
                    "first": {
                        "label": "first option"
                    },
                    "second": {
                        "label": "second option",
                        "isOn": "on",   //  If you use Angular versions 1.3.x and up, this needs to be changed to "'on'"...
                        "isOff": "off"  //  If you use Angular versions 1.3.x and up, this needs to be changed to "'off'"...
                    }
                }
            }

        }
    }
}
);