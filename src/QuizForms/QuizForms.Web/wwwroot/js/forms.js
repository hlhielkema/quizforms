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