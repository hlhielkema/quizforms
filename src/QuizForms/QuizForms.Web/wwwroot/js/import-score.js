// Form submit logic
(function () {
    // Get the submit button element     
    var submitElement = document.querySelector('form .submit');

    // Bind 
    submitElement.addEventListener('click', function () {
        // Ignore when active
        if (submitElement.classList.contains('working') || submitElement.classList.contains('done')) {
            return;
        }

        // Get the form element
        var form = document.querySelector('form');

        var redirectUrl = form.dataset.redirectAfterSubmit;

        var rows = form.querySelectorAll('.import-score-row');
        var results = [];
        
        for (var i = 0; i < rows.length; i++) {

            var rowElement = rows[i];
            var selectElement = rowElement.querySelector('select');
            var id = rowElement.dataset.id;            
            var value = selectElement.options[selectElement.selectedIndex].value;
            if (value.length > 0) {
                results.push({
                    Key: id,
                    Value: value
                });
            }
        }

        console.log('form data', results);

        submitElement.classList.add('working');
        submitElement.innerText = 'Bezig met versturen...';

        var model = {
            Mappings: results
        };

        var xhr = new XMLHttpRequest();
        var url = "";
        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.setRequestHeader("X-CSRF-TOKEN", document.querySelector('form input[name="AntiforgeryToken"]').value);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {

                // Reset submit button state
                submitElement.classList.remove('working');
                submitElement.innerText = 'Score importeren';

                if (xhr.status === 200) {
                    // OK                        
                    submitElement.classList.add('done');
                    submitElement.innerText = 'Verzonden';
                
                    // Redirect to the overview
                    document.location = redirectUrl;
                }
                else if (xhr.status === 400) {
                    alert('Bad request: ' + xhr.responseText);
                }
                else if (xhr.status === 500) {
                    alert('Server error: ' + xhr.responseText);
                }
                else {
                    alert('Error: ' + xhr.responseText);
                }
            }
        };
        var data = JSON.stringify(model);
        xhr.send(data);
    });
})();