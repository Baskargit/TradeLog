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

function getAllSymbolTypes() 
{
    get(symbolTypeApiEndpoint).done(data => { symbolTypes = data; });
}

function getSymbolTypeById(symbolTypeId)
{
    if (symbolTypeId && symbolTypes && symbolTypes.length > 0)
    {
        var symbolType = symbolTypes.filter(function(x) 
        { 
            return x.id == symbolTypeId;
        });

        return symbolType;
    }
}

// DOM Functions
function GetViewData() 
{
    var element = $(VIEW_MODEL_CONTAINER_SELECTOR);

    if (element && element.length > 0) 
    {
        var existingContext = ko.contextFor(element[0]);
        if (existingContext && ko.isObservable(existingContext.$rawData)) 
        {
            return existingContext.$rawData();
        }
    }
    
    return null;
}

function UpdateDataGrid(symbols)
{
    if (symbols != null) 
    {
        // Initialize table
        dataTable = $('#symbol_table').DataTable({
            "columns": [
                {
                    "data": "s.no",
                    "render": function (data, type, row, meta) 
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
                        var editElement = '<button class="editRow button" data-id="' + data.id + '"><i class="fa fa-info-circle fa-lg" aria-hidden="true" style="color:#1d63bf;"></i></button>';
                        var deleteElement = '<button class="deleteRow button" data-id="' + data.id + '"><i class="fa fa-trash" aria-hidden="true" style="color:#b52121;"></i></button>';
                        return editElement + deleteElement;
                    },
                    
                },
                {
                    "data": "id",
                    "visible": false
                },
            ],
            "pageLength": 10
        });
        
        dataTable.rows.add(symbols);
        dataTable.draw();    
    }
}

// Data Action functions
$(document).on("click",".editRow", function () 
{
    DATA_OP = DATA_OPERATION.UPDATE;

    // Get id from the click item
    var id = $(this).attr('data-id');

    createModalPrompt("Update Symbol",$("#" + symbolModalId).html(),saveSymbol);

    // Get symbol object from the symbols variable
    var promise = getSymbolById(id);
    promise.done(symbol => 
    {
        symbol['symbolTypes'] = symbolTypes;
        UpdateView(symbol,VIEW_MODEL_CONTAINER_SELECTOR);
    });
 });

$(document).on("click",".deleteRow", function () 
{
    // Get id from the clicked item
    var id = $(this).attr('data-id');

    var parentRow = getParentRowById(id);
    var parentRowData = dataTable.row($(parentRow)).data();

    createDeletePrompt('Confirmation','Are you sure want to delete <strong>' + parentRowData.name + '</strong> ?',() => 
    { 
        var promise = removeSymbol(id);
        promise.done(result => 
        {
            dataTable.row(parentRow).remove().draw();
            createNotification(parentRowData.name + " deleted successfully",NOTIFICATION_TYPE.SUCCESS);
        })
        .fail(result => 
        {
            createNotification(parentRowData.name + " deletion failed",NOTIFICATION_TYPE.ERROR);
        });
    },null);

    getSymbols();
 });

 $(document).on("click","#createNewSymbol", function () 
 {
    // Set OP_Code
    DATA_OP = DATA_OPERATION.CREATE;

    // Show Modal Pop-up
    createModalPrompt("Create new Symbol",$("#" + symbolModalId).html());

    // Update View for the loaded pop-up
    UpdateView({ 
        id:null, 
        name: null, 
        code: null, 
        price: null, 
        symbolTypeId: null,
        symbolTypes: ko.observableArray(symbolTypes) 
    },symbolModalId);
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
            symbolTypeId:  ($.isNumeric(symbolObject.symbolTypeId)) ? parseInt(symbolObject.symbolTypeId) : 0
        }
    : null;
}

function getParentRowById(id)
{
    var tr = $('table tr .editRow[data-id="' + id + '"]');

    if (tr.length > 0) 
    {
        return tr[0].parentElement.parentElement;
    }

    return null;
}

function getParentRowByS_No(id)
{

}

function saveSymbol()
{
    // Get data
    var viewData = GetViewData();
    
    var promise = (DATA_OP == DATA_OPERATION.CREATE) ? createSymbol(viewData) : updateSymbol(viewData);
    promise.done(x => 
    {
        if (DATA_OP == DATA_OPERATION.CREATE) 
        {
            dataTable.row.add(x);
            createNotification(viewData.name + " created successfully",NOTIFICATION_TYPE.SUCCESS);
        }
        else 
        {
            // Get he parent row
            var parentRow = getParentRowById(x.id);
            dataTable.row(parentRow).data(x).invalidate();
            createNotification(viewData.name + " updated successfully",NOTIFICATION_TYPE.SUCCESS);
        }

        // Update datatable ui
        dataTable.draw();
    }).fail((responseObject) => 
    {
        if (DATA_OP == DATA_OPERATION.CREATE) 
        {
            createNotification(viewData.name + " creation failed",NOTIFICATION_TYPE.ERROR);
        }
        else
        {
            createNotification(viewData.name + " update failed",NOTIFICATION_TYPE.ERROR);
        }
        console.log(responseObject);
    }).always(x => 
    {
        
    });
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
        symbolTemplateContent = data;

        // Fetch symbols and Update UI
        var promise = getSymbols();
        promise.done(symbols => {
            $(dynamicpagecontentKey).html(symbolTemplateContent);
            activeSideMenu("symbol");
            UpdateDataGrid(symbols);
        });
    });
}


// Tracker variables
var DATA_OP = -1; // 'C' => Create an entity , 'U' => Update an entity

// Assign constants
var symbolTemplatePath = "templates/symbol/symbol.template.html";
var symbolsApiEndpoint = "Symbol";
var symbolTypeApiEndpoint = "SymbolType";
var symbolModalId = "symbolModal";
var symbolTemplateContent = null;
var dataTable = {};
var symbolTypes = null;

$(document).ready(function () 
{
    getAllSymbolTypes();
    loadSymbolsUi();
});