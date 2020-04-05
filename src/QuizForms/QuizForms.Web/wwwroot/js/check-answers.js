(function () {
    var scoreSelectors = document.querySelectorAll('.score-selector');

    for (var i = 0; i < scoreSelectors.length; i++) {
        let scoreSelector = scoreSelectors[i];
        let options = scoreSelector.querySelectorAll('.score-option');
        let inputElement = scoreSelector.querySelector('input');

        for (var j = 0; j < options.length; j++) {
            let option = options[j];

            option.addEventListener('click', function () {
                // Only do anything if the option is not selected yet
                if (!option.classList.contains('selected')) {
                    // Update the visual state of the selector by changing the selected classes
                    scoreSelector.querySelector('.selected').classList.remove('selected');
                    option.classList.add('selected');

                    // Update the value in the hidden input
                    inputElement.value = option.dataset.value;
                }
            });
        }
    }
})();


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
        
        var inputElements = form.querySelectorAll('.score-selector input');        
        var results = [];
        var empty = 0;

        for (var i = 0; i < inputElements.length; i++) {
            
            var name = inputElements[i].name;
            var value = inputElements[i].value;
           
            if (value.length === 0) {
                empty++;
            }
            else {
                results.push({
                    Key: name,
                    Value: +value
                });
            }
        }

        console.log('form data', results);

        // Ask for confirmation if some answers are still empty
        var confirmed = empty === 0 || confirm(empty + ' antwoorden hebben nog geen score. wil je toch doorgaan?');
        if (confirmed) {
            submitElement.classList.add('working');
            submitElement.innerText = 'Bezig met versturen...';   

            var model = {
                Scores: results
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
                    submitElement.innerText = 'Scores versturen';

                    if (xhr.status === 200) {
                        // OK                        
                        submitElement.classList.add('done');
                        submitElement.innerText = 'Verzonden';

                        // Notify the user
                        //alert('De scores zijn verzonden!');

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
        }
    });
})();