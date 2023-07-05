
const docs = document.getElementById("docs");
const item = document.querySelectorAll(".document-item");

function switch_page(root, content, index) {
    unactive();
    active(index);
    docs.src = `./pages/${root}/${content}.html`;
}

function active(index) {
    item[index - 1].classList.add("focus");
}

function unactive() {
    item.forEach(i => {
        i.classList.remove("focus");
    });
}

switch_page('started', 'introduction', 1);