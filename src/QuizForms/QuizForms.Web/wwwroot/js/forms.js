// TODO
// - Button loading indicator
// - Send to back-end
// - OK notification design.
// - Notification when leaving the page

// Multiple-choice control
(function () {
    // Get all option buttons
    var options = document.querySelectorAll('.question.question-multiple-choice .option');

    // Loop through the option buttons to add click event listeners
    for (var i = 0; i < options.length; i++) {
        options[i].addEventListener('click', function (e) {            
            // Try to find the question element
            var questionElement = null;
            for (var j = 0; j < e.path.length; j++) {
                if (e.path[j].classList.contains('question')) {
                    questionElement = e.path[j];
                    break;
                }
            }

            // Update the hidden form value
            var inputElement = questionElement.querySelector('input');
            inputElement.value = this.dataset.value;

            // Remove the "selected" class from all other options
            var questionOptionElements = questionElement.querySelectorAll('.option');
            for (var x = 0; x < questionOptionElements.length; x++) {
                questionOptionElements[x].classList.remove('selected');
            }

            //  Add the "selected" class
            this.classList.add('selected');
        });
    }
})();

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

        // Query the question elements
        var questionElements = form.querySelectorAll('.question input');

        // Collect the input values from the form elements
        var results = [];
        var empty = 0;
        for (var i = 0; i < questionElements.length; i++) {
            // Get the input name and value
            var name = questionElements[i].name;
            var value = questionElements[i].value;

            results.push({
                Key: name,
                Value: value
            });

            // Count empty answers
            if (value.length === 0) {
                empty++;
            }
        }

        // Ask for confirmation if some answers are still empty
        var confirmed = empty === 0 || confirm(empty + ' antwoorden zijn nog leeg. Weet je zeker dat je de antwoorden wil versturen?');
        if (confirmed) {
            submitElement.classList.add('working');
            submitElement.innerText = 'Bezig met versturen...';

            console.log('form data', results);

            var model = {                
                Answers: results                         
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

                    if (xhr.status === 200) {
                        // OK                        
                        submitElement.classList.add('done');
                        submitElement.innerText = 'Verzonden';

                        // Notify the user
                        alert('De antwoorden zijn verzonden!');

                        // Redirect to the homepage
                        document.location = '/';
                    }
                    else if (xhr.status === 400) {
                        alert('Bad request: ' + xhr.responseText);
                    }
                    else if (xhr.status === 500) {
                        alert('Server error: ' + xhr.responseText);
                    }
                }
            };
            var data = JSON.stringify(model);
            xhr.send(data);
        }        
    });
}) ();

