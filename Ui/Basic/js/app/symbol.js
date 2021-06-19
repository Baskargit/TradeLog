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
    promise.done(symbol => 
    {
        // Bind viewModel data to view
        var elementToBind = document.getElementById(symbolModelId);
        var existingContext = ko.contextFor(elementToBind);
        if (existingContext && ko.isObservable(existingContext.$rawData)) 
        {
            // update observable with new view model
            existingContext.$rawData(symbol);
        } 
        else 
        {
            // initialize with observable view model
            ko.applyBindings(ko.observable(symbol), elementToBind);
        }
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

 $(document).on("click",".closeModal", function () 
 {
    //ko.cleanNode(document.getElementById(symbolModelId));
    //$("#" + symbolModelId).remove();
    //console.log("removed item");
});


function loadSymbolsUi() 
{
    // Fetch table template
    fetch(symbolTemplatePath)
    .then(response => {
        return response.text()
    })
    .then(data => 
    {
        // assign symbol template
        document.querySelector(dynamicpagecontentKey).innerHTML = data;

        // Fetch symbols and Update UI
        var promise = getSymbols();
        promise.done(symbols => {
            UpdateDataGrid(symbols);
        });
    });

}

// Assign constants
var symbolTemplatePath = "templates/symbol/symbol.template.html";
var symbolsApiEndpoint = "Symbol";
var dataGrid = {};
var symbolModelId = "symbolModal";

loadSymbolsUi();

