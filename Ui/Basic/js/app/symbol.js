loadScriptsUi();

function loadScriptsUi() 
{
    fetch("templates/symbol.html")
    .then(response => {
        return response.text()
    })
    .then(data => {
        document.querySelector("dynamicpagecontent").innerHTML = data;
    });
}