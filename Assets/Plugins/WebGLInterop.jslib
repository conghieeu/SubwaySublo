mergeInto(LibraryManager.library, {
    SetLocalStorage: function(key, value) {
        localStorage.setItem(key, value);
    },
    GetLocalStorage: function(key) {
        return localStorage.getItem(key);
    }
});