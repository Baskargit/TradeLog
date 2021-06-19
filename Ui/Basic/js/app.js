function loadHeader() 
{
    fetch("templates/generic/header.html")
    .then(response => {
        return response.text()
    })
    .then(data => {
        document.querySelector("header").innerHTML = data;

        // Attach click handler
        $(document).ready(function () {
            $('#sidebarCollapse').on('click', function () {
                $('#sidebar').toggleClass('active');
                $(this).toggleClass('active');
            });
        });
    });
}

function get(url)
{
    return $.ajax({
        url: host + url,
        type: 'GET',
        contentType: 'application/json',
   });
}

function create(url,data) 
{
    return $.ajax({
        url: host + url,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data)
   });
}

function update(url,data) 
{
    return $.ajax({
        url: host + url,
        type: 'PUT',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(data)
   });
}

function remove(url)
{
    return $.ajax({
        url: host + url,
        type: 'DELETE',
        contentType: 'application/json',
   });
}

var host = "http://localhost:5000/";
var dynamicpagecontentKey = "dynamicpagecontent";
var dataModelKey = "dataModel";

loadHeader();
