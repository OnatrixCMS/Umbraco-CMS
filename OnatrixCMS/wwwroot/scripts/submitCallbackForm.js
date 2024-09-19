document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('contact-form');
    const message = document.querySelector('.form-success-message');

    const nameError = document.getElementById('error_Name');
    const emailError = document.getElementById('error_Email');
    const phoneError = document.getElementById('error_Phone');

    console.log(nameError, emailError, phoneError);

    if (!message || !form) {
        console.error('Message or form element not found');
        return;
    }

    form.addEventListener('submit', async function (e) {
        e.preventDefault();

        console.log('Form submitted');

        const formData = new FormData(form);

        try {
            console.log('Sending form data to server');

            const response = await fetch('ContactSurface/SubmitForm', {
                method: 'POST',
                body: formData
            });

            console.log('Response received from server', response);

            const result = await response.json();

            // Tar bort gamla meddelanden
            message.style.display = 'none';
            clearAllErrors();

            if (result.success) {
                console.log('Form submitted successfully');

                message.style.display = 'block';
                message.style.backgroundColor = '#90EE90';
                message.textContent = result.message || 'Form submitted successfully!';
                form.reset();
            } else {
                console.log('Form submission failed with validation errors.');

                message.style.display = 'block';
                message.textContent = result.error || 'An error occurred. Please check your input!';

                // Function som dynamiskt visar errors
                displayErrors(result.errors);
            }
        } catch (error) {
            console.error('Error occurred during form submission', error);

            message.style.display = 'block';
            message.style.backgroundColor = '#FFCCCB';
            message.textContent = 'An unexpected error occurred. Please try again later.';
        }
    });

    // Function som döljer meddelanden. 
    function clearAllErrors() {
        const errorElements = document.querySelectorAll('.form-input-error');
        errorElements.forEach(el => {
            el.style.display = 'none';

        });
    }

    // Function som hämtar och visar de dynamiska meddelandena från blocklisten
    function displayErrors(errors) {
        console.log('Displaying errors:', errors);

        for (let field in errors) {
            if (errors[field]) {
                // Bygger upp ID:t baserat på fältets namn, sätter stor bokstav på första.
                const fieldId = `error_${capitalizeFirstLetter(field)}`;
                console.log(`Looking for error element with ID: ${fieldId}`);

                const errorElement = document.getElementById(fieldId);

                if (errorElement) {
                    // Kontrollerar och loggar det aktuella felmeddelandet
                    const errorMessage = errorElement.textContent.trim(); // Använder textContect för att komma åt innehållet i diven
                    console.log(`Error element found for field: ${field}`);
                    console.log(`Error message in element: ${errorMessage}`); 

                    errorElement.style.display = 'block';
                    errorElement.textContent = errorMessage; 
                } else {
                    console.error(`Error element not found for field: ${field}`);
                }
            }
        }
    }

    // Ser till att första bokstaven är stor.
    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
});