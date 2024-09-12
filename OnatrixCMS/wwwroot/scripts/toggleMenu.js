const toggleMenu = () => {
    const menu = document.getElementById('menu');
    const barsIcon = document.getElementById('barsIcon');
    const crossIcon = document.getElementById('crossIcon');

    menu.classList.toggle('hide');
    barsIcon.classList.toggle('hide');
    crossIcon.classList.toggle('hide');

    document.body.classList.toggle('no-scroll');
}

const resetMenu = () => {
    const menu = document.getElementById('menu');
    const barsIcon = document.getElementById('barsIcon');
    const crossIcon = document.getElementById('crossIcon');

    menu.classList.add('hide');
    barsIcon.classList.remove('hide');
    crossIcon.classList.add('hide');
    document.body.classList.remove('no-scroll');
}

const checkScreenSize = () => {
    const menu = document.getElementById('menu');
    const barsIcon = document.getElementById('barsIcon');
    const crossIcon = document.getElementById('crossIcon');

    if (window.innerWidth >= 992) {
        menu.classList.add('hide');
        barsIcon.classList.remove('hide');
        crossIcon.classList.add('hide');
    } else {
        if (!menu.classList.contains('hide')) {
            barsIcon.classList.add('hide');
            crossIcon.classList.remove('hide');
        } else {
            barsIcon.classList.remove('hide');
            crossIcon.classList.add('hide');
        }
    }
};

window.addEventListener('resize', checkScreenSize);
window.addEventListener('load', resetMenu);

const menuLinks = document.querySelectorAll('.menu-link');
menuLinks.forEach(link => {
    link.addEventListener('click', () => {
        if (window.innerWidth < 992) {
            toggleMenu();
        }
    });
});

checkScreenSize();