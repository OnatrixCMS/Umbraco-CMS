document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const message = document.querySelector('.form-success-message');
    const errorMessage = document.querySelector('.form-input-error');
    const emailInput = form.querySelector("input[name='Email']"); 

    form.addEventListener("submit", async function (e) {
        e.preventDefault();

        const emailValue = emailInput.value.trim();

        // Checks if e-mail input is empty/valid
        if (!emailValue) {
            errorMessage.style.display = 'block';
            errorMessage.textContent = 'Please enter your email address.';
            return;
        } else if (!validateEmail(emailValue)) {
            errorMessage.style.display = 'block';
            errorMessage.textContent = 'Please enter a valid email address.';
            return; 
        }

        const formData = new FormData(form);

        try {
            const response = await fetch('ContactSurface/SubmitSmallForm', {
                method: 'POST',
                body: formData,
            });

            const result = await response.json();

            // Removes old messages
            message.style.display = 'none';
            clearAllErrors();

            if (result.success) {
                message.style.display = 'block';
                message.textContent = result.message || 'Form submitted successfully!';
                form.reset();
            } else {
                errorMessage.style.display = 'block';
                errorMessage.textContent = result.error || 'An error occurred. Please check your input!';
            }
        } catch (error) {
            console.error('Error sending form data:', error);
        }
    });

    // Regex-validation for email
    function validateEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(String(email).toLowerCase());
    }

    // Hides messages 
    function clearAllErrors() {
        const errorElements = document.querySelectorAll('.form-input-error');
        errorElements.forEach(el => {
            el.style.display = 'none';
        });
    }
});