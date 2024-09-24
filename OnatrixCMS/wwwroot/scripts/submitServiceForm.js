document.addEventListener('DOMContentLoaded', function () {

    const serviceForm = document.getElementById('serviceForm');
    console.log('Service Form:', serviceForm);

    const message = document.querySelector('.serviceForm-success-message');

    const nameError = document.getElementById('error_Name');
    const emailError = document.getElementById('error_Email');
    const questionError = document.getElementById('error_Question');

    console.log(nameError, emailError, questionError);

    serviceForm.addEventListener('submit', async function (e) {
        e.preventDefault();

        console.log('Form submitted');

        const formData = new FormData(serviceForm);

        try {
            console.log('Sending form data to server');

            const response = await fetch('ContactSurface/SubmitServiceForm', {
                method: 'POST',
                body: formData
            });

            console.log('Response received from server', response);

            const result = await response.json();

            // Removes old messages
            message.style.display = 'none';
            clearAllErrors();

            if (result.success) {
                console.log('Form submitted successfully');

                message.style.display = 'block';
                message.style.backgroundColor = '#d4edda';
                message.style.color = '#155724';
                message.style.borderColor = '#c3e6cb';
                message.textContent = result.message || 'Form submitted successfully!';
                serviceForm.reset(); 
            } else {
                console.log('Form submission failed with validation errors.');

                message.style.display = 'block';
                message.style.backgroundColor = '#f8d7da'; 
                message.style.color = '#721c24'; 
                message.style.borderColor = '#721c24';
                message.textContent = result.error || 'An error occurred. Please check your input!';

                // Funtion to dynamically display error messages.
                displayErrors(result.errors);
            }
        } catch (error) {
            console.error('Error occurred during form submission', error);

            message.style.display = 'block';
            message.style.backgroundColor = '#FFCCCB';
            message.textContent = 'An unexpected error occurred. Please try again later.';
        }
    });

    // Function that hides messages
    function clearAllErrors() {
        const errorElements = document.querySelectorAll('.form-input-error');
        errorElements.forEach(el => {
            el.style.display = 'none';
        });
    }

    // Display messages depending on response from server. 
    function displayErrors(errors) {
        console.log('Displaying errors:', errors);

        for (let field in errors) {
            if (errors[field]) {
                const errorElement = document.getElementById(`error_${capitalizeFirstLetter(field)}`);
                if (errorElement) {
                    const errorMessage = errorElement.textContent.trim();

                    errorElement.style.display = 'block';
                    errorElement.textContent = errorMessage;
                }
            }
        }
    }


    // Function to apitalize first letter. 
    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
});