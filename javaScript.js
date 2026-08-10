window.addEventListener('load', () => {
    document.querySelector('.body1').classList.add('page-loaded');
});

document.querySelectorAll('a').forEach(link => {
    link.addEventListener('click', function (event) {
        event.preventDefault();

        const destination = this.href;
        const body1 = document.querySelector('.body1');

        body1.classList.remove('page-loaded');
        body1.classList.add('fade-out');

        setTimeout(() => {
            window.location.href = destination;
        }, 500);
    });
});