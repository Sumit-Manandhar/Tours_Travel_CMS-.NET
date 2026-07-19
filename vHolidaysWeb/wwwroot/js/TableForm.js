
var tableForm = function (selector, dataRowPrefix, templateRowPrefix) {
    var me = this;
    var table = $(selector);
    var autocompleteEdit = [];

    var setRowIndex = function () {
        var regex = new RegExp(dataRowPrefix + "\\[\\d+\\]\\.");
        var bodyRows = table.find("tbody tr");
        for (var i = 0; i < bodyRows.length; i++) {
            var elements = $(bodyRows[i]).find("input,select,textarea,div.templateDesign");
            $(elements).each(function () {
                var el = $(this);
                var name = el.attr("name");
                if (name != undefined) {
                    name = name.replace(regex, dataRowPrefix + "[" + i + "].");
                    el.attr("name", name);
                    el.attr("id", name);
                    if (el.is(':checkbox')) {
                        el.attr("id", name);
                        $(this).siblings("label").attr("for", name);
                    }
                }
            });
        }
    }
    this.reIndexRow = function () {
        setRowIndex();
    }

    var addRow = function (trigger) {
        var row = $(trigger).parent().parent();

        if (me.onRowAdding != null) {
            if (!me.onRowAdding(row))
                return;
        }
        var cell = row.find("[data-fieldtype='destination']");
        if ($(cell).data('select2')) {
            $(cell).select2('destroy');
        }
        var rowToAdd = $(row).clone();

        if (me.onRowCloning != null)
            me.onRowCloning(rowToAdd);

        var section = rowToAdd.find('button').data('action');

        switch (section) {
            case "add-row":
                rowToAdd.find("button[data-action='add-row']").remove();
                rowToAdd.find("button[data-action='delete-row']").show();
                break;
            case "add-row-sectionone":
                rowToAdd.find("button[data-action='add-row-sectionone']").remove();
                rowToAdd.find("button[data-action='delete-row-sectionone']").show();
                break;
            case "add-row-sectiontwo":
                rowToAdd.find("button[data-action='add-row-sectiontwo']").remove();
                rowToAdd.find("button[data-action='delete-row-sectiontwo']").show();
                break;
        }

        rowToAdd.find("input,select,textarea,div.templateDesign").each(function () {
            var element = $(this);

            if (element[0].tagName.toUpperCase() == 'SELECT' || element[0].tagName.toUpperCase() == 'TEXTAREA') {
                var ov = row.find("[name='" + element[0].name + "']").val();
                element.val(ov);
            }
            if (element.attr("name") != undefined) {
                var name = element.attr("name").replace(templateRowPrefix + ".", dataRowPrefix + "[999].");
                element.attr("name", name);
                element.attr("id", name);
            }
        });
        table.find("tbody").append(rowToAdd);

        $(row).find("input:not([data-copy])").val('');
        $(row).find("select,textarea").val('');
        $(row).find("select").val('').trigger('change');
        $(row).find('div.templateDesign').html('');
        setRowIndex();
        if (me.onRowAdded != null)
            me.onRowAdded(rowToAdd);
    }

    var removeRow = function (trigger) {
        var row = $(trigger).parent().parent();
        $(row).remove();
        setRowIndex();
    }

    this.onRowAdding = null;
    this.onRowAdded = null;
    this.onRowCloning = null;

    this.isTemplateRowEmpty = function (skipSelect) {

        var row = table.find("tfoot tr");
        var elements = row.find("input,select,textarea");
        if (skipSelect == true) {
            elements = row.find("input,textarea");
        }
        var isEmpty = true;
        $(elements).each(function () {
            console.log($(this).is('[readonly]'));
            if (($(this).val() != '' && $(this).val() !== null) && !$(this).is('[readonly]') && !$(this).is(':checkbox') && !$(this).is(':hidden') && !$(this).data('fieldtype')) {
                isEmpty = false;
                return false;
            }
        });
        return isEmpty;
    }

    this.isTemplateRowEmptyForInput = function () {

        var row = table.find("tfoot tr");
        var elements = row.find("input");
        var isEmpty = true;
        $(elements).each(function () {
            console.log($(this).is('[readonly]'));

            if ($(this).val() != '' && !$(this).is('[readonly]')) {
                isEmpty = false;
                return false;
            }
        });
        return isEmpty;
    }

    this.addTemplateRowToBody = function () {
        var btn = table.find("tfoot tr button[data-action='add-row']");
        btn.trigger('click');
    }
    this.addTemplateRowToBodySectionOne = function () {
        var btn = table.find("tfoot tr button[data-action='add-row-sectionone']");
        btn.trigger('click');
    }
    this.addTemplateRowToBodySectiontwo = function () {

        var btn = table.find("tfoot tr button[data-action='add-row-sectiontwo']");
        btn.trigger('click');
    }
    var findGeolocation = function (cityName, editAutocomplete) {
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': cityName }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                setBoundHotel(results[0].geometry.viewport.getSouthWest(), results[0].geometry.viewport.getNorthEast(), editAutocomplete);
            }
        });
    }
    var setCityBounds = function (id) {
        if (id != null && id != undefined) {
            var cityName = $("select[name='[" + id + "].CityId'] option:selected").text();
        }
        else {
            var cityName = $("#CityId option:selected").text();
        }
        findGeolocation(cityName, id);
    }
    this.init = function () {

        table.on("click", "button", function () {
            var action = $(this).data("action");
            switch (action) {
                case "add-row":
                    addRow(this);
                    var rows = $("#tblAirport tbody tr");
                    $.each(rows, function (i, v) {
                        initAutocomplete($(this).find("input[name$='AutoAirportName']"), i)
                    });

                    break;
                case "delete-row":
                    removeRow(this);
                    break;
                case "add-row-sectionone":
                    addRow(this);
                    break;
                case "add-row-sectiontwo":
                    addRow(this);
                    break;
                case "delete-row-sectionone":
                    removeRow(this);
                    break;
                case "delete-row-sectiontwo":
                    removeRow(this);
                    break;

            }
        });
    }

}