document.addEventListener("DOMContentLoaded", function () {
    const counters = document.querySelectorAll("#success-stories h6");

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const target = entry.target;
                const targetValue = parseInt(target.innerText, 10);
                let currentValue = 0;

                const updateCounter = () => {
                    const increment = Math.ceil(targetValue / 100);
                    if (currentValue < targetValue) {
                        currentValue += increment;
                        if (currentValue > targetValue) currentValue = targetValue;
                        target.innerText = currentValue;
                        setTimeout(updateCounter, 20);
                    }
                };

                updateCounter();
                observer.unobserve(target);
            }
        });
    });

    counters.forEach(counter => {
        observer.observe(counter);
    });
});