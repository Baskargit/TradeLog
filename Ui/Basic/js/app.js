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

// Global helper functions
function toggleButton(buttonSelector) 
{
    var button = $(buttonSelector);

    if (button.length > 0)
    {
        var attr = $(button[0]).prop('disabled');

        if (typeof attr !== 'undefined' && attr !== false)
        {
            $(button[0]).prop('disabled',false);
        }
        else
        {
            $(button[0]).prop('disabled',true);
        }
    }
}

function closeModal(modalSelector)
{
    if ($(modalSelector).length > 0)
    {
        $(modalSelector).modal('hide');    
    }
}

var host = "http://localhost:5000/";
var dynamicpagecontentKey = "dynamicpagecontent";

loadHeader();