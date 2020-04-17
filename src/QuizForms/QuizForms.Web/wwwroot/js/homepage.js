(function () {
    var checkInterval = 3000; // 3s

    // Prevent loading the script a second time
    if (window.last_changed_loaded === true) {
        return;
    }
    window.last_changed_loaded = true;

    // Simple get request
    function getRequest(url, callback) {
        var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function () {
            // Wait for a completed request
            if (xhr.readyState === 4) {
                callback(xhr.status, xhr);
            }
        };
        xhr.open("GET", url, true);
        xhr.send(null);
    }

    // Read the current timestamp
    var current = document.querySelector('#quiz-form-list').dataset.timestamp;

    // Reload the page in the background
    function reloadInBackground() {
        getRequest('/', function (status, xhr) {      
            // Update the content of the page
            document.open();
            document.write(xhr.response);
            document.close();
        });
    }

    // Check for changes in the timestamp every 3s
    var ready = true;
    setInterval(function () {
        if (ready) {
            ready = false;
            getRequest('/last-changed', function (status, xhr) {
                if (status === 200) {
                    // Compare the timestamp of the page and the timestamp of the server
                    if (current !== xhr.response) {
                        // Update current
                        current = xhr.response;

                        // Reload the page in the background
                        reloadInBackground();
                    }
                }

                ready = true;
            });
        }
    }, checkInterval);
})();