
window.addEventListener('load', () => {

    const body1 = document.querySelector('.body1');

    if (body1) {
        body1.classList.add('page-loaded');
    }

});


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

$(document).ready(function () {

    $(".box2").click(function () {
        window.location.href = "WindowsForm.html";
    });

});

$(document).ready(function () {

    $(".box3").click(function () {
        window.location.href = "DataStructures&Algorithms.html";
    });

});

$(document).ready(function() {
    $("#btnDownload").click(function() {
        window.location.href = "https://mega.nz/folder/0ZNVHSqD#S-vMVD6tQe4Q_KVpOdga5g";
    });
});


const codeFiles = document.querySelectorAll(".codeFile");
const sourceCode = document.getElementById("sourceCode");
const currentFileName = document.getElementById("currentFileName");

async function loadCode(fileName) {
    try {
        const response = await fetch(`code/${fileName}`);

        if (!response.ok) {
            throw new Error(`Could not load ${fileName}`);
        }

        const code = await response.text();

        sourceCode.textContent = code;
        currentFileName.textContent = fileName;

    } catch (error) {
        sourceCode.textContent = "Unable to load source code.";
        console.error(error);
    }
}

codeFiles.forEach(button => {
    button.addEventListener("click", () => {

        codeFiles.forEach(btn => btn.classList.remove("active"));
        button.classList.add("active");

        loadCode(button.dataset.file);
    });
});

if (window.location.pathname.endsWith("WindowsForm.html")) {
    loadCode("Form1.cs");
}
else if (window.location.pathname.endsWith("DataStructures&Algorithms.html")) {
    loadCode("SortingAlgorithm.cs");
}

