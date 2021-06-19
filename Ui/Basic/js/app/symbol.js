function getSymbols() 
{
    return get(symbolsApiEndpoint);
}

function getSymbolById(id)
{
    return get(symbolsApiEndpoint + "/" + id);
}

function createSymbol(newSymbol) 
{
    return create(symbolsApiEndpoint,newSymbol);
}

function updateSymbol(symbol)
{
    return update(symbolsApiEndpoint,symbol);
}

function removeSymbol(id)
{
    return remove(symbolsApiEndpoint + "/" + id);
}

// DOM Functions
function UpdateDataGrid(symbols)
{
    if (symbols != null) 
    {
        // Initialize table
        dataGrid = $('#symbol_table').DataTable({
            "columns": [
                {"data": "id"},
                {"data": "name"},
                {"data": "code"},
                {"data": "price"},
                {
                    "data": null,
                    "sortable": false,
                    "mRender": function(data, type, full) {
                        var editElement = '<button data-toggle="modal" data-target="#symbolModal" class="editRow btn btn-info btn-sm mr-1" data-id="' + data.id + '">Edit</button>';
                        var deleteElement = '<button class="deleteRow btn btn-info btn-sm mr-1" data-id="' + data.id + '">Delete</button>';
                        var infoElement = '<button class="infoRow btn btn-info btn-sm mr-1" data-id="' + data.id + '">Info</button>';
                        return editElement + deleteElement + infoElement;
                    }
                }
            ],
            "pageLength": 25
        });
        
        dataGrid.rows.add(symbols);
        dataGrid.draw();    
    }
}

$(document).on("click",".editRow", function () 
{
    // Get id from the click item
    var id = $(this).attr('data-id');

    // Get symbol object from the symbols variable
    var promise = getSymbolById(id);
    promise.done(data => 
    {
        // Set pop-up modal content
        //document.querySelector(dataModelKey).innerHTML = symbolModel;

        if (viewModel == null) {
            viewModel = ko.mapping.fromJS(data);
        }

        // Popup-Modal with all symbol info
        //viewModel.name(data.name);
        ko.mapping.fromJS(data, viewModel);
        console.log(data);
        console.log(JSON.stringify(data));


        // If save requested, call edit endpoint and reload the grid 
    });

    
       
 });

 $(document).on("click",".deleteRow", function () 
{
    // Get id from the click item
    var id = $(this).attr('data-id');
    

    getSymbols();
 });

 $(document).on("click",".infoRow", function () 
{
    // Get id from the click item
    var id = $(this).attr('data-id');
    

    getSymbols();
 });

 function Symbol()
 {
    this.name = ko.observable('name');
    this.code = ko.observable('code');
    this.price = ko.observable('price');
 }

function loadSymbolsUi() 
{
    // Fetch table template
    fetch(symbolTemplatePath)
    .then(response => {
        return response.text()
    })
    .then(data => {
        // Set table template
        document.querySelector(dynamicpagecontentKey).innerHTML = data;

        // Fetch symbols and Update UI
        var promise = getSymbols();
        promise.done(data => {
            UpdateDataGrid(data);
        });

    });

    // Fetch symbol model
    fetch(symbolModelTemplatePath)
    .then(response => 
    {
        return response.text()
    })
    .then(data => 
    {
        symbolModel = data;
        
        // Set pop-up modal content
        document.querySelector(dataModelKey).innerHTML = symbolModel;

        // viewModel = {
        //     name : ko.observable('name'),
        //     code : ko.observable('code'),
        //     price : ko.observable('price')
        // };

        //viewModel = ko.mapping.fromJS(symbol);
    });
}


// Assign constants
var symbolTemplatePath = "templates/symbol/symbol.html";
var symbolModelTemplatePath = "templates/symbol/symbolModel.html";
var symbolsApiEndpoint = "Symbol";
var dataGrid = {};
var symbolModel = null;
var isModelBindingDone = false;
var viewModel = null;
var symbol = {
    name : '',
    code : '',
    price : 0
}

loadSymbolsUi();

