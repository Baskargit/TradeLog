// Api functions
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
    return create(symbolsApiEndpoint,GetSymbolModel(newSymbol));
}

function updateSymbol(symbol)
{
    return update((symbol != null) ? symbolsApiEndpoint + "/" + symbol.id : symbolsApiEndpoint,GetSymbolModel(symbol));
}

function removeSymbol(id)
{
    return remove(symbolsApiEndpoint + "/" + id);
}

// DOM Functions
function UpdateView(data) 
{
    // Bind viewModel data to view
    var elementToBind = document.getElementById(symbolModalId);
    var existingContext = ko.contextFor(elementToBind);
    if (existingContext && ko.isObservable(existingContext.$rawData)) 
    {
        // update observable with new view model
        existingContext.$rawData(data);
    } 
    else 
    {
        // initialize with new observable view model
        ko.applyBindings(ko.observable(data), elementToBind);
    }
}

function GetViewData() 
{
    var existingContext = ko.contextFor(document.getElementById(symbolModalId));
    if (existingContext && ko.isObservable(existingContext.$rawData)) 
    {
        return existingContext.$rawData();
    }

    return null;
}

function UpdateDataGrid(symbols)
{
    if (symbols != null) 
    {
        // Initialize table
        dataGrid = $('#symbol_table').DataTable({
            "columns": [
                {"data": "s.no",
                    render: function (data, type, row, meta) 
                    {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },
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
                    },
                    
                },
                {
                    "data": "id",
                    "visible": false
                },
            ],
            "pageLength": 25
        });
        
        dataGrid.rows.add(symbols);
        dataGrid.draw();    
    }
}

// Data Action functions
$(document).on("click",".editRow", function () 
{
    DATA_OPERATION = 'U';

    // Get id from the click item
    var id = $(this).attr('data-id');

    // Get symbol object from the symbols variable
    var promise = getSymbolById(id);
    promise.done(symbol => 
    {
        UpdateView(symbol);
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

 $(document).on("click","#createNewSymbol", function () 
 {
    DATA_OPERATION = 'C';
    UpdateView({ id:null, name: null, code: null, price: null, symbolTypeId: null });
});

$(document).on("click","#saveModal", function () 
{
    toggleButton('#saveModal');

    // Identify the action type
    if (DATA_OPERATION)
    {
        // Get data
        var viewData = GetViewData();

        var promise = (DATA_OPERATION == 'C') ? createSymbol(viewData) : updateSymbol(viewData);
        promise.done(x => 
        {
            if (DATA_OPERATION == 'C') {
                dataGrid.row.add(x);
            } else {
                // Get he parent row
                var tr = $('table tr .editRow[data-id="' + x.id + '"]');
                if (tr.length > 0) 
                {
                    // Update the parent row data by 'DataTable' api
                    var parentRow = tr[0].parentElement.parentElement;
                    dataGrid.row(parentRow).data(x).invalidate();
                }
            }

            // Update datatable ui
            dataGrid.draw();
        }).fail((responseObject) => 
        {
            console.log(responseObject);
        }).always(x => 
            {
            toggleButton('#saveModal');
            closeModal('#' + symbolModalId);
        });
    }
});

// Helper functions
function GetSymbolModel(symbolObject) 
{
    return (symbolObject != null)
    ?   { 
            id: ($.isNumeric(symbolObject.id)) ? parseInt(symbolObject.id) : 0, 
            name: symbolObject.name, 
            code: symbolObject.code, 
            price: ($.isNumeric(symbolObject.price)) ? parseFloat(symbolObject.price) : 0.0,
            symbolTypeId:  ($.isNumeric(symbolObject.symbolTypeId)) ? parseInt(symbolObject.symbolTypeId) : 1
        }
    : null;
}

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

// Tracker variables
var DATA_OPERATION = ''; // 'C' => Create an entity , 'U' => Update an entity

// Assign constants
var symbolTemplatePath = "templates/symbol/symbol.template.html";
var symbolsApiEndpoint = "Symbol";
var symbolModalId = "symbolModal";
var dataGrid = {};

loadSymbolsUi();

