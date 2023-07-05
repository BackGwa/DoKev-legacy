
const docs = document.getElementById("docs");

function switch_page(root, content) {
    docs.src = `./pages/${root}/${content}.html`;
}

switch_page('started', 'introduction');