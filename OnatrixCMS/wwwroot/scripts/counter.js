document.addEventListener("DOMContentLoaded", function () {
    const counters = document.querySelectorAll("#success-stories h6, #our-story h3");

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const target = entry.target;

                const originalText = target.innerText;
                const numberPart = originalText.replace(/[^0-9]/g, '');
                const formatPart = originalText.replace(/[0-9]/g, '');
                const targetValue = parseInt(numberPart, 10);
                
                if (isNaN(targetValue)) {
                    return;
                }

                let currentValue = 0;

                const updateCounter = () => {
                    const increment = Math.ceil(targetValue / 100);
                    if (currentValue < targetValue) {
                        currentValue += increment;
                        if (currentValue > targetValue) currentValue = targetValue;
                        
                        // Ser till att komma stannar i rätt läge
                        const formattedValue = currentValue.toLocaleString('en-US');
                        // Lägger till + efter siffrorna
                        target.innerText = formattedValue + formatPart;

                        setTimeout(updateCounter, 20);
                    } else {
                        // När uppräkningen är klar, sätter det slutliga värdet
                        const finalValue = targetValue.toLocaleString('en-US');
                        target.innerText = finalValue + formatPart;
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