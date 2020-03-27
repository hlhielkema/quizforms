// TODO
// - Button loading indicator
// - Send to back-end
// - OK notification design.


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
        var results = {};
        var empty = 0;
        for (var i = 0; i < questionElements.length; i++) {
            // Get the input value
            var value = questionElements[i].value;

            // Add the value to the model
            results[questionElements[i].name] = value;

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

            // Test
            setTimeout(function () {
                submitElement.classList.remove('working');
                submitElement.classList.add('done');
                submitElement.innerText = 'Verzonden';
            }, 3000);


            // TODO: Send form data to the backend

            // Notify the user
            //alert('De antwoorden zijn verzonden!');

            // Redirect to the homepage
            //document.location = '/';
        }        
    });
}) ();

