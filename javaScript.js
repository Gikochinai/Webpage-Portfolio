// Page fade-in
window.addEventListener('load', () => {

    const body1 = document.querySelector('.body1');

    if (body1) {
        body1.classList.add('page-loaded');
    }

});


// Navigation link transition
document.querySelectorAll('a').forEach(link => {

    link.addEventListener('click', function (event) {

        event.preventDefault();

        const destination = this.href;
        const body1 = document.querySelector('.body1');

        if (body1) {
            body1.classList.remove('page-loaded');
            body1.classList.add('fade-out');
        }

        setTimeout(() => {
            window.location.href = destination;
        }, 500);

    });

});


$(document).ready(function () {

    $(".box1").click(function () {
        window.location.href = "GameDev.html";
    });

});