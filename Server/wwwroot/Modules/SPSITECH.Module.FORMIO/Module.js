/* Module Script */
var SPSITECH = SPSITECH || {};

SPSITECH.FORMIO = {

    renderForm: function (theFormJSON, submissionJSON) {
        console.log(document.readyState);
        var theForm2 = JSON.parse(theFormJSON.tableJSON);
        var submissionJSON2 = JSON.parse(submissionJSON);
        if (Formio) {
            if (SPSITECH.Items.gFormExists) return;
            Formio.createForm(document.getElementById('formio'), theForm2, submissionJSON2).then(function (form) {
                gFormExists = true;
                SPSITECH.Items.formReference = form;
                if (submissionJSON2 && submissionJSON2.data) {
                    form.submission = {
                        data: submissionJSON2.data
                    };
                }

                form.on('submit', function (submission) {
                    DotNet.invokeMethodAsync('SPSITECH.Module.FORMIO.Client.Oqtane', 'FormIOJSONUpdated_Insert', submission);
                    console.log('Submission was made!', submission);
                });

                // Everytime the form changes, this will fire.
                form.on('change', function (changed) {
                    console.log('Form was changed', changed);
                });
            });
        }
        else {
            console.log('Error - FORMIO does not exist!');
        }
    },

    renderFormBuilder: function (theFormJSON, theCustomJSON) {

        console.log("renderFormBuilder" + document.readyState);


        let theForm2;
        try {
            theForm2 = JSON.parse(theFormJSON);
        } catch (error) {
            console.error('Error parsing JSON:', error);
        }

        let theCustomJSON2;
        try {
            theCustomJSON2 = JSON.parse(theCustomJSON);
        } catch (error) {
            console.error('Error parsing JSON:', error);
        }



        if (Formio) {

            console.log("Creating FormIO" + document.getElementById('formio'));

            Formio.builder(document.getElementById('formio'), theForm2, {
                builder: theCustomJSON2
            }).then(function (instance) {
                builder = instance;

                var onForm = function (form) {
                    form.on('change', function () {
                        onsole.log("change");
                    });
                };

                var onBuild = function (build) {
                    console.log("formio onbuild");
  
                    DotNet.invokeMethodAsync('SPSITECH.Module.FORMIO.Client.Oqtane', 'FormIOJSONUpdated', instance.schema);
                };

                var onReady = function () {
                    console.log("formio onready called");
                    instance.on('change', onBuild);
                };

                instance.ready.then(onReady);
                console.log("formio init then");
            });
        }
        else {
            console.log('Error - FORMIO does not exist!');
        }
    },
   
    FormIODataAPI: function (url, component, triggerValue) {
        console.log('triggerValue: ' + triggerValue);
        if (triggerValue != "1") {  // 1 means we have already loaded the data once
            fetch(url).then(function (response) {
                response.json().then(function (dataRecords) {
                    console.log('dataRecords: ' + JSON.stringify(dataRecords));
                    const dataGrid = SPSITECH.FORMIO.formReference.getComponent(component);
                    dataGrid.setValue(dataRecords.map(function (dataRecord) {
                        return dataRecord;
                    }));
                });
            });
        }
    }


};