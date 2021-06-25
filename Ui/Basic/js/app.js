function loadHeader() 
{
    fetch("templates/generic/header.html")
    .then(response => 
    {
        return response.text()
    })
    .then(data => 
    {
        // Set Sidebar menu
        $(document).ready(function () {
            $("header").replaceWith(data);
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

function initializeNotificationDefaults() 
{
    toastr.options = {
        "closeButton": true,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "2000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
}

function initializePromptDefaults()
{
    alertify.defaults['theme'].ok = 'ajs-ok button alert';
    alertify.defaults['theme'].cancel = 'ajs-cancel button primary';
}

// Global DOM Helper Functions
function UpdateView(data,elementSelector) 
{
    if (elementSelector)
    {
        // Bind viewModel data to view
        var elementToBind = $(elementSelector);
        var existingContext = ko.contextFor(elementToBind[0]);
        if (existingContext && ko.isObservable(existingContext.$rawData)) 
        {
            // update observable with new view model
            existingContext.$rawData(data);
        } 
        else 
        {
            // initialize with new observable view model
            ko.applyBindings(ko.observable(data), elementToBind[0]);
        }
    }
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

function createNotification(message,type=0,title="") 
{
    switch (type) 
    {
        case NOTIFICATION_TYPE.SUCCESS:
            toastr.success(message, title ? title : 'Success');
            break;
        case NOTIFICATION_TYPE.ERROR:
            toastr.error(message, title ? title : 'Error');
            break;
        case NOTIFICATION_TYPE.INFO:
            toastr.info(message, title ? title : 'Info');
            break;
        case NOTIFICATION_TYPE.WARNING:
            toastr.warning(message, title ? title : 'Warning');
            break;
        default:
            break;
    }
}

function createDeletePrompt(title,message,onOkHandler,onCancelHandler)
{
    var prompt = alertify.confirm();

    prompt.set({
        transition:'zoom',
        movable: false,
        resizable: false,
        closable:true,
        pinnable:false,
        pinned:false,
        labels: {
            ok:'Yes', cancel:'No'
        },
        title: title,
        message: message,
        onok: onOkHandler,
        oncancel: onCancelHandler
    });

    prompt.show();
}

function createModalPrompt(title,modalContent,onSaveHandler,onCancelHandler)
{
    var prompt = alertify.confirm();

    prompt.set({
        transition:'zoom',
        overflow: false,
        movable: false,
        resizable: false,
        closable:true,
        pinnable:false,
        pinned:false,
        labels: {
            ok:'Save', cancel:'Cancel'
        },
        title: title,
        message: modalContent,
        onok: onSaveHandler,
        oncancel: onCancelHandler
    });

    prompt.show();
}

function activeSideMenu(id)
{
    var element = $("ul#side-menu #" + id);

    if (element.length > 0)
    {
        element.addClass("active");
    } 
}


// Assign constants
const NOTIFICATION_TYPE = Object.freeze({"SUCCESS" : 0, "ERROR" : 1, "WARNING" : 2, "INFO" : 3});
const DATA_OPERATION = Object.freeze({ "CREATE" : 0, "READ" : 1, "UPDATE" : 2, "DELETE" : 3 });
const host = "http://localhost:5000/";
const dynamicpagecontentKey = "dynamicpagecontent";

// Set scrollbar color
$(document).ready(function () {
    $("html").attr('class','scrollbar-type-1 sb-cyan');
    loadHeader();
    initializeNotificationDefaults();
    initializePromptDefaults();
});
