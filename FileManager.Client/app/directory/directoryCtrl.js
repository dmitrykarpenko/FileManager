(function () {
    "use strict";
    angular
        .module("directoryManagement")
        .controller("DirectoryCtrl",
                    ["directoryResource",
                     DirectoryCtrl]);

    function DirectoryCtrl(directoryResource) {
        var vm = this;

        directoryResource.query(function (data) {
            vm.directory = data;
        })
    }
}());
