mergeInto(LibraryManager.library, {
    CallJS: function(jsCode) {
        eval(jsCode);
    }
});