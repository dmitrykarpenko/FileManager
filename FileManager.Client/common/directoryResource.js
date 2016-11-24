(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("directoryResource",
                 ["$resource",
                  "appSettings",
                  directoryResource])

    function directoryResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/directory/:id",
                         { },
                         { 'query': { isArray: false } });
    }
}());

