// Api functions
function getSymbols() 
{
    return get(SYMBOL_API_URL);
}

function getSymbolById(id)
{
    return get(SYMBOL_API_URL + "/" + id);
}

function createSymbol(newSymbol) 
{
    return create(SYMBOL_API_URL,GetSymbolModel(newSymbol));
}

function updateSymbol(symbol)
{
    return update((symbol != null) ? SYMBOL_API_URL + "/" + symbol.id : SYMBOL_API_URL,GetSymbolModel(symbol));
}

function removeSymbol(id)
{
    return remove(SYMBOL_API_URL + "/" + id);
}

function getAllSymbolTypes() 
{
    get(SYMBOL_TYPES_API_URL).done(data => { symbolTypes = data; });
}

function getAllMarkets() 
{
    get(MARKET_API_URL).done(data => { markets = data; });
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

function UpdateDataTable(symbols=null)
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
                    },
                    "width": '10%'
                },
                {"data": "name"},
                {"data": "code"},
                {"data": "price" , "width": '10%'},
                {
                    "data": null,
                    "sortable": false,
                    "mRender": function(data, type, full) {
                        var editElement = '<button class="editRow button" data-id="' + data.id + '"><i class="fa fa-info-circle fa-lg" aria-hidden="true" style="color:#1d63bf;"></i></button>';
                        var deleteElement = '<button class="deleteRow button" data-id="' + data.id + '"><i class="fa fa-trash" aria-hidden="true" style="color:#b52121;"></i></button>';
                        return editElement + deleteElement;
                    },
                    "width": '10%'
                },
                {
                    "data": "id",
                    "visible": false
                },
            ],
            "pageLength": 10
        });
        
        dataTable.rows.add(symbols);
    }

    dataTable.draw();
}

// Data Action functions
$(document).on("click",".editRow", function () 
{
    // Set OP_Code
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

    // Get parent row from table
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
 });

 $(document).on("click","#createNewSymbol", function () 
 {
    // Set OP_Code
    DATA_OP = DATA_OPERATION.CREATE;

    // Show Modal Pop-up
    createModalPrompt("Create new Symbol",$("#" + symbolModalId).html(),saveSymbol);

    // Update View for the loaded pop-up
    UpdateView({ 
        id:null, 
        name: null, 
        code: null, 
        price: null, 
        symbolTypeId: null,
        symbolTypes: ko.observableArray(symbolTypes) 
    },VIEW_MODEL_CONTAINER_SELECTOR);
});


// Helper functions
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
            // Add new row
            if (dataViewType == DATA_VIEW_TYPE.TABLE) { dataTable.row.add(x); }
            if (dataViewType == DATA_VIEW_TYPE.CARD) {  }
            createNotification(viewData.name + " created successfully",NOTIFICATION_TYPE.SUCCESS);
        }
        else 
        {
            // Get he parent row and update the data
            if (dataViewType == DATA_VIEW_TYPE.TABLE) {  
                var parentRow = getParentRowById(x.id);
                dataTable.row(parentRow).data(x).invalidate();
            }
            
            if (dataViewType == DATA_VIEW_TYPE.CARD) {  
                //UpdateView(x,cardModelInfoSelector);
            }
            createNotification(viewData.name + " updated successfully",NOTIFICATION_TYPE.SUCCESS);
        }

        // Update datatable ui
        if (dataViewType == DATA_VIEW_TYPE.TABLE) { dataTable.draw(); }
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

function loadSymbolsUi(viewType = 0) 
{
    // Fetch table template
    fetch((viewType == DATA_VIEW_TYPE.TABLE) ? symbolTableTemplatePath : symbolCardTemplatePath)
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

            if (viewType == DATA_VIEW_TYPE.TABLE) {
                UpdateDataTable(symbols);
            } else {
                UpdateView({
                    symbol: symbols,
                    symbolTypes: symbolTypes,
                    market: markets,
                },$(cardModelInfoSelector));
            }
        });
    });
}


// Tracker variables
var DATA_OP = -1; // 'C' => Create an entity , 'U' => Update an entity

// Assign constants
var symbolTableTemplatePath = "templates/symbol/symbol.table.template.html";
var symbolCardTemplatePath = "templates/symbol/symbol.card.template.html";
var symbolModalId = "symbolModal";
var symbolTemplateContent = null;
var dataTable = {};
var symbolTypes = null;
var markets = null;
var dataViewType = DATA_VIEW_TYPE.CARD;
var cardModelInfoSelector = "#card-model-info";

$(document).ready(function () 
{
    getAllSymbolTypes();
    getAllMarkets();
    loadSymbolsUi(dataViewType);    
});