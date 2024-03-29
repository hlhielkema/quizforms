﻿// TODO
// - OK notification design.

// Show a warning when the user tries to leave the page before submitting the answers
var showWarningWhenLeavingPage = true;
window.addEventListener("beforeunload", function (e) {
    if (showWarningWhenLeavingPage) {
        var confirmationMessage = 'Weet je zeker dat je de pagina wil verlaten? De antwoorden zijn nog niet verzonden.';
        (e || window.event).returnValue = confirmationMessage; //Gecko + IE
        return confirmationMessage; //Gecko + Webkit, Safari, Chrome etc.
    }
});

// Multiple-choice control
(function () {
    // Function to create an event handler for the option buttons of multiple-choice questions
    var createHandler = function (element) {
        return function () {
            // Get the question element
            var questionElement = element.parentNode.parentNode;

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
        };
    };

    // Get all option buttons
    var options = document.querySelectorAll('.question.question-multiple-choice .option');

    // Loop through the option buttons to add click event listeners
    for (var i = 0; i < options.length; i++) {
        var handler = createHandler(options[i]);
        options[i].addEventListener('click', handler);      
    }
})();

// Form submit logic
(function () {
    // Get the submit button element     
    var submitElement = document.querySelector('form .submit');

    // Try to restore the team name if the local storage is available    
    if (window.localStorage !== undefined) {        
        var team = window.localStorage.getItem('team');       
        if (team !== null) {            
            var teamInput = document.querySelector('#main input[name="Teamname"]');
            if (teamInput !== null) {
                teamInput.value = team;
            }
        }
    }

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
        var teamnameSet = false;
        var team = null;
        for (var i = 0; i < questionElements.length; i++) {
            // Get the input name and value
            var name = questionElements[i].name;
            var value = questionElements[i].value;

            if (name === 'Teamname' && value.length > 0) {
                team = value;
                teamnameSet = true;
            }

            results.push({
                Key: name,
                Value: value
            });

            // Count empty answers
            if (value.length === 0) {
                empty++;
            }
        }

        if (!teamnameSet) {
            alert('Vul een teamnaam in om de antwoorden te kunnen verzenden.');
            return;
        }

        // Put the team name in the local storage if available
        if (window.localStorage !== undefined) {
            localStorage.setItem('team', team);
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
                    submitElement.innerText = 'Antwoorden versturen';

                    if (xhr.status === 200) {
                        // OK                        
                        submitElement.classList.add('done');
                        submitElement.innerText = 'Verzonden';

                        // Notify the user
                        alert('De antwoorden zijn verzonden!');

                        // Disable warning when leaving the page
                        showWarningWhenLeavingPage = false;

                        // Redirect to the homepage
                        document.location = '/';
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
}) ();