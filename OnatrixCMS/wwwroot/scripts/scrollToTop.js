function scrollToTop() {
    const scrollStep = -window.scrollY / 25; 
    const scrollInterval = setInterval(function(){
        if (window.scrollY != 0) {
            window.scrollBy(0, scrollStep);
        } else {
            clearInterval(scrollInterval);
        }
    }, 15); 
}

window.onscroll = function() {
    const scrollBtn = document.getElementById("scrollToTopBtn");
    if (document.body.scrollTop > 520 || document.documentElement.scrollTop > 520) {
        scrollBtn.style.display = "block";
        scrollBtn.classList.add("show");
    } else {
        scrollBtn.style.display = "none";
        scrollBtn.classList.remove("show");
    }
};

document.getElementById("scrollToTopBtn").addEventListener("click", scrollToTop);